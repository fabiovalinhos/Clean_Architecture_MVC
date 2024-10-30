using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CleanArchMvc.Infra.Ioc
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchMvc.API", Version = "v1" });

                conf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    //definir as configs
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'"
                });

                conf.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { 
                    // definir o requerimento
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        },
                        Scheme = "bearer",
                        Name = "Authorization",
                        In = ParameterLocation.Header
                    },
                    new string[]{ }
                    }
                });
            });

            return services;
        }
    }
}
