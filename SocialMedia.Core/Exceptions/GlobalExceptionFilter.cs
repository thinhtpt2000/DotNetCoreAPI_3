using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialMedia.Core.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() != typeof(BusinessException)) return;
            var exception = (BusinessException) context.Exception;
            var validation = new
            {
                status = 400,
                Title = "Bad Request",
                Description = exception.Message
            };

            var json = new
            {
                errors = new[] {validation}
            };

            context.Result = new BadRequestObjectResult(json);
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.ExceptionHandled = true;
        }
    }
}