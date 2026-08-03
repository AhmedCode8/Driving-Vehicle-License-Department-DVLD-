using DVLD_DataAccess;
using DVLD_DTOs;
using System.Data;

namespace DVDL_Logic_layer.driver
{
    public static class clsDrivers
    {
        // 1. إضافة سائق جديد
        public static int AddNewDriver(clsDriverDTO driverDTO)
        {
            return clsDriverData.AddNewDriver(driverDTO);
        }

        // 2. جلب قائمة جميع السائقين
        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

        // 3. البحث عن سائق بواسطة PersonID
        public static clsDriverDTO GetDriverByPersonID(int personID)
        {
            return clsDriverData.GetDriverByPersonID(personID);
        }


        // 4. البحث عن سائق بواسطة DriverID
        public static clsDriverDTO GetDriverByID(int driverID)
        {
            return clsDriverData.GetDriverByID(driverID);
        }
    }
}