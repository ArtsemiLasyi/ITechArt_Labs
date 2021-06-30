using System.Linq;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class IdentityService
    {
        public int? GetUserId(ClaimsIdentity? identity)
        {
            int id;
            if (identity == null)
            {
                return null;
            }
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