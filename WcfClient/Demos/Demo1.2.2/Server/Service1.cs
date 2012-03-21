using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // 注意: 如果更改此处的类名 "Service1"，也必须更新 App.config 中对 "Service1" 的引用。
    public class Service1 : IService1
    {
        #region IService1 成员

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        #endregion
    }
}
