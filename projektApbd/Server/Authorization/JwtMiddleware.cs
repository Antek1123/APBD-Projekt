using projektApbd.Server.Services;
using projektApbd.Shared.Models;

namespace projektApbd.Server.Authorization

{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public JwtMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? userId = jwtUtils.ValidateToken(token);

            if (userId != null)
            {
                User? user = await userService.GetUserById(userId.Value);
                if (user != null)
                    context.Items["User"] = user;
            }

            await _requestDelegate(context);
        }
    }
}
