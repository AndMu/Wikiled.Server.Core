using System;
using Microsoft.AspNetCore.Authentication;

namespace Wikiled.Server.Core.Testing.Authentication
{
    public static class TestAuthenticationExtensions
    {
        public static AuthenticationBuilder AddTestAuthentication(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            string displayName,
            Action<TestAuthenticationOptions> configOptions)
        {
            return builder.AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>(
                authenticationScheme,
                displayName,
                configOptions);
        }
    }
}
