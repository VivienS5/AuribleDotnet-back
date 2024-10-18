
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AuribleDotnet_back.Service.AuthServices{
    public static class AuthSettings
    {
        public static IServiceCollection ConfigurationAuth(this IServiceCollection service, IConfiguration config){
            var azureAd = config.GetSection("AzureAd");
            service.AddDistributedMemoryCache();
            service.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Nécessaire pour les sessions
            });
            service.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Ne pas forcer HTTPS pour le développement
            });
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Ne pas forcer HTTPS pour le développement
            })
        .AddOpenIdConnect(options =>
            {
                var azureAd = config.GetSection("AzureAd");
                options.ClientId = azureAd["ClientId"];
                options.ClientSecret = azureAd["ClientSecret"];
                options.Authority = $"https://login.microsoftonline.com/{azureAd["TenantId"]}/v2.0";
                options.ResponseType = "code"; // Utiliser le code d'autorisation

                options.SaveTokens = true; // Enregistrer les tokens
                options.CallbackPath = azureAd["CallbackPath"];

                // Les événements peuvent être configurés ici si nécessaire
                options.Events = new OpenIdConnectEvents
                {
                    OnRedirectToIdentityProvider = context =>
                    {
                        // Vous pouvez ajouter des logiques ici si nécessaire
                        return Task.CompletedTask;
                    }
                };
            });


        return service;
        }

    }
}