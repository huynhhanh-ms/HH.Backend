using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HH.Domain.Common;
using HH.Domain.Dto;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using System.Net;
using HH.Domain.Dto.Authen;
using HH.Domain.Dto.WeighingHistory;
using System.Reflection;

namespace PI.WebApi.Configuration
{
    public static class FluentValidationConfigurations
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            // for scan all validators, must have
            services.AddFluentValidationAutoValidation(configuration =>
            {
                // Disable the built-in .NET model (data annotations) validation.
                configuration.DisableBuiltInModelValidation = false;

                // Only validate controllers decorated with the `FluentValidationAutoValidation` attribute.
                configuration.ValidationStrategy = ValidationStrategy.All;

                // Enable validation for parameters bound from `BindingSource.Body` binding sources.
                configuration.EnableBodyBindingSourceAutomaticValidation = true;

                // Enable validation for parameters bound from `BindingSource.Form` binding sources.
                configuration.EnableFormBindingSourceAutomaticValidation = true;

                // Enable validation for parameters bound from `BindingSource.Query` binding sources.
                configuration.EnableQueryBindingSourceAutomaticValidation = true;

                // Enable validation for parameters bound from `BindingSource.DefaultPath` binding sources.
                configuration.EnablePathBindingSourceAutomaticValidation = true;

                // Enable validation for parameters bound from 'BindingSource.Custom' binding sources.
                configuration.EnableCustomBindingSourceAutomaticValidation = true;

                // Replace the default result factory with a custom implementation.
                configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
            });
        }

        public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
        {
            public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
            {
                string errorMessage = validationProblemDetails != null ? string.Join(", ", validationProblemDetails.Errors.Values.Select(t => string.Join(", ", t)))
                                                                        : "";
                return new BadRequestObjectResult(new ApiResponse<object>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = errorMessage,
                    Data = null
                });
            }
        }
    }
}
