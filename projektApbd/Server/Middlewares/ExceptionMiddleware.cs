﻿using projektApbd.Server.Exceptions;

namespace projektApbd.Server.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            } 
            catch (BadRequestException exc)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(exc.Message);
            }
            catch (NotFoundException exc)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(exc.Message);
            }
            catch (TooManyRequestException exc)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync(exc.Message);
            }
            catch (Exception e)
            {
                Logger.Log(e);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong.");
            }
        }
    }
}
