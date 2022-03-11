using Autofac;
using Service.KYC.Turnover.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.KYC.Turnover.Client
{
    public static class AutofacHelper
    {
        public static void RegisterKYCTurnoverClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new KYCTurnoverClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
