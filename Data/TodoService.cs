using System.Collections.Generic;
using System.Linq;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoService
    {
        private static List<TodoItem> _items = new();
        private static int _nextId = 1;

        public List<TodoItem> GetAll() => _items;

        public void Add(string title)
        {
            _items.Add(new TodoItem { Id = _nextId++, Title = title, IsDone = false });
        }

        public TodoItem? Get(int id) => _items.FirstOrDefault(x => x.Id == id);

        public void Update(TodoItem item)
        {
            var existing = Get(item.Id);
            if (existing != null)
            {
                existing.Title = item.Title;
                existing.IsDone = item.IsDone;
            }
        }

        public void Delete(int id)
        {
            var existing = Get(id);
            if (existing != null)
                _items.Remove(existing);
        }
    }
}
