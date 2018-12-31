using System.Collections.Generic;
using AADx.Common.Models;
using LiteDB;

namespace AADx.EventsApi.DAL
{
    public class LiteEventRepository : IEventRepository
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

        public long Add(EventItem item)
        {
            return db.GetCollection<EventItem>("Events").Insert(item);
        }

        public void Delete(long id)
        {
            db.GetCollection<EventItem>("Events").Delete(id);
        }

        public IEnumerable<EventItem> GetAll()
        {
            var collection = db.GetCollection<EventItem>("Events");
            return collection.FindAll();
        }

        public EventItem GetById(long id)
        {
            var collection = db.GetCollection<EventItem>("Events");
            return collection.FindById(id);
        }

        public void Update(EventItem item)
        {
            db.GetCollection<EventItem>("Events").Update(item);
        }
    }
}