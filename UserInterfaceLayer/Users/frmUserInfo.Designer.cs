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
            // 1. حجز الذاكرة للكنترول والزر (هذا السطر كان مفقوداً تماماً عندك)
            this.ctrlUserCard = new UserInterfaceLayer.User_Control.ctrlUserCard();
            this.txtClose = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // ctrlUserCard
            // 
            this.ctrlUserCard.Location = new System.Drawing.Point(12, 12);
            this.ctrlUserCard.Name = "ctrlUserCard";
            this.ctrlUserCard.Size = new System.Drawing.Size(751, 407);
            this.ctrlUserCard.TabIndex = 0;
            // 
            // txtClose
            // 
            this.txtClose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClose.Image = global::UserInterfaceLayer.Properties.Resources.Close_32;
            this.txtClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtClose.Location = new System.Drawing.Point(646, 413);
            this.txtClose.Name = "txtClose";
            this.txtClose.Size = new System.Drawing.Size(98, 39);
            this.txtClose.TabIndex = 1;
            this.txtClose.Text = "Close";
            this.txtClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtClose.UseVisualStyleBackColor = false;
            this.txtClose.Click += new System.EventHandler(this.txtClose_Click);
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 463);
            this.Controls.Add(this.txtClose);
            this.Controls.Add(this.ctrlUserCard);
            this.Name = "frmUserInfo";
            this.Text = "frmUserInfo";
            this.ResumeLayout(false);
        }

        #endregion

        // 2. تعريف الكنترول بالـ Namespace الكامل للمشروع ليجده المصمم فوراً
        private UserInterfaceLayer.User_Control.ctrlUserCard ctrlUserCard;
        private System.Windows.Forms.Button txtClose;
    }
}