using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SASC_Final.Helpers
{
    public class JwtHelper
    {
        public static IEnumerable<Claim> GetClaimsFromToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            return token.Claims;
        }
        public static Dictionary<string,string> GetClaims(string jwtToken)
        {
            var result = new Dictionary<string, string>();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            foreach (var c in token.Claims) 
            {
                result.Add(c.Type, c.Value);
            }
            return result;
        }

        public static bool TokenIsValid(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                return false;
            }
            try
            {
                var jwtToken = handler.ReadJwtToken(token);
                var expClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Exp);
                if (expClaim != null)
                {
                    var expUnix = long.Parse(expClaim.Value);
                    var expDateTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;

                    if (expDateTime < DateTime.UtcNow)
                    {
                        return false;
                    }
                    return true;
                }
            }
            finally { }
            return false;
        }

        public static DateTime ConvertExpToDateTime(string expValue)
        {
            long expUnix = 0;
            var res = long.TryParse(expValue, out expUnix);
            if (res)
            { 
                var expDateTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                return expDateTime;
            }
            return DateTime.MinValue;
        }
    }
}
