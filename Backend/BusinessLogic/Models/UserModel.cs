namespace BusinessLogic.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
