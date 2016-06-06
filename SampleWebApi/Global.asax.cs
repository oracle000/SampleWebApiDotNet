using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace SampleWebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static WindsorContainer Container { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Container = new WindsorContainer();
            Container.Install(FromAssembly.This());
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(Container.Kernel);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }

    internal sealed class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _container;

        public WindsorDependencyResolver(IKernel container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public object GetService(Type t)
        {
            return _container.HasComponent(t)
                 ? _container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return _container.ResolveAll(t).Cast<object>().ToArray();
        }



        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }

        public void Dispose()
        {

        }
    }

    internal sealed class WindsorDependencyScope : IDependencyScope
    {
        private readonly IKernel _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IKernel container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
            _scope = container.BeginScope();
        }

        public object GetService(Type t)
        {
            return _container.HasComponent(t) ? _container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return _container.ResolveAll(t).Cast<object>().ToArray();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
