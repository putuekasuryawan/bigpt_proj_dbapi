using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace BiGptWebApi.Helpers
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var PotentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apikey = config.GetValue<string>(key: "ApiKey");
            if (!apikey.Equals(PotentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            await next();
        }
    }
}
