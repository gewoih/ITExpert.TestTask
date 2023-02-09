using ITExpert.Services.TodoManager.DAL.DbContexts;
using ITExpert.Services.TodoManager.Middleware;
using ITExpert.Services.TodoManager.Services;
using ITExpert.Services.TodoManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ITExpert.Services.TodoManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Services.AddSingleton(logger);

            var postgreSqlConnectionString = builder.Configuration.GetConnectionString("PostgreSQL");
            builder.Services.AddDbContext<TodoDbContext>(options => options.UseNpgsql(postgreSqlConnectionString));
            builder.Services.AddTransient<ITodoService, TodoService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<HttpLoggingMiddleware>();

            app.Run();
        }
    }
}