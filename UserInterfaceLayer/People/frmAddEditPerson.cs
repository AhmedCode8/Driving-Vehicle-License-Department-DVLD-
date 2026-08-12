//using DVDL_Logic_layer.Person;
//using DVLD_DTOs;
//using System;
//using System.Windows.Forms;

//namespace UserInterfaceLayer.People
//{
//    public partial class frmAddEditPerson : Form
//    {


//        public clsPerson person = new clsPerson();


//        public frmAddEditPerson()
//        {
//            InitializeComponent();
//        }
//        public frmAddEditPerson(clsPersonDTO Info)
//        {
//            InitializeComponent();
//            ctrlAddNewPersonCard1.FillFormFields(Info);
//            if (Info == null) return;
//            person.Mode = clsPerson.enMode.Update;
//            person.PersonID = Info.PersonID;
//            lblPersonID.Text = person.PersonID.ToString();
//        }

//        public event Action<int> PersonSaved;



//        private void frmAddEditPerson_Load(object sender, System.EventArgs e)
//        {
//            // تحديث عنوان الشاشة بناءً على الحالة
//            this.Text = (person.Mode == clsPerson.enMode.AddNew) ? "Add New Person" : "Edit Person Info";
//            this.lblTitle.Text = this.Text;

//        }


//        private void btnSave_Click(object sender, System.EventArgs e)
//        {

//            clsPersonDTO data = ctrlAddNewPersonCard1.ItemValues();

//            person.PersonID = data.PersonID;
//            person.NationalNo = data.NationalNo;
//            person.FirstName = data.FirstName;
//            person.SecondName = data.SecondName;
//            person.ThirdName = data.ThirdName;
//            person.LastName = data.LastName;
//            person.DateOfBirth = data.DateOfBirth;
//            person.Gendor = data.Gendor;
//            person.Address = data.Address;
//            person.Phone = data.Phone;
//            person.Email = data.Email;
//            person.NationalityCountryID = data.NationalityCountryID;
//            person.ImagePath = data.ImagePath;


//            int savedPersonID = person.Save();

//            if (savedPersonID != -1)
//            {
//                PersonSaved?.Invoke(savedPersonID);

//                MessageBox.Show("Data Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                person.PersonID = savedPersonID;
//                ctrlAddNewPersonCard1.PersonID = savedPersonID; // 👈 هذا السطر سينقذك من الفخ

//                person.Mode = clsPerson.enMode.Update;
//                this.Text = "Edit Person Info";
//                this.lblTitle.Text = this.Text;
//                lblPersonID.Text = person.PersonID.ToString();
//            }
//            else
//            {
//                MessageBox.Show("Error: Data could not be saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }


//        }

//        private void btnClose_Click(object sender, System.EventArgs e)
//        {
//            this.Close();
//        }
//    }
//}
using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.People
{
    public partial class frmAddEditPerson : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _mode = enMode.AddNew;

        private int _personID = -1;
        private clsPersonDTO _personDTO;

        // حدث يُستخدم لإعلام الشاشة الأب بتعديل أو إضافة شخص (Loose Coupling)
        public event Action<int> PersonSaved;

        // الباني الإفتراضي لشهادة الإضافة
        public frmAddEditPerson()
        {
            InitializeComponent();
            _mode = enMode.AddNew;
        }

        // باني التعديل باستخدام PersonID
        public frmAddEditPerson(int personID) : this()
        {
            _personID = personID;
            _mode = enMode.Update;
        }

        // باني التعديل باستخدام DTO جاهز
        public frmAddEditPerson(clsPersonDTO personDTO) : this()
        {
            if (personDTO != null)
            {
                _personDTO = personDTO;
                _personID = personDTO.PersonID;
                _mode = enMode.Update;
            }
        }

        // ==========================================
        // 1. Form Load & UI Setup
        // ==========================================

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _SetFormTitles();

            if (_mode == enMode.Update)
            {
                _LoadPersonData();
            }
        }

        private void _SetFormTitles()
        {
            bool isAddNew = (_mode == enMode.AddNew);

            this.Text = isAddNew ? "Add New Person" : "Edit Person Info";
            lblTitle.Text = this.Text;
            lblPersonID.Text = isAddNew ? "N/A" : _personID.ToString();
        }

        private void _LoadPersonData()
        {
            if (_personDTO == null)
            {
                _personDTO = clsPerson.GetPersonById(_personID);
            }

            if (_personDTO == null)
            {
                MessageBox.Show("عفواً، لم يتم العثور على بيانات هذا الشخص!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlAddNewPersonCard1.LoadPersonData(_personDTO);
        }

        // ==========================================
        // 2. Save Actions & State Management
        // ==========================================

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. استخراج البيانات المحدثة من الـ UserControl
            clsPersonDTO updatedDTO = ctrlAddNewPersonCard1.GetPersonDTOFromUI();

            // 2. إنشاء كائن البزنس
            clsPerson person = new clsPerson(updatedDTO);

            // 3. التنفيذ بناءً على القيمة البولينية (bool)
            if (person.Save())
            {
                // تم تحديث PersonID داخل كائن البزنس تلقائياً أثناء دالة _AddNew()
                _UpdateUIStateAfterSave(person.PersonID, updatedDTO);

                MessageBox.Show("تم حفظ البيانات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // إشعار الشاشة الأب برقم الشخص
                PersonSaved?.Invoke(person.PersonID);
            }
            else
            {
                MessageBox.Show("خطأ: تعذر حفظ البيانات.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _UpdateUIStateAfterSave(int savedPersonID, clsPersonDTO dto)
        {
            _personID = savedPersonID;
            dto.PersonID = savedPersonID;

            _mode = enMode.Update;
            _SetFormTitles();

            // تحديث الـ UserControl بالحالة الجديدة لمنع تكرار الإضافة بنفس المعرف
            ctrlAddNewPersonCard1.LoadPersonData(dto);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
