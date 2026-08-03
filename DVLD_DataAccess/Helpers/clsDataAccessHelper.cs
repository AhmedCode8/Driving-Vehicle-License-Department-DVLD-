using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    // ان كنت تريد استخدام هذه الدوال يجب ان تكون داخل نفس المشروع او تضيف مرجع لهذا المشروع في مشروعك using DVLD_DataAccess.Helpers;
    internal class clsDataAccessHelper
    {
        // 1. معالجة النصوص: إذا كانت القيمة فارغة يعيد نصاً فارغاً "" بدلاً من Null
        public static string ConvertToString(object value)
        {
            return (value == DBNull.Value || value == null) ? "" : Convert.ToString(value);
        }

        // 2. معالجة الأرقام الصحيحة: إذا كانت القيمة فارغة يعيد 0
        public static int ConvertToInt(object value)
        {
            return (value == DBNull.Value || value == null) ? 0 : Convert.ToInt32(value);
        }

        // 3. معالجة القيم المنطقية: إذا كانت القيمة فارغة يعيد false
        public static bool ConvertToBool(object value)
        {
            return (value == DBNull.Value || value == null) ? false : Convert.ToBoolean(value);
        }

        // 4. معالجة التواريخ: إذا كانت القيمة فارغة يعيد أقل تاريخ ممكن لتجنب الانهيار
        public static DateTime ConvertToDateTime(object value)
        {
            return (value == DBNull.Value || value == null) ? DateTime.MinValue : Convert.ToDateTime(value);
        }
    }
}
