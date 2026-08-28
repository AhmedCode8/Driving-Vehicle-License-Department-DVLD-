using DVDL_Logic_layer.Test_Types;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.Test_Types
{
    public partial class frmUpdateTestTypes : Form
    {
        clsTestTypeDTO Info;
        public frmUpdateTestTypes()
        {
            InitializeComponent();
        }
        public frmUpdateTestTypes(int testTypeID)
        {
            InitializeComponent();
            Info = clsTestTypes.GetTestTypeByID(testTypeID);
        }
        public event Action TestTypesUpdated;


        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            lblTestTypeID.Text = Info.TestTypeID.ToString();
            txtTitle.Text = Info.TestTypeTitle;
            txtFees.Text = Info.TestTypeFees.ToString();
            txtDescription.Text = Info.TestTypeDescription;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Info.TestTypeTitle = txtTitle.Text.Trim();
            Info.TestTypeDescription = txtDescription.Text.Trim();

            if (!decimal.TryParse(txtFees.Text.Trim(), out decimal fees))
            {
                MessageBox.Show("Please enter a valid fees value (numbers only).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Info.TestTypeFees = fees;

            if (clsTestTypes.UpdateTestType(Info) == 0)
            {
                MessageBox.Show("An error occurred while saving the data. Please try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TestTypesUpdated?.Invoke();
            MessageBox.Show("Test type data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
