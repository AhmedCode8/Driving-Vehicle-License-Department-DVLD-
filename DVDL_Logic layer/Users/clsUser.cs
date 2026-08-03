using DVDL_Logic_layer.Person;
using DVLD_DataAccess;
using DVLD_DTOs;
using System.Data;
namespace DVDL_Logic_layer.Users
{
    public class clsUser
    {
        #region Person Data Actions ( Object State & Properties)
        // 1. Object State.
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        // clsUserDTO
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson Person = new clsPerson();

        public clsUser()
        {

        }
        public clsUser(clsUserDTO userDTO)
        {
            if (userDTO == null) return;

            this.UserID = userDTO.UserID;
            this.PersonID = userDTO.PersonID;
            this.UserName = userDTO.UserName;
            this.Password = userDTO.Password;
            this.IsActive = userDTO.IsActive;

            // إذا كان الـ ID يساوي -1 فهذا مستخدم جديد، غير ذلك فهو قادم من قاعدة البيانات للتعديل
            this.Mode = (userDTO.UserID == -1) ? enMode.AddNew : enMode.Update;
        }
        #endregion

        #region User Data Actions (Add and edit)

        private int _AddNew()
        {
            // 1. إنشاء كائن الـ DTO وملؤه من خصائص المستخدم الحالي (this)
            clsUserDTO userDTO = new clsUserDTO
            {
                PersonID = this.PersonID,
                UserName = this.UserName,
                Password = this.Password,
                IsActive = this.IsActive
            };

            // 2. تمرير الـ DTO مباشرة لطبقة الوصول للبيانات واستقبال الـ ID الجديد
            // 💡 تنبيه: استبدل "clsUserData" باسم الكلاس الفعلي الذي يحتوي على دالات الـ DAL لديك
            this.UserID = clsUserData.AddNewUser(userDTO);

            return this.UserID;
        }

        private bool _Update()
        {
            // 1. إنشاء كائن الـ DTO ونمرر الـ UserID لأننا نقوم بالتعديل
            clsUserDTO userDTO = new clsUserDTO
            {
                UserID = this.UserID,
                PersonID = this.PersonID,
                UserName = this.UserName,
                Password = this.Password,
                IsActive = this.IsActive
            };

            // 2. استدعاء دالة التعديل وفحص الأسطر المتأثرة (Rows Affected)
            return (clsUserData.UpdateUser(userDTO) > 0);
        }

        public int Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        Mode = enMode.Update;
                        return _AddNew();
                    }
                case enMode.Update:
                    {
                        return _Update() ? this.UserID : -1;
                    }
                default:
                    return -1;
            }
        }

        #endregion

        public static bool IsPersonLinkedToUser(int PersonID)
        {
            return clsUserData.IsPersonLinkedToUser(PersonID);
        }
        public static clsUserDTO GetUserByID(int UserID)
        {
            return clsUserData.GetUserByID(UserID);
        }

        public static bool IsUserExists(string userName)
        {
            return clsUserData.IsUserExists(userName);
        }


        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static int DeleteUser(int userID)
        {
            return clsUserData.DeleteUser(userID);
        }
        public static int UpdatePassword(int userID, string newPassword)
        {
            return clsUserData.UpdatePassword(userID, newPassword);
        }
        public static clsUserDTO GetUserByUsernameAndPassword(string userName, string password)
        {
            return clsUserData.GetUserByUsernameAndPassword(userName, password);
        }



    }
}
