
using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.Data;
using System.Windows.Forms;

namespace UserInterfaceLayer.People
{
    public partial class frmManagePeople : Form
    {
        private DataTable _dtSource;

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void fmManagePeople_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshPersonList(0);
            _LayoutDataGridView();
            StartExportPerson();
        }


        private async void StartExportPerson()
        {
            pbExportPersons.Value = 0;
            DataTable dtPersons = clsPerson.GetPersonList();
            clsPerson person = new clsPerson();
            await person.ExportAllPersonsAsync(dtPersons, (progress) =>
            {
                pbExportPersons.Value = progress;
            }
            );
        }

        #region Data Loading & Grid Layout

        private void _RefreshPersonList(int personID)
        {
            _dtSource = clsPerson.GetPersonList();
            dgvPeople.DataSource = _dtSource;
            lblRecordsCount.Text = _dtSource.Rows.Count.ToString();
        }

        private void _LayoutDataGridView()
        {
            dgvPeople.AllowUserToAddRows = false;
            dgvPeople.AllowUserToDeleteRows = false;
            dgvPeople.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPeople.MultiSelect = false;
            dgvPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPeople.ReadOnly = true;
        }

        #endregion

        #region Form Navigation & Actions (DRY Principle)

        private void _OpenAddEditPersonForm(clsPersonDTO personDTO = null)
        {
            frmAddEditPerson addEditForm = (personDTO == null)
                ? new frmAddEditPerson()
                : new frmAddEditPerson(personDTO);

            addEditForm.PersonSaved += _RefreshPersonList;
            addEditForm.ShowDialog();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            _OpenAddEditPersonForm();
        }

        private void tsmiAddNewPerson_Click(object sender, EventArgs e)
        {
            _OpenAddEditPersonForm();
        }

        private void tsmiEditPerson_Click(object sender, EventArgs e)
        {
            if (!_IsRowSelected()) return;

            int personID = _GetSelectedPersonID();
            clsPersonDTO personDTO = clsPerson.GetPersonById(personID);

            if (personDTO != null)
            {
                _OpenAddEditPersonForm(personDTO);
            }
            else
            {
                _ShowNotFoundError();
            }
        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            if (!_IsRowSelected()) return;

            int personID = _GetSelectedPersonID();
            clsPersonDTO personDTO = clsPerson.GetPersonById(personID);

            if (personDTO != null)
            {
                frmPersonDetails detailsForm = new frmPersonDetails(personDTO);
                detailsForm.ShowDialog();
            }
            else
            {
                _ShowNotFoundError();
            }
        }

        private void tsmiDeletePerson_Click(object sender, EventArgs e)
        {
            if (!_IsRowSelected()) return;

            if (MessageBox.Show("Are you sure you want to delete this person?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int personID = _GetSelectedPersonID();
                clsPerson.DeletePerson(personID);
                _RefreshPersonList(0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Filtering System (Clean & Small Functions)

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            if (_dtSource == null) return;

            string filterText = txtFilterValue.Text.Trim();
            string filterBy = cbFilterBy.Text;

            if (string.IsNullOrEmpty(filterText) || filterBy == "None")
            {
                _dtSource.DefaultView.RowFilter = "";
                return;
            }

            string columnName = _GetDatabaseColumnName(filterBy);
            if (columnName == "None") return;

            _ApplyRowFilterToView(columnName, filterText);
        }

        private string _GetDatabaseColumnName(string filterByText)
        {
            switch (filterByText)
            {
                case "Person ID":
                    return "PersonID";
                case "National No.":
                    return "NationalNo";
                case "First Name":
                    return "FirstName";
                case "Second Name":
                    return "SecondName";
                case "Third Name":
                    return "ThirdName";
                case "Last Name":
                    return "LastName";
                case "Nationality":
                    return "CountryName";
                case "Gender":
                    return "Gendor";
                case "Phone":
                    return "Phone";
                case "Email":
                    return "Email";
                default:
                    return "None";
            }
        }

        private void _ApplyRowFilterToView(string columnName, string filterText)
        {
            if (columnName == "PersonID")
            {
                if (int.TryParse(filterText, out int id))
                {
                    _dtSource.DefaultView.RowFilter = $"{columnName} = {id}";
                }
                else
                {
                    _dtSource.DefaultView.RowFilter = "1 = 0"; // إرجاع نتيجة فارغة في حال كتب نصاً غير رقمي
                }
            }
            else
            {
                _dtSource.DefaultView.RowFilter = $"{columnName} LIKE '{filterText}%'";
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Focus();
            }

            if (_dtSource != null)
            {
                _dtSource.DefaultView.RowFilter = "";
            }
        }

        #endregion

        #region Helper Methods (Security & Validation)

        private bool _IsRowSelected()
        {
            return dgvPeople.CurrentRow != null;
        }

        private int _GetSelectedPersonID()
        {
            return (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;
        }

        private void _ShowNotFoundError()
        {
            MessageBox.Show("Sorry, this person's data was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}