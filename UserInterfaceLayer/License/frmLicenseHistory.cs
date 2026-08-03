using DVDL_Logic_layer.driver;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.License
{
    public partial class frmLicenseHistory : Form
    {
        int _PersonID = -1;
        int _Driver = -1;

        public frmLicenseHistory()
        {
            InitializeComponent();
        }
        public frmLicenseHistory(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
            clsDriverDTO info = clsDrivers.GetDriverByPersonID(_PersonID);

            _Driver = info.DriverID;

        }
        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID == 1)
            {
                MessageBox.Show("No _PersonID Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            ctrlFindUserCard1.LoadPersonData(_PersonID);
            ctrlLicenseHistoryCard1.LoadData(_Driver);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
