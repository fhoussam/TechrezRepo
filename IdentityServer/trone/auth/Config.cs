using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace auth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
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
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1" )
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client()
                {
                    AllowedScopes = {
                        "openid",
                        "profile",
                        "api1",
                        "email",
                        "complementary_profile",
                    },
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientId = "jsclient",
                    RedirectUris = { "http://localhost:5003/callback.html" },
                    RequireConsent = false,
                    ClientUri = "http://localhost:5003",

                    AllowedCorsOrigins = new List<string>(){ "http://localhost", "http://localhost:5003" },
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,

                    //ClientSecrets = { new Secret("mvcclientsecret".Sha256()) },
                    RequireClientSecret = false,

                    AccessTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.ReUse //this option let us reuse the refresh token but we should store in the backend later
                },

                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "angularclient",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "http://localhost:5001/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5001/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

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

                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "consoleclient",
                    //ClientName = "Console Client",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        "openid",
                        "profile",
                        "api1",
                        "email",
                        "complementary_profile",
                    },
                    RequireConsent = false
                },
            };
        }
    }
}