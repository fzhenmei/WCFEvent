using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFHostManager
{
    public class CommonErrorHandler : System.ServiceModel.Dispatcher.IErrorHandler
    {
        #region IErrorHandler 成员

        public bool HandleError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
