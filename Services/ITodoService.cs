using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<IEnumerable> GetAllTodosAsync();
        Task GetTodoByIdAsync(int id);
        Task CreateTodoAsync(TodoItem item);
        Task UpdateTodoAsync(TodoItem item);
        Task DeleteTodoAsync(int id);
        Task ToggleCompletionAsync(int id);
        Task GetStatisticsAsync();
    }
}