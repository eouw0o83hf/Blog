using Blog.Service;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web
{
    public class BlogWindsorInstaller : ServiceBaseWindsorInstaller
    {
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            base.Install(container, store);

            container.Register(Classes.FromThisAssembly()
                                .BasedOn<IController>()
                                .LifestyleTransient());

            container.Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>().LifestyleSingleton());
        }
    }
}