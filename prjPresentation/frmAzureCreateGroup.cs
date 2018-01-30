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
    public partial class frmAzureCreateGroup : Form
    {
        public frmAzureCreateGroup()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {

            AzureFaceAPIRecognition azureFaceAPIRecognition = new AzureFaceAPIRecognition();
            string strMessage = await azureFaceAPIRecognition.MakeCreateGroupRequest(txtGroupID.Text, txtGroupName.Text, txtDescription.Text);
            MessageBox.Show(strMessage);

        }
    }
}
