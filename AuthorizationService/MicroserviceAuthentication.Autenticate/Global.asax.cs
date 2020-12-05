using System.Web.Http;

namespace Authentication.Web
{
	public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var instance = AutofacConfig.Instance;

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
