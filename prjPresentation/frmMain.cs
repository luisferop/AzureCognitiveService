using AForge.Video.DirectShow;
using prjAzure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            if (videosources != null)
            {
                videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(videosources[0].MonikerString);
                videoSource.NewFrame += (s, e) =>
                {
                    if (picBoxVideo.Image != null)
                    {
                        picBoxVideo.Image.Dispose();
                    }

                    picBoxVideo.Image = (Bitmap)e.Frame.Clone();
                };
                videoSource.Start();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTakePhoto_Click(object sender, EventArgs e)
        {
            currentPhoto = @"c:\\azure_photos\\snapshot" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";
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
    }
}
