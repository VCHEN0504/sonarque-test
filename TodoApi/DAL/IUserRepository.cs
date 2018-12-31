using AADx.Common.Models;
using System;
using System.Collections.Generic;

namespace AADx.EventsApi.DAL
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<UserProfile> GetAll();
        UserProfile GetById(long id);
        long Add(UserProfile item);
        void Delete(long id);
        void Update(UserProfile item);
    }
}
