
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static class CustomJwtAuthExtention
{

    public static void AddjwtAuth(this IServiceCollection services)
    {
       services.AddAuthentication(Option =>
         {
              Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         }).AddJwtBearer(Option =>
         {
            Option.RequireHttpsMetadata = false;
            Option.SaveToken = true;
            Option.TokenValidationParameters = new TokenValidationParameters
            {
               
                ValidateIssuerSigningKey = true,
                
            };

         });

    }
    
}