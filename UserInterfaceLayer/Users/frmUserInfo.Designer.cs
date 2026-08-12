namespace UserInterfaceLayer.Users
{
    partial class frmUserInfo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtClose = new System.Windows.Forms.Button();
            this.ctrlUserCard = new UserInterfaceLayer.User_Control.ctrlUserCard();
            this.SuspendLayout();
            // 
            // txtClose
            // 
            this.txtClose.BackColor = System.Drawing.Color.White;
            this.txtClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtClose.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.txtClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtClose.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtClose.Image = global::UserInterfaceLayer.Properties.Resources.Close_32;
            this.txtClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtClose.Location = new System.Drawing.Point(830, 455);
            this.txtClose.Name = "txtClose";
            this.txtClose.Size = new System.Drawing.Size(110, 40);
            this.txtClose.TabIndex = 1;
            this.txtClose.Text = "Close";
            this.txtClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtClose.UseVisualStyleBackColor = false;
            this.txtClose.Click += new System.EventHandler(this.txtClose_Click);
            // 
            // ctrlUserCard
            // 
            this.ctrlUserCard.BackColor = System.Drawing.Color.White;
            this.ctrlUserCard.Location = new System.Drawing.Point(20, 20);
            this.ctrlUserCard.Name = "ctrlUserCard";
            this.ctrlUserCard.Size = new System.Drawing.Size(920, 420);
            this.ctrlUserCard.TabIndex = 0;
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(960, 515);
            this.Controls.Add(this.txtClose);
            this.Controls.Add(this.ctrlUserCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Information";
            this.Load += new System.EventHandler(this.frmUserInfo_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        // 2. تعريف الكنترول بالـ Namespace الكامل للمشروع ليجده المصمم فوراً
        private UserInterfaceLayer.User_Control.ctrlUserCard ctrlUserCard;
        private System.Windows.Forms.Button txtClose;
    }
}