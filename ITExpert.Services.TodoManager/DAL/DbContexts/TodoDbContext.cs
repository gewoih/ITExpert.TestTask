using ITExpert.Libraries.SharedLibrary.Enums;
using ITExpert.Libraries.SharedLibrary.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace ITExpert.Services.TodoManager.DAL.DbContexts
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public TodoDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var createTicketTodo = new Todo
            {
                Id = 1,
                Title = "Create a ticket",
                Category = TodoCategory.Analytics,
                Color = TodoColor.Red
            };

            var requestInformationTodo = new Todo
            {
                Id = 2,
                Title = "Request information",
                Category = TodoCategory.Analytics,
                Color = TodoColor.Green
            };
            
            var seedComment1 = new Comment { Id = 1, Text = "Comment1", TodoId = 1};
            var seedComment2 = new Comment { Id = 2, Text = "Comment2", TodoId = 1 };
            var seedComment3 = new Comment { Id = 3, Text = "Comment3", TodoId = 1 };

            modelBuilder.Entity<Todo>()
                .HasData(createTicketTodo, requestInformationTodo);

            modelBuilder.Entity<Comment>()
                .HasData(seedComment1, seedComment2, seedComment3);


            modelBuilder.Entity<Todo>()
                .Property(property => property.CreationDateTime)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Todo>()
                .HasIndex(todo => new { todo.Title, todo.Category })
                .IsUnique();
        }
    }
}
