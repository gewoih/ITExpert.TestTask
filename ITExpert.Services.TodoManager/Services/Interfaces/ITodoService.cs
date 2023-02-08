using ITExpert.Libraries.SharedLibrary.Enums;
using ITExpert.Libraries.SharedLibrary.Models.DAO;

namespace ITExpert.Services.TodoManager.Services.Interfaces
{
    public interface ITodoService
    {
        public Task<List<Tuple<Todo, string>>> GetTodosWithHashAsync();
        public Task CreateTodoAsync(string title, TodoCategory category, TodoColor color);
        public Task<Todo> GetTodoAsync(int id);
        public Task DeleteTodoAsync(int id);
        public Task UpdateTodoTitleAsync(int id, string title);
        public Task<List<Comment>> GetTodoCommentsAsync(int id);
        public Task AddTodoCommentAsync(int id, string comment);
    }
}
