using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System.Windows.Forms;

namespace UserInterfaceLayer.People
{
    public partial class frmAddEditPerson : Form
    {


        public clsPerson person = new clsPerson();


        public frmAddEditPerson()
        {
            InitializeComponent();
        }
        public frmAddEditPerson(clsPersonDTO Info)
        {
            InitializeComponent();
            ctrlAddNewPersonCard1.FillFormFields(Info);
            if (Info == null) return;
            person.Mode = clsPerson.enMode.Update;
            person.PersonID = Info.PersonID;
            lblPersonID.Text = person.PersonID.ToString(); ;
        }


        private void frmAddEditPerson_Load(object sender, System.EventArgs e)
        {
            // تحديث عنوان الشاشة بناءً على الحالة
            this.Text = (person.Mode == clsPerson.enMode.AddNew) ? "Add New Person" : "Edit Person Info";
            this.lblTitle.Text = this.Text;

        }


        private void btnSave_Click(object sender, System.EventArgs e)
        {

            clsPersonDTO data = ctrlAddNewPersonCard1.ItemValues();

            // نقل البيانات من الـ DTO إلى كائن البزنس (clsPerson)
            person.PersonID = data.PersonID; // 🌟 خطوة هامة جداً للتعديل
            person.NationalNo = data.NationalNo;
            person.FirstName = data.FirstName;
            person.SecondName = data.SecondName;
            person.ThirdName = data.ThirdName;
            person.LastName = data.LastName;
            person.DateOfBirth = data.DateOfBirth;
            person.Gendor = data.Gendor;
            person.Address = data.Address;
            person.Phone = data.Phone;
            person.Email = data.Email;
            person.NationalityCountryID = data.NationalityCountryID;
            person.ImagePath = data.ImagePath;


            int savedPersonID = person.Save();

            if (savedPersonID != -1)
            {
                MessageBox.Show("Data Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                person.PersonID = savedPersonID;
                ctrlAddNewPersonCard1.PersonID = savedPersonID; // 👈 هذا السطر سينقذك من الفخ

                person.Mode = clsPerson.enMode.Update;
                this.Text = "Edit Person Info";
                this.lblTitle.Text = this.Text;
                lblPersonID.Text = person.PersonID.ToString();
            }
            else
            {
                MessageBox.Show("Error: Data could not be saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
