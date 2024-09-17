using HH.Domain.Common;
using HH.Domain.Exceptions;
using Newtonsoft.Json;
using HH.Domain.Exceptions;
//using HH.Domain.Infrastructure.Discord;
using System.Net;

namespace HH.Api.Middleware
{
    public class ExceptionMiddleware(
        //IDiscordService discordClient
        ) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidateException ex)
            {
                ApiResponse<string> response = new ApiResponse<string>();
                response.StatusCode = ex.StatusCode;
                response.Message = ex.Message;
                context.Response.StatusCode = (int)response.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
            catch (CustomException ex)
            {
                ApiResponse<string> response = new ApiResponse<string>();
                response.StatusCode = ex.StatusCode;
                response.Message = ex.Message;

                context.Response.StatusCode = (int)response.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                ApiResponse<string> response = new ApiResponse<string>();
                if (AppConfig.IsDevelopmentEnvironment)
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = ex.GetExceptionMessage();
                }
                else
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Internal Server Error";
                }
                context.Response.StatusCode = (int)response.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));

                //await discordClient.SendErrorMessage($"Source:{ex.Source};" +
                //    $"{Environment.NewLine} From method: {ex.TargetSite};" +
                //    $"{Environment.NewLine} Error: {ex.GetExceptionMessage()}");
            }
        }


    }
}