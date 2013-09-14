using Blog.Service;
using Blog.Web.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DbSync;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web
{
    public class BlogWindsorInstaller : ServiceBaseWindsorInstaller
    {
        protected override Property ConnectionStringDependency_eouw0o83hf
        {
            get 
            { 
                return Dependency.OnValue("connectionString", ConfigurationManager.ConnectionStrings["eouw0o83hf"].ConnectionString); 
            }
        } 

        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            base.Install(container, store);

            container.Register(Component.For<BlogControllerContext>()
                                .LifestyleTransient()
                                .DependsOn(
                                    Dependency.OnValue("CdnAccountName", ConfigurationManager.AppSettings["Cdn_AccountName"]),
                                    Dependency.OnValue("CdnAccessKey", ConfigurationManager.AppSettings["Cdn_AccessKey_Primary"])
                                ));

            container.Register(Classes.FromThisAssembly()
                                .BasedOn<IController>()
                                .LifestyleTransient());

            container.Register(Component.For<IControllerFactory>()
                                        .ImplementedBy<WindsorControllerFactory>()
                                        .LifestyleSingleton());

            container.Register(Component.For<SynchronizationService>()
                                        .LifestyleTransient()
                                        .DependsOn(ConnectionStringDependency_eouw0o83hf));
        }
    }
}