namespace UserInterfaceLayer
{
    partial class frmTestFrom
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtLicenseIDFilter = new System.Windows.Forms.TextBox();
            this.lblFilterTag = new System.Windows.Forms.Label();
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
            this.llShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.epValidation = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFilter.SuspendLayout();
            this.gbDetainInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epValidation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(810, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Release Detained License";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnSearch);
            this.gbFilter.Controls.Add(this.txtLicenseIDFilter);
            this.gbFilter.Controls.Add(this.lblFilterTag);
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbFilter.Location = new System.Drawing.Point(20, 68);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(802, 72);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter License";
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(365, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtLicenseIDFilter
            // 
            this.txtLicenseIDFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLicenseIDFilter.Location = new System.Drawing.Point(135, 30);
            this.txtLicenseIDFilter.Name = "txtLicenseIDFilter";
            this.txtLicenseIDFilter.Size = new System.Drawing.Size(210, 25);
            this.txtLicenseIDFilter.TabIndex = 1;
            this.txtLicenseIDFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseIDFilter_KeyPress);
            this.txtLicenseIDFilter.Validating += new System.ComponentModel.CancelEventHandler(this.txtLicenseIDFilter_Validating);
            // 
            // lblFilterTag
            // 
            this.lblFilterTag.AutoSize = true;
            this.lblFilterTag.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFilterTag.Location = new System.Drawing.Point(25, 33);
            this.lblFilterTag.Name = "lblFilterTag";
            this.lblFilterTag.Size = new System.Drawing.Size(79, 19);
            this.lblFilterTag.TabIndex = 0;
            this.lblFilterTag.Text = "License ID:";
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
            this.gbDetainInfo.Location = new System.Drawing.Point(20, 150);
            this.gbDetainInfo.Name = "gbDetainInfo";
            this.gbDetainInfo.Size = new System.Drawing.Size(802, 220);
            this.gbDetainInfo.TabIndex = 2;
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
            // llShowLicenseHistory
            // 
            this.llShowLicenseHistory.AutoSize = true;
            this.llShowLicenseHistory.Enabled = false;
            this.llShowLicenseHistory.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.llShowLicenseHistory.Location = new System.Drawing.Point(20, 395);
            this.llShowLicenseHistory.Name = "llShowLicenseHistory";
            this.llShowLicenseHistory.Size = new System.Drawing.Size(144, 19);
            this.llShowLicenseHistory.TabIndex = 3;
            this.llShowLicenseHistory.TabStop = true;
            this.llShowLicenseHistory.Text = "Show License History";
            this.llShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseHistory_LinkClicked);
            // 
            // llShowLicenseInfo
            // 
            this.llShowLicenseInfo.AutoSize = true;
            this.llShowLicenseInfo.Enabled = false;
            this.llShowLicenseInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.llShowLicenseInfo.Location = new System.Drawing.Point(185, 395);
            this.llShowLicenseInfo.Name = "llShowLicenseInfo";
            this.llShowLicenseInfo.Size = new System.Drawing.Size(129, 19);
            this.llShowLicenseInfo.TabIndex = 4;
            this.llShowLicenseInfo.TabStop = true;
            this.llShowLicenseInfo.Text = "Show Licenses Info";
            this.llShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseInfo_LinkClicked);
            // 
            // btnRelease
            // 
            this.btnRelease.Enabled = false;
            this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelease.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnRelease.Location = new System.Drawing.Point(712, 388);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(110, 32);
            this.btnRelease.TabIndex = 5;
            this.btnRelease.Text = "Release";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(595, 388);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // epValidation
            // 
            this.epValidation.ContainerControl = this;
            // 
            // frmTestFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 507);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.llShowLicenseInfo);
            this.Controls.Add(this.llShowLicenseHistory);
            this.Controls.Add(this.gbDetainInfo);
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTestFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Release Detained License Application";
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.gbDetainInfo.ResumeLayout(false);
            this.gbDetainInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epValidation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtLicenseIDFilter;
        private System.Windows.Forms.Label lblFilterTag;
        private System.Windows.Forms.GroupBox gbDetainInfo;
        private System.Windows.Forms.Label lblDetainIDTag;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.Label lblDetainDateTag;
        private System.Windows.Forms.Label lblDetainDate;
        private System.Windows.Forms.Label lblApplicationFeesTag;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblTotalFeesTag;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.Label lblLicenseIDTag;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.Label lblCreatedByTag;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblFineFeesTag;
        private System.Windows.Forms.Label lblFineFees;
        private System.Windows.Forms.Label lblApplicationIDTag;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.LinkLabel llShowLicenseHistory;
        private System.Windows.Forms.LinkLabel llShowLicenseInfo;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider epValidation;
    }
}