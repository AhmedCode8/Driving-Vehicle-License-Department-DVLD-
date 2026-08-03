using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace UserInterfaceLayer
{
    public partial class frmTestFrom : Form
    {
        private int _selectedLicenseID = -1;

        public frmTestFrom()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please enter a valid License ID before searching.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedLicenseID = Convert.ToInt32(txtLicenseIDFilter.Text.Trim());
            LoadDetainInformation(_selectedLicenseID);
        }

        private void LoadDetainInformation(int licenseID)
        {
            lblDetainID.Text = "104";
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblLicenseID.Text = licenseID.ToString();
            lblCreatedBy.Text = "Admin";
            lblFineFees.Text = "$150.00";

            decimal appFees = 15.00m;
            decimal fineFees = 150.00m;
            lblTotalFees.Text = $"${appFees + fineFees:F2}";

            btnRelease.Enabled = true;
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained license?", "Confirm Release", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lblApplicationID.Text = "5002";
                btnRelease.Enabled = false;
                gbFilter.Enabled = false;

                MessageBox.Show("Detained License Released Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtLicenseIDFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtLicenseIDFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicenseIDFilter.Text))
            {
                e.Cancel = true;
                txtLicenseIDFilter.Focus();
                epValidation.SetError(txtLicenseIDFilter, "License ID is required!");
            }
            else
            {
                e.Cancel = false;
                epValidation.SetError(txtLicenseIDFilter, string.Empty);
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Opening License History...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Opening License Details...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}