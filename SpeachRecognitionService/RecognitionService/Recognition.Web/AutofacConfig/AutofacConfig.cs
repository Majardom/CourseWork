using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Mvc;
using Recognition.Service.Services;
using Recognition.Services.Interfaces.Services;

namespace Recognition.Web
{
	public class AutofacConfig
	{

		private static AutofacConfig _instance;

		public static AutofacConfig Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new AutofacConfig();

				}

				return _instance;
			}
		}

		private AutofacDependencyResolver _resolver;

		private AutofacConfig()
		{
			ConfigureContainer();
		}

		public TService GetService<TService>()
		{
			return _resolver.GetService<TService>();
		}

		private void ConfigureContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			builder.RegisterType<RecognitionService>().As<IRecognitionService>().WithParameter("baseDirectory", @"C:\tmp\samples");

			var container = builder.Build();

			_resolver = new AutofacDependencyResolver(container);
			var config = GlobalConfiguration.Configuration;
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}
	}
}