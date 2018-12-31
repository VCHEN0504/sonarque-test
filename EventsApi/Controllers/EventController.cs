using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AADx.Common.Models;
using AADx.EventsApi.DAL;

namespace AADx.EventsApi.Controllers
{
    [Authorize]
    public class EventController : ApiController
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IEventRepository repo = null;

        public EventController(IEventRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/Event
        [Authorize(Roles = "EventObserver,EventWriter,EventApprover,EventAdmin,GlobalAdmin")]
        public IEnumerable<EventItem> GetEventItems()
        {
            logger.Debug("returning all ...");
            return repo.GetAll();
        }

        // GET: api/Event/5
        [Authorize(Roles = "EventObserver,EventWriter,EventApprover,EventAdmin,GlobalAdmin")]
        [ResponseType(typeof(EventItem))]
        public IHttpActionResult GetEventItem(long id)
        {
            logger.Debug(String.Format("Getting eventItem {0}", id));
            EventItem item = repo.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Event/5
        [Authorize(Roles = "EventApprover,EventAdmin,GlobalAdmin")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventItem(long id, EventItem eventItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventItem.Id)
            {
                return BadRequest();
            }

            logger.Debug(string.Format("Updating eventItem with id {0}", eventItem.Id));
            logger.Debug(string.Format("Updating eventItem with id {0}", eventItem.Description));

            try
            {
                repo.Update(eventItem);
            }
            catch (Exception)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(eventItem);
        }

        // POST: api/Event
        [Authorize(Roles = "EventWriter,EventAdmin,GlobalAdmin")]
        [ResponseType(typeof(EventItem))]
        public IHttpActionResult PostEventItem(EventItem eventItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = repo.Add(eventItem);
            logger.Debug(string.Format("Created eventItem with id {0}", id));

            return CreatedAtRoute("DefaultApi", new { id = id }, eventItem);
        }

        // DELETE: api/Event/5
        [Authorize(Roles = "EventAdmin,GlobalAdmin")]
        [ResponseType(typeof(EventItem))]
        public IHttpActionResult DeleteEventItem(long id)
        {
            EventItem eventItem = repo.GetById(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            logger.Debug(string.Format("Deleting event with id {0}", eventItem.Id));
            repo.Delete(id);

            return Ok(eventItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(long id)
        {
            return repo.GetById(id) != null;
        }
    }
}