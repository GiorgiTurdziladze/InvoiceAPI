﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperTest.Validation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute,IAsyncActionFilter

    {
        private const string ApiHeaderName = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiHeaderName, out var potKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
                

            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = config.GetValue<string>(ApiHeaderName);

            if (!apiKey.Equals(potKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
