using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AADx.Common.Models;
using AADx.TodosApi.DAL;

namespace AADx.TodoApi.Controllers
{
    [Authorize]
    public class TodoController : ApiController
    {
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            );
        private ITodoRepository repo = null;

        public TodoController(ITodoRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/Todo
        [Authorize(Roles = "ToDoObserver,ToDoWriter,ToDoApprover,ToDoAdmin,GlobalAdmin")]
        public IEnumerable<TodoItem> GetTodoItems()
        {
            logger.Debug("returning all ...");
            return repo.GetAll();
        }

        // GET: api/Todo/5
        [Authorize(Roles = "ToDoObserver,ToDoWriter,ToDoApprover,ToDoAdmin,GlobalAdmin")]
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult GetTodoItem(long id)
        {
            logger.Debug(String.Format("Getting todoItem {0}", id));
            TodoItem item = repo.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Todo/5
        [Authorize(Roles = "ToDoApprover,ToDoAdmin,GlobalAdmin")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTodoItem(long id, TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            logger.Debug(string.Format("Updating todoItem with id {0}", todoItem.Id));
            logger.Debug(string.Format("Updating todoItem with id {0}", todoItem.Description));

            try
            {
                repo.Update(todoItem);
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

            return Ok(todoItem);
        }

        // POST: api/Todo
        [Authorize(Roles = "ToDoWriter,ToDoAdmin,GlobalAdmin")]
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult PostTodoItem(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = repo.Add(todoItem);
            logger.Debug(string.Format("Created todoItem with id {0}", id));

            return CreatedAtRoute("DefaultApi", new { id = id }, todoItem);
        }

        // DELETE: api/Todo/5
        [Authorize(Roles = "ToDoAdmin,GlobalAdmin")]
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult DeleteTodoItem(long id)
        {
            logger.Debug(string.Format(">. Deleting event with id {0}", id));
            TodoItem todoItem = repo.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            logger.Debug(string.Format(">> Deleting event with id {0}", todoItem.Id));
            repo.Delete(id);

            return Ok(todoItem);
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