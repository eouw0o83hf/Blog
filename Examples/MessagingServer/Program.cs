using Castle.Windsor;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Server");

            var container = new WindsorContainer();
            container.Install(new WindsorInstaller());

            container.ResolveAll<IServiceBus>();

            Console.Read();
        }
    }
}
