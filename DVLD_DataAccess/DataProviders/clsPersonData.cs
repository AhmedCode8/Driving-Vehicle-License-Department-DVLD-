using DVLD_DTOs;
using System;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_DataAccess
{

    public class clsPersonData
    {
        private const string InsertPersonQuery = @"
        INSERT INTO People 
        (
            NationalNo, FirstName, SecondName, ThirdName, LastName, 
            DateOfBirth, Gendor, Address, Phone, Email, 
            NationalityCountryID, ImagePath
        )
        VALUES 
        (
            @NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, 
            @DateOfBirth, @Gendor, @Address, @Phone, @Email, 
            @NationalityCountryID, @ImagePath
        );
        SELECT SCOPE_IDENTITY();";
        private const string UpdatePersonQuery = @"
         UPDATE People
         SET
               NationalNo = @NationalNo,  FirstName = @FirstName,
               SecondName = @SecondName, ThirdName = @ThirdName,
               LastName = @LastName,  DateOfBirth = @DateOfBirth,
               Gendor = @Gendor, Phone = @Phone,
               Email = @Email,Address = @Address,
               NationalityCountryID = @NationalityCountryID,       
               ImagePath = @ImagePath
         
         WHERE PersonID = @PersonID;";

        private const string DeletePersonQuery = @"
        delete from People where PersonID = @PersonID;";
        private const string GetPersonListQuery = @"
         SELECT 
               People.PersonID,
               People.NationalNo,
               People.FirstName,
               People.SecondName,
               People.ThirdName,
               People.LastName,
               CASE WHEN People.Gendor = 0 THEN 'Male' ELSE 'Female' END AS Gender,
               People.DateOfBirth,
               Countries.CountryName AS Nationality,
               People.Phone,
               People.Email
               FROM People
         INNER JOIN Countries ON People.NationalityCountryID = Countries.CountryID;
";
        private const string IsPersonExistsByNationalNoQuery = @"
         SELECT CASE 
             WHEN EXISTS (SELECT 1 FROM People WHERE NationalNo = @NationalNo) THEN 1
             ELSE 0 
         END;";
        private const string GetPersonByIDQuery = @"
        SELECT * FROM People WHERE PersonID = @PersonID;";

        private const string GetPersonByNationalNoQuery = @"
          SELECT * FROM People WHERE NationalNo = @NationalNo;";
        public static int InsertPerson(clsPersonDTO person)
        {
            int newPersonID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Command = connection.CreateCommand())
                {
                    // ✅ إصلاح الخطأ القاتل: ربط الاستعلام بالـ Command مباشرة
                    Command.CommandText = InsertPersonQuery;

                    // 1. الحقول الإجبارية في قاعدة البيانات (Not Null)
                    Command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 255).Value = person.NationalNo;
                    Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255).Value = person.FirstName;
                    Command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 255).Value = person.SecondName;
                    Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 255).Value = person.LastName;
                    Command.Parameters.Add("@Gendor", SqlDbType.TinyInt).Value = person.Gendor;
                    Command.Parameters.Add("@NationalityCountryID", SqlDbType.Int).Value = person.NationalityCountryID;

                    // 2. 🛡️ صمامات أمان للحقول الاختيارية (Null) لحمايتها من الانهيار
                    Command.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.ThirdName) ? (object)DBNull.Value : person.ThirdName;

                    Command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value =
                        (person.DateOfBirth == DateTime.MinValue) ? (object)DBNull.Value : person.DateOfBirth;

                    Command.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.Address) ? (object)DBNull.Value : person.Address;

                    Command.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.Phone) ? (object)DBNull.Value : person.Phone;

                    Command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.Email) ? (object)DBNull.Value : person.Email;

                    Command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.ImagePath) ? (object)DBNull.Value : person.ImagePath;
                    try
                    {
                        connection.Open();
                        object result = Command.ExecuteScalar();

                        // استخدام الدالة المساعدة مباشرة لتحديث المعرف الجديد بأمان
                        int tempID = clsDataAccessHelper.ConvertToInt(result);

                        if (tempID > 0)
                        {
                            newPersonID = tempID;
                        }
                    }
                    catch (Exception ex)
                    {
                        // رمي الخطأ للطبقات الأعلى للتعامل معه
                        throw new Exception("Error inserting person: " + ex.Message);
                    }
                }
            }

            return newPersonID;
        }
        public static int UpdatePerson(clsPersonDTO person)
        {
            int rowsAffected = 0;

            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Command = connection.CreateCommand())
                {
                    Command.CommandText = UpdatePersonQuery;
                    // الحقول الإجبارية
                    Command.Parameters.Add("@PersonID", SqlDbType.Int).Value = person.PersonID;

                    Command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 255).Value = person.NationalNo;
                    Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255).Value = person.FirstName;
                    Command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 255).Value = person.SecondName;
                    Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 255).Value = person.LastName;
                    Command.Parameters.Add("@Gendor", SqlDbType.TinyInt).Value = person.Gendor;
                    Command.Parameters.Add("@NationalityCountryID", SqlDbType.Int).Value = person.NationalityCountryID;

                    // الحقول الاختيارية مع معالجة DBNull
                    Command.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.ThirdName) ? (object)DBNull.Value : person.ThirdName;

                    Command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value =
                        person.DateOfBirth == DateTime.MinValue ? (object)DBNull.Value : person.DateOfBirth;

                    Command.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.Address) ? (object)DBNull.Value : person.Address;

                    Command.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.Phone) ? (object)DBNull.Value : person.Phone;

                    Command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.Email) ? (object)DBNull.Value : person.Email;

                    Command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 255).Value =
                        string.IsNullOrEmpty(person.ImagePath) ? (object)DBNull.Value : person.ImagePath;
                    try
                    {
                        connection.Open();
                        rowsAffected = Command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error updating person: " + ex.Message);
                    }
                }
            }

            return rowsAffected;
        }
        public static int DeletePerson(int personID)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Command = connection.CreateCommand())
                {
                    Command.CommandText = DeletePersonQuery;
                    Command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
                    try
                    {
                        connection.Open();
                        rowsAffected = Command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // يمكن تسجيل الخطأ في ملف سجلات (Log)
                        throw new Exception("Error deleting person with ID: " + personID, ex);
                    }
                }
            }
            return rowsAffected;
        }
        public static DataTable GetPersonList()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Command = connection.CreateCommand())
                {
                    Command.CommandText = GetPersonListQuery;

                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(Command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error retrieving person list", ex);
                    }
                }
            }
            return dt;
        }
        public static bool IsPersonExists(string nationalNo)
        {
            bool isExists = false;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand Command = connection.CreateCommand())
                {
                    Command.CommandText = IsPersonExistsByNationalNoQuery;

                    // تمرير المعرف الوطني كـ Parameter لحماية الاستعلام وتجنب مشاكل تحويل الأنواع
                    Command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 255).Value = nationalNo;

                    try
                    {
                        connection.Open();
                        object result = Command.ExecuteScalar();

                        // تحويل النتيجة (0 أو 1) باستخدام الدالة المساعدة المتاحة عندك
                        int tempResult = clsDataAccessHelper.ConvertToInt(result);

                        // إذا كانت النتيجة 1 فهذا يعني أن الشخص موجود فعلاً
                        if (tempResult == 1)
                        {
                            isExists = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        // رمي الخطأ للطبقات الأعلى للتعامل معه
                        throw new Exception("Error checking person existence: " + ex.Message);
                    }
                }
            }

            return isExists;
        }


        // ========== 1. الدالة الرئيسية المشتركة (Private Master Method) ==========
        // الدالة الرئيسية المشتركة (Private Master Method)
        private static clsPersonDTO _GetPersonMasterQuery(string query, string paramName, object value, SqlDbType dbType)
        {
            clsPersonDTO Info = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand Command = connection.CreateCommand())
            {
                Command.CommandText = query;
                Command.Parameters.Add(paramName, dbType).Value = value;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Info = new clsPersonDTO();

                            Info.PersonID = clsDataAccessHelper.ConvertToInt(reader["PersonID"]);
                            Info.NationalNo = clsDataAccessHelper.ConvertToString(reader["NationalNo"]);
                            Info.FirstName = clsDataAccessHelper.ConvertToString(reader["FirstName"]);
                            Info.SecondName = clsDataAccessHelper.ConvertToString(reader["SecondName"]);
                            Info.ThirdName = clsDataAccessHelper.ConvertToString(reader["ThirdName"]);
                            Info.LastName = clsDataAccessHelper.ConvertToString(reader["LastName"]);
                            Info.DateOfBirth = clsDataAccessHelper.ConvertToDateTime(reader["DateOfBirth"]);
                            Info.Gendor = Convert.ToByte(reader["Gendor"]);
                            Info.Address = clsDataAccessHelper.ConvertToString(reader["Address"]);
                            Info.Phone = clsDataAccessHelper.ConvertToString(reader["Phone"]);
                            Info.Email = clsDataAccessHelper.ConvertToString(reader["Email"]);
                            Info.NationalityCountryID = clsDataAccessHelper.ConvertToInt(reader["NationalityCountryID"]);
                            Info.ImagePath = clsDataAccessHelper.ConvertToString(reader["ImagePath"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving person data.", ex);
                }
            }

            return Info;
        }

        // دالة جلب الشخص بواسطة الـ ID
        public static clsPersonDTO GetPersonByID(int personID)
        {
            return _GetPersonMasterQuery(GetPersonByIDQuery, "@PersonID", personID, SqlDbType.Int);
        }

        // دالة جلب الشخص بواسطة الـ NationalNo
        public static clsPersonDTO GetPersonByNationalNo(string nationalNo)
        {
            return _GetPersonMasterQuery(GetPersonByNationalNoQuery, "@NationalNo", nationalNo, SqlDbType.NVarChar);
        }
    }
}


