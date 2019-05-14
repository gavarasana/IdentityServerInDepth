// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Ravi.Learn.IdenityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[] {
                new ApiResource("MagazinesApi", "Api to access magazines information")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[] {
                new Client{
                    ClientId = "Magazine",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =  new Secret[] { new Secret("greenTrees".Sha256()) },
                    AllowedScopes = {"MagazinesApi"}
                }
            };
        }


    }
}