using DVLD_DTOs;
using System;
using System.Windows.Forms;
namespace UserInterfaceLayer.Local_Driving_License_Application
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        private clsLocalDrivingLicenseApplicationDTO _InfoLocalDrivingLicenseApplication;
        private clsApplicationDTO _InfoApplication;
        int _PassedTestCount;
        public frmLocalDrivingLicenseApplicationInfo(clsApplicationDTO infoApplication, clsLocalDrivingLicenseApplicationDTO infoLocalDrivingLicenseApplication, int PassedTestCount)
        {
            InitializeComponent();
            _InfoApplication = infoApplication;
            _InfoLocalDrivingLicenseApplication = infoLocalDrivingLicenseApplication;
            _PassedTestCount = PassedTestCount;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlLicenseApplicationInInfoCard1.LoadData(_InfoApplication, _InfoLocalDrivingLicenseApplication, _PassedTestCount);

        }
    }
}
