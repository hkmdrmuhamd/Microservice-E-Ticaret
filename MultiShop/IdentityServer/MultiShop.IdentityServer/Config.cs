using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            //ResourceCatalog ismine sahip bir api oluşturmak için:
            new ApiResource("ResourceCatalog"){ Scopes = { "CatalogFullPermission", "CatalogReadPermission" } }, //Bu apiye erişebilecek olan scope'lar.(Yani ResourceCatalog'a erişimi olanlar CatalogFullPermission'a ve CatalogReadPermission'a erişebilir)
            new ApiResource("ResourceDiscount"){ Scopes = { "DiscountFullPermission" } },
            new ApiResource("ResourceOrder"){ Scopes = { "OrderFullPermission" } },
            new ApiResource("ResoruceCargo"){ Scopes = { "CargoFullPermission" } },
            new ApiResource("ResoruceBasket"){ Scopes = { "BasketFullPermission" } },
            new ApiResource("ResoruceComment"){ Scopes = { "CommentFullPermission" } },
            new ApiResource("ResorucePayment"){ Scopes = { "PaymentFullPermission" } },
            new ApiResource("ResoruceImages"){ Scopes = { "ImagesFullPermission" } },
            new ApiResource("ResoruceOcelot"){ Scopes = { "OcelotFullPermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName) //LocalApi.ScopeName, Kullanıcılar'a IdentityServer'a erişim izni verip API'nin kullanılabilmesini sağlayan scope'dur.
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
            new ApiScope("OrderFullPermission", "Full authority for order operations"),
            new ApiScope("CargoFullPermission", "Full authority for cargo operations"),
            new ApiScope("BasketFullPermission", "Full authority for basket operations"),
            new ApiScope("CommentFullPermission", "Full authority for comment operations"),
            new ApiScope("PaymentFullPermission", "Full authority for payment operations"),
            new ApiScope("ImagesFullPermission", "Full authority for images operations"),
            new ApiScope("OcelotFullPermission", "Full authority for ocelot operations"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
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
                AllowedScopes = { "CatalogFullPermission", "CatalogReadPermission", "OcelotFullPermission", "CommentFullPermission", "ImagesFullPermission" }
            },

            //Manager
            new Client
            {
                ClientId = "MultiShopManagerId",
                ClientName = "Multi Shop Manager User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, //ResourceOwnerPassword, kullanıcı adı ve şifre ile token alınmasını sağlar.
                ClientSecrets = { new Secret("multishopsecret".Sha256()) },
                AllowedScopes = 
                { 
                    "CatalogFullPermission", "CatalogReadPermission", "BasketFullPermission", 
                    "OcelotFullPermission", "CommentFullPermission" , "PaymentFullPermission", "ImagesFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            },

            //Admin
            new Client
            {
                ClientId = "MultiShopAdminId",
                ClientName = "Multi Shop Admin User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("multishopsecret".Sha256()) },
                AllowedScopes = 
                { 
                    "CatalogFullPermission", "CatalogReadPermission", "DiscountFullPermission", 
                    "OrderFullPermission", "CargoFullPermission", "BasketFullPermission", 
                    "OcelotFullPermission", "CommentFullPermission", "PaymentFullPermission", "ImagesFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName, 
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime = 600 //Token geçerlilik süresidir eğer girilmezse default olarak 3600 saniye(1 saat) geçerli olacaktır.
            }
        };
    }
}