using Autofac;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.Bitgo.DepositDetector.Domain.Models;
using Service.Client.Turnover.Domain.Models.NoSql;
using Service.Client.Turnover.Jobs;
using Service.IndexPrices.Client;
using Service.KYC.Domain.Models.Messages;

namespace Service.Client.Turnover.Modules
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