
using DVLD_DTOs;
namespace DVDL_Logic_layer.Global_Classes
{
    public static class clsGlobal
    {
        // 🌟 هذا هو المتغير السحري الذي سيحمل بيانات المستخدم الحالي في الذاكرة طوال فترة تشغيل البرنامج
        public static clsUserDTO CurrentUser { get; set; }
    }
}
