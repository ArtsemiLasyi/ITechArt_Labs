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

        public static int? GetUserRole(this IIdentity? identity)
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
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .SingleOrDefault(),
                out int role
            );
            if (!converted)
            {
                return null;
            }
            return role;
        }

        public static string? GetUserEmail(this IIdentity? identity)
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

            string? email = claimsIdentity
                .Claims
                .Where(c => c.Type == ClaimTypes.Email)
                .Select(c => c.Value)
                .SingleOrDefault();
            return email;
        }
    }
}
