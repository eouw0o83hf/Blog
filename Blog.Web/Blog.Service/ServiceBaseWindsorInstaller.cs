using Blog.Data;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public abstract class ServiceBaseWindsorInstaller : DataBaseWindsorInstaller
    {
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            base.Install(container, store);

            // Service
            container.Register(Component.For<IBlogService>().ImplementedBy<BlogService>().LifestyleTransient());
        }
    }
}
