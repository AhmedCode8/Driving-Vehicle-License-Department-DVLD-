using DVLD_DataAccess;
using DVLD_DTOs;
using System;
using System.Data;
namespace DVDL_Logic_layer.Local_Driving_License_Application
{
    public class clsLocalDrivingLicenseApplication
    {
        #region Object State & Properties

        // 1. تحديد حالة الكائن (إضافة جديد أم تعديل)
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        // 2. خصائص طلب رخصة القيادة المحلية
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        #endregion

        #region Constructors (المشيدات)

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;

            // تحديد الحالة كإضافة جديد
            this.Mode = enMode.AddNew;
        }

        public clsLocalDrivingLicenseApplication(clsLocalDrivingLicenseApplicationDTO localAppInfo)
        {
            // حماية برمجية ضد القيم الفارغة
            if (localAppInfo == null)
            {
                throw new ArgumentNullException(nameof(localAppInfo), "Local Driving License Application DTO cannot be null.");
            }

            this.LocalDrivingLicenseApplicationID = localAppInfo.LocalDrivingLicenseApplicationID;
            this.ApplicationID = localAppInfo.ApplicationID;
            this.LicenseClassID = localAppInfo.LicenseClassID;

            // بما أن البيانات قادمة من قاعدة البيانات، فالحالة بالتأكيد هي تعديل
            this.Mode = enMode.Update;
        }

        #endregion

        #region Private Saving Methods (الدوال الخاصة بالحفظ)

        private int _AddNew()
        {
            clsLocalDrivingLicenseApplicationDTO dto = new clsLocalDrivingLicenseApplicationDTO
            {
                ApplicationID = this.ApplicationID,
                LicenseClassID = this.LicenseClassID
            };

            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddLocalDrivingLicenseApplication(dto);

            return this.LocalDrivingLicenseApplicationID;
        }

        private bool _Update()
        {
            clsLocalDrivingLicenseApplicationDTO dto = new clsLocalDrivingLicenseApplicationDTO
            {
                LocalDrivingLicenseApplicationID = this.LocalDrivingLicenseApplicationID, // 💡 أساسي للتعديل
                ApplicationID = this.ApplicationID,
                LicenseClassID = this.LicenseClassID
            };

            return (clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(dto) > 0);
        }

        #endregion

        #region Public Save Method (الدالة العامة للحفظ)

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew() != -1)
                    {
                        this.Mode = enMode.Update;
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

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }
        public static clsLocalDrivingLicenseApplicationDTO GetLocalDrivingLicenseApplicationByID(int localDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByID(localDrivingLicenseApplicationID);
        }
        public static clsLocalDrivingLicenseApplicationDTO GetLocalDrivingLicenseApplicationByApplicationID(int applicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByApplicationID(applicationID);
        }
        public static int DeleteLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(localDrivingLicenseApplicationID);
        }
        public static bool IsThereAnActiveApplication(int PersonID, int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.IsLicenseClassAlreadyObtained(PersonID, LicenseClassID);
        }
        public static bool DoesPersonHaveActiveLicenseForThisClass(int PersonID, int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPersonHaveActiveApplicationForLicenseClass(PersonID, LicenseClassID);
        }
        public static int GetPassedTestCount(int localDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTestCount(localDrivingLicenseApplicationID);
        }

    }
}
