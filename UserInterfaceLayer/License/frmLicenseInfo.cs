using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.License
{
    public partial class frmLicenseInfo : Form
    {
        clsLicenseDTO _License;
        // Constructor يستقبل رقم الرخصة
        public frmLicenseInfo(clsLicenseDTO license)
        {
            InitializeComponent();
            _License = license;
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            if (_License == null)
            {
                MessageBox.Show("No License Found1111", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ctrlDriverLicenseInfoCard1.LoadInfo(_License);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}