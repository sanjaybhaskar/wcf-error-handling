using System;
using System.Collections.Generic;
using System.Linq;
using GeoLib.Contracts;
using GeoLib.Data;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;

namespace GeoLib.Services
{
    public class ArgumentExceptionHandlerExtention : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ArgumentExceptionHandlerAttribute); }
        }

        protected override object CreateBehavior()
        {
            return new ArgumentExceptionHandlerAttribute();
        }
    }
}
