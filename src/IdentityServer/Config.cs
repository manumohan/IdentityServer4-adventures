﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope("api1.all"),
                new ApiScope("api1.custom"),
                new ApiScope("api2.all"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("api1", "My DotNet Core API")
                {
                    Scopes = { "api1.all", "api1.custom" }
                },
                new ApiResource("api2", "My DotNet 4.5 API")
                {
                    Scopes = { "api2.all" },
                    ApiSecrets = new Secret[]
                    {
                        new Secret("secret3".Sha256())
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Console app",
                    ClientId = "consoleclient",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret1".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1.all", "api1.custom", "api2.all" }
                },
                new Client
                {
                    ClientName = "MVC website",
                    ClientId = "mvcclient",
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret2".Sha256())
                    },

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = {"openid", "profile", "offline_access", "api1.all", "api1.custom", "api2.all" },

                    AllowOfflineAccess = true,
                },
                new Client
                {
                    ClientId = "jsclient",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =  { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =
                        {
                            "http://localhost:5003"
                        },

                    AllowedScopes = {"openid", "profile", "offline_access", "api1.all", "api1.custom", "api2.all" },
                },
                new Client
                {
                    ClientName = ".NET 4 MVC website",
                    ClientId = "net4mvcclient",
                    ClientSecrets =
                    {
                        new Secret("secret3".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:49816/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:49816" },


                    AllowedScopes = {"openid", "profile", "offline_access", "api1.all", "api1.custom", "api2.all" }
                },
                new Client
                {
                    ClientName = ".NET 4 MVC website - external",
                    ClientId = "net4mvcclientexternal",
                    ClientSecrets =
                    {
                        new Secret("secret4".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    RedirectUris = { "http://localhost:44319/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:44319" },


                    AllowedScopes = {"openid", "profile", "offline_access", "api1.all", "api1.custom", "api2.all" }
                },
                new Client
                {
                    ClientId = "razorappclient",
                    ClientName = "Razor Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = { "http://localhost:5005/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5005/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                    }
                }
            };
    }
}