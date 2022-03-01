using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.Client.Turnover.Grpc;

namespace Service.Client.Turnover.Client
{
    [UsedImplicitly]
    public class TurnoverClientFactory: MyGrpcClientFactory
    {
        public TurnoverClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
