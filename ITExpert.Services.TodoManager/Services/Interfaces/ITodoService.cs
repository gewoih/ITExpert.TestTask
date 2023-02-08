using ITExpert.Libraries.SharedLibrary.Enums;
using ITExpert.Libraries.SharedLibrary.Models.DAO;

namespace ITExpert.Services.TodoManager.Services.Interfaces
{
    public interface ITodoService
    {
        public Task<Dictionary<Todo, string>> GetTodosWithHashAsync();
        public Task<bool> CreateTodoAsync(string title, TodoCategory category, TodoColor color);
        public Task<Todo> GetTodoAsync(int id);
        public Task<bool> DeleteTodoAsync(int id);
        public Task<bool> UpdateTodoTitleAsync(int id, string title);
        public Task<List<Comment>> GetTodoCommentsAsync(int id);
        public Task<bool> AddTodoCommentAsync(int id, Comment comment);
    }
}
