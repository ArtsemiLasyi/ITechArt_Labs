namespace BusinessLogic.Models
{
    public class UserPasswordModel
    {
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] Salt { get; set; } = null!;
    }
}
