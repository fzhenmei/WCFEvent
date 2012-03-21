using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // 注意: 如果更改此处的接口名称 "IService2"，也必须更新 App.config 中对 "IService2" 的引用。
    [ServiceContract]
    public interface IService2
    {
        [OperationContract]
        string GetData(int value);
    }
}
