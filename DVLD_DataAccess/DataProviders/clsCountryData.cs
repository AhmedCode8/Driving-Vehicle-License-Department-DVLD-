using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_DTOs;

namespace DVLD_DataAccess
{
    public class clsCountryData
    {
        // ========== استعلامات SQL ==========
        private const string GetAllCountriesQuery = @"
            SELECT 
                CountryID,
                CountryName
            FROM Countries
            ORDER BY CountryName;
        ";

        private const string GetCountryByIDQuery = @"
            SELECT 
                CountryID,
                CountryName
            FROM Countries
            WHERE CountryID = @CountryID;
        ";

        // ========== 1. جلب قائمة جميع الدول (للـ ComboBox) ==========
        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetAllCountriesQuery;

                try
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving countries list.", ex);
                }
            }

            return dt;
        }

        // ========== 2. جلب دولة محددة بالـ ID ==========
        public static clsCountryDTO GetCountryByID(int countryID)
        {
            clsCountryDTO country = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetCountryByIDQuery;
                command.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            country = new clsCountryDTO
                            {
                                CountryID = clsDataAccessHelper.ConvertToInt(reader["CountryID"]),
                                CountryName = clsDataAccessHelper.ConvertToString(reader["CountryName"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving country with ID {countryID}.", ex);
                }
            }

            return country;
        }
    }
}