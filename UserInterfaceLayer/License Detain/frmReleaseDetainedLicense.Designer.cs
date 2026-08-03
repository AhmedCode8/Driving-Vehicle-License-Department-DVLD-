namespace UserInterfaceLayer.License_Detain
{
    partial class frmReleaseDetainedLicense
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
            this.ctrlFilterIicenseCard1 = new UserInterfaceLayer.User_Control.ctrlFilterIicenseCard();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRelease = new System.Windows.Forms.Button();
            this.llShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.gbDetainInfo = new System.Windows.Forms.GroupBox();
            this.lblApplicationID = new System.Windows.Forms.Label();
            this.lblApplicationIDTag = new System.Windows.Forms.Label();
            this.lblFineFees = new System.Windows.Forms.Label();
            this.lblFineFeesTag = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblCreatedByTag = new System.Windows.Forms.Label();
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.lblLicenseIDTag = new System.Windows.Forms.Label();
            this.lblTotalFees = new System.Windows.Forms.Label();
            this.lblTotalFeesTag = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblApplicationFeesTag = new System.Windows.Forms.Label();
            this.lblDetainDate = new System.Windows.Forms.Label();
            this.lblDetainDateTag = new System.Windows.Forms.Label();
            this.lblDetainID = new System.Windows.Forms.Label();
            this.lblDetainIDTag = new System.Windows.Forms.Label();
            this.gbDetainInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlFilterIicenseCard1
            // 
            this.ctrlFilterIicenseCard1.Location = new System.Drawing.Point(12, 12);
            this.ctrlFilterIicenseCard1.Name = "ctrlFilterIicenseCard1";
            this.ctrlFilterIicenseCard1.Size = new System.Drawing.Size(945, 439);
            this.ctrlFilterIicenseCard1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(707, 694);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRelease
            // 
            this.btnRelease.Enabled = false;
            this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelease.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnRelease.Location = new System.Drawing.Point(824, 694);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(110, 32);
            this.btnRelease.TabIndex = 10;
            this.btnRelease.Text = "Release";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // llShowLicenseInfo
            // 
            this.llShowLicenseInfo.AutoSize = true;
            this.llShowLicenseInfo.Enabled = false;
            this.llShowLicenseInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.llShowLicenseInfo.Location = new System.Drawing.Point(177, 702);
            this.llShowLicenseInfo.Name = "llShowLicenseInfo";
            this.llShowLicenseInfo.Size = new System.Drawing.Size(129, 19);
            this.llShowLicenseInfo.TabIndex = 9;
            this.llShowLicenseInfo.TabStop = true;
            this.llShowLicenseInfo.Text = "Show Licenses Info";
            // 
            // llShowLicenseHistory
            // 
            this.llShowLicenseHistory.AutoSize = true;
            this.llShowLicenseHistory.Enabled = false;
            this.llShowLicenseHistory.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.llShowLicenseHistory.Location = new System.Drawing.Point(12, 702);
            this.llShowLicenseHistory.Name = "llShowLicenseHistory";
            this.llShowLicenseHistory.Size = new System.Drawing.Size(144, 19);
            this.llShowLicenseHistory.TabIndex = 8;
            this.llShowLicenseHistory.TabStop = true;
            this.llShowLicenseHistory.Text = "Show License History";
            // 
            // gbDetainInfo
            // 
            this.gbDetainInfo.Controls.Add(this.lblApplicationID);
            this.gbDetainInfo.Controls.Add(this.lblApplicationIDTag);
            this.gbDetainInfo.Controls.Add(this.lblFineFees);
            this.gbDetainInfo.Controls.Add(this.lblFineFeesTag);
            this.gbDetainInfo.Controls.Add(this.lblCreatedBy);
            this.gbDetainInfo.Controls.Add(this.lblCreatedByTag);
            this.gbDetainInfo.Controls.Add(this.lblLicenseID);
            this.gbDetainInfo.Controls.Add(this.lblLicenseIDTag);
            this.gbDetainInfo.Controls.Add(this.lblTotalFees);
            this.gbDetainInfo.Controls.Add(this.lblTotalFeesTag);
            this.gbDetainInfo.Controls.Add(this.lblApplicationFees);
            this.gbDetainInfo.Controls.Add(this.lblApplicationFeesTag);
            this.gbDetainInfo.Controls.Add(this.lblDetainDate);
            this.gbDetainInfo.Controls.Add(this.lblDetainDateTag);
            this.gbDetainInfo.Controls.Add(this.lblDetainID);
            this.gbDetainInfo.Controls.Add(this.lblDetainIDTag);
            this.gbDetainInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbDetainInfo.Location = new System.Drawing.Point(12, 457);
            this.gbDetainInfo.Name = "gbDetainInfo";
            this.gbDetainInfo.Size = new System.Drawing.Size(922, 220);
            this.gbDetainInfo.TabIndex = 7;
            this.gbDetainInfo.TabStop = false;
            this.gbDetainInfo.Text = "Detain & Application Info";
            // 
            // lblApplicationID
            // 
            this.lblApplicationID.AutoSize = true;
            this.lblApplicationID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblApplicationID.Location = new System.Drawing.Point(560, 165);
            this.lblApplicationID.Name = "lblApplicationID";
            this.lblApplicationID.Size = new System.Drawing.Size(37, 19);
            this.lblApplicationID.TabIndex = 15;
            this.lblApplicationID.Text = "[???]";
            // 
            // lblApplicationIDTag
            // 
            this.lblApplicationIDTag.AutoSize = true;
            this.lblApplicationIDTag.Location = new System.Drawing.Point(420, 165);
            this.lblApplicationIDTag.Name = "lblApplicationIDTag";
            this.lblApplicationIDTag.Size = new System.Drawing.Size(102, 19);
            this.lblApplicationIDTag.TabIndex = 14;
            this.lblApplicationIDTag.Text = "Application ID:";
            // 
            // lblFineFees
            // 
            this.lblFineFees.AutoSize = true;
            this.lblFineFees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFineFees.Location = new System.Drawing.Point(560, 125);
            this.lblFineFees.Name = "lblFineFees";
            this.lblFineFees.Size = new System.Drawing.Size(45, 19);
            this.lblFineFees.TabIndex = 13;
            this.lblFineFees.Text = "$0.00";
            // 
            // lblFineFeesTag
            // 
            this.lblFineFeesTag.AutoSize = true;
            this.lblFineFeesTag.Location = new System.Drawing.Point(420, 125);
            this.lblFineFeesTag.Name = "lblFineFeesTag";
            this.lblFineFeesTag.Size = new System.Drawing.Size(69, 19);
            this.lblFineFeesTag.TabIndex = 12;
            this.lblFineFeesTag.Text = "Fine Fees:";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCreatedBy.Location = new System.Drawing.Point(560, 85);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(43, 19);
            this.lblCreatedBy.TabIndex = 11;
            this.lblCreatedBy.Text = "[????]";
            // 
            // lblCreatedByTag
            // 
            this.lblCreatedByTag.AutoSize = true;
            this.lblCreatedByTag.Location = new System.Drawing.Point(420, 85);
            this.lblCreatedByTag.Name = "lblCreatedByTag";
            this.lblCreatedByTag.Size = new System.Drawing.Size(79, 19);
            this.lblCreatedByTag.TabIndex = 10;
            this.lblCreatedByTag.Text = "Created By:";
            // 
            // lblLicenseID
            // 
            this.lblLicenseID.AutoSize = true;
            this.lblLicenseID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLicenseID.Location = new System.Drawing.Point(560, 45);
            this.lblLicenseID.Name = "lblLicenseID";
            this.lblLicenseID.Size = new System.Drawing.Size(37, 19);
            this.lblLicenseID.TabIndex = 9;
            this.lblLicenseID.Text = "[???]";
            // 
            // lblLicenseIDTag
            // 
            this.lblLicenseIDTag.AutoSize = true;
            this.lblLicenseIDTag.Location = new System.Drawing.Point(420, 45);
            this.lblLicenseIDTag.Name = "lblLicenseIDTag";
            this.lblLicenseIDTag.Size = new System.Drawing.Size(76, 19);
            this.lblLicenseIDTag.TabIndex = 8;
            this.lblLicenseIDTag.Text = "License ID:";
            // 
            // lblTotalFees
            // 
            this.lblTotalFees.AutoSize = true;
            this.lblTotalFees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalFees.ForeColor = System.Drawing.Color.Green;
            this.lblTotalFees.Location = new System.Drawing.Point(165, 165);
            this.lblTotalFees.Name = "lblTotalFees";
            this.lblTotalFees.Size = new System.Drawing.Size(45, 19);
            this.lblTotalFees.TabIndex = 7;
            this.lblTotalFees.Text = "$0.00";
            // 
            // lblTotalFeesTag
            // 
            this.lblTotalFeesTag.AutoSize = true;
            this.lblTotalFeesTag.Location = new System.Drawing.Point(25, 165);
            this.lblTotalFeesTag.Name = "lblTotalFeesTag";
            this.lblTotalFeesTag.Size = new System.Drawing.Size(74, 19);
            this.lblTotalFeesTag.TabIndex = 6;
            this.lblTotalFeesTag.Text = "Total Fees:";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblApplicationFees.Location = new System.Drawing.Point(165, 125);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(53, 19);
            this.lblApplicationFees.TabIndex = 5;
            this.lblApplicationFees.Text = "$15.00";
            // 
            // lblApplicationFeesTag
            // 
            this.lblApplicationFeesTag.AutoSize = true;
            this.lblApplicationFeesTag.Location = new System.Drawing.Point(25, 125);
            this.lblApplicationFeesTag.Name = "lblApplicationFeesTag";
            this.lblApplicationFeesTag.Size = new System.Drawing.Size(115, 19);
            this.lblApplicationFeesTag.TabIndex = 4;
            this.lblApplicationFeesTag.Text = "Application Fees:";
            // 
            // lblDetainDate
            // 
            this.lblDetainDate.AutoSize = true;
            this.lblDetainDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetainDate.Location = new System.Drawing.Point(165, 85);
            this.lblDetainDate.Name = "lblDetainDate";
            this.lblDetainDate.Size = new System.Drawing.Size(79, 19);
            this.lblDetainDate.TabIndex = 3;
            this.lblDetainDate.Text = "[??/??/????]";
            // 
            // lblDetainDateTag
            // 
            this.lblDetainDateTag.AutoSize = true;
            this.lblDetainDateTag.Location = new System.Drawing.Point(25, 85);
            this.lblDetainDateTag.Name = "lblDetainDateTag";
            this.lblDetainDateTag.Size = new System.Drawing.Size(86, 19);
            this.lblDetainDateTag.TabIndex = 2;
            this.lblDetainDateTag.Text = "Detain Date:";
            // 
            // lblDetainID
            // 
            this.lblDetainID.AutoSize = true;
            this.lblDetainID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetainID.Location = new System.Drawing.Point(165, 45);
            this.lblDetainID.Name = "lblDetainID";
            this.lblDetainID.Size = new System.Drawing.Size(37, 19);
            this.lblDetainID.TabIndex = 1;
            this.lblDetainID.Text = "[???]";
            // 
            // lblDetainIDTag
            // 
            this.lblDetainIDTag.AutoSize = true;
            this.lblDetainIDTag.Location = new System.Drawing.Point(25, 45);
            this.lblDetainIDTag.Name = "lblDetainIDTag";
            this.lblDetainIDTag.Size = new System.Drawing.Size(71, 19);
            this.lblDetainIDTag.TabIndex = 0;
            this.lblDetainIDTag.Text = "Detain ID:";
            // 
            // frmManageDetainedLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 736);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.llShowLicenseInfo);
            this.Controls.Add(this.llShowLicenseHistory);
            this.Controls.Add(this.gbDetainInfo);
            this.Controls.Add(this.ctrlFilterIicenseCard1);
            this.Name = "frmReleaseDetainedLicense";
            this.Text = "frmReleaseDetainedLicense";
            this.Load += new System.EventHandler(this.frmReleaseDetainedLicense_Load);
            this.gbDetainInfo.ResumeLayout(false);
            this.gbDetainInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private User_Control.ctrlFilterIicenseCard ctrlFilterIicenseCard1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.LinkLabel llShowLicenseInfo;
        private System.Windows.Forms.LinkLabel llShowLicenseHistory;
        private System.Windows.Forms.GroupBox gbDetainInfo;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.Label lblApplicationIDTag;
        private System.Windows.Forms.Label lblFineFees;
        private System.Windows.Forms.Label lblFineFeesTag;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblCreatedByTag;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.Label lblLicenseIDTag;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.Label lblTotalFeesTag;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblApplicationFeesTag;
        private System.Windows.Forms.Label lblDetainDate;
        private System.Windows.Forms.Label lblDetainDateTag;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.Label lblDetainIDTag;
    }
}