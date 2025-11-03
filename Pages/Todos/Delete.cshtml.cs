using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Pages.Todos
{
    public class DeleteModel : PageModel
    {
        private readonly ITodoService _todoService;

        public DeleteModel(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public TodoItem? TodoItem { get; set; }

        public async Task OnGetAsync(int id)
        {
            TodoItem = await _todoService.GetTodoByIdAsync(id);

            if (TodoItem == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task OnPostAsync(int id)
        {
            var success = await _todoService.DeleteTodoAsync(id);

            if (!success)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Todo deleted successfully!";
            return RedirectToPage("Index");
        }
    }
}