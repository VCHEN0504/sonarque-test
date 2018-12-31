using System;
using System.Configuration;
using System.Web.Http;
namespace AADx.TodoApi.Controllers
{
    [Authorize]
    public class TestController : ApiController
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [AllowAnonymous]
        // GET: api/test
        public IHttpActionResult GetTest()
        {
            var mssg = "SUCCESS: " + DateTime.Now.ToString() + "; "
                        + ConfigurationManager.AppSettings["Registration"];
            logger.Debug(mssg);
            return Ok(mssg);
        }

        [Route("api/test/no-roles")]
        // GET: api/test/no-roles
        public IHttpActionResult GetTestNoRoles()
        {
            var mssg = "SUCCESS (no roles_: " + DateTime.Now.ToString() + "; "
                        + ConfigurationManager.AppSettings["Registration"];
            logger.Debug(mssg);
            return Ok(mssg);
        }
    }
}
