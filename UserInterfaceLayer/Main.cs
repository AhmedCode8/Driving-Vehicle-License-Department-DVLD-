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
        //
        // People Management
        private void tsmiPeople_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();
        }

        // Users Management & Drivers
        private void tsmiUsers_Click(object sender, EventArgs e)
        {
            frmManageUsers manageUsersForm = new frmManageUsers();
            manageUsersForm.ShowDialog();
        }
        private void tsmiDrivers_Click(object sender, EventArgs e)
        {
            frmManageDrivers frm = new frmManageDrivers();
            frm.ShowDialog();
        }

        // Application & Test Types Management
        private void tsmiManageApplicationTypes_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void tsmiManageTestTypes_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        // Driving License Applications Sub-Menu
        private void tsmiLocalLicense_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void tsmiLocalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications frm = new frmManageLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void tsmiInternationalLicense_Click(object sender, EventArgs e)
        {
            frmNewlnternationalLicenseApplication frm = new frmNewlnternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void tsmiInternationalLicenseApplications_Click(object sender, EventArgs e)
        {
            frmManageInternationalLicenseApplications frm = new frmManageInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void tsmiRenewDrivingLicense_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplication frm = new frmRenewLicenseApplication();
            frm.ShowDialog();
        }

        private void tsmiReplacementForLostOrDamagedLicense_Click(object sender, EventArgs e)
        {
            frmReplacementforDamagedLicense frm = new frmReplacementforDamagedLicense();
            frm.ShowDialog();
        }

        // Detain & Release Licenses Sub-Menu
        private void tsmiDetainLicense_Click(object sender, EventArgs e)
        {
            frmLicenseDetain frm = new frmLicenseDetain();
            frm.ShowDialog();
        }

        private void tsmiReleaseDetainedLicense_Click(object sender, EventArgs e)
        {

            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();

        }

        private void tsmiManageDetainedLicenses_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicenses frm = new frmManageDetainedLicenses();
            frm.ShowDialog();
        }

        private void tsmiReleaseDetainedDrivingLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void tsmiNewDrivingLicense_Click(object sender, EventArgs e)
        {
            // إضافية جاهزة للاستخدام مستقبلاً
        }


        // Account Settings Sub-Menu
        private void tsmiCurrentUserInfo_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUser = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frmUser.ShowDialog();
        }
        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword(clsGlobal.CurrentUser);
            frmChangePassword.ShowDialog();
        }
        private void tsmiSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
            frmLoginScreen loginScreen = new frmLoginScreen();
            loginScreen.Show();
        }



    }
}