using AForge.Video.DirectShow;
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
            string fileName = "c:\\azure_images\\snapshot" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";
            picBoxVideo.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

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
    }
}
