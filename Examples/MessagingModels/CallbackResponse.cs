using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingModels
{
    public interface ICallbackResponse
    {
        string ModifiedMessage { get; }
    }

    public class CallbackResponse : ICallbackResponse
    {
        public CallbackResponse(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; private set; }

        public string ModifiedMessage { get; set; }
    }
}
