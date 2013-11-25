using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new WindsorInstaller());

            var client = container.Resolve<Client>();
            client.Run();
        }
    }
}
