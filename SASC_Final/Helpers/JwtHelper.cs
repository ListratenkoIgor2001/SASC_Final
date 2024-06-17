using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    }
}
