using projektApbd.Server.Exceptions;

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
            catch (BadReqiestException exc)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(exc.Message);
            }
            catch (NotFoundException exc)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(exc.Message);
            }
        }
    }
}
