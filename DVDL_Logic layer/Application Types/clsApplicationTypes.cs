using DVLD_DataAccess;
using DVLD_DTOs;
using System.Data;
namespace DVDL_Logic_layer.Application_Types
{
    public class clsApplicationTypes
    {
        public static int UpdateApplicationType(clsApplicationTypeDTO applicationType)
        {
            return clsApplicationTypeData.UpdateApplicationType(applicationType);
        }
        public static clsApplicationTypeDTO GetApplicationTypeByID(int applicationTypeID)
        {
            return clsApplicationTypeData.GetApplicationTypeByID(applicationTypeID);
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }


    }
}
