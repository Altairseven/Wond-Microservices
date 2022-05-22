using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Wond.Shared.Configuration;

public static class SwaggerConfiguration {


    public static void ConfigureWondSwagger(this SwaggerGenOptions c, string ApiName) {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wond Params", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                          "Enter 'Bearer'[space] and then your token in the text input below. \r\n\r\n" +
                          "Example: 'Bearer 12345abcdef'"

        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",

                    }
                },
                new string[] {}
            }
        });
    }
}
