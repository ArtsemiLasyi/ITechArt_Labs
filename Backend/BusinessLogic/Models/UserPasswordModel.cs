using System;

namespace BusinessLogic.Models
{
    public class UserPasswordModel
    {
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] Salt { get; set; } = Array.Empty<byte>();
    }
}
