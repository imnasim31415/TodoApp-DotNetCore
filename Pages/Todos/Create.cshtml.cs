using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Pages.Todos
{
    public class CreateModel : PageModel
    {
        private readonly ITodoService _todoService;

        public CreateModel(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [BindProperty]
        public TodoItem TodoItem { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _todoService.CreateTodoAsync(TodoItem);

            TempData["SuccessMessage"] = "Todo created successfully!";
            return RedirectToPage("Index");
        }
    }
}