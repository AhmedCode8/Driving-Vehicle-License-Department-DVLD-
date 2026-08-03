using DVDL_Logic_layer.Country;
using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlAddNewPersonCard : UserControl
    {
        // أضف هذه الخاصية في أي مكان داخل الكلاس ctrlAddNewPersonCard
        public int PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }

        private int _PersonID = -1;
        public ctrlAddNewPersonCard()
        {
            InitializeComponent();
            _FillCountriesComboBox();
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
        }
        public ctrlAddNewPersonCard(clsPersonDTO Info)
        {
            InitializeComponent();
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            FillFormFields(Info);
        }

        public clsPersonDTO ItemValues()
        {
            return new clsPersonDTO
            {
                PersonID = _PersonID, // 🌟 مهم جداً لكي تعرف طبقة البزنس أننا نعدل نفس الشخص
                NationalNo = txtNationalNo.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                SecondName = txtSecondName.Text.Trim(),
                ThirdName = txtThirdName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                DateOfBirth = dtpDateOfBirth.Value,
                Gendor = (byte)(rbFemale.Checked ? 1 : 0),
                Address = txtAddress.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                NationalityCountryID = Convert.ToInt32(cbCountries.SelectedValue),
                ImagePath = pbPersonImage.ImageLocation
            };
        }
        private void _FillCountriesComboBox()
        {
            DataTable dtCountries = clsCountryRepository.GetAllCountry();
            cbCountries.DataSource = dtCountries;
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";
            cbCountries.SelectedIndex = 82; // Iraq
        }
        public void FillFormFields(clsPersonDTO person)
        {
            if (person == null)
            {
                MessageBox.Show("No Person Data Provided!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _PersonID = person.PersonID;

            // نقل البيانات المباشر
            txtNationalNo.Text = person.NationalNo;
            txtFirstName.Text = person.FirstName;
            txtSecondName.Text = person.SecondName;
            txtThirdName.Text = person.ThirdName;
            txtLastName.Text = person.LastName;
            txtAddress.Text = person.Address;
            txtPhone.Text = person.Phone;
            txtEmail.Text = person.Email;

            dtpDateOfBirth.Value = person.DateOfBirth;
            cbCountries.SelectedValue = person.NationalityCountryID;

            // تحديد الجنس بدون If/Else قبيحة
            rbMale.Checked = (person.Gendor == 0);
            rbFemale.Checked = (person.Gendor == 1);

            // استدعاء معالجة الصورة الذكي
            _LoadPersonImage(person.ImagePath, person.Gendor);
        }
        private void _LoadPersonImage(string imagePath, byte gendor)
        {
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                pbPersonImage.ImageLocation = imagePath;
                return;
            }

            // تعيين الصورة الافتراضية إذا لم تكن هناك صورة مخصصة مرفوعة
            pbPersonImage.ImageLocation = null; // تصفير المسار أولاً
            pbPersonImage.Image = (gendor == 0) ? Properties.Resources.Male_512 : Properties.Resources.Female_512;
        }


        #region UI Events & Validations
        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pbPersonImage.ImageLocation) && rbFemale.Checked)
            {
                pbPersonImage.Image = Properties.Resources.Female_512;
            }
        }
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pbPersonImage.ImageLocation) && rbMale.Checked)
            {
                pbPersonImage.Image = Properties.Resources.Male_512;
            }
        }
        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. تصفية الملفات ليظهر للمستخدم الصور فقط لمنع اختيار ملفات غريبة (مثل PDF أو text)
            ofdPersonPictuer.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            // 2. فتح النافذة للمستخدم والانتظار حتى يختار صورة ويضغط OK
            if (ofdPersonPictuer.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.ImageLocation = ofdPersonPictuer.FileName;
            }
        }
        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            // 💡 ملاحظة: في حالة التعديل، يجب أن نسمح له بالاحتفاظ برقم الهوية الحالي الخاص به دون إطلاق خطأ
            if (ModeIsUpdateButSameNo()) return;

            if (clsPerson.IsPersonExists(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This ID is already in use, please enter another ID");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, "");
            }
        }
        private bool ModeIsUpdateButSameNo()
        {
            // دالة مساعدة سريعة للتحقق ما إذا كان الرقم ملكاً لنفس الشخص أثناء التعديل
            if (_PersonID == -1) return false;
            var person = clsPerson.GetPersonById(_PersonID);
            return person != null && person.NationalNo == txtNationalNo.Text.Trim();
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string emailText = txtEmail.Text.Trim();

            //    If the field is empty, allow it to exit and do not consider it an error
            if (string.IsNullOrEmpty(emailText))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
                return;
            }

            if (clsPerson.IsEmailFormatCorrect(emailText))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid email format! It must end with @gmail.com");
            }

        }
        #endregion
    }
}
