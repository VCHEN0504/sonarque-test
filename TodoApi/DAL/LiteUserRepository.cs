using System.Collections.Generic;
using AADx.Common.Models;
using AADx.EventsApi.DAL;
using LiteDB;

namespace AADx.UsersApi.DAL
{
    public class LiteUserRepository : IUserRepository
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

        public long Add(UserProfile item)
        {
            return db.GetCollection<UserProfile>("Users").Insert(item);
        }

        public void Delete(long id)
        {
            db.GetCollection<UserProfile>("Users").Delete(id);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            var collection = db.GetCollection<UserProfile>("Users");
            return collection.FindAll();
        }

        public UserProfile GetById(long id)
        {
            var collection = db.GetCollection<UserProfile>("Users");
            return collection.FindById(id);
        }

        public void Update(UserProfile item)
        {
            db.GetCollection<UserProfile>("Users").Update(item);
        }
    }
}