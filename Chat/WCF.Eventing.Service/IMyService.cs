using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCF.Eventing.Service
{
    /// <summary>
    /// A chat room service based on events and duplex communication in WCF.
    /// </summary>
    [ServiceContract(CallbackContract=typeof(IMyCallback),SessionMode=SessionMode.Required)]
    interface IMyService
    {
        /// <summary>
        /// Broadcasts the message to all the subscribed clients along with the id provided.
        /// </summary>
        /// <param name="id">The ID of the person who is chatting.</param>
        /// <param name="message">The message to be broadcasted.</param>
        [OperationContract(IsOneWay = true)]
        void SendMessage(string id, string message);

        /// <summary>
        /// Subscribes the client to the service with the unique ID provided.
        /// </summary>
        /// <param name="id">The ID based on which the client can be uniquely identified.</param>
        [OperationContract(IsOneWay = true)]
        void Join(string id);

        /// <summary>
        /// Unsubscribes the client to the service with the unique ID provided.
        /// </summary>
        /// <param name="id">The ID based on which the client can be uniquely identified.</param>
        [OperationContract(IsOneWay = true)]
        void Leave(string id);
    }

    
}
