using AADx.Common.Models;
using System;
using System.Collections.Generic;

namespace AADx.EventsApi.DAL
{
    public interface IEventRepository : IDisposable
    {
        IEnumerable<EventItem> GetAll();
        EventItem GetById(long id);
        long Add(EventItem item);
        void Delete(long id);
        void Update(EventItem item);
    }
}
