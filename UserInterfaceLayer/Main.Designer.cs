namespace UserInterfaceLayer
{
    partial class frMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDrivingLicensesServices = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLocalLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInternationalLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRenewDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiReplacementForLostOrDamagedLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiReleaseDetainedDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRetakeTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiManageApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLocalDrivingLicenseApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInternationalLicenseApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDetainLicenses = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManageDetainedLicenses = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDetainLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReleaseDetainedLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManageApplicationTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManageTestTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPeople = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDrivers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAccountSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCurrentUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiApplications,
            this.tsmiPeople,
            this.tsmiDrivers,
            this.tsmiUsers,
            this.tsmiAccountSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1420, 72);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiApplications
            // 
            this.tsmiApplications.BackColor = System.Drawing.SystemColors.Control;
            this.tsmiApplications.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDrivingLicensesServices,
            this.toolStripSeparator5,
            this.tsmiManageApplications,
            this.toolStripSeparator2,
            this.tsmiDetainLicenses,
            this.tsmiManageApplicationTypes,
            this.tsmiManageTestTypes});
            this.tsmiApplications.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tsmiApplications.Image = global::UserInterfaceLayer.Properties.Resources.Applications_64;
            this.tsmiApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiApplications.Name = "tsmiApplications";
            this.tsmiApplications.Size = new System.Drawing.Size(240, 68);
            this.tsmiApplications.Text = "Applications";
            // 
            // tsmiDrivingLicensesServices
            // 
            this.tsmiDrivingLicensesServices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewDrivingLicense,
            this.tsmiRenewDrivingLicense,
            this.toolStripSeparator3,
            this.tsmiReplacementForLostOrDamagedLicense,
            this.toolStripSeparator4,
            this.tsmiReleaseDetainedDrivingLicense,
            this.tsmiRetakeTest});
            this.tsmiDrivingLicensesServices.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.tsmiDrivingLicensesServices.Image = global::UserInterfaceLayer.Properties.Resources.Driver_License_48;
            this.tsmiDrivingLicensesServices.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDrivingLicensesServices.Name = "tsmiDrivingLicensesServices";
            this.tsmiDrivingLicensesServices.Size = new System.Drawing.Size(369, 70);
            this.tsmiDrivingLicensesServices.Text = "Driving Licenses Services";
            // 
            // tsmiNewDrivingLicense
            // 
            this.tsmiNewDrivingLicense.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLocalLicense,
            this.tsmiInternationalLicense});
            this.tsmiNewDrivingLicense.Image = global::UserInterfaceLayer.Properties.Resources.New_Driving_License_32;
            this.tsmiNewDrivingLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiNewDrivingLicense.Name = "tsmiNewDrivingLicense";
            this.tsmiNewDrivingLicense.Size = new System.Drawing.Size(464, 38);
            this.tsmiNewDrivingLicense.Text = "New Driving License";
            this.tsmiNewDrivingLicense.Click += new System.EventHandler(this.tsmiNewDrivingLicense_Click);
            // 
            // tsmiLocalLicense
            // 
            this.tsmiLocalLicense.Image = global::UserInterfaceLayer.Properties.Resources.Local_32;
            this.tsmiLocalLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiLocalLicense.Name = "tsmiLocalLicense";
            this.tsmiLocalLicense.Size = new System.Drawing.Size(283, 38);
            this.tsmiLocalLicense.Text = "LocaI License ";
            this.tsmiLocalLicense.Click += new System.EventHandler(this.tsmiLocalLicense_Click);
            // 
            // tsmiInternationalLicense
            // 
            this.tsmiInternationalLicense.Image = global::UserInterfaceLayer.Properties.Resources.International_32;
            this.tsmiInternationalLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiInternationalLicense.Name = "tsmiInternationalLicense";
            this.tsmiInternationalLicense.Size = new System.Drawing.Size(283, 38);
            this.tsmiInternationalLicense.Text = "International License ";
            this.tsmiInternationalLicense.Click += new System.EventHandler(this.tsmiInternationalLicense_Click);
            // 
            // tsmiRenewDrivingLicense
            // 
            this.tsmiRenewDrivingLicense.Image = global::UserInterfaceLayer.Properties.Resources.Renew_Driving_License_32;
            this.tsmiRenewDrivingLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiRenewDrivingLicense.Name = "tsmiRenewDrivingLicense";
            this.tsmiRenewDrivingLicense.Size = new System.Drawing.Size(464, 38);
            this.tsmiRenewDrivingLicense.Text = "Renew Driving License";
            this.tsmiRenewDrivingLicense.Click += new System.EventHandler(this.tsmiRenewDrivingLicense_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(461, 6);
            // 
            // tsmiReplacementForLostOrDamagedLicense
            // 
            this.tsmiReplacementForLostOrDamagedLicense.Image = global::UserInterfaceLayer.Properties.Resources.Damaged_Driving_License_32;
            this.tsmiReplacementForLostOrDamagedLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiReplacementForLostOrDamagedLicense.Name = "tsmiReplacementForLostOrDamagedLicense";
            this.tsmiReplacementForLostOrDamagedLicense.Size = new System.Drawing.Size(464, 38);
            this.tsmiReplacementForLostOrDamagedLicense.Text = "Replacement for Lost or Damaged License";
            this.tsmiReplacementForLostOrDamagedLicense.Click += new System.EventHandler(this.tsmiReplacementForLostOrDamagedLicense_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(461, 6);
            // 
            // tsmiReleaseDetainedDrivingLicense
            // 
            this.tsmiReleaseDetainedDrivingLicense.Image = global::UserInterfaceLayer.Properties.Resources.Detained_Driving_License_32;
            this.tsmiReleaseDetainedDrivingLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiReleaseDetainedDrivingLicense.Name = "tsmiReleaseDetainedDrivingLicense";
            this.tsmiReleaseDetainedDrivingLicense.Size = new System.Drawing.Size(464, 38);
            this.tsmiReleaseDetainedDrivingLicense.Text = "Release Detained Driving License";
            this.tsmiReleaseDetainedDrivingLicense.Click += new System.EventHandler(this.tsmiReleaseDetainedDrivingLicense_Click);
            // 
            // tsmiRetakeTest
            // 
            this.tsmiRetakeTest.Image = global::UserInterfaceLayer.Properties.Resources.Retake_Test_32;
            this.tsmiRetakeTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiRetakeTest.Name = "tsmiRetakeTest";
            this.tsmiRetakeTest.Size = new System.Drawing.Size(464, 38);
            this.tsmiRetakeTest.Text = "Retake Test";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(366, 6);
            // 
            // tsmiManageApplications
            // 
            this.tsmiManageApplications.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLocalDrivingLicenseApplications,
            this.tsmiInternationalLicenseApplications});
            this.tsmiManageApplications.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.tsmiManageApplications.Image = global::UserInterfaceLayer.Properties.Resources.Manage_Applications_64;
            this.tsmiManageApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiManageApplications.Name = "tsmiManageApplications";
            this.tsmiManageApplications.Size = new System.Drawing.Size(369, 70);
            this.tsmiManageApplications.Text = "Manage Applications";
            // 
            // tsmiLocalDrivingLicenseApplications
            // 
            this.tsmiLocalDrivingLicenseApplications.Image = global::UserInterfaceLayer.Properties.Resources.Driver_License_48;
            this.tsmiLocalDrivingLicenseApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiLocalDrivingLicenseApplications.Name = "tsmiLocalDrivingLicenseApplications";
            this.tsmiLocalDrivingLicenseApplications.Size = new System.Drawing.Size(411, 54);
            this.tsmiLocalDrivingLicenseApplications.Text = "LocaI Driving License Applications";
            this.tsmiLocalDrivingLicenseApplications.Click += new System.EventHandler(this.tsmiLocalDrivingLicenseApplications_Click);
            // 
            // tsmiInternationalLicenseApplications
            // 
            this.tsmiInternationalLicenseApplications.Image = global::UserInterfaceLayer.Properties.Resources.International_32;
            this.tsmiInternationalLicenseApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiInternationalLicenseApplications.Name = "tsmiInternationalLicenseApplications";
            this.tsmiInternationalLicenseApplications.Size = new System.Drawing.Size(411, 54);
            this.tsmiInternationalLicenseApplications.Text = "International License Applications";
            this.tsmiInternationalLicenseApplications.Click += new System.EventHandler(this.tsmiInternationalLicenseApplications_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(366, 6);
            // 
            // tsmiDetainLicenses
            // 
            this.tsmiDetainLicenses.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiManageDetainedLicenses,
            this.tsmiDetainLicense,
            this.tsmiReleaseDetainedLicense});
            this.tsmiDetainLicenses.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.tsmiDetainLicenses.Image = global::UserInterfaceLayer.Properties.Resources.Detain_64;
            this.tsmiDetainLicenses.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDetainLicenses.Name = "tsmiDetainLicenses";
            this.tsmiDetainLicenses.Size = new System.Drawing.Size(369, 70);
            this.tsmiDetainLicenses.Text = "Detain Licenses";
            // 
            // tsmiManageDetainedLicenses
            // 
            this.tsmiManageDetainedLicenses.Image = global::UserInterfaceLayer.Properties.Resources.Detain_64;
            this.tsmiManageDetainedLicenses.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiManageDetainedLicenses.Name = "tsmiManageDetainedLicenses";
            this.tsmiManageDetainedLicenses.Size = new System.Drawing.Size(363, 70);
            this.tsmiManageDetainedLicenses.Text = "Manage Detained Licenses";
            this.tsmiManageDetainedLicenses.Click += new System.EventHandler(this.tsmiManageDetainedLicenses_Click);
            // 
            // tsmiDetainLicense
            // 
            this.tsmiDetainLicense.Image = global::UserInterfaceLayer.Properties.Resources.Detain_64;
            this.tsmiDetainLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDetainLicense.Name = "tsmiDetainLicense";
            this.tsmiDetainLicense.Size = new System.Drawing.Size(363, 70);
            this.tsmiDetainLicense.Text = "Detain License";
            this.tsmiDetainLicense.Click += new System.EventHandler(this.tsmiDetainLicense_Click);
            // 
            // tsmiReleaseDetainedLicense
            // 
            this.tsmiReleaseDetainedLicense.Image = global::UserInterfaceLayer.Properties.Resources.Release_Detained_License_64;
            this.tsmiReleaseDetainedLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiReleaseDetainedLicense.Name = "tsmiReleaseDetainedLicense";
            this.tsmiReleaseDetainedLicense.Size = new System.Drawing.Size(363, 70);
            this.tsmiReleaseDetainedLicense.Text = "Release Detained License";
            this.tsmiReleaseDetainedLicense.Click += new System.EventHandler(this.tsmiReleaseDetainedLicense_Click);
            // 
            // tsmiManageApplicationTypes
            // 
            this.tsmiManageApplicationTypes.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.tsmiManageApplicationTypes.Image = global::UserInterfaceLayer.Properties.Resources.Application_Types_64;
            this.tsmiManageApplicationTypes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiManageApplicationTypes.Name = "tsmiManageApplicationTypes";
            this.tsmiManageApplicationTypes.Size = new System.Drawing.Size(369, 70);
            this.tsmiManageApplicationTypes.Text = "Manage Application Types";
            this.tsmiManageApplicationTypes.Click += new System.EventHandler(this.tsmiManageApplicationTypes_Click);
            // 
            // tsmiManageTestTypes
            // 
            this.tsmiManageTestTypes.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.tsmiManageTestTypes.Image = global::UserInterfaceLayer.Properties.Resources.Test_Type_64;
            this.tsmiManageTestTypes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiManageTestTypes.Name = "tsmiManageTestTypes";
            this.tsmiManageTestTypes.Size = new System.Drawing.Size(369, 70);
            this.tsmiManageTestTypes.Text = "Manage Test Types";
            this.tsmiManageTestTypes.Click += new System.EventHandler(this.tsmiManageTestTypes_Click);
            // 
            // tsmiPeople
            // 
            this.tsmiPeople.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tsmiPeople.Image = global::UserInterfaceLayer.Properties.Resources.People_64;
            this.tsmiPeople.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiPeople.Name = "tsmiPeople";
            this.tsmiPeople.Size = new System.Drawing.Size(174, 68);
            this.tsmiPeople.Text = "People";
            this.tsmiPeople.Click += new System.EventHandler(this.tsmiPeople_Click);
            // 
            // tsmiDrivers
            // 
            this.tsmiDrivers.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tsmiDrivers.Image = global::UserInterfaceLayer.Properties.Resources.Drivers_64;
            this.tsmiDrivers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDrivers.Name = "tsmiDrivers";
            this.tsmiDrivers.Size = new System.Drawing.Size(175, 68);
            this.tsmiDrivers.Text = "Drivers";
            // 
            // tsmiUsers
            // 
            this.tsmiUsers.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tsmiUsers.Image = global::UserInterfaceLayer.Properties.Resources.Users_2_64;
            this.tsmiUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiUsers.Name = "tsmiUsers";
            this.tsmiUsers.Size = new System.Drawing.Size(157, 68);
            this.tsmiUsers.Text = "Users";
            this.tsmiUsers.Click += new System.EventHandler(this.tsmiUsers_Click);
            // 
            // tsmiAccountSettings
            // 
            this.tsmiAccountSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCurrentUserInfo,
            this.tsmiChangePassword,
            this.toolStripSeparator1,
            this.tsmiSignOut});
            this.tsmiAccountSettings.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.tsmiAccountSettings.Image = global::UserInterfaceLayer.Properties.Resources.account_settings_64;
            this.tsmiAccountSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAccountSettings.Name = "tsmiAccountSettings";
            this.tsmiAccountSettings.Size = new System.Drawing.Size(291, 68);
            this.tsmiAccountSettings.Text = "Account Settings";
            // 
            // tsmiCurrentUserInfo
            // 
            this.tsmiCurrentUserInfo.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.tsmiCurrentUserInfo.Image = global::UserInterfaceLayer.Properties.Resources.PersonDetails_32;
            this.tsmiCurrentUserInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiCurrentUserInfo.Name = "tsmiCurrentUserInfo";
            this.tsmiCurrentUserInfo.Size = new System.Drawing.Size(257, 38);
            this.tsmiCurrentUserInfo.Text = "Current User Info";
            this.tsmiCurrentUserInfo.Click += new System.EventHandler(this.tsmiCurrentUserInfo_Click);
            // 
            // tsmiChangePassword
            // 
            this.tsmiChangePassword.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.tsmiChangePassword.Image = global::UserInterfaceLayer.Properties.Resources.Password_32;
            this.tsmiChangePassword.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiChangePassword.Name = "tsmiChangePassword";
            this.tsmiChangePassword.Size = new System.Drawing.Size(257, 38);
            this.tsmiChangePassword.Text = "Change Password";
            this.tsmiChangePassword.Click += new System.EventHandler(this.tsmiChangePassword_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(254, 6);
            // 
            // tsmiSignOut
            // 
            this.tsmiSignOut.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.tsmiSignOut.Image = global::UserInterfaceLayer.Properties.Resources.sign_out_32__2;
            this.tsmiSignOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiSignOut.Name = "tsmiSignOut";
            this.tsmiSignOut.Size = new System.Drawing.Size(257, 38);
            this.tsmiSignOut.Text = "Sign Out";
            this.tsmiSignOut.Click += new System.EventHandler(this.tsmiSignOut_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1420, 726);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // frMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1420, 798);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frMain";
            this.Text = "Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiApplications;
        private System.Windows.Forms.ToolStripMenuItem tsmiPeople;
        private System.Windows.Forms.ToolStripMenuItem tsmiDrivers;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsers;
        private System.Windows.Forms.ToolStripMenuItem tsmiAccountSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiCurrentUserInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangePassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSignOut;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageApplications;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageApplicationTypes;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageTestTypes;
        private System.Windows.Forms.ToolStripMenuItem tsmiDrivingLicensesServices;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewDrivingLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiLocalLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiInternationalLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiRenewDrivingLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiReplacementForLostOrDamagedLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiReleaseDetainedDrivingLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiRetakeTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiDetainLicenses;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageDetainedLicenses;
        private System.Windows.Forms.ToolStripMenuItem tsmiDetainLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiReleaseDetainedLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiLocalDrivingLicenseApplications;
        private System.Windows.Forms.ToolStripMenuItem tsmiInternationalLicenseApplications;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

