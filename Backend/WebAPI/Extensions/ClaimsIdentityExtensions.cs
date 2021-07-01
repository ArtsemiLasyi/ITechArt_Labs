using System.Linq;
using System.Security.Claims;

namespace WebAPI.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static int? GetUserId(this ClaimsIdentity identity)
        {
            int id;
            bool converted = int.TryParse(
                identity
                    .Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value)
                    .SingleOrDefault(),
                out id
            );
            if (!converted)
            {
                return null;
            }
            return id;
        }
    }
}