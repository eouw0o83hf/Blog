using MassTransit;
using MessagingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingClient
{
    public class Client
    {
        private readonly IServiceBus _serviceBus;
        public Client(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public void Run()
        {
            var message = new CallbackRequest
            {
                Message = "Foo"
            };

            Console.WriteLine("Sending: {0}", message.Message);

            ICallbackResponse response = null;
            _serviceBus.PublishRequest(message, callback =>
                {
                    callback.Handle<ICallbackResponse>(a => response = a);
                });

            Console.WriteLine("Requested: {0}{1}Responded: {2}", message.Message, Environment.NewLine, response.ModifiedMessage);
            Console.Read();
        }
    }
}
