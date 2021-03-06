﻿using Castle.Windsor;
using DbSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web
{
    public class MigrationRunner
    {
        public static void RunMigrations(IWindsorContainer container)
        {
            var service = container.Resolve<SynchronizationService>();
            var target = container.Resolve<TargetAssembly>();

            service.RunMigrations(target);
        }
    }
}