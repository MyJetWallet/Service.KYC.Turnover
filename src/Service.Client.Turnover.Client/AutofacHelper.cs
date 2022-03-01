using Autofac;
using Service.Client.Turnover.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.Client.Turnover.Client
{
    public static class AutofacHelper
    {
        public static void RegisterTurnoverClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new TurnoverClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
