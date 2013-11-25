using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingClient
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Client>()
                                        .LifestyleTransient());

            container.Register(Component.For<IServiceBus>()
                                        .Instance(ServiceBusFactory.New(configure =>
                                            {
                                                configure.UseRabbitMq();

                                                configure.ReceiveFrom("rabbitmq://localhost/messagingClient");
                                            }))
                                        .LifestyleSingleton());
        }
    }
}
