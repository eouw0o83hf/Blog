using MassTransit;
using MessagingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingServer
{
    public class Server :
        Consumes<ICallbackRequest>.Context
    {
        public void Consume(IConsumeContext<ICallbackRequest> context)
        {
            Console.WriteLine("Consuming {0}: {1}", context.CorrelationId, context.Message.Message);

            var response = new CallbackResponse(context.Message.CorrelationId)
            {
                ModifiedMessage = context.Message.Message + " Bar!"
            };

            context.Respond(response);
        }
    }
}
<br class="Apple-interchange-newline"><img id="playingAlbumArt" src="https://lh3.googleusercontent.com/_2IcSh7YfNyBuzkwSnm4IFo1bPmDk8QpDCFTMRAa9bqNQVjPB5CVXisuGfb68A" style="border: none; background-color: rgb(250, 250, 250); display: block; position: absolute; top: 0px; left: 0px; width: 62px; height: 62px; color: rgb(0, 0, 0); font-family: Roboto, arial, sans-serif; font-size: 13px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: nowrap; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px;"><div class="hover-overlay" style="outline: none; position: absolute; top: 0px; left: 0px; width: 62px; height: 62px; cursor: pointer; background-color: black; opacity: 0.7; transition: opacity 0.218s; -webkit-transition: opacity 0.218s; color: rgb(0, 0, 0); font-family: Roboto, arial, sans-serif; font-size: 13px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: nowrap; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: initial initial;"><br class="Apple-interchange-newline">