using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.KYC.Turnover.Grpc;

namespace Service.KYC.Turnover.Client
{
    [UsedImplicitly]
    public class KYCTurnoverClientFactory: MyGrpcClientFactory
    {
        public KYCTurnoverClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
