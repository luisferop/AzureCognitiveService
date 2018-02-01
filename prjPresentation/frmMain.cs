using AForge.Video.DirectShow;
using Microsoft.ProjectOxford.Face.Contract;
using prjAzure;
using prjAzure.VideoFrameAnalyzer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjPresentation
{
    public partial class frmMain : Form
    {
        VideoCaptureDevice videoSource;
        string currentPhoto;
        public frmMain()
        {
            InitializeComponent();
            AForge.Video.DirectShow.FilterInfoCollection videosources = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);

            //if (videosources != null)
            //{
            //    videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(videosources[0].MonikerString);
            //    videoSource.NewFrame += (s, e) =>
            //    {
            //        if (picBoxVideo.Image != null)
            //        {
            //            picBoxVideo.Image.Dispose();
            //        }

            //        picBoxVideo.Image = (Bitmap)e.Frame.Clone();
            //    };
            //    videoSource.Start();
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTakePhoto_Click(object sender, EventArgs e)
        {
            currentPhoto = @"c:\azure_photos\snapshot" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";
            picBoxVideo.Image.Save(currentPhoto, System.Drawing.Imaging.ImageFormat.Png);
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
        }

        private async Task btnAzure_ClickAsync()
        {
            AzureFaceAPIRecognition azureFaceAPIRecognition = new AzureFaceAPIRecognition();
            //txtResult.Text = await azureFaceAPIRecognition.MakeAnalysisRequest(currentPhoto);
            txtResult.Text = await azureFaceAPIRecognition.IdentifyPerson(txtCurrentGroup.Text, currentPhoto);
        }

        private async void btnAzure_Click(object sender, EventArgs e)
        {
            await btnAzure_ClickAsync();
        }

        private void createGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAzureCreateGroup group = new frmAzureCreateGroup();
            group.ShowDialog();
        }

        private void addPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAzureAddPersonToGroup frmAzureAdd = new frmAzureAddPersonToGroup();
            frmAzureAdd.ShowDialog();
        }

        private void trainingGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrainingGroup frmTrainingGroup = new frmTrainingGroup();
            frmTrainingGroup.ShowDialog();
            
        }


        private void btnAutomaticRecognition_Click(object sender, EventArgs e)
        {
            // Create grabber. 
            FrameGrabber<Face[]> grabber = new FrameGrabber<Face[]>();

            // Create Face API Client.
            //FaceServiceClient faceClient = new FaceServiceClient(subscriptionKey, uriAzureBase);

            // Set up a listener for when we acquire a new frame.
            grabber.NewFrameProvided += (s, ef) =>
            {
                //txtResult.Text =  $"New frame acquired at {ef.Frame.Metadata.Timestamp}";
            };

            // Set up Face API call.
            grabber.AnalysisFunction = async frame =>
            {
                //txtResult.Text = $"Submitting frame acquired at {frame.Metadata.Timestamp}";
                // Encode image and submit to Face API. 
                Stream stream = frame.Image.ToMemoryStream(".jpg");
                string fileName = @"c:\azure_photos\snapshot_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".jpg";
                var fileStream = File.Create(fileName);
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                fileStream.Close();

                //using (Stream file = File.Create(fileName))
                //{
                //    CopyStream(stream, file);
                //}
                string test = await new AzureFaceAPIRecognition().MakeAnalysisRequest(currentPhoto);
                return await new AzureFaceAPIRecognition().DetectFaces(frame.Image.ToMemoryStream(".jpg"));
            };

            // Set up a listener for when we receive a new result from an API call. 
            grabber.NewResultAvailable += (s, ef) =>
            {
                if (ef.TimedOut)
                {
                    //txtResult.Text = $"API call timed out.";
                }
                else if (ef.Exception != null)
                {
                    //txtResult.Text = $"API call threw an exception.";
                }
                    
                else if(ef.Analysis.Length > 0)
                {

                    //txtResult.Text = $"New result received for frame acquired at {ef.Frame.Metadata.Timestamp}. {ef.Analysis.Length} faces detected";

                }

            };

            // Tell grabber when to call API.
            // See also TriggerAnalysisOnPredicate
            grabber.TriggerAnalysisOnInterval(TimeSpan.FromMilliseconds(5000));

            // Start running in the background.
            grabber.StartProcessingCameraAsync().Wait();

            // Wait for keypress to stop
            //Console.WriteLine("Press any key to stop...");
            //Console.ReadKey();

            //// Stop, blocking until done.
            //grabber.StopProcessingAsync().Wait();
        }
        private void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
