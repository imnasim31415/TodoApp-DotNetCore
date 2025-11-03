using TodoApp.Data.Repositories;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly ILogger _logger;

        public TodoService(ITodoRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable> GetAllTodosAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all todos");
                throw;
            }
        }

        public async Task GetTodoByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving todo with id {Id}", id);
                throw;
            }
        }

        public async Task CreateTodoAsync(TodoItem item)
        {
            try
            {
                return await _repository.CreateAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating todo");
                throw;
            }
        }

        public async Task UpdateTodoAsync(TodoItem item)
        {
            try
            {
                return await _repository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating todo with id {Id}", item.Id);
                throw;
            }
        }

        public async Task DeleteTodoAsync(int id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting todo with id {Id}", id);
                throw;
            }
        }

        public async Task ToggleCompletionAsync(int id)
        {
            try
            {
                var todo = await _repository.GetByIdAsync(id);
                if (todo == null)
                    return false;

                todo.IsCompleted = !todo.IsCompleted;
                todo.CompletedAt = todo.IsCompleted ? DateTime.UtcNow : null;

                await _repository.UpdateAsync(todo);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling completion for todo with id {Id}", id);
                throw;
            }
        }

        public async Task GetStatisticsAsync()
        {
            try
            {
                var total = await _repository.GetTotalCountAsync();
                var completed = await _repository.GetCompletedCountAsync();
                return (total, completed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving statistics");
                throw;
            }
        }
    }
}