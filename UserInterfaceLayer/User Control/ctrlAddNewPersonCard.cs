using DVDL_Logic_layer.Country;
using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlAddNewPersonCard : UserControl
    {
        public enum enGender : byte { Male = 0, Female = 1 }

        private int _personID = -1;
        private string _originalNationalNo = string.Empty;

        private clsPersonDTO _personDTO;

        public int PersonID => _personID;

        public ctrlAddNewPersonCard()
        {
            InitializeComponent();
        }

        public ctrlAddNewPersonCard(clsPersonDTO personInfo) : this()
        {
            _personDTO = personInfo;
        }

        // ==========================================
        // 1. Form Initialization & Data Loading
        // ==========================================

        private void ctrlAddNewPersonCard_Load(object sender, EventArgs e)
        {
            _InitializeFormSettings();

            if (_personDTO != null)
            {
                LoadPersonData(_personDTO);
            }
        }

        private void _InitializeFormSettings()
        {
            _FillCountriesComboBox();
            // جلب الحد الأدنى للسن القانوني من قوانين البزنس (BLL)
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-clsPerson.MinimumAllowedAge);
        }

        public void LoadPersonData(clsPersonDTO person)
        {
            if (person == null)
            {
                MessageBox.Show("لم يتم توفير بيانات للشخص!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _personDTO = person;
            _personID = person.PersonID;
            _originalNationalNo = person.NationalNo;

            _MapDTOToForm(person);
        }

        public clsPersonDTO GetPersonDTOFromUI()
        {
            return new clsPersonDTO
            {
                PersonID = _personID,
                NationalNo = txtNationalNo.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                SecondName = txtSecondName.Text.Trim(),
                ThirdName = txtThirdName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                DateOfBirth = dtpDateOfBirth.Value,
                Gendor = (byte)(rbFemale.Checked ? enGender.Female : enGender.Male),
                Address = txtAddress.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                NationalityCountryID = Convert.ToInt32(cbCountries.SelectedValue),
                ImagePath = pbPersonImage.ImageLocation
            };
        }

        // ==========================================
        // 2. Data Mapping & Controls Binding
        // ==========================================

        private void _FillCountriesComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountry();
            cbCountries.DataSource = dtCountries;
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";

            cbCountries.SelectedValue = clsCountry.DefaultCountryID;
        }

        private void _MapDTOToForm(clsPersonDTO person)
        {
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

            rbMale.Checked = (person.Gendor == (byte)enGender.Male);
            rbFemale.Checked = (person.Gendor == (byte)enGender.Female);

            _LoadPersonImage(person.ImagePath, (enGender)person.Gendor);
        }

        // ==========================================
        // 3. Image Handling Logic
        // ==========================================

        private void _LoadPersonImage(string imagePath, enGender gender)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                pbPersonImage.ImageLocation = imagePath;
                return;
            }

            pbPersonImage.ImageLocation = null;
            _SetDefaultAvatar(gender);
        }

        private void _SetDefaultAvatar(enGender gender)
        {
            pbPersonImage.Image = (gender == enGender.Male)
                ? Properties.Resources.Male_512
                : Properties.Resources.Female_512;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pbPersonImage.ImageLocation) && rbMale.Checked)
                _SetDefaultAvatar(enGender.Male);
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pbPersonImage.ImageLocation) && rbFemale.Checked)
                _SetDefaultAvatar(enGender.Female);
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdPersonPictuer.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (ofdPersonPictuer.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.ImageLocation = ofdPersonPictuer.FileName;
            }
        }

        // ==========================================
        // 4. Input Validations
        // ==========================================

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            string enteredNationalNo = txtNationalNo.Text.Trim();

            if (string.IsNullOrEmpty(enteredNationalNo))
            {
                _SetValidationError(e, txtNationalNo, "الرقم القومي مطلوب!");
                return;
            }

            // التحقق في الذاكرة أولاً لمنع الاستعلام الزائد عن الحاجة من قاعدة البيانات
            if (_IsNationalNoUnchanged(enteredNationalNo))
            {
                _ClearValidationError(e, txtNationalNo);
                return;
            }

            // الذهاب للـ BLL فقط إذا تم تغيير النص فعلياً
            if (clsPerson.IsPersonExists(enteredNationalNo))
            {
                _SetValidationError(e, txtNationalNo, "الرقم القومي مستخدم بالفعل لشخص آخر!");
            }
            else
            {
                _ClearValidationError(e, txtNationalNo);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string emailText = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(emailText))
            {
                _ClearValidationError(e, txtEmail);
                return;
            }

            //  تفويض الفحص بالكامل للـ BLL
            if (clsPerson.IsValidEmail(emailText))
            {
                _ClearValidationError(e, txtEmail);
            }
            else
            {
                _SetValidationError(e, txtEmail, "صيغة البريد الإلكتروني غير صالحة!");
            }
        }

        private bool _IsNationalNoUnchanged(string enteredNationalNo)
        {
            return _personID != -1 && string.Equals(enteredNationalNo, _originalNationalNo, StringComparison.OrdinalIgnoreCase);
        }

        private void _SetValidationError(CancelEventArgs e, Control control, string errorMessage)
        {
            e.Cancel = true;
            errorProvider1.SetError(control, errorMessage);
        }

        private void _ClearValidationError(CancelEventArgs e, Control control)
        {
            e.Cancel = false;
            errorProvider1.SetError(control, string.Empty);
        }
    }
}







































