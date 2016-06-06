using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SampleWebApi.Data;

namespace SampleWebApi.Installers
{
    public class DbContextInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISampleWebApiContext>()
                    .ImplementedBy(typeof(SampleWebApiContext))
                    .DependsOn(Dependency.OnValue<string>("name=SampleWebApiContext"))
                    .LifestylePerWebRequest());
        }
    }
}
