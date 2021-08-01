﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SocialMedia.Infrastructure.Filters
{
    public class AuthOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo
                .DeclaringType?
                .GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>()
                .Distinct();

            if (authAttributes == null || !authAttributes.Any()) return;
            operation.Responses.TryAdd("401", new OpenApiResponse {Description = "Unauthorized"});
            operation.Responses.TryAdd("403", new OpenApiResponse {Description = "Forbidden"});

            var jwtBearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [jwtBearerScheme] = new string[] { }
                }
            };
        }
    }
}