using System;

namespace QuanLyDuAn.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            // Kiểm tra nếu lỗi 403 (Không có quyền)
            if (context.Response.StatusCode == 403|| context.Response.StatusCode == 302)
            {
                // Nếu là yêu cầu Ajax, trả về JSON thay vì chuyển hướng
                if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"error\": \"Bạn không có quyền truy cập!\"}");
                }
                else
                {
                   
                    // Nếu không phải Ajax, chuyển hướng đến trang thông báo
                    context.Response.Redirect("/User/Register");
                }
            }
        }
    }
}
