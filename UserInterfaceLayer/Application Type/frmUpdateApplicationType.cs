using DVDL_Logic_layer.Application_Types;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.Application_Type
{
    public partial class frmUpdateApplicationType : Form
    {
        clsApplicationTypeDTO Info;
        public frmUpdateApplicationType()
        {
            InitializeComponent();
        }
        public event Action ApplicationTypeUpdated;
        public frmUpdateApplicationType(int applicationTypeID)
        {
            InitializeComponent();

            Info = clsApplicationTypes.GetApplicationTypeByID(applicationTypeID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            if (Info == null) return;
            lblApplicationTypeID.Text = Info.ApplicationTypeID.ToString();
            txtTitle.Text = Info.ApplicationTypeTitle;
            txtFees.Text = Info.ApplicationFees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Info.ApplicationTypeTitle = txtTitle.Text.Trim();

            if (decimal.TryParse(txtFees.Text.Trim(), out decimal fees))
            {
                Info.ApplicationFees = fees;
            }
            else
            {
                MessageBox.Show("Please enter a valid fee value (numbers only).", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (clsApplicationTypes.UpdateApplicationType(Info) != 0)
            {
                ApplicationTypeUpdated?.Invoke();
                MessageBox.Show("Order type data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred while trying to update data.", "Update failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
