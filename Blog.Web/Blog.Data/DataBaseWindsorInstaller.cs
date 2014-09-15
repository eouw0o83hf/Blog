using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public abstract class DataBaseWindsorInstaller : IWindsorInstaller
    {
        protected abstract Property ConnectionStringDependency_eouw0o83hf { get; }

        public virtual void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Repository
            container.Register(Component.For<BlogDataContext>().ImplementedBy<BlogDataContextWrapper>().DependsOn(ConnectionStringDependency_eouw0o83hf).LifestyleTransient());
            
            // Migrator
            container.Register(Component.For<TargetAssembly>().Instance(TargetAssembly).LifestyleSingleton());
        }

        private TargetAssembly TargetAssembly
        {
            get
            {
                return new TargetAssembly
                {
                    Target = typeof(DataBaseWindsorInstaller).Assembly
                };
            }
        }
    }
}
