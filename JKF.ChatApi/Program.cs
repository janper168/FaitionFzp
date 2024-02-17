using Newtonsoft.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace JKF.ChatApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".MyChat.Session";
                options.IdleTimeout = System.TimeSpan.FromDays(1);//设置session的过期时间
                //options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            });
            builder.Services.AddControllers(option =>
            {
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;//解决后端传到前端全大写
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);//解决后端返回数据中文被编码
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
             });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
           
            builder.Services.AddCors(option => option.AddPolicy("cors",
                policy => policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:8080")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSession();
            app.UseAuthorization();
            app.UseCors("cors");
            app.MapHub<UserHub>("/chatuser");
            app.MapHub<ChatHub>("/chathub/{id?}");
            app.MapControllers();
                    
            app.Run();
        }
    }
}
