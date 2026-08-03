using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_DTOs;

namespace DVLD_DataAccess
{
    public class clsApplicationTypeData
    {
        // ========== استعلامات SQL ==========
        private const string GetAllApplicationTypesQuery = @"
            SELECT 
                ApplicationTypeID,
                ApplicationTypeTitle,
                ApplicationFees
            FROM ApplicationTypes
            ORDER BY ApplicationTypeID;
        ";

        private const string GetApplicationTypeByIDQuery = @"
            SELECT 
                ApplicationTypeID,
                ApplicationTypeTitle,
                ApplicationFees
            FROM ApplicationTypes
            WHERE ApplicationTypeID = @ApplicationTypeID;
        ";

        private const string UpdateApplicationTypeQuery = @"
            UPDATE ApplicationTypes
            SET 
                ApplicationTypeTitle = @ApplicationTypeTitle,
                ApplicationFees = @ApplicationFees
            WHERE ApplicationTypeID = @ApplicationTypeID;
        ";

        // ========== 1. جلب قائمة جميع أنواع الطلبات ==========
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetAllApplicationTypesQuery;

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
                    throw new Exception("Error retrieving application types list.", ex);
                }
            }

            return dt;
        }

        // ========== 2. جلب نوع طلب محدد بالـ ID ==========
        public static clsApplicationTypeDTO GetApplicationTypeByID(int applicationTypeID)
        {
            clsApplicationTypeDTO applicationType = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetApplicationTypeByIDQuery;
                command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationTypeID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            applicationType = new clsApplicationTypeDTO
                            {
                                ApplicationTypeID = clsDataAccessHelper.ConvertToInt(reader["ApplicationTypeID"]),
                                ApplicationTypeTitle = clsDataAccessHelper.ConvertToString(reader["ApplicationTypeTitle"]),
                                ApplicationFees = Convert.ToDecimal(reader["ApplicationFees"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving application type with ID {applicationTypeID}.", ex);
                }
            }

            return applicationType;
        }

        // ========== 3. تحديث بيانات نوع طلب ==========
        public static int UpdateApplicationType(clsApplicationTypeDTO applicationType)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = UpdateApplicationTypeQuery;

                command.Parameters.Add("@ApplicationTypeTitle", SqlDbType.NVarChar, 255).Value = applicationType.ApplicationTypeTitle;
                command.Parameters.Add("@ApplicationFees", SqlDbType.Decimal).Value = applicationType.ApplicationFees;
                command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationType.ApplicationTypeID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating application type with ID {applicationType.ApplicationTypeID}.", ex);
                }
            }

            return rowsAffected;
        }
    }
}