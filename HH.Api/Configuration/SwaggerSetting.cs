using HH.Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace HH.Api.Configuration
{
    public static class SwaggerSetting
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // Set configuration for sercurity
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.Http,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }
                });

                // Set the server when in dev and prod environment
                if (AppConfig.IsDevelopmentEnvironment)
                {
                    // Set the server
                    c.AddServer(new OpenApiServer
                    {
                        Url = "http://localhost:6789/",
                        Description = "Local"
                    });

                    c.AddServer(new OpenApiServer
                    {
                        Url = "https://huynhhanh.com",
                        Description = "Production"
                    });
                }
                else
                {
                    c.AddServer(new OpenApiServer
                    {
                        Url = "https://huynhhanh.com",
                        Description = "Production"
                    });
                    c.AddServer(new OpenApiServer
                    {
                        Url = "http://huynhhanh.com",
                        Description = "Production"
                    });
                }

                // Set the document
                c.SwaggerDoc(AppConfig.SwaggerConfig.Version, new OpenApiInfo
                {
                    Title = AppConfig.SwaggerConfig.Title,
                    Version = AppConfig.SwaggerConfig.Version,
                    Description = AppConfig.SwaggerConfig.Description,
                    Contact = new OpenApiContact
                    {
                        Name = AppConfig.SwaggerConfig.ContactName,
                        Email = AppConfig.SwaggerConfig.ContactEmail,
                        Url = new Uri(AppConfig.SwaggerConfig.ContactUrl)
                    },
                    License = new OpenApiLicense
                    {
                        Name = AppConfig.SwaggerConfig.LicenseName,
                        Url = new Uri(AppConfig.SwaggerConfig.LicenseUrl)
                    }
                });
            });

            return services;
        }

        //use swagger
        public static IApplicationBuilder UseSwaggers(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}