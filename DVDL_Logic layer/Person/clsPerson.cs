using DVLD_DataAccess;
using DVLD_DTOs;
using System;
using System.Data;
using System.Text.RegularExpressions; // 🌟 ضرورية لاستخدام الـ Regex
namespace DVDL_Logic_layer.Person
{
    #region Person Data Actions ()

    #endregion
    public class clsPerson
    {
        #region Person Data Actions ( Object State & Properties)
        // 1. Object State.
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;


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
        private int _AddNew()
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
            return this.PersonID;
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

        public int Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        {
                            Mode = enMode.Update;
                            return _AddNew();
                        }
                        return -1;
                    }
                case enMode.Update:
                    {
                        return _Update() ? this.PersonID : -1;
                    }
                default:
                    return -1;
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

        #region Person Data Actions (Filter system )

        #endregion


        public clsPerson()
        {

        }
        public clsPerson(clsPersonDTO personDTO)
        {
            // خطوة حماية: للتأكد من أن كائن الـ DTO يحتوي على بيانات فعلاً
            if (personDTO == null) return;

            // مِلء الخصائص (Properties) بناءً على قيم الـ DTO
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

            // طالما أن الكائن تم إنشاؤه من DTO، فهذا يعني أنه محمل من قاعدة البيانات
            // لذلك نقوم بتحديث الـ Mode ليكون Update تلقائياً
            this.Mode = enMode.Update;
        }
        public static bool IsEmailFormatCorrect(string text)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";

            return Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase);
        }

    }
}
//a@gmail.com