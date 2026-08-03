using DVLD_DataAccess;
using System.Data;
namespace DVDL_Logic_layer.Country
{
    public static class clsCountryRepository
    {

        public static DataTable GetAllCountry()
        {
            return clsCountryData.GetAllCountries();
        }

    }
}
