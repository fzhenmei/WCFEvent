using System.ServiceModel;

namespace MyContract
{
    [ServiceContract]
    public interface IMyWcfService
    {
        [OperationContract]
        void DoWork();
    }
}
