using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Options
{
    public class JwtOptions
    {
        public const string JwToken = "JwToken";

        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!;
        public TimeSpan Lifetime { get; set; }
    }
}
