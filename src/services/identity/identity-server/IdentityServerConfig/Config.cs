using System;
using System.Security.Claims;
using System.Text.Json;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;

namespace identity_server.IdentityServerConfig;

public static class Config
{
    public static List<TestUser> Users
    {
        get
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = 69118,
                country = "Germany"
            };

            return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "818727",
                        Username = "alice",
                        Password = "alice",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.Role, "admin"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    },
                    new TestUser
                     {
                        SubjectId = "88421113",
                        Username = "bob",
                        Password = "bob",
                        Claims =
                        {
                          new Claim(JwtClaimTypes.Name, "Bob Smith"),
                          new Claim(JwtClaimTypes.GivenName, "Bob"),
                          new Claim(JwtClaimTypes.FamilyName, "Smith"),
                          new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                          new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                          new Claim(JwtClaimTypes.Role, "user"),
                          new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                          new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                            IdentityServerConstants.ClaimValueTypes.Json)
                        }
                     }
                };
        }
    }

    public static IEnumerable<IdentityResource> IdentityResources =>
        new[]
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                },
                new IdentityResources.Email()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
                KnownScope.ServerAccess,
                KnownScope.ClientAccess
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
                // ensure user claims are in access token if profile scope is used
                new ApiResource(
                    IdentityServerConstants.StandardScopes.Profile,                    
                    "Profile Scope",
                    ProfileResource.ResourceUserClaims)
                {
                    Scopes = { 
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new ApiResource("user_resource", "user api")
                {
                    Scopes = new List<string> {
                        KnownScope.ServerAccess.ToScope(),
                        KnownScope.ClientAccess.ToScope(),
                        KnownScope.Role.ToScope(),
                        IdentityServerConstants.StandardScopes.OpenId
                    },
                    //ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role", "email"}
                }
        };

    public static IEnumerable<Client> DevelopmentClients =>
        new List<Client> 
        {
                new()
                {
                    ClientId = "development.swagger",
                    ClientName = "Swagger UI for Development Identity Server",
                    ClientSecrets =
                    {
                        new Secret("secret@786".Sha256())
                    },

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },
                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,

                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = new List<string>() { 
                        "http://localhost", 
                        "https://localhost"
                    },
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,

                    AllowedScopes = {
                        KnownScope.ClientAccess.ToScope(),
                        KnownScope.ServerAccess.ToScope(),
                        KnownScope.Role.ToScope(),
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Email
                    }
                }
        };

    public static async Task CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
        if (!adminRoleExists) await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {               
                new Client
                {
                    ClientId = "clientaggregator.api",
                    ClientName = "Client Aggregator API",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256())},

                    AllowedScopes =
                    {
                        KnownScope.ServerAccess.ToScope(),
                        KnownScope.ClientAccess.ToScope(),
                        KnownScope.Role.ToScope(),
                        IdentityServerConstants.StandardScopes.OpenId
                    }
                },
                // blazor wasm clientapp
                new Client
                {
                    ClientId = "blazorWASM",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins =
                    {
                        "https://localhost:4001"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        KnownScope.ClientAccess.ToScope(),
                        IdentityServerConstants.StandardScopes.Email
                    },
                    RedirectUris =
                    {
                        "https://localhost:7186/authentication/login-callback"
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:7186/authentication/logout-callback"
                    }
                }
        };
}

