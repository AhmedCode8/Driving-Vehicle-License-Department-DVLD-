using DVLD_DataAccess;
using DVLD_DTOs;
using System.Data;
namespace DVDL_Logic_layer.License_Class
{
    public class clsLicenseClass
    {

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }

        public static clsLicenseClassDTO GetLicenseClassByID(int licenseClassID)
        {
            return clsLicenseClassData.GetLicenseClassByID(licenseClassID);
        }

    }
}
