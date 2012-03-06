using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFHostManager
{
    public class CommonServiceBehavior : System.ServiceModel.Description.IServiceBehavior
    {
        #region IServiceBehavior 成员

        public void AddBindingParameters(System.ServiceModel.Description.ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<System.ServiceModel.Description.ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(System.ServiceModel.Description.ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            throw new NotImplementedException();
        }

        public void Validate(System.ServiceModel.Description.ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
