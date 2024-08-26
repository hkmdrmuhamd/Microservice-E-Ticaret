﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            //ResourceCatalog ismine sahip bir api oluşturmak için:
            new ApiResource("ResourceCatalog") 
            {
                //Bu apiye erişebilecek olan scope'lar.(Yani ResourceCatalog'a erişimi olanlar CatalogFullPermission'a ve CatalogReadPermission'a erişebilir)
                Scopes = { "CatalogFullPermission", "CatalogReadPermission" }
            },
            new ApiResource("ResourceDiscount")
            {
                Scopes = { "DiscountFullPermission" }
            },
            new ApiResource("ResourceOrder")
            {
                Scopes = { "OrderFullPermission" }
            },
        };

        //IdentityResources, IdentityServer'a kayıtlı olan kullanıcıların hangi bilgilerine erişebileceğimizi belirler.
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(), //IdentityResources üzerinden OpenId scope'una erişim sağlanabilir.
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        //Token alan kişi hangi scope'lara sahipse o kişilerin yapabileceği işlemlerdir.
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("CatalogFullPermission", "Full authority for catalog operations"),
            new ApiScope("CatalogReadPermission", "Reading authority for catalog operations"),
            new ApiScope("DiscountFullPermission", "Full authority for discount operations"),
            new ApiScope("OrderFullPermission", "Full authority for order operations")
        };

        //Client'ların sahip olacakları izinleri belirlemek için:
        public static IEnumerable<Client> Clients => new Client[]
        {
            //Visitor
            new Client
            {
                ClientId = "MultiShopVisitorId",
                ClientName = "Multi Shop Visitor User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("multishopsecret".Sha256()) },
                AllowedScopes = { "CatalogReadPermission" }
            },

            //Manager
            new Client
            {
                ClientId = "MultiShopManagerId",
                ClientName = "Multi Shop Manager User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("multishopsecret".Sha256()) },
                AllowedScopes = { "CatalogFullPermission", "CatalogReadPermission" }
            },

            //Admin
            new Client
            {
                ClientId = "MultiShopAdminId",
                ClientName = "Multi Shop Admin User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("multishopsecret".Sha256()) },
                AllowedScopes = 
                { 
                    "CatalogFullPermission", "CatalogReadPermission", "DiscountFullPermission", "OrderFullPermission", 
                    IdentityServerConstants.LocalApi.ScopeName, 
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime = 600 //10 dk
            }
        };
    }
}