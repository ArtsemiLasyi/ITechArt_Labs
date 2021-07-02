using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace WebAPI.Extensions
{
    public static class IIdentityExtensions
    {
        public static int? GetUserId(this IIdentity? identity)
        {
            if (identity == null)
            {
                return null;
            }

            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return null;
            }
            
            bool converted = int.TryParse(
                claimsIdentity
                    .Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value)
                    .SingleOrDefault(),
                out int id
            );
            if (!converted)
            {
                return null;
            }
            return id;
        }
    }
}
