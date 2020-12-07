using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Authentication.Web.Startup))]

namespace Authentication.Web
{
	public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
