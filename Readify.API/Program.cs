using Microsoft.AspNetCore.WebSockets;
using Readify.API.Extensions;
using Readify.API.Helpers;
using Readify.API.Middlewares;

namespace Readify.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDatabaseContext(builder.Configuration)
                           .AddIdentityConfiguration()
                           .AddCustomServices(builder.Configuration)
                           .AddCustomControllers()
                           .AddCustomCors()
                           .AddSwagger();

            builder.Services.AddWebSockets(options =>
            {
                // Configure WebSocket options if needed
                options.KeepAliveInterval = TimeSpan.FromMinutes(2);
            });

            var app = builder.Build();

            // Use CORS policy
            app.UseCors("AllowedApps");

            await ApplySeeding.ApplySeedingAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseWebSockets();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<JwtTokenValidationMiddleware>();
            app.UseMiddleware<WebSocketChatMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}