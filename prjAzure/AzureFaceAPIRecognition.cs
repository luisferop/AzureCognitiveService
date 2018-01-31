using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using prjAzure.VideoFrameAnalyzer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace prjAzure
{
    public class AzureFaceAPIRecognition
    {
        // Replace the subscriptionKey string value with your valid subscription key.
        const string subscriptionKey = "e59adbc4e8c94cd582bf060dc76324d5";
        const string uriAzureBase = "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0";
        const string uriBaseDetect = uriAzureBase + "/detect";
        const string uriBaseCreateGroup = uriAzureBase + "/persongroups/";
        const string uriBaseAddPersonToGroup = uriAzureBase + "persongroups/@personGroupId/persons";
        FaceServiceClient faceServiceClient = null;
        public AzureFaceAPIRecognition()
        {
            faceServiceClient = new FaceServiceClient(subscriptionKey, uriAzureBase);
        }
        #region Face API Detection
        public async Task<string> MakeAnalysisRequest(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            // Assemble the URI for the REST API Call.
            string uri = uriBaseDetect + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                return JsonPrettyPrint(contentString);
            }
        }

        public async Task<Face[]> DetectFaces(Stream stream)
        {
            return await faceServiceClient.DetectAsync(stream);
        }
        #endregion

        #region Create Groups
        public async Task<string> MakeCreateGroupRequest(string personGroupId, string groupName, string description)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request URI string.
            // NOTE: You must use the same region in your REST call as you used to obtain your subscription keys.
            //   For example, if you obtained your subscription keys from westus, replace "westcentralus" in the 
            //   URI below with "westus".
            string uri = uriBaseCreateGroup + personGroupId;

            // Here "name" is for display and doesn't have to be unique. Also, "userData" is optional.
            string json = "{\"name\":\"@groupName\", \"userData\":\"@groupDescription\"}";
            json = json.Replace("@groupName", groupName);
            json = json.Replace("@groupDescription", description);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PutAsync(uri, content);

            // If the group was created successfully, you'll see "OK".
            // Otherwise, if a group with the same personGroupId has been created before, you'll see "Conflict".
            return "Response status: " + response.StatusCode;
        }
        public async Task<string> AddPeopleToGroup(string personGroupId, string personName, string personLogin)
        {

            
            CreatePersonResult friend = await faceServiceClient.CreatePersonAsync(
                // Id of the person group that the person belonged to
                personGroupId,
                // Name of the person
                personName
            );
            string friendImageDir = @"C:\azure_photos\upload\" + personLogin;
            foreach (string imagePath in Directory.GetFiles(friendImageDir, "*.jpg"))
            {
                using (Stream s = File.OpenRead(imagePath))
                {
                    // Detect faces in the image and add to Anna
                    await faceServiceClient.AddPersonFaceAsync(
                        personGroupId, friend.PersonId, s);
                }
            }
            return "OK";
        }
        public async void TrainingGroup(string groupId)
        {
            await faceServiceClient.TrainPersonGroupAsync(groupId);
            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await faceServiceClient.GetPersonGroupTrainingStatusAsync(groupId);

                if (trainingStatus.Status != Status.Running)
                {
                    break;
                }

                await Task.Delay(1000);
            }
        }
        #endregion

        #region Identify Person
        public async Task<string> IdentifyPerson(string groupId, string imageFile)
        {
            string strPerson = "No one identified";
            using (Stream s = File.OpenRead(imageFile))
            {
                var faces = await faceServiceClient.DetectAsync(s);
                var faceIds = faces.Select(face => face.FaceId).ToArray();

                var results = await faceServiceClient.IdentifyAsync(groupId, faceIds);
                foreach (var identifyResult in results)
                {
                    //Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
                    if (identifyResult.Candidates.Length == 0)
                    {
                        strPerson = "No one identified";
                    }
                    else
                    {
                        // Get top 1 among all candidates returned
                        var candidateId = identifyResult.Candidates[0].PersonId;
                        var person = await faceServiceClient.GetPersonAsync(groupId, candidateId);
                        strPerson = $"Identified as {person.Name}";
                    }
                }
            }
            return strPerson;
        }
        public async Task<string> IdentifyPerson(string groupId, Face[] face)
        {
            return string.Empty;
        }
        #endregion

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        private byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }


        /// <summary>
        /// Formats the given JSON string by adding line breaks and indents.
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>The formatted JSON string.</returns>
        private string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }
    }

}

