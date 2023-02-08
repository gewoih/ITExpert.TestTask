using ITExpert.Libraries.SharedLibrary.Enums;
using ITExpert.Libraries.SharedLibrary.Models.DAO;
using ITExpert.Services_.Services.Interfaces;

namespace ITExpert.Services_.Services
{
    public sealed class TodoService : ITodoService
    {
        public Task<bool> AddTodoCommentAsync(int id, Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateTodoAsync(string title, TodoCategory category, TodoColor color)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTodoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Todo> GetTodoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetTodoCommentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<Todo, string>> GetTodosWithHashAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTodoTitleAsync(int id, string title)
        {
            throw new NotImplementedException();
        }
    }
}
