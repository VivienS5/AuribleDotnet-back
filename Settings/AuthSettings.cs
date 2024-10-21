
using System.Security.Claims;
using AuribleDotnet_back.Transformations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace AuribleDotnet_back.Service.AuthServices{
    public static class AuthSettings
    {
        public static IServiceCollection ConfigurationAuth(this IServiceCollection service, IConfiguration config){
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(config.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi() 
                .AddInMemoryTokenCaches();
            service.AddScoped<IClaimsTransformation, RoleTransformation>();
            service.AddAuthorizationBuilder()
                .AddPolicy("RequireAdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            return service;

        }

    }
}