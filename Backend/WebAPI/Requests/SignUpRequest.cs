namespace WebAPI.Requests
{
    public class SignUpRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
