using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Pages.Todos
{
    public class IndexModel : PageModel
    {
        private readonly ITodoService _todoService;

        public IndexModel(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public IEnumerable Todos { get; set; } = new List();
        public int TotalCount { get; set; }
        public int CompletedCount { get; set; }

        [TempData]
        public string? SuccessMessage { get; set; }
        
        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            Todos = await _todoService.GetAllTodosAsync();
            var stats = await _todoService.GetStatisticsAsync();
            TotalCount = stats.total;
            CompletedCount = stats.completed;
        }

        public async Task OnPostToggleAsync(int id)
        {
            var success = await _todoService.ToggleCompletionAsync(id);
            
            if (success)
                SuccessMessage = "Todo status updated successfully!";
            else
                ErrorMessage = "Failed to update todo status.";

            return RedirectToPage();
        }
    }
}