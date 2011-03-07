using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCF.Eventing.Service
{
    /// <summary>
    /// The callback interface which is to be implemented by the client.
    /// </summary>
    public interface IMyCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(string message);
    }
}
