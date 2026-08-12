using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.People
{
    public partial class frmPersonDetails : Form
    {
        private int _personID = -1;
        private string _nationalNo = string.Empty;
        private clsPersonDTO _personInfo;

        // Constructor accepting Person ID
        public frmPersonDetails(int personID)
        {
            InitializeComponent();
            _personID = personID;
        }

        // Constructor accepting National Number
        public frmPersonDetails(string nationalNo)
        {
            InitializeComponent();
            _nationalNo = nationalNo;
        }

        // Constructor accepting DTO object directly
        public frmPersonDetails(clsPersonDTO personInfo)
        {
            InitializeComponent();
            _personInfo = personInfo;
            _personID = personInfo != null ? personInfo.PersonID : -1;
        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {
            _LoadPersonDetails();
        }

        private void _LoadPersonDetails()
        {
            if (_personInfo != null)
            {
                ctrlPersonCard1.LoadPersonData(_personInfo);
            }
            else if (_personID != -1)
            {
                ctrlPersonCard1.LoadPersonData(_personID);
            }
            else if (!string.IsNullOrWhiteSpace(_nationalNo))
            {
                ctrlPersonCard1.LoadPersonData(_nationalNo);
            }
            else
            {
                MessageBox.Show("No valid person identifier was provided!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}