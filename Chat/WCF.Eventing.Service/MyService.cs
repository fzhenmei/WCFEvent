using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Remoting.Messaging;

namespace WCF.Eventing.Service
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession , ConcurrencyMode=ConcurrencyMode.Reentrant)]
    public class MyService: IMyService
    {

        #region Member Variables

        public  delegate void MessageEventHandler(object sender, EventArgs e);
        //The event when raised has to be invoked for all the subscribed clients and hence made static.
        public  static event MessageEventHandler Broadcast;
        
        IMyCallback _callbackInstance;
        public static string _message = String.Empty;
        public static string _id = String.Empty;
        
        #endregion  

        #region IMyService Members
        public void SendMessage(string id, string message)
        {
            _id = id;
            _message = message;
            Console.WriteLine(DateTime.Now+" : "+id+" has sent a message.");
            Iterate(new EventArgs());

        }

        public void Join(string id)
        {
            _callbackInstance = OperationContext.Current.GetCallbackChannel<IMyCallback>();
            Broadcast += new MessageEventHandler(RaiseBroadcastEvent);
            Console.WriteLine(DateTime.Now + " : " + id + " has connected.");

        }

        public void Leave(string id)
        {
            Broadcast -= new MessageEventHandler(RaiseBroadcastEvent);
            Console.WriteLine(DateTime.Now + " : " + id + " has disconnected.");
        }

        #endregion

        #region private methods
        //Method to be invoked when the 'Broadcast' event is raised.
        void RaiseBroadcastEvent(object sender, EventArgs e)
        {
            _callbackInstance.ReceiveMessage(_id+" : "+_message);
        }

        //Iterates through the list of registered clients and invokes the 'RaiseBroadcastEvent' method.
        void Iterate( EventArgs e)
        {

            if (Broadcast != null)
            {
                foreach (MessageEventHandler _handler in Broadcast.GetInvocationList())
                {
                    _handler.BeginInvoke(this, e, null, null);
                }
            }
        }

        #endregion


    }
}
