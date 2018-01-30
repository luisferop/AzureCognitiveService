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
    public partial class frmAzureAddPersonToGroup : Form
    {
        public frmAzureAddPersonToGroup()
        {
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            AzureFaceAPIRecognition azureFaceAPIRecognition = new AzureFaceAPIRecognition();
            string result = await azureFaceAPIRecognition.AddPeopleToGroup(txtGroupID.Text, txtPersonName.Text, txtPersonLogin.Text);
            MessageBox.Show(result);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