//using DVDL_Logic_layer.Country;
//using DVDL_Logic_layer.Person;
//using DVLD_DTOs;
//using System;
//using System.ComponentModel;
//using System.Data;
//using System.Windows.Forms;

//namespace UserInterfaceLayer.User_Control
//{
//    public partial class ctrlAddNewPersonCard : UserControl
//    {
//        public int PersonID
//        {
//            get { return _PersonID; }
//            set { _PersonID = value; }
//        }

//        private int _PersonID = -1;

//        clsPersonDTO _Person;
//        enum enGender
//        { 
//            Male   = 0,
//            Female = 1 
//        }
//        enGender gender = new enGender();
//        public ctrlAddNewPersonCard()
//        {
//            InitializeComponent();

//        }
//        public ctrlAddNewPersonCard(clsPersonDTO Info)
//        {
//            InitializeComponent();
//            _Person = Info;
//        }
//        private void ctrlAddNewPersonCard_Load(object sender, EventArgs e)
//        {
//            _FillCountriesComboBox();
//            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
//            FillFormFields(_Person);


//        }

//        public clsPersonDTO ItemValues()
//        {
//            return new clsPersonDTO
//            {
//                PersonID = _PersonID, // 🌟 مهم جداً لكي تعرف طبقة البزنس أننا نعدل نفس الشخص
//                NationalNo = txtNationalNo.Text.Trim(),
//                FirstName = txtFirstName.Text.Trim(),
//                SecondName = txtSecondName.Text.Trim(),
//                ThirdName = txtThirdName.Text.Trim(),
//                LastName = txtLastName.Text.Trim(),
//                DateOfBirth = dtpDateOfBirth.Value,
//                Gendor = (byte)(rbFemale.Checked ? 1 : 0),
//                Address = txtAddress.Text.Trim(),
//                Phone = txtPhone.Text.Trim(),
//                Email = txtEmail.Text.Trim(),
//                NationalityCountryID = Convert.ToInt32(cbCountries.SelectedValue),
//                ImagePath = pbPersonImage.ImageLocation
//            };
//        }
//        public void FillFormFields(clsPersonDTO person)
//        {
//            if (person == null)
//            {
//                MessageBox.Show("No Person Data Provided!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            _PersonID = person.PersonID;

//            // نقل البيانات المباشر
//            txtNationalNo.Text = person.NationalNo;
//            txtFirstName.Text = person.FirstName;
//            txtSecondName.Text = person.SecondName;
//            txtThirdName.Text = person.ThirdName;
//            txtLastName.Text = person.LastName;
//            txtAddress.Text = person.Address;
//            txtPhone.Text = person.Phone;
//            txtEmail.Text = person.Email;

