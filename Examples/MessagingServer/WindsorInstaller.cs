using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;
using MessagingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingServer
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Server>()
                                        .Forward<IConsumer>()
                                        .LifestyleSingleton());

            container.Register(Component.For<IServiceBus>()
                                        .Instance(ServiceBusFactory.New(configure =>
                                        {
                                            configure.UseRabbitMq();

                                            configure.ReceiveFrom("rabbitmq://localhost/messagingServer");

                                            configure.Subscribe(a => a.LoadFrom(container));
                                        }))
                                        .LifestyleSingleton());
        }
    }
}
