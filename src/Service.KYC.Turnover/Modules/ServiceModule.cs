using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.Bitgo.DepositDetector.Domain.Models;
using Service.IndexPrices.Client;
using Service.KYC.Domain.Models.Messages;
using Service.KYC.Turnover.Domain.Models.NoSql;
using Service.KYC.Turnover.Jobs;

namespace Service.KYC.Turnover.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMyNoSqlWriter<TurnoverSettingsNoSqlEntity>(
                Program.ReloadedSettings(t => t.MyNoSqlWriterUrl), TurnoverSettingsNoSqlEntity.TableName);
            
            var serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), Program.LogFactory);
            builder.RegisterMyServiceBusPublisher<KycLevelUpdateMessage>(serviceBusClient, KycLevelUpdateMessage.TopicName, true);

            var myNoSqlClient = builder.CreateNoSqlClient(Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort));
            var queueName = "KYC.Turnover";

            builder.RegisterMyServiceBusSubscriberSingle<Deposit>(serviceBusClient, Deposit.TopicName, queueName, TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterConvertIndexPricesClient(myNoSqlClient);

            builder.RegisterMyNoSqlReader<TurnoverSettingsNoSqlEntity>(myNoSqlClient,
                TurnoverSettingsNoSqlEntity.TableName);


            builder.RegisterType<DepositCalculationJob>().AutoActivate().SingleInstance();
        }
    }
}