using System.IO;
using System.Web.Http;

namespace AADx.TodoApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
