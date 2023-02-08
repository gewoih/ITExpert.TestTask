using ITExpert.Libraries.SharedLibrary.Enums;
using ITExpert.Libraries.SharedLibrary.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace ITExpert.Services.TodoManager.DAL.DbContexts
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public TodoDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var createTicketTodo = new Todo
            {
                Title = "Create a ticket",
                Category = TodoCategory.Analytics,
                Color = TodoColor.Red,
                Comments = new List<Comment>
                {
                    new Comment { Text = "Comment1" },
                    new Comment { Text = "Comment2" },
                    new Comment { Text = "Comment3" }
                }
            };

            var requestInformationTodo = new Todo
            {
                Title = "Request information",
                Category = TodoCategory.Analytics,
                Color = TodoColor.Green
            };

            modelBuilder.Entity<Todo>().HasData(createTicketTodo, requestInformationTodo);
        }
    }
}
