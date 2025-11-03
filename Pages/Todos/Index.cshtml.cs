using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TodoService _service;

        public IndexModel(TodoService service)
        {
            _service = service;
        }

        public List<TodoItem> Items { get; set; } = new();

        [BindProperty]
        public string? NewTitle { get; set; }

        public void OnGet()
        {
            Items = _service.GetAll();
        }

        public IActionResult OnPostAdd()
        {
            if (!string.IsNullOrWhiteSpace(NewTitle))
                _service.Add(NewTitle);
            return RedirectToPage();
        }

        public IActionResult OnPostToggle(int id)
        {
            var item = _service.Get(id);
            if (item != null)
            {
                item.IsDone = !item.IsDone;
                _service.Update(item);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.Delete(id);
            return RedirectToPage();
        }
    }
}
