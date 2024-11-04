using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Architecture.Helpers
{
    public static class JwtTokenHelper
    {
        public static IEnumerable<Claim> DecodeToken (string token)
        {
            var handler = new JwtSecurityTokenHandler ();
            var jwtToken = handler.ReadJwtToken (token);    
            return jwtToken.Claims;
        }

        public static List<string> GetUserRoles (string token)
        {
            var claims = DecodeToken (token);
            return claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList ();
        }

        public static int GetUserId(string token)
        {
            var claims = DecodeToken(token);
            var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
    }
}
