using DVLD_DataAccess;
using DVLD_DTOs;
using System.Data;
namespace DVDL_Logic_layer.Test_Types
{
    public class clsTestTypes
    {
        public enum enTestType
        {
            VisionTest = 1,
            WrittenTest = 2,
            StreetTest = 3
        }

        public static int UpdateTestType(clsTestTypeDTO testType)
        {
            return clsTestTypeData.UpdateTestType(testType);
        }
        public static clsTestTypeDTO GetTestTypeByID(int testTypeID)

        {
            return clsTestTypeData.GetTestTypeByID(testTypeID);
        }
        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }
    }
}
