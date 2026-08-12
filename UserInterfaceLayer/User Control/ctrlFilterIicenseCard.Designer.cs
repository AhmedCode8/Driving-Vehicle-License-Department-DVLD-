namespace UserInterfaceLayer.User_Control
{
    partial class ctrlFilterIicenseCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFindLicense = new System.Windows.Forms.Button();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.ctrlDriverLicenseInfoCard1 = new UserInterfaceLayer.User_Control.ctrlDriverLicenseInfoCard();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Controls.Add(this.btnFindLicense);
            this.gbFilter.Controls.Add(this.txtLicenseID);
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gbFilter.Location = new System.Drawing.Point(10, 10);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(930, 85);
            this.gbFilter.TabIndex = 0;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(20, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 103;
            this.label1.Text = "License ID:";
            // 
            // btnFindLicense
            // 
            this.btnFindLicense.BackColor = System.Drawing.Color.White;
            this.btnFindLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFindLicense.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnFindLicense.FlatAppearance.BorderSize = 1;
            this.btnFindLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindLicense.Image = global::UserInterfaceLayer.Properties.Resources.License_View_32;
            this.btnFindLicense.Location = new System.Drawing.Point(390, 31);
            this.btnFindLicense.Name = "btnFindLicense";
            this.btnFindLicense.Size = new System.Drawing.Size(45, 35);
            this.btnFindLicense.TabIndex = 102;
            this.btnFindLicense.UseVisualStyleBackColor = false;
            this.btnFindLicense.Click += new System.EventHandler(this.btnFindLicense_Click);
            // 
            // txtLicenseID
            // 
            this.txtLicenseID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicenseID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicenseID.ForeColor = System.Drawing.Color.Black;
            this.txtLicenseID.Location = new System.Drawing.Point(115, 34);
            this.txtLicenseID.Name = "txtLicenseID";
            this.txtLicenseID.Size = new System.Drawing.Size(260, 29);
            this.txtLicenseID.TabIndex = 101;
            this.txtLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseID_KeyPress);
            // 
            // ctrlDriverLicenseInfoCard1
            // 
            this.ctrlDriverLicenseInfoCard1.BackColor = System.Drawing.Color.White;
            this.ctrlDriverLicenseInfoCard1.Location = new System.Drawing.Point(6, 100);
            this.ctrlDriverLicenseInfoCard1.Name = "ctrlDriverLicenseInfoCard1";
            this.ctrlDriverLicenseInfoCard1.Size = new System.Drawing.Size(937, 343);
            this.ctrlDriverLicenseInfoCard1.TabIndex = 1;
            // 
            // ctrlFilterIicenseCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ctrlDriverLicenseInfoCard1);
            this.Controls.Add(this.gbFilter);
            this.Name = "ctrlFilterIicenseCard";
            this.Size = new System.Drawing.Size(950, 450);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button btnFindLicense;
        private System.Windows.Forms.TextBox txtLicenseID;
        private System.Windows.Forms.Label label1;
        private ctrlDriverLicenseInfoCard ctrlDriverLicenseInfoCard1;
    }
}