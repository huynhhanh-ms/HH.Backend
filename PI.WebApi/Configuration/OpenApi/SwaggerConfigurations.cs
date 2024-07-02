using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace PI.WebApi.Configuration.OpenApi
{
    public static class SwaggerConfigurations
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            //services.AddOpenApiDocument((document, service) =>
            //{
            //    document.PostProcess = doc =>
            //    {
            //        doc.Info.Title = AppConfig.SwaggerConfig.Title;
            //        doc.Info.Version = AppConfig.SwaggerConfig.Version;
            //        doc.Info.Description = AppConfig.SwaggerConfig.Description;
            //        doc.Info.Contact = new()
            //        {
            //            Name = AppConfig.SwaggerConfig.ContactName,
            //            Email = AppConfig.SwaggerConfig.ContactEmail,
            //            Url = AppConfig.SwaggerConfig.ContactUrl
            //        };
            //        doc.Info.License = new()
            //        {
            //            Name = AppConfig.SwaggerConfig.LicenseName,
            //            Url = AppConfig.SwaggerConfig.LicenseUrl
            //        };

            //    };
            //    document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            //    {
            //        Name = "Authorization",
            //        Description = "Input your Bearer token to access this API",
            //        In = OpenApiSecurityApiKeyLocation.Header,
            //        Type = OpenApiSecuritySchemeType.Http,
            //        Scheme = JwtBearerDefaults.AuthenticationScheme,
            //        BearerFormat = "JWT",
            //    });
            //    document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
            //    document.OperationProcessors.Add(new SwaggerGlobalAuthProcessor());

            //    document.OperationProcessors.Add(new SwaggerHeaderAttributeProcessor());
            //});

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
                // Set the server
                c.AddServer(new OpenApiServer
                {
                    Url = "http://localhost:8080/",
                    Description = "Local"
                });
                c.AddServer(new OpenApiServer
                {
                    Url = "https://pharmacy.wyvernp.id.vn/",
                    Description = "Production"
                });
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
            //app.UseOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseSwaggerUi(options =>
            //{
            //    options.DefaultModelExpandDepth = -1;
            //    options.DocExpansion = "none";
            //    options.TagsSorter = "alpha";
            //});
            return app;
        }
    }
}