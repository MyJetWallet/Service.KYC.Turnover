using System.ServiceModel;
using System.Threading.Tasks;
using Service.KYC.Turnover.Grpc.Models;

namespace Service.KYC.Turnover.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}