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
    public class ArgumentExceptionHandlerAttribute : Attribute, IErrorHandler, IServiceBehavior
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is ArgumentException)
            {
                FaultException<ArgumentException> faultException = new FaultException<ArgumentException>(new ArgumentException(error.Message));
                fault = Message.CreateMessage(version, faultException.CreateMessageFault(), faultException.Action);
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
                channelDispatcher.ErrorHandlers.Add(this);
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ServiceEndpoint endpoint in serviceDescription.Endpoints)
                if (endpoint.Contract.Name == "IGeoService")
                    foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
                        if (operationDescription.Name == "GetZipInfo")
                            if (operationDescription.Faults.FirstOrDefault(item => item.DetailType.Equals(typeof(ArgumentException))) == null)
                                throw new InvalidOperationException("GetZipInfo operation requires a fault contract for ArgumentException.");
        }
    }
}