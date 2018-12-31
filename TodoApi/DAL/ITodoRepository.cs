using AADx.Common.Models;
using System;
using System.Collections.Generic;

namespace AADx.TodosApi.DAL
{
    public interface ITodoRepository : IDisposable
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem GetById(long id);
        long Add(TodoItem item);
        void Delete(long id);
        void Update(TodoItem item);
    }
}
