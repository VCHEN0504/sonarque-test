using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AADx.Common.Models;
using AADx.EventsApi.DAL;

namespace AADx.TodoApi.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            );
        private IUserRepository repo = null;

        public UserController(IUserRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/User
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            logger.Debug("returning all ...");
            return repo.GetAll();
        }

        // GET: api/User/5
        [ResponseType(typeof(UserProfile))]
        public IHttpActionResult GetUserProfile(long id)
        {
            logger.Debug(string.Format("Getting user {0}", id));
            UserProfile user = repo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserProfile(long id, UserProfile user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            logger.Debug(string.Format("Updating user with id {0}", user.Id));


            try
            {
                repo.Update(user);
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

            return Ok(user);
        }

        // POST: api/User
        [ResponseType(typeof(UserProfile))]
        public IHttpActionResult PostUserProfile(UserProfile user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = repo.Add(user);
            logger.Debug(string.Format("Created user with id {0}", id));

            return CreatedAtRoute("DefaultApi", new { id = id }, user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(UserProfile))]
        public IHttpActionResult DeleteUserProfile(long id)
        {
            UserProfile user = repo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            logger.Debug(string.Format("Deleting user with id {0}", user.Id.ToString()));
            repo.Delete(id);

            return Ok(user);
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