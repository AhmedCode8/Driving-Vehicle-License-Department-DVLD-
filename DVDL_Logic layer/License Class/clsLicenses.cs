using DVLD_DataAccess;
using DVLD_DTOs;
using System.Data;

namespace DVDL_Logic_layer.License_Class
{
    public static class clsLicenses
    {
        // 1. إضافة رخصة جديدة
        public static int AddNewLicense(clsLicenseDTO licenseDTO)
        {
            return clsLicenseData.AddNewLicense(licenseDTO);
        }

        // 2. جلب رخصة محددة بواسطة ID
        public static clsLicenseDTO GetLicenseByID(int licenseID)
        {
            return clsLicenseData.GetLicenseByID(licenseID);
        }

        // 3. إلغاء تفعيل رخصة
        public static bool DeactivateLicense(int licenseID)
        {
            return clsLicenseData.DeactivateLicense(licenseID) > 0;
        }

        // 4. جلب كافة رخص سائق معين
        public static DataTable GetLicensesByDriverID(int driverID)
        {
            return clsLicenseData.GetLicensesByDriverID(driverID);
        }

        // 5. جلب رقم الرخصة النشطة لشخص وصنف معين
        public static int GetActiveLicenseIDByPersonIDAndClass(int personID, int licenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonIDAndClass(personID, licenseClassID);
        }

        // 6. جلب كائن الرخصة النشطة الكامل لشخص وصنف معين
        public static clsLicenseDTO GetActiveLicenseByLicenseClassAndPersonID(int personID, int licenseClassID)
        {
            return clsLicenseData.GetActiveLicenseByLicenseClassAndPersonID(personID, licenseClassID);

        }
        public static clsLicenseDTO GetLicenseByApplicationID(int applicationID)
        {
            return clsLicenseData.GetLicenseByApplicationID(applicationID);
        }
    }
}