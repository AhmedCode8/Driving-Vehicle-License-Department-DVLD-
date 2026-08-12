//using DVDL_Logic_layer.Person;
//using DVLD_DTOs;
//using System;
//using System.Windows.Forms;
//using UserInterfaceLayer.People;

//namespace UserInterfaceLayer.User_Control
//{
//    public partial class ctrlFindUserCard : UserControl
//    {
//        public int ReturnPersonID = -1;
//        public ctrlFindUserCard()
//        {
//            InitializeComponent();
//            cbFilterBy.SelectedIndex = 0;
//        }

//        private void btnAddNewPerson_Click(object sender, EventArgs e)
//        {
//            frmAddEditPerson frm = new frmAddEditPerson();
//            frm.ShowDialog();

//            // 💡 حركة ذكية: إذا كانت شاشة الإضافة لديك تعرض الـ PersonID الجديد بعد الحفظ في خاصية
//            // يمكنك التقاطه فوراً وتمريره لدالة التحميل ليعرض في الـ Card تلقائياً!
//            //if (frm.pe != -1)
//            //{
//            //    LoadPersonData(frm.NewPersonID);
//            //}
//        }
//        private void btnFindPerson_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(txtFindValue.Text))
//            {
//                MessageBox.Show("Please enter a search value first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            // 🌟 خطوة الأمان: تصفير القيمة فوراً قبل أي عملية بحث جديدة للتخلص من "القيمة الشبحية"
//            ReturnPersonID = -1;

//            clsPersonDTO person = _GetPersonInfo();

//            // شرط الحارس: إذا لم يتم العثور على الشخص
//            if (person == null)
//            {
//                MessageBox.Show("No person was found with the provided criteria.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                // يمكنك هنا استدعاء دالة لتفريغ الـ Card القديم إذا أردت (مثلاً: ctrlPersonCard.Clear();)
//                return;
//            }

//            // في حال النجاح
//            ctrlPersonCard.LoadPersonData(person);
//            ReturnPersonID = person.PersonID; // الآن القيمة مضمونة وحديثة 100%
//        }
//        private clsPersonDTO _GetPersonInfo()
//        {
//            if (string.IsNullOrWhiteSpace(txtFindValue.Text))
//                return null;

//            string filterValue = txtFindValue.Text.Trim();
//            string selectedFilter = cbFilterBy.Text;

//            if (selectedFilter == "Person ID")
//            {
//                if (int.TryParse(filterValue, out int personID))
//                {
//                    return clsPerson.GetPersonById(personID);
//                }
//                return null;
//            }

//            if (selectedFilter == "National No.")
//            {
//                return clsPerson.GetPersonByNationalNo(filterValue);
//            }

//            return null;
//        }
//        public void LoadPersonData(int personID = -1, string nationalNo = "")
//        {
//            clsPersonDTO personInfo = null;

//            // Guard Clause: Optimize DB hits based on the provided parameter
//            if (personID > 0)
//            {
//                personInfo = clsPerson.GetPersonById(personID);
//            }
//            else if (!string.IsNullOrWhiteSpace(nationalNo))
//            {
//                personInfo = clsPerson.GetPersonByNationalNo(nationalNo);
//            }

//            // Guard Clause: Handle not found cases safely
//            if (personInfo == null)
//            {
//                string identifier = personID > 0 ? $"ID [{personID}]" : $"National No. [{nationalNo}]";
//                MessageBox.Show($"Person with {identifier} was not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

//                ReturnPersonID = -1;
//                return;
//            }

//            // Populate data into UI controls
//            ctrlPersonCard.LoadPersonData(personInfo);
//            txtFindValue.Text = personInfo.PersonID.ToString();
//            ReturnPersonID = personInfo.PersonID;
//        }
//    }
//}


using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.Windows.Forms;
using UserInterfaceLayer.People;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlFindUserCard : UserControl
    {
        // Event to notify host forms when a person is selected/found
        public event Action<int> OnPersonSelected;

        // Encapsulated property for the selected Person ID
        public int PersonID { get; private set; } = -1;

        public ctrlFindUserCard()
        {
            InitializeComponent();
            cbFilterBy.SelectedIndex = 0;
        }

        // ==========================================
        // 1. Search & Data Loading Operations
        // ==========================================

        public void LoadPersonData(int personID)
        {
            if (personID <= 0)
            {
                _ResetCard();
                return;
            }

            clsPersonDTO personInfo = clsPerson.GetPersonById(personID);
            _ProcessSearchResult(personInfo);
        }

        public void LoadPersonData(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                _ResetCard();
                return;
            }

            clsPersonDTO personInfo = clsPerson.GetPersonByNationalNo(nationalNo);
            _ProcessSearchResult(personInfo);
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            string searchKey = txtFindValue.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchKey))
            {
                MessageBox.Show("Please enter a search value first!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsPersonDTO personInfo = _FindPersonByFilter(searchKey);
            _ProcessSearchResult(personInfo);
        }

        private clsPersonDTO _FindPersonByFilter(string searchKey)
        {
            // Assuming Index 0 = "Person ID", Index 1 = "National No."
            if (cbFilterBy.SelectedIndex == 0)
            {
                if (int.TryParse(searchKey, out int personID))
                {
                    return clsPerson.GetPersonById(personID);
                }

                MessageBox.Show("Please enter a valid numeric Person ID!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return clsPerson.GetPersonByNationalNo(searchKey);
        }

        private void _ProcessSearchResult(clsPersonDTO personInfo)
        {
            if (personInfo == null)
            {
                MessageBox.Show("No person was found with the provided criteria.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetCard();
                return;
            }

            // Display person info and update state
            ctrlPersonCard.LoadPersonData(personInfo);
            PersonID = personInfo.PersonID;
            txtFindValue.Text = personInfo.PersonID.ToString();

            // Notify subscriber forms
            OnPersonSelected?.Invoke(PersonID);
        }

        private void _ResetCard()
        {
            PersonID = -1;
            txtFindValue.Text = string.Empty;
        }

        // ==========================================
        // 2. Add New Person Integration
        // ==========================================

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();

            // Subscribe to the save event to automatically auto-fill and load the created person
            frm.PersonSaved += (newPersonID) =>
            {
                cbFilterBy.SelectedIndex = 0;
                txtFindValue.Text = newPersonID.ToString();
                LoadPersonData(newPersonID);
            };

            frm.ShowDialog();
        }
    }
}