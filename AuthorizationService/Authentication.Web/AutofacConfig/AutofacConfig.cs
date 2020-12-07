using System.Web.Mvc;
using Authorization.Data;
using Authentication.Interfaces;
using Authentication.Services;
using Authentication.Services.Intefaces;
using Autofac;
using Autofac.Integration.Mvc;
using Authentication.Data;
using System.Reflection;
using Autofac.Integration.WebApi;
using System.Web.Http;

namespace Authentication.Web
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

            builder.RegisterType<AuthenticationDbContext>().As<AuthenticationDbContext>();

            builder.RegisterType<TokenIdentityRepository>().As<ITokenIdentityRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();

            var container = builder.Build();

            _resolver = new AutofacDependencyResolver(container);
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}