﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchMvc.Infra.Ioc
{
    public static class DependencyInjectionJWT
    {
        public static IServiceCollection AddInfraStructureJWT(this IServiceCollection services,
            IConfiguration configuration)
        {
            // informar o tipo de autenticação JWT-Bearer
            // definir o modelo de desafio de autenticação
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             // habilita a autenticação JWT usando o esquema e desafio definidos
             // validar o token
             .AddJwtBearer(opt =>
             {
                 opt.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     //valores validos
                     ValidIssuer = configuration["Jwt:Issuer"],
                     ValidAudience = configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                     ClockSkew = TimeSpan.Zero
                 };
             });

            return services;
        }
    }
}
