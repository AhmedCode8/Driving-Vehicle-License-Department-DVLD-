using DVLD_DataAccess;
using DVLD_DTOs;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace DVDL_Logic_layer.Person
{
    #region Person Data Actions ()

    #endregion
    public class clsPerson
    {

        public clsPerson(clsPersonDTO personDTO)
        {
            if (personDTO == null) return;

            this.PersonID = personDTO.PersonID;
            this.NationalNo = personDTO.NationalNo;
            this.FirstName = personDTO.FirstName;
            this.SecondName = personDTO.SecondName;
            this.ThirdName = personDTO.ThirdName;
            this.LastName = personDTO.LastName;
            this.DateOfBirth = personDTO.DateOfBirth;
            this.Gendor = personDTO.Gendor;
            this.Address = personDTO.Address;
            this.Phone = personDTO.Phone;
            this.Email = personDTO.Email;
            this.NationalityCountryID = personDTO.NationalityCountryID;
            this.ImagePath = personDTO.ImagePath;

            // 🌟 الحل الأذكى: الكائن يحدد حالته تلقائياً بناءً على الـ ID
            this.Mode = (this.PersonID != -1 && this.PersonID != 0) ? enMode.Update : enMode.AddNew;
        }
        public clsPerson() { }




        #region Person Data Actions ( Object State & Properties)
        // 1. Object State.
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public static int MinimumAllowedAge = 18;



        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        #endregion

        #region Person Data Actions (Add and edit)
        private bool _AddNew()
        {
            // 1. ننشئ كائن الـ DTO الذي تتوقعه طبقة البيانات، ونملأه من خصائص الكائن الحالي (this)
            clsPersonDTO personDTO = new clsPersonDTO
            {
                // بما أنها إضافة جديدة، الـ PersonID يكون صفراً أو لا نمرره، وسيتم تحديثه لاحقاً
                NationalNo = this.NationalNo,
                FirstName = this.FirstName,
                SecondName = this.SecondName,
                ThirdName = this.ThirdName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                Gendor = this.Gendor,
                Address = this.Address,
                Phone = this.Phone,
                Email = this.Email,
                NationalityCountryID = this.NationalityCountryID,
                ImagePath = this.ImagePath
            };

            // 2. نمرر الـ DTO مباشرة لطبقة البيانات دون الحاجة لتعديل دالة InsertPerson
            this.PersonID = clsPersonData.InsertPerson(personDTO);

            // إذا نجحت العملية سيعود الـ ID برقم صحيح
            return PersonID != -1;
        }

        private bool _Update()
        {
            // 1. ننشئ كائن الـ DTO ونملأه، وهنا يجب تمرير الـ PersonID لأننا نقوم بالتعديل
            clsPersonDTO personDTO = new clsPersonDTO
            {
                PersonID = this.PersonID, // 💡 مهم جداً في التعديل
                NationalNo = this.NationalNo,
                FirstName = this.FirstName,
                SecondName = this.SecondName,
                ThirdName = this.ThirdName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                Gendor = this.Gendor,
                Address = this.Address,
                Phone = this.Phone,
                Email = this.Email,
                NationalityCountryID = this.NationalityCountryID,
                ImagePath = this.ImagePath
            };

            // 2. نمرر الـ DTO المحدث لطبقة البيانات
            // ملاحظة: تأكد إن كانت دالة الـ Update ترجع bool في الـ DAL عندك ليتوافق الكود تماماً
            return (clsPersonData.UpdatePerson(personDTO) > 0);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _Update();

                default:
                    return false;
            }
        }

        #endregion

        #region Person Data Actions (operations)

        public static void DeletePerson(int personId)
        {
            clsPersonData.DeletePerson(personId);
        }
        public static clsPersonDTO GetPersonById(int personId)
        {
            return clsPersonData.GetPersonByID(personId);
        }

        public static clsPersonDTO GetPersonByNationalNo(string nationalNo)
        {
            return clsPersonData.GetPersonByNationalNo(nationalNo);
        }

        public static DataTable GetPersonList()
        {
            return clsPersonData.GetPersonList();
        }

        public static bool IsPersonExists(string nationalNo)
        {
            return clsPersonData.IsPersonExists(nationalNo);
        }
        #endregion



        public async Task ExportAllPersonsAsync(DataTable personsTable, Action<int> onProgressChanged)
        {
            // التحقق من وجود الجدول وتحويته على صفوف
            if (personsTable == null || personsTable.Rows.Count == 0) return;

            int totalCount = personsTable.Rows.Count;

            for (int i = 0; i < totalCount; i++)
            {
                // محاكاة عملية معالجة لكل صف
                //    await Task.Delay(10);

                int progressPercent = ((i + 1) * 100) / totalCount;
                onProgressChanged?.Invoke(progressPercent);
            }
        }
        public static bool IsValidEmail(string emailText)
        {
            if (string.IsNullOrWhiteSpace(emailText))
                return false;

            // string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";

            return Regex.IsMatch(emailText, pattern, RegexOptions.IgnoreCase);
        }
    }
}
