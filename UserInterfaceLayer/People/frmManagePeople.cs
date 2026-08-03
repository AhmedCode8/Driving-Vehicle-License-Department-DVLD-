using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.Data;
using System.Windows.Forms;
namespace UserInterfaceLayer.People
{
    public partial class fmManagePeople : Form
    {
        public fmManagePeople()
        {
            InitializeComponent();
        }


        private DataTable _dtSource;
        private void _RefreshPersonList()
        {
            // جلب البيانات وتخزينها في المتغير العام أولاً
            _dtSource = clsPerson.GetPersonList();

            // ربط الـ Grid بالمتغير العام مباشرة لتتضح الرؤية
            dgvPeople.DataSource = _dtSource;
            lblRecordsCount.Text = _dtSource.Rows.Count.ToString();
        }
        private void _LayoutDataGridView()
        {
            // Prevent user from adding or deleting rows manually
            dgvPeople.AllowUserToAddRows = false;
            dgvPeople.AllowUserToDeleteRows = false;

            // Enable full row selection instead of individual cell selection
            dgvPeople.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPeople.MultiSelect = false;

            // Adjust column widths automatically to fill the grid
            dgvPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Make the grid read-only
            dgvPeople.ReadOnly = true;
        }


        private void tsmiAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson addEditPersonForm = new frmAddEditPerson();
            addEditPersonForm.ShowDialog();
        }
        private void tsmiEditPerson_Click(object sender, EventArgs e)
        {

            if (dgvPeople.CurrentRow != null)
            {

                int personID = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

                clsPerson person = new clsPerson();


                clsPersonDTO personDTO = clsPerson.GetPersonById(personID);


                if (personDTO != null)
                {

                    frmAddEditPerson frmAddEditPerson = new frmAddEditPerson(personDTO);
                    frmAddEditPerson.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sorry, this person's data was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAddEditPerson = new frmAddEditPerson();
            frmAddEditPerson.ShowDialog();
        }
        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {

            if (dgvPeople.CurrentRow != null)
            {
                // 2️⃣ اصطياد الـ PersonID من السطر الحالي (نوع البيانات هنا هو int)
                int personID = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

                // 3️⃣ استدعاء طبقة المنطق لجلب كائن الـ DTO الخاص بهذا الشخص
                clsPerson person = new clsPerson();

                // (افترضت هنا أن لديك دالة في الـ Repository تبحث عن الشخص بواسطة الـ ID وترجع الـ DTO الخاص به)
                clsPersonDTO personDTO = clsPerson.GetPersonById(personID);

                // 4️⃣ التأكد من أن البيانات وُجدت بنجاح قبل فتح الشاشة
                if (personDTO != null)
                {
                    // نمرر كائن الـ DTO مباشرة إلى مشيد (Constructor) الشاشة الجديدة
                    frmPersonDetails personDetailsForm = new frmPersonDetails(personDTO);
                    personDetailsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sorry, this person's data was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void tsmiDeletePerson_Click(object sender, EventArgs e)
        {
            int personId = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

            clsPerson.DeletePerson(personId);
            _RefreshPersonList();
            _LayoutDataGridView();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }
        private void Filter()
        {
            if (_dtSource == null) return;

            // Reset filter if text is empty or search column is None
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()) || cbFilterBy.Text == "None")
            {
                _dtSource.DefaultView.RowFilter = "";
                return;
            }

            // Map ComboBox items to Database Column Names
            string columnName = "";
            switch (cbFilterBy.Text)
            {
                case "Person ID": columnName = "PersonID"; break;
                case "National No.": columnName = "NationalNo"; break;
                case "First Name": columnName = "FirstName"; break;
                case "Second Name": columnName = "SecondName"; break;
                case "Third Name": columnName = "ThirdName"; break;
                case "Last Name": columnName = "LastName"; break;
                case "Nationality": columnName = "CountryName"; break; // Adjust to your view column name
                case "Gender": columnName = "Gendor"; break;
                case "Phone": columnName = "Phone"; break;
                case "Email": columnName = "Email"; break;
                default: columnName = "None"; break;
            }

            // Apply Filter based on data type
            if (columnName == "PersonID")
            {
                // Check if the text is a valid number to prevent crash
                if (int.TryParse(txtFilterValue.Text.Trim(), out int id))
                {
                    _dtSource.DefaultView.RowFilter = $"{columnName} = {id}";
                }
                else
                {
                    _dtSource.DefaultView.RowFilter = ""; // Clear filter if invalid or empty
                }
            }
            else
            {
                // String filter (LIKE)
                _dtSource.DefaultView.RowFilter = $"{columnName} LIKE '{txtFilterValue.Text.Trim()}%'";
            }
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If filtering by Person ID, allow digits and control keys (like Backspace) only
            if (cbFilterBy.Text == "Person ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; // Block the character
                }
            }
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";

            // Hide text box if selection is "None", otherwise show it
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Focus();
            }

            // Clear filter when selection changes
            if (_dtSource != null)
            {
                _dtSource.DefaultView.RowFilter = "";
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void fmManagePeople_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshPersonList();
            _LayoutDataGridView();
        }
    }
}
