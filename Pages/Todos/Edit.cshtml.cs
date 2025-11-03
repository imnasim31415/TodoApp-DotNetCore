using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Pages.Todos
{
    public class EditModel : PageModel
    {
        private readonly ITodoService _todoService;

        public EditModel(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [BindProperty]
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

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid || TodoItem == null)
            {
                return Page();
            }

            var result = await _todoService.UpdateTodoAsync(TodoItem);

            if (result == null)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Todo updated successfully!";
            return RedirectToPage("Index");
        }
    }
}