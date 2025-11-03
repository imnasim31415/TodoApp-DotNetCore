using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable> GetAllAsync()
        {
            return await _context.TodoItems
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task CreateAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync(TodoItem item)
        {
            var existing = await _context.TodoItems.FindAsync(item.Id);
            if (existing == null)
                return null;

            existing.Title = item.Title;
            existing.Description = item.Description;
            existing.IsCompleted = item.IsCompleted;
            existing.CompletedAt = item.IsCompleted ? DateTime.UtcNow : null;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null)
                return false;

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task GetTotalCountAsync()
        {
            return await _context.TodoItems.CountAsync();
        }

        public async Task GetCompletedCountAsync()
        {
            return await _context.TodoItems.CountAsync(t => t.IsCompleted);
        }
    }
}