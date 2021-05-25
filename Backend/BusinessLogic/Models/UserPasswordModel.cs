namespace BusinessLogic.Models
{
    public class UserPasswordModel
    {
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
