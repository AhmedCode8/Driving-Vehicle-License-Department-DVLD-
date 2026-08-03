
using DVDL_Logic_layer.Global_Classes;
using System;
using System.Windows.Forms;
using UserInterfaceLayer.Application_Type;
using UserInterfaceLayer.International_License_Applications;
using UserInterfaceLayer.License_Detain;
using UserInterfaceLayer.Local_Driving_License_Application;
using UserInterfaceLayer.People;
using UserInterfaceLayer.Test_Types;
using UserInterfaceLayer.Users;

namespace UserInterfaceLayer
{
    public partial class frMain : Form
    {
        public frMain()
        {
            InitializeComponent();
        }

        // People Management
        private void tsmiPeople_Click(object sender, EventArgs e)
        {
            fmManagePeople managePeopleForm = new fmManagePeople();
            managePeopleForm.ShowDialog();
        }

        // Users Management
        private void tsmiUsers_Click(object sender, EventArgs e)
        {
            frmManageUsers manageUsersForm = new frmManageUsers();
            manageUsersForm.ShowDialog();
        }

        // Account Settings Sub-Menu
        private void tsmiCurrentUserInfo_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo(clsGlobal.CurrentUser);
            frmUserInfo.ShowDialog();
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword(clsGlobal.CurrentUser);
            frmChangePassword.ShowDialog();
        }

        // Application & Test Types Management
        private void tsmiManageApplicationTypes_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frmManageApplicationTypes = new frmManageApplicationTypes();
            frmManageApplicationTypes.ShowDialog();
        }

        private void tsmiManageTestTypes_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frmManageTestTypes = new frmManageTestTypes();
            frmManageTestTypes.ShowDialog();
        }

        // Driving License Applications Sub-Menu
        private void tsmiLocalLicense_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.ShowDialog();
        }

        private void tsmiLocalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications frmManageLocalDrivingLicenseApplications = new frmManageLocalDrivingLicenseApplications();
            frmManageLocalDrivingLicenseApplications.ShowDialog();
        }

        private void tsmiInternationalLicense_Click(object sender, EventArgs e)
        {
            frmNewlnternationalLicenseApplication frmNew = new frmNewlnternationalLicenseApplication();
            frmNew.ShowDialog();
        }

        private void tsmiInternationalLicenseApplications_Click(object sender, EventArgs e)
        {
            frmManageInternationalLicenseApplications frmManageInternational = new frmManageInternationalLicenseApplications();
            frmManageInternational.ShowDialog();
        }

        private void tsmiRenewDrivingLicense_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplication frmRenewLicenseApplication = new frmRenewLicenseApplication();
            frmRenewLicenseApplication.ShowDialog();
        }

        private void tsmiReplacementForLostOrDamagedLicense_Click(object sender, EventArgs e)
        {
            frmReplacementforDamagedLicense frmReplacementFor = new frmReplacementforDamagedLicense();
            frmReplacementFor.ShowDialog();
        }

        // Detain & Release Licenses Sub-Menu
        private void tsmiDetainLicense_Click(object sender, EventArgs e)
        {
            frmLicenseDetain frmLicenseDetain = new frmLicenseDetain();
            frmLicenseDetain.ShowDialog();
        }

        private void tsmiReleaseDetainedLicense_Click(object sender, EventArgs e)
        {

            frmReleaseDetainedLicense frmManageDetainedLicense = new frmReleaseDetainedLicense();
            frmManageDetainedLicense.ShowDialog();

        }

        private void tsmiManageDetainedLicenses_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicenses frmManageDetainedLicenses = new frmManageDetainedLicenses();
            frmManageDetainedLicenses.ShowDialog();
        }

        private void tsmiReleaseDetainedDrivingLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frmReleaseDetainedLicense = new frmReleaseDetainedLicense();
            frmReleaseDetainedLicense.ShowDialog();
        }

        private void tsmiNewDrivingLicense_Click(object sender, EventArgs e)
        {
            // إضافية جاهزة للاستخدام مستقبلاً
        }

        private void tsmiSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
            frmLoginScreen loginScreen = new frmLoginScreen();
            loginScreen.Show();
        }
    }
}