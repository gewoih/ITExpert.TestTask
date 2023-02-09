using ITExpert.Libraries.SharedLibrary.Enums;
using ITExpert.Libraries.SharedLibrary.Exceptions;
using ITExpert.Libraries.SharedLibrary.Models.DAO;
using ITExpert.Services.TodoManager.DAL.DbContexts;
using ITExpert.Services.TodoManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using Serilog.Sinks.PostgreSQL;
using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace ITExpert.Services.TodoManager.Services
{
	public sealed class TodoService : ITodoService
	{
		private readonly TodoDbContext _todoDbContext;

		public TodoService(TodoDbContext todoDbContext)
		{
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

			try
			{
				await _todoDbContext.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				throw new EntityNotFoundException($"Deleting Todo failed. Todo with id='{id}' was not found in database.");
			}
		}

		public async Task<Todo> GetTodoAsync(int id)
		{
			var findedTodo = await _todoDbContext.Todos
								.Include(todo => todo.Comments)
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

		public async Task<List<Tuple<Todo, string>>> GetTodosWithHashAsync(List<int> idList, string requiredTitle)
		{
			using var md5 = MD5.Create();

			var allTodos = _todoDbContext.Todos
							.AsNoTracking();

			if (idList.Any())
				allTodos = allTodos.Where(todo => idList.Contains(todo.Id));

			if (!string.IsNullOrEmpty(requiredTitle))
				allTodos = allTodos.Where(todo => todo.Title.Contains(requiredTitle));

			return await allTodos
						.Include(todo => todo.Comments)
						.Select(todo =>
							Tuple.Create(
								todo,
								BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(todo.Title)))))
							.ToListAsync();
		}

		public async Task UpdateTodoTitleAsync(int id, string title)
		{
			var findedTodo = await _todoDbContext.Todos
				.FirstOrDefaultAsync(todo => todo.Id == id);

			if (findedTodo is null)
				throw new EntityNotFoundException($"Updating Todo title failed. Todo with id='{id}' was not found in database.");

			findedTodo.Title = title;

			await _todoDbContext.SaveChangesAsync();
		}
	}
}
