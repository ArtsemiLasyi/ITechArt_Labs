namespace BusinessLogic.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
