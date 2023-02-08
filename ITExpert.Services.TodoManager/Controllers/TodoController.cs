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
    }
}
