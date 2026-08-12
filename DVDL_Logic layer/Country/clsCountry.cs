using DVLD_DataAccess;
using DVLD_DTOs;
using System.Data;
namespace DVDL_Logic_layer.Country
{
    public static class clsCountry
    {

        public static DataTable GetAllCountry()
        {
            return clsCountryData.GetAllCountries();
        }
        public static clsCountryDTO GetCountryByID(int countryID)
        {
            return clsCountryData.GetCountryByID(countryID);
        }
        public const int DefaultCountryID = 82;
    }
}
