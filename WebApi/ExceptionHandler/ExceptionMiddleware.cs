using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using BLL.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

namespace WebApi.ExceptionHandler
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            { 

                switch (ex)
                {
                    case NotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

            }
        }

    }
}