//            dtpDateOfBirth.Value = person.DateOfBirth;
//            cbCountries.SelectedValue = person.NationalityCountryID;

//            rbMale.Checked = (person.Gendor == );
//            rbFemale.Checked = (person.Gendor == 1);
//            _OriginalNationalNo = person.NationalNo;

//            // استدعاء معالجة الصورة الذكي
//            _LoadPersonImage(person.ImagePath, person.Gendor);
//        }

//        private void _FillCountriesComboBox()
//        {
//            DataTable dtCountries = clsCountryRepository.GetAllCountry();
//            cbCountries.DataSource = dtCountries;
//            cbCountries.DisplayMember = "CountryName";
//            cbCountries.ValueMember = "CountryID";
//            cbCountries.SelectedIndex = 82; // Iraq
//        }
//        private void _LoadPersonImage(string imagePath, byte gendor)
//        {
//            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
//            {
//                pbPersonImage.ImageLocation = imagePath;
//                return;
//            }

//            // تعيين الصورة الافتراضية إذا لم تكن هناك صورة مخصصة مرفوعة
//            pbPersonImage.ImageLocation = null; // تصفير المسار أولاً
//            pbPersonImage.Image = (gendor == 0) ? Properties.Resources.Male_512 : Properties.Resources.Female_512;
//        }
//        private void rbFemale_CheckedChanged(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(pbPersonImage.ImageLocation) && rbFemale.Checked)
//            {
//                pbPersonImage.Image = Properties.Resources.Female_512;
//            }
//        }
//        private void rbMale_CheckedChanged(object sender, EventArgs e)
//        {
//            if (string.IsNullOrEmpty(pbPersonImage.ImageLocation) && rbMale.Checked)
//            {
//                pbPersonImage.Image = Properties.Resources.Male_512;
//            }
//        }
//        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
//        {
//            // 1. تصفية الملفات ليظهر للمستخدم الصور فقط لمنع اختيار ملفات غريبة (مثل PDF أو text)
//            ofdPersonPictuer.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

//            // 2. فتح النافذة للمستخدم والانتظار حتى يختار صورة ويضغط OK
//            if (ofdPersonPictuer.ShowDialog() == DialogResult.OK)
//            {
//                pbPersonImage.ImageLocation = ofdPersonPictuer.FileName;
//            }
//        }
//        private string _OriginalNationalNo = "";
//        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
//        {




//            string enteredNo = txtNationalNo.Text.Trim();

//            // 1. إذا كان الرقم المدخل هو نفس الرقم القديم تماماً، فلا توجد مشكلة (تخطي الفحص تماماً!)
//            if (_PersonID != -1 && enteredNo == _OriginalNationalNo)
//            {
//                e.Cancel = false;
//                errorProvider1.SetError(txtNationalNo, "");
//                return;
//            }

//            // 2. إذا غير الرقم، نتحقق هل الرقم الجديد موجود لشخص آخر في قاعدة البيانات أم لا
//            if (clsPerson.IsPersonExists(enteredNo))
//            {
//                e.Cancel = true;
//                errorProvider1.SetError(txtNationalNo, "This ID is already in use by another person!");
//            }
//            else
//            {
//                e.Cancel = false;
//                errorProvider1.SetError(txtNationalNo, "");
//            }
//        }

//        private void txtEmail_Validating(object sender, CancelEventArgs e)
//        {
//            string emailText = txtEmail.Text.Trim();

//            //    If the field is empty, allow it to exit and do not consider it an error
//            if (string.IsNullOrEmpty(emailText))
//            {
//                e.Cancel = false;
//                errorProvider1.SetError(txtEmail, "");
//                return;
//            }

//            if (clsPerson.IsEmailFormatCorrect(emailText))
//            {
//                e.Cancel = false;
//                errorProvider1.SetError(txtEmail, "");
//            }
//            else
//            {
//                e.Cancel = true;
//                errorProvider1.SetError(txtEmail, "Invalid email format! It must end with @gmail.com");
//            }

//        }


//    }
//}
