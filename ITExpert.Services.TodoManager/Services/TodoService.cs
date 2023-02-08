using ITExpert.Libraries.SharedLibrary.Enums;
using ITExpert.Libraries.SharedLibrary.Exceptions;
using ITExpert.Libraries.SharedLibrary.Models.DAO;
using ITExpert.Services.TodoManager.DAL.DbContexts;
using ITExpert.Services.TodoManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITExpert.Services.TodoManager.Services
{
    public sealed class TodoService : ITodoService
    {
        private readonly TodoDbContext _todoDbContext;
        private readonly ILogger<TodoService> _logger;

        public TodoService(ILogger<TodoService> logger, TodoDbContext todoDbContext)
        {
            _logger = logger;
            _todoDbContext = todoDbContext;
        }

        public async Task AddTodoCommentAsync(int id, string comment)
        {
            var findedTodo = await _todoDbContext.Todos
                .Include(todo => todo.Comments)
                .FirstOrDefaultAsync(todo => todo.Id == id);

            if (findedTodo == null)
                throw new EntityNotFoundException($"Adding new comment to Todo failed. Todo with id='{id}' was not found in database.");

            if (findedTodo.Comments.FirstOrDefault(c => c.Text.Equals(comment)) is not null)
                throw new ArgumentException($"Comment '{comment}' already exists in Todo with id='{id}'.");

            findedTodo.Comments.Add(new Comment { Text = comment });
            await _todoDbContext.SaveChangesAsync();
        }

        public async Task CreateTodoAsync(string title, TodoCategory category, TodoColor color)
        {
            var newTodo = new Todo
            {
                Title = title,
                Category = category,
                Color = color
            };

            await _todoDbContext.Todos.AddAsync(newTodo);
            await _todoDbContext.SaveChangesAsync();
        }

        public async Task DeleteTodoAsync(int id)
        {
            var todoForDeleting = new Todo { Id = id };

            _todoDbContext.Todos.Attach(todoForDeleting);
            _todoDbContext.Remove(todoForDeleting);
            await _todoDbContext.SaveChangesAsync();
        }

        public async Task<Todo> GetTodoAsync(int id)
        {
            var findedTodo = await _todoDbContext.Todos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(todo => todo.Id == id);

            if (findedTodo is null)
                throw new EntityNotFoundException($"Todo with id='{id}' was not found.");

            return findedTodo;
        }

        public async Task<List<Comment>> GetTodoCommentsAsync(int id)
        {
            return await _todoDbContext.Comments
                .AsNoTracking()
                .Where(comment => comment.TodoId == id)
                .ToListAsync();
        }

        public async Task<List<Todo>> GetTodosWithHashAsync()
        {
            return await _todoDbContext.Todos
                .AsNoTracking()
                .Include(todo => todo.Comments)
                .ToListAsync();
        }

        public async Task UpdateTodoTitleAsync(int id, string title)
        {
            var findedTodo = await _todoDbContext.Todos
                .Select(todo => new Todo
                {
                    Id = todo.Id,

                })
                .FirstOrDefaultAsync(todo => todo.Id == id);

            if (findedTodo is null)
                throw new EntityNotFoundException($"Updating Todo title failed. Todo with id='{id}' was not found in database.");

            findedTodo.Title = title;

            await _todoDbContext.SaveChangesAsync();
        }
    }
}
