using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_DTOs;

namespace DVLD_DataAccess
{
    public class clsLicenseClassData
    {
        // ========== استعلامات SQL ==========
        private const string GetAllLicenseClassesQuery = @"
            SELECT 
                LicenseClassID,
                ClassName
            FROM LicenseClasses
            ORDER BY ClassName;
        ";

        private const string GetLicenseClassByIDQuery = @"
            SELECT 
                LicenseClassID,
                ClassName,
                ClassDescription,
                MinimumAllowedAge,
                DefaultValidityLength,
                ClassFees
            FROM LicenseClasses
            WHERE LicenseClassID = @LicenseClassID;
        ";

        // ========== 1. جلب قائمة جميع أصناف الرخص (للـ ComboBox) ==========
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetAllLicenseClassesQuery;

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
                    throw new Exception("Error retrieving license classes list.", ex);
                }
            }

            return dt;
        }

        // ========== 2. جلب تفاصيل صنف رخصة محدد بالـ ID ==========
        public static clsLicenseClassDTO GetLicenseClassByID(int licenseClassID)
        {
            clsLicenseClassDTO licenseClass = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetLicenseClassByIDQuery;
                command.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            licenseClass = new clsLicenseClassDTO
                            {
                                LicenseClassID = clsDataAccessHelper.ConvertToInt(reader["LicenseClassID"]),
                                ClassName = clsDataAccessHelper.ConvertToString(reader["ClassName"]),
                                ClassDescription = clsDataAccessHelper.ConvertToString(reader["ClassDescription"]),
                                MinimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]),
                                DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]),
                                ClassFees = Convert.ToDecimal(reader["ClassFees"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving license class with ID {licenseClassID}.", ex);
                }
            }

            return licenseClass;
        }
    }
}