using Readify.API.Extensions;
using Readify.API.Middlewares;

namespace ExamSupervisionPortal.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDatabaseContext(builder.Configuration)
                           .AddIdentityConfiguration()
                           .AddCustomServices(builder.Configuration)
                           .AddCustomControllers()
                           .AddCustomCors()
                           .AddSwagger();

            var app = builder.Build();

            // Use CORS policy
            app.UseCors("AllowedApps");

            // await ApplySeeding.ApplySeedingAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<JwtTokenValidationMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
