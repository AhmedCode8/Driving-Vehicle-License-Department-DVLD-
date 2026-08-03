using DVLD_DataAccess;
using DVLD_DTOs;
using System.Data;

namespace DVDL_Logic_layer.tests
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode { get; private set; } = enMode.AddNew;

        // ========== الخصائص (Properties) ==========
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        /// <summary>
        /// خاصية مساعدة للتحويل السريع إلى DTO
        /// </summary>
        public clsTestDTO DTO
        {
            get
            {
                return new clsTestDTO
                {
                    TestID = this.TestID,
                    TestAppointmentID = this.TestAppointmentID,
                    TestResult = this.TestResult,
                    Notes = this.Notes,
                    CreatedByUserID = this.CreatedByUserID
                };
            }
        }

        // ========== المشيدات (Constructors) ==========
        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsTest(clsTestDTO testDTO)
        {
            this.TestID = testDTO.TestID;
            this.TestAppointmentID = testDTO.TestAppointmentID;
            this.TestResult = testDTO.TestResult;
            this.Notes = testDTO.Notes;
            this.CreatedByUserID = testDTO.CreatedByUserID;

            Mode = enMode.Update;
        }

        // ========== الطرق الخاصة (Private Methods) ==========
        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.DTO);
            return (this.TestID != -1);
        }

        // ========== الطرق العامة للـ Instance (Public Instance Methods) ==========
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    // التعديل على نتائج الاختبارات عادة غير متاح في النظام، لكن يمكن إضافته مستقبلاً إذا لزم الأمر
                    return false;
            }

            return false;
        }

        // ========== الطرق الثابتة والاستعلامات (Static Methods) ==========

        /// <summary>
        /// البحث عن اختبار بناءً على الـ TestID
        /// </summary>
        public static clsTest Find(int testID)
        {
            clsTestDTO testDTO = clsTestData.GetTestByID(testID);

            if (testDTO != null)
            {
                return new clsTest(testDTO);
            }

            return null;
        }

        /// <summary>
        /// جلب جميع الاختبارات المرتبطة بموعد محدد
        /// </summary>
        public static DataTable GetTestsByAppointmentID(int appointmentID)
        {
            return clsTestData.GetTestsByAppointmentID(appointmentID);
        }

        /// <summary>
        /// التحقق مما إذا كان المتقدم قد نجح في نوع اختبار معين
        /// </summary>
        public static bool DoesPassTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsTestData.DoesPassTestType(localDrivingLicenseApplicationID, testTypeID);
        }

        /// <summary>
        /// جلب آخر نتيجة اختبار لنوع اختبار محدد
        /// </summary>
        public static DataTable GetLastTestResultByApplicationIDAndTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsTestData.GetLastTestResultByApplicationIDAndTestType(localDrivingLicenseApplicationID, testTypeID);
        }

        /// <summary>
        /// حساب عدد مرات الرسوب لنوع اختبار معين
        /// </summary>
        public static int GetFailedTestsCount(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsTestData.GetFailedTestsCount(localDrivingLicenseApplicationID, testTypeID);
        }

        /// <summary>
        /// التحقق مما إذا كان المتقدم قد اجتاز جميع الاختبارات الثلاثة
        /// </summary>
        public static bool HasPassedAllTests(int localDrivingLicenseApplicationID)
        {
            return clsTestData.HasPassedAllTests(localDrivingLicenseApplicationID);
        }
    }
}