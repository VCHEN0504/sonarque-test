using System.Collections.Generic;
using AADx.Common.Models;
using LiteDB;

namespace AADx.TodosApi.DAL
{
    public class LiteTodoRepository : ITodoRepository
    {
        private LiteDatabase db = new LiteDatabase(
            System.Configuration.ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString);

        public void Dispose()
        {
            if (db != null)
            {
                //db.Dispose();
            }
        }

        public long Add(TodoItem item)
        {
            return db.GetCollection<TodoItem>("Todos").Insert(item);
        }

        public void Delete(long id)
        {
            db.GetCollection<TodoItem>("Todos").Delete(id);
        }

        public IEnumerable<TodoItem> GetAll()
        {
            var collection = db.GetCollection<TodoItem>("Todos");
            return collection.FindAll();
        }

        public TodoItem GetById(long id)
        {
            var collection = db.GetCollection<TodoItem>("Todos");
            return collection.FindById(id);
        }

        public void Update(TodoItem item)
        {
            db.GetCollection<TodoItem>("Todos").Update(item);
        }
    }
}