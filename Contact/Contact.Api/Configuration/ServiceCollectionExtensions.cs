
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using System.Security.Claims;
using Utilities.Framework.Exceptions;

namespace Contact.Api.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authority = configuration["SiteSettings:JwtBearer:Authority"];
            bool requireHttps = Convert.ToBoolean(configuration["SiteSettings:JwtBearer:RequireHttpsMetadata"]);

            services
             .AddAuthentication(option =>
             {
                 option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

             })
            .AddJwtBearer(o =>
            {
                o.Authority = authority;
                o.TokenValidationParameters.ValidateAudience = false;
                o.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                o.RequireHttpsMetadata = requireHttps;
                o.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var claims = context.Principal.Identity as ClaimsIdentity;
                        if (!claims.Claims.Any())
                            context.Fail("This token has no claims.");
                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                            throw new AppException("Authenticate failure.", HttpStatusCode.Unauthorized);
                        throw new AppException("You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                    }
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRole", policy => policy.RequireClaim("roleType", "Admin"));
            });
        }
    }
}
