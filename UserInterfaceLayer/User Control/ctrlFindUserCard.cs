using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.Windows.Forms;
using UserInterfaceLayer.People;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlFindUserCard : UserControl
    {
        public int ReturnPersonID = -1;
        public ctrlFindUserCard()
        {
            InitializeComponent();
            cbFilterBy.SelectedIndex = 0;
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();

            // 💡 حركة ذكية: إذا كانت شاشة الإضافة لديك تعرض الـ PersonID الجديد بعد الحفظ في خاصية
            // يمكنك التقاطه فوراً وتمريره لدالة التحميل ليعرض في الـ Card تلقائياً!
            // if (frm.NewPersonID != -1) 
            // {
            //     LoadPersonData(frm.NewPersonID);
            // }
        }
        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFindValue.Text))
            {
                MessageBox.Show("Please enter a search value first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🌟 خطوة الأمان: تصفير القيمة فوراً قبل أي عملية بحث جديدة للتخلص من "القيمة الشبحية"
            ReturnPersonID = -1;

            clsPersonDTO person = _GetPersonInfo();

            // شرط الحارس: إذا لم يتم العثور على الشخص
            if (person == null)
            {
                MessageBox.Show("No person was found with the provided criteria.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // يمكنك هنا استدعاء دالة لتفريغ الـ Card القديم إذا أردت (مثلاً: ctrlPersonCard.Clear();)
                return;
            }

            // في حال النجاح
            ctrlPersonCard.LoadPersonData(person);
            ReturnPersonID = person.PersonID; // الآن القيمة مضمونة وحديثة 100%
        }
        private clsPersonDTO _GetPersonInfo()
        {
            if (string.IsNullOrWhiteSpace(txtFindValue.Text))
                return null;

            string filterValue = txtFindValue.Text.Trim();
            string selectedFilter = cbFilterBy.Text;

            if (selectedFilter == "Person ID")
            {
                if (int.TryParse(filterValue, out int personID))
                {
                    return clsPerson.GetPersonById(personID);
                }
                return null;
            }

            if (selectedFilter == "National No.")
            {
                return clsPerson.GetPersonByNationalNo(filterValue);
            }

            return null;
        }
        public void LoadPersonData(int personID = -1, string nationalNo = "")
        {
            clsPersonDTO personInfo = null;

            // Guard Clause: Optimize DB hits based on the provided parameter
            if (personID > 0)
            {
                personInfo = clsPerson.GetPersonById(personID);
            }
            else if (!string.IsNullOrWhiteSpace(nationalNo))
            {
                personInfo = clsPerson.GetPersonByNationalNo(nationalNo);
            }

            // Guard Clause: Handle not found cases safely
            if (personInfo == null)
            {
                string identifier = personID > 0 ? $"ID [{personID}]" : $"National No. [{nationalNo}]";
                MessageBox.Show($"Person with {identifier} was not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ReturnPersonID = -1;
                return;
            }

            // Populate data into UI controls
            ctrlPersonCard.LoadPersonData(personInfo);
            txtFindValue.Text = personInfo.PersonID.ToString();
            ReturnPersonID = personInfo.PersonID;
        }
    }
}


