namespace DVLD_DTOs
{
    public class clsUserDTO
    {
        // clsUserDTO
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        // كائن الشخص كخاصية مستقلة تماماً
        public clsPersonDTO PersonInfo { get; set; }
    }
}
