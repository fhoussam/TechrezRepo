using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace auth
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Email(),
                //no need for that as Profile method already generates an IndetityResource that has an array of claims that includs the two claims below :)
                new IdentityResource()
                {
                    Name = "complementary_profile",
                    UserClaims = {
                        JwtClaimTypes.BirthDate,
                        JwtClaimTypes.Gender,
                        "favcolor"
                    }
                }
            };
        

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "angularclient",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "https://localhost:44334/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44334/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44334/signout-callback-oidc" },
                    AllowedCorsOrigins = new List<string>(){ "http://localhost", "https://localhost:44334" },

                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        "openid",
                        "profile",
                        "api1",
                        "email",
                        "complementary_profile",
                    },

                    RequireConsent = false,
                },
            };
    }
}
