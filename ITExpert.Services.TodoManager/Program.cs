using ITExpert.Services.TodoManager.DAL.DbContexts;
using ITExpert.Services.TodoManager.Services;
using ITExpert.Services.TodoManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITExpert.Services.TodoManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var postgreSqlConnectionString = builder.Configuration.GetConnectionString("PostgreSQL");
            builder.Services.AddDbContext<TodoDbContext>(options => options.UseNpgsql(postgreSqlConnectionString));

            builder.Services.AddTransient<ITodoService, TodoService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}