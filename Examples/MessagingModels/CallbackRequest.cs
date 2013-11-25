using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingModels
{
    public interface ICallbackRequest : CorrelatedBy<Guid>
    {
        string Message { get; }
    }

    public class CallbackRequest : ICallbackRequest
    {
        public CallbackRequest()
        {
            CorrelationId = Guid.NewGuid();
        }

        public Guid CorrelationId { get; private set; }

        public string Message { get; set; }
    }
}
