using HH.Api.Configuration;
using HH.Api.Middleware;
using HH.Application.Common;
using HH.Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PI.WebApi.Configuration;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Register appsettings.json global variables
builder.Configuration.SettingsBinding();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtSetting.IssuerSigningKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
        // SignalR
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = async context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/notification"))
                {
                    context.Token = accessToken;
                }

                await Task.CompletedTask;
            }
        };
    });
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "http://localhost:8080/realms/petrol";
//        //options.Audience = "account";
//        options.RequireHttpsMetadata = false; // Chỉ dùng khi chạy local
//    });



// Add DI services (DbContext, Cors Policy, ...)
builder.Services.AddServices();

// Add AutoFac
builder.ConfigureAutofacContainer();

builder.Services.AddHttpClient();

// Register FluentValidation
builder.Services.AddFluentValidation();

builder.Services.AddSwagger();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwaggers();
}

//set timezone
//app.Use(async (context, next) =>
//{
//    TimeZoneInfo timeZoneInfo = TimeZoneInfo.Utc;
//    DateTime utcNow = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
//    await next.Invoke();
//});


app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<CurrentAccountMiddleware>();

//app.UseHttpsRedirection(); // redirect http to https when have ssl and configure in launchSettings.json
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/secure-data", (HttpContext context) =>
{
    return Results.Json(new { message = "Welcome!", user = context.User.Identity.Name });
}).RequireAuthorization();

app.UseCorsPolicy();


app.MapControllers();





app.Run();
