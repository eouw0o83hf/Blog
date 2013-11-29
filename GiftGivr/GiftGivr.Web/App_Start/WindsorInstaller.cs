using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DbSync;
using GiftGivr.Web.Classes;
using GiftGivr.Web.Data;
using SendGrid;
using SendGrid.Transport;
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
                return Dependency.OnValue("connectionString", ConfigurationManager.ConnectionStrings["GiftGivrConnectionString"].ConnectionString);
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

            container.Register(Component.For<CryptoProvider>()
                                        .LifestyleTransient());


            container.Register(Component.For<ITransport>()
                                        .UsingFactoryMethod(a =>
                                                SMTP.GetInstance(new System.Net.NetworkCredential
                                                {
                                                    UserName = ConfigurationManager.AppSettings["SendGrid_Username"],
                                                    Password = ConfigurationManager.AppSettings["SendGrid_Password"]
                                                }, ConfigurationManager.AppSettings["SendGrid_SmtpServer"])
                                            )
                                        .LifestyleTransient());

            //// Controllers
            container.Register(Component.For<GiftGivrControllerContext>()
                                .LifestyleTransient());

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