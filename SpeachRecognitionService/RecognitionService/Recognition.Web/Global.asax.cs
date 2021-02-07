using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Recognition.Interfaces;
using SpeackerRecognition.Core.ComparisonCore;

namespace Recognition.Web
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			var instance = AutofacConfig.Instance;

			ComparisonCore.Instance.InitializeCore(instance.GetService<IUnitOfWork>().Speakers.GetAllWithFeatures().ToList());

			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
