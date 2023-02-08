using ITExpert.Libraries.SharedLibrary.Enums;
using ITExpert.Libraries.SharedLibrary.Exceptions;
using ITExpert.Libraries.SharedLibrary.Models.DAO;
using ITExpert.Services.TodoManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITExpert.Services.TodoManager.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService) 
        {
            _todoService = todoService;
        }

        [HttpGet]
        [Route("get_all_todos")]
        public async Task<List<Tuple<Todo, string>>> GetAllTodos()
        {
            return await _todoService.GetTodosWithHashAsync();
        }

        [HttpGet]
        [Route("get_todo")]
        public async Task<IActionResult> GetTodo(int id)
        {
            try
            {
                return Json(await _todoService.GetTodoAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("get_todo_comments")]
        public async Task<IActionResult> GetTodoComments(int id)
        {
            try
            {
                return Json(await _todoService.GetTodoCommentsAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("create_new_todo")]
        public async Task<IActionResult> CreateTodo(string title, TodoCategory category, TodoColor color)
        {
            try
            {
                await _todoService.CreateTodoAsync(title, category, color);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("update_todo_title")]
        public async Task<IActionResult> UpdateTodoTitle(int id, string title)
        {
            try
            {
                await _todoService.UpdateTodoTitleAsync(id, title);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        [Route("add_todo_comment")]
        public async Task<IActionResult> AddTodoComment(int id, string comment)
        {
            try
            {
                await _todoService.AddTodoCommentAsync(id, comment);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete_todo")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            try
            {
                await _todoService.DeleteTodoAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
