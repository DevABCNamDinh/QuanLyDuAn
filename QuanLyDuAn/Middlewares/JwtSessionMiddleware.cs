using QuanLyDuAn.Service;

namespace QuanLyDuAn.Middlewares
{
    public class JwtSessionMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtService jwtService)
        {
            var token = context.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var principal = jwtService.ValidateToken(token);
                    context.User = principal;
                }
                catch
                {
                    // Token không hợp lệ hoặc đã hết hạn
                    context.Session.Remove("AuthToken");
                }
            }

            await _next(context);
        }
    }
}
