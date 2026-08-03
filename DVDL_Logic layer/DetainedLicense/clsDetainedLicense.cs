using DVLD_DataAccess;
using DVLD_DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDL_Logic_layer.DetainedLicense
{
    public class clsDetainedLicense
    {
        // ========== 1. حجز رخصة جديدة ==========
        public static int DetainLicense(clsDetainedLicenseDTO detainedLicense)
        {
            return clsDetainedLicenseData.DetainLicense(detainedLicense);
        }
        // ========== 2. فك حجز رخصة ==========
        public static int ReleaseLicense(int detainID, int releaseApplicationID, int releasedByUserID, DateTime releaseDate)
        {
            return clsDetainedLicenseData.ReleaseLicense(detainID, releaseApplicationID, releasedByUserID, releaseDate);
        }
        // ========== 3. التحقق من أن الرخصة محجوزة ==========
        public static bool IsLicenseDetained(int licenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(licenseID);
        }
        // ========== 4. جلب قائمة جميع الرخص المحجوزة ==========
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }
        // ========== 5. جلب سجل حجز محدد بالـ ID ==========
        public static clsDetainedLicenseDTO GetDetainedLicenseByID(int detainID)
        {
            return clsDetainedLicenseData.GetDetainedLicenseByID(detainID);
        }
        // ========== 6. جلب سجل حجز بواسطة LicenseID (للمحجوزة وغير المفرج عنها) ==========
        public static clsDetainedLicenseDTO GetDetainedLicenseByLicenseID(int licenseID)
        {
            return clsDetainedLicenseData.GetDetainedLicenseByLicenseID(licenseID);
        }
       

    }
}
