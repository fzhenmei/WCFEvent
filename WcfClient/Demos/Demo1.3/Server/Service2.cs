using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // 注意: 如果更改此处的类名 "Service2"，也必须更新 App.config 中对 "Service2" 的引用。
    public class Service2 : IService2
    {
        #region IService2 成员

        public string GetData(int value)
        {
            return string.Format("You entered: {0}, from: {1}", value, GetType()); 
        }

        #endregion
    }
}
