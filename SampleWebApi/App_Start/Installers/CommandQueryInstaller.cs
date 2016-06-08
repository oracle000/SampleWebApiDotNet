using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SampleWebApi.Data.Queries;
using ShortBus;

namespace SampleWebApi.Installers
{
    public class CommandQueryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            DependencyResolver.SetResolver(new ShortBus.Windsor.WindsorDependencyResolver(container));

            container.Register(Component.For<IDependencyResolver>().Instance(DependencyResolver.Current));

            container.Register(Component.For(typeof(IMediator)).ImplementedBy(typeof(Mediator)));

            container.Register(Classes.FromAssembly(typeof(GetTodosQuery).Assembly).BasedOn
                (
                    typeof(IRequestHandler<,>),
                    typeof(IAsyncRequestHandler<,>),
                    typeof(INotificationHandler<>),
                    typeof(IAsyncNotificationHandler<>)
                ).WithService.Base()
                .LifestylePerWebRequest());
        }
    }
}
