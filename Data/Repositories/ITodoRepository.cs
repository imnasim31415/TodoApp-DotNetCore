using TodoApp.Models;

namespace TodoApp.Data.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable> GetAllAsync();
        Task GetByIdAsync(int id);
        Task CreateAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(int id);
        Task GetTotalCountAsync();
        Task GetCompletedCountAsync();
    }
}