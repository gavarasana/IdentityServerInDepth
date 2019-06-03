// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4.Test;
using System;
using System.Security.Claims;
using IdentityServer4;

namespace Ravi.Learn.IdenityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[] {
                new ApiResource("MagazinesApi", "Api to access magazines information"),
                new ApiResource( "BooksApi", "Api to access information about books")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[] {
                new Client{
                    ClientId = "Magazine",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =  new Secret[] { new Secret("greenTrees".Sha256()) },
                    AllowedScopes = { "MagazinesApi", "BooksApi"}
                },
                new Client
                {
                    ClientId = "swagger",
                    ClientName = "Swagger Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:56735/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:56735/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "MagazinesApi", "BooksApi"
                    }
                },
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientName = "Resource owner",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256(), DateTime.UtcNow.AddYears(1))
                    },
                    AllowedScopes = { "MagazinesApi" }
                },
                new Client
                {
                    ClientId = "mvc.client",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {"https://localhost:44344/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:44344/signout-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    IsActive = true,
                    Username = "keving",
                    Password = "passw0rd",
                    SubjectId = "4961F953-17B8-443B-916F-7B0FAD21245F",
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "Kevin Grant"),
                        new Claim(ClaimTypes.NameIdentifier, "keving")
                    }
                }
            };
        }
    }
}