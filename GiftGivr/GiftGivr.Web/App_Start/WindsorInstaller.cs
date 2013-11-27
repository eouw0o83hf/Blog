using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DbSync;
using GiftGivr.Web.Classes;
using GiftGivr.Web.Data;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WindsorClasses = Castle.MicroKernel.Registration.Classes;

namespace GiftGivr.Web.App_Start
{
    public class WindsorInstaller : IWindsorInstaller
    {
        protected Property ConnectionStringDependency_GiftGivr
        {
            get
            {
                return Dependency.OnValue("connectionString", ConfigurationManager.ConnectionStrings["giftGivr"].ConnectionString);
            }
        }

        private TargetAssembly TargetAssembly
        {
            get
            {
                return new TargetAssembly
                {
                    Target = typeof(Migrations._001_InitialDbCreate).Assembly
                };
            }
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //// DB
            // Repository
            container.Register(Component.For<GiftGivrDataContext>().DependsOn(ConnectionStringDependency_GiftGivr).LifestyleTransient());

            // Migrator
            container.Register(Component.For<TargetAssembly>().Instance(TargetAssembly).LifestyleSingleton());

            //// Services
            container.Register(Component.For<ICryptoService>()
                                        .ImplementedBy<PBKDF2>()
                                        .LifestyleTransient());

            //// Controllers
            container.Register(Component.For<GiftGivrControllerContext>()
                                .LifestyleTransient()
                                .DependsOn(
                                    Dependency.OnValue("SendGridSmtpServer", ConfigurationManager.AppSettings["SendGrid_SmtpServer"]),
                                    Dependency.OnValue("SendGridUsername", ConfigurationManager.AppSettings["SendGrid_Username"]),
                                    Dependency.OnValue("SendGridPassword", ConfigurationManager.AppSettings["SendGrid_Password"])
                                ));

            container.Register(WindsorClasses.FromThisAssembly()
                                .BasedOn<IController>()
                                .LifestyleTransient());

            container.Register(Component.For<IControllerFactory>()
                                        .ImplementedBy<WindsorControllerFactory>()
                                        .LifestyleSingleton());

            container.Register(Component.For<SynchronizationService>()
                                        .LifestyleTransient()
                                        .DependsOn(ConnectionStringDependency_GiftGivr));
        }
    }
}