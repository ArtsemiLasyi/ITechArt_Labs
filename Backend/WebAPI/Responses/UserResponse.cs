using BusinessLogic.Models;

namespace WebAPI.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
