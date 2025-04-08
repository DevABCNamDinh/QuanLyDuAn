using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLyDuAn.AppDBContext;
using QuanLyDuAn.Middlewares;
using QuanLyDuAn.Repository;
using QuanLyDuAn.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Lấy các giá trị từ appsettings.json trực tiếp
        var secretKey = builder.Configuration["JwtSettings:SecretKey"];
        var expiresInMinutes = int.Parse(builder.Configuration["JwtSettings:ExpiresInMinutes"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,  // Không kiểm tra Issuer, nếu bạn cần có thể bật lên
            ValidateAudience = false,  // Không kiểm tra Audience, nếu bạn cần có thể bật lên
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero  // Không có độ trễ khi kiểm tra thời gian
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSession();

app.UseMiddleware<JwtSessionMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
