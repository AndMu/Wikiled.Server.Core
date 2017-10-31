using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Wikiled.Server.Core.Testing.Authentication
{
    public class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
        private static ClaimsIdentity active;

        public ClaimsIdentity Identity => active;

        public static void SetActiveUser(string name)
        {
            SetActiveUser(new Claim(ClaimTypes.Name, name));
        }

        public static void SetActiveUser(params Claim[] claims)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);
            active = claimsIdentity;
        }
    }
}
