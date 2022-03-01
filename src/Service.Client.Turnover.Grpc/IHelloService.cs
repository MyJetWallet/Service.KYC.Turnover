using System.ServiceModel;
using System.Threading.Tasks;
using Service.Client.Turnover.Grpc.Models;

namespace Service.Client.Turnover.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}