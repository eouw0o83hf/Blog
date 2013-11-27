using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GiftGivr.Web.Classes
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        protected readonly IKernel Kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            Kernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, "The controller path could not be found.");
            }

            return (IController)Kernel.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            Kernel.ReleaseComponent(controller);
            base.ReleaseController(controller);
        }
    }
}