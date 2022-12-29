using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RiseTechDemoApp.Domain.DTO;
using RiseTechDemoApp.Domain.Enums;
using RiseTechDemoApp.Domain.Extensions;

namespace ReportService.UI.Attributes
{
    public class AuthAttribute : ActionFilterAttribute
    {
        readonly IConfiguration _configuration;

        public AuthAttribute(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Kimlik Kontrollerini Yapar
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Result unauthenticatedResult = new() { Type = ResultName.Unauthenticated.ToLowerString() };

            if (string.IsNullOrEmpty(context.HttpContext.Request.Headers["Authorization"]))
            {
                context.Result = new JsonResult(unauthenticatedResult);
            }

            string? authToken = context.HttpContext.Request.Headers["Authorization"];

            if (authToken != _configuration.GetSection("AuthToken").Value)
            {
                context.Result = new JsonResult(unauthenticatedResult);
            }

            base.OnActionExecuting(context);
        }
    }
}