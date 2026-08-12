using DVDL_Logic_layer.Country;
using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System.IO;
using System.Windows.Forms;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlPersonCard : UserControl
    {
        public enum enGender : byte { Male = 0, Female = 1 }

        private int _personID = -1;
        public int PersonID => _personID;

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        // ==========================================
        // 1. Public Loading Methods (Overloading)
        // ==========================================

        public void LoadPersonData(int personID)
        {
            clsPersonDTO personInfo = clsPerson.GetPersonById(personID);

            if (personInfo == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("Could not find details for this person!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _DisplayPersonInfo(personInfo);
        }

        public void LoadPersonData(string NationalNo)
        {
            clsPersonDTO personInfo = clsPerson.GetPersonByNationalNo(NationalNo);

            if (personInfo == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("Could not find details for this person!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _DisplayPersonInfo(personInfo);
        }

        public void LoadPersonData(clsPersonDTO personInfo)
        {
            if (personInfo == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No person data available to display!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _DisplayPersonInfo(personInfo);
        }

        // ==========================================
        // 2. Internal Display & Mapping Logic
        // ==========================================

        private void _DisplayPersonInfo(clsPersonDTO personInfo)
        {
            _personID = personInfo.PersonID;

            _FillTextData(personInfo);
            _FillCountryData(personInfo.NationalityCountryID);
            _LoadPersonImage(personInfo.ImagePath, (enGender)personInfo.Gendor);
        }

        private void _FillTextData(clsPersonDTO personInfo)
        {
            lblPersonID.Text = personInfo.PersonID.ToString();
            lblNationalNo.Text = personInfo.NationalNo;
            lblFullName.Text = personInfo.FullName;
            lblPhone.Text = personInfo.Phone;
            lblAddress.Text = personInfo.Address;
            lblDateOfBirth.Text = personInfo.DateOfBirth.ToShortDateString();
            lblEmail.Text = string.IsNullOrWhiteSpace(personInfo.Email) ? "No Email Provided" : personInfo.Email;
            lblGender.Text = (personInfo.Gendor == (byte)enGender.Male) ? "Male" : "Female";
        }

        private void _FillCountryData(int countryID)
        {
            clsCountryDTO countryDTO = clsCountry.GetCountryByID(countryID);
            lblCountry.Text = countryDTO != null ? countryDTO.CountryName : "Unknown";
        }

        private void _LoadPersonImage(string imagePath, enGender gender)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                pbPersonImage.ImageLocation = imagePath;
                return;
            }

            pbPersonImage.ImageLocation = null;
            pbPersonImage.Image = (gender == enGender.Male)
                ? Properties.Resources.Man_32
                : Properties.Resources.Female_512;
        }

        private void _ResetPersonInfo()
        {
            _personID = -1;
            lblPersonID.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblFullName.Text = "[???]";
            lblGender.Text = "[???]";
            lblEmail.Text = "[???]";
            lblPhone.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblCountry.Text = "[???]";
            lblAddress.Text = "[???]";
            pbPersonImage.Image = Properties.Resources.Man_32;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdPersonPictuer.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (ofdPersonPictuer.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.ImageLocation = ofdPersonPictuer.FileName;
            }
        }
    }
}