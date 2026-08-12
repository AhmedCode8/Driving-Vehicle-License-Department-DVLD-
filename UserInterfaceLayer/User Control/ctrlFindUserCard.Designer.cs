namespace UserInterfaceLayer.User_Control
{
    partial class ctrlFindUserCard
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnFindPerson = new System.Windows.Forms.Button();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.txtFindValue = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlPersonCard = new UserInterfaceLayer.User_Control.ctrlPersonCard();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnFindPerson);
            this.gbFilter.Controls.Add(this.btnAddNewPerson);
            this.gbFilter.Controls.Add(this.txtFindValue);
            this.gbFilter.Controls.Add(this.cbFilterBy);
            this.gbFilter.Controls.Add(this.label2);
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gbFilter.Location = new System.Drawing.Point(10, 10);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(890, 85);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnFindPerson
            // 
            this.btnFindPerson.BackColor = System.Drawing.Color.White;
            this.btnFindPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFindPerson.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnFindPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindPerson.Image = global::UserInterfaceLayer.Properties.Resources.SearchPerson;
            this.btnFindPerson.Location = new System.Drawing.Point(520, 31);
            this.btnFindPerson.Name = "btnFindPerson";
            this.btnFindPerson.Size = new System.Drawing.Size(45, 35);
            this.btnFindPerson.TabIndex = 12;
            this.btnFindPerson.UseVisualStyleBackColor = false;
            this.btnFindPerson.Click += new System.EventHandler(this.btnFindPerson_Click);
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.BackColor = System.Drawing.Color.White;
            this.btnAddNewPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewPerson.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAddNewPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewPerson.Image = global::UserInterfaceLayer.Properties.Resources.AddPerson_32;
            this.btnAddNewPerson.Location = new System.Drawing.Point(575, 31);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(45, 35);
            this.btnAddNewPerson.TabIndex = 11;
            this.btnAddNewPerson.UseVisualStyleBackColor = false;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // txtFindValue
            // 
            this.txtFindValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFindValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFindValue.ForeColor = System.Drawing.Color.Black;
            this.txtFindValue.Location = new System.Drawing.Point(285, 34);
            this.txtFindValue.Name = "txtFindValue";
            this.txtFindValue.Size = new System.Drawing.Size(220, 29);
            this.txtFindValue.TabIndex = 10;
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFilterBy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "Person ID",
            "National No."});
            this.cbFilterBy.Location = new System.Drawing.Point(100, 34);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(170, 29);
            this.cbFilterBy.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(20, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Find By:";
            // 
            // ctrlPersonCard
            // 
            this.ctrlPersonCard.BackColor = System.Drawing.Color.White;
            this.ctrlPersonCard.Location = new System.Drawing.Point(10, 105);
            this.ctrlPersonCard.Name = "ctrlPersonCard";
            this.ctrlPersonCard.Size = new System.Drawing.Size(900, 310);
            this.ctrlPersonCard.TabIndex = 0;
            // 
            // ctrlFindUserCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.ctrlPersonCard);
            this.Name = "ctrlFindUserCard";
            this.Size = new System.Drawing.Size(920, 425);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPersonCard ctrlPersonCard;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox txtFindValue;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFindPerson;
        private System.Windows.Forms.Button btnAddNewPerson;
    }
}