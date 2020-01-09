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
                new ApiResource("api1", "My API #1"
                //, new List<string> {"role"}
                )
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
                    ClientId = "ionicclient",
                    RedirectUris =
                    {
                        "http://localhost:8100/landing",
                        "http://localhost:8100/home",
                        "ioniclient://ioniclient.trone/",
                    },
                    RequireConsent = false,
                    ClientUri = "http://localhost:8100",

                    AllowedCorsOrigins = new List<string>()
                    {
                        "http://localhost",
                        "http://localhost:8100",
                        "ioniclient://ioniclient.trone"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,

                    //ClientSecrets = { new Secret("mvcclientsecret".Sha256()) },
                    RequireClientSecret = false,

                    AccessTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.ReUse //this option let us reuse the refresh token but we should store in the backend later
                },

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

                    RedirectUris = { "https://localhost:44301/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44301/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44301/signout-callback-oidc" },
                    AllowedCorsOrigins = new List<string>(){ "http://localhost", "https://localhost:44301" },

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