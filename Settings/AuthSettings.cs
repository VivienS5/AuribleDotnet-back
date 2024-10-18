
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AuribleDotnet_back.Service.AuthServices{
    public static class AuthSettings
    {
        public static IServiceCollection ConfigurationAuth(this IServiceCollection service, IConfiguration config){
            // service.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            // .AddMicrosoftIdentityWebApp(config, "AzureAd")
            //     .EnableTokenAcquisitionToCallDownstreamApi(["user.read"])
            //     .AddMicrosoftGraph(config.GetSection("Graph"))
            //     .AddDistributedTokenCaches();
            
            // service.AddDistributedMemoryCache();
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(config, "AzureAd")
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddMicrosoftGraph(config.GetSection("GraphBeta"))
                .AddInMemoryTokenCaches();
            

            service.AddAuthorization(config =>
            {
                config.AddPolicy("AuthZPolicy", policyBuilder =>
                policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));
            });
            return service;
        }

    }
}