using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;
using MyNoSqlServer.Abstractions;
using Service.Bitgo.DepositDetector.Domain.Models;
using Service.IndexPrices.Client;
using Service.KYC.Domain.Models.Enum;
using Service.KYC.Domain.Models.Messages;
using Service.KYC.Turnover.Domain.Models;
using Service.KYC.Turnover.Domain.Models.NoSql;
using Service.KYC.Turnover.Postgres;

namespace Service.KYC.Turnover.Jobs
{
    public class DepositCalculationJob
    {
        private readonly IMyNoSqlServerDataWriter<TurnoverSettingsNoSqlEntity> _settingsWriter;
        private readonly ILogger<DepositCalculationJob> _logger;
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
        private readonly IServiceBusPublisher<KycLevelUpdateMessage> _publisher;
        private readonly IConvertIndexPricesClient _indexPricesClient;
        private TurnoverSettings _settings;
        
        
        public DepositCalculationJob(IMyNoSqlServerDataWriter<TurnoverSettingsNoSqlEntity> settingsWriter, 
            ILogger<DepositCalculationJob> logger, 
            DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder,
            ISubscriber<Deposit> subscriber, 
            IServiceBusPublisher<KycLevelUpdateMessage> publisher, 
            IConvertIndexPricesClient indexPricesClient, 
            IMyNoSqlServerDataReader<TurnoverSettingsNoSqlEntity> settingsReader)
        {
            _settingsWriter = settingsWriter;
            _logger = logger;
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
            _publisher = publisher;
            _indexPricesClient = indexPricesClient;
            settingsReader.SubscribeToUpdateEvents(SettingsChanged, null);
            subscriber.Subscribe(HandleDeposit);
            
            InitSettings().GetAwaiter().GetResult();
        }

        private async ValueTask HandleDeposit(Deposit message)
        {
            try
            {
                if (message.Status == DepositStatus.Processed)
                {
                    await using var context = new DatabaseContext(_dbContextOptionsBuilder.Options);
                    await context.UpsertAsync(new[] { message });

                    // decimal depositedAmount = 0;
                    // var depositsGroups = context.Deposits.Where(t => t.ClientId == message.ClientId).GroupBy(t => t.AssetSymbol).Select(deposits=> new
                    // {
                    //     Asset = deposits.Key,
                    //     Amount = deposits.Sum(deposit=>deposit.Amount)
                    // });
                    // foreach (var depositsGroup in depositsGroups)
                    // {
                    //     var price = _indexPricesClient.GetConvertIndexPriceByPairAsync(depositsGroup.Asset, _settings.LimitAsset).Price;
                    //
                    //     depositedAmount += depositsGroup.Amount * price;
                    // }
                    //
                    // if (depositedAmount >= _settings.LimitLevel2 && depositedAmount < _settings.LimitLevel3)
                    // {
                    //     await _publisher.PublishAsync(new KycLevelUpdateMessage()
                    //     {
                    //         ClientId = message.ClientId,
                    //         KycLevel = KycLevel.Level2
                    //     });
                    // }
                    //
                    // if (depositedAmount >= _settings.LimitLevel3)
                    // {
                    //     await _publisher.PublishAsync(new KycLevelUpdateMessage()
                    //     {
                    //         ClientId = message.ClientId,
                    //         KycLevel = KycLevel.Level3
                    //     });
                    // }                
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "When handling deposit message for clientId {clientId} and depositId {depositId}", message.ClientId, message.Id);
                throw;
            }
        }
        
        private async Task InitSettings()
        {
            var entity = await _settingsWriter.GetAsync(TurnoverSettingsNoSqlEntity.GeneratePartitionKey(),
                TurnoverSettingsNoSqlEntity.GenerateRowKey());
            if (entity == null)
            {
                await using var context = new DatabaseContext(_dbContextOptionsBuilder.Options);
                var settings = context.Settings.FirstOrDefault();

                if (settings == null)
                {
                    _logger.LogError("Turnover settings not initialized");
                    await Task.Delay(10000);
                    throw new Exception("Turnover settings not initialized");
                }

                await _settingsWriter.InsertOrReplaceAsync(TurnoverSettingsNoSqlEntity.Create(settings));
                _settings = settings;
            }
            else
            {
                _settings = entity.Settings;
            }
        }

        private void SettingsChanged(IReadOnlyList<TurnoverSettingsNoSqlEntity> list)
        {
            using var context = new DatabaseContext(_dbContextOptionsBuilder.Options);
            context.UpsertAsync(list.Select(t => t.Settings)).GetAwaiter().GetResult();
            _settings = list.First().Settings;
        }
    }
}