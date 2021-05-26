using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Options
{
    public class JwtOptions
    {
        public const string JwToken = "JwToken";

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public string Lifetime { get; set; }
    }
}
