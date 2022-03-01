using MyNoSqlServer.Abstractions;

namespace Service.Client.Turnover.Domain.Models.NoSql
{
    public class TurnoverSettingsNoSqlEntity: MyNoSqlDbEntity
    {
        public const string TableName = "myjetwallet-kyc-turnover-settings";

        public static string GeneratePartitionKey() => "TurnOverSettings";

        public static string GenerateRowKey() => "TurnOverSettings";
        
        public TurnoverSettings Settings { get; set; }

        public static TurnoverSettingsNoSqlEntity Create(TurnoverSettings settings) =>
            new()
            {
                PartitionKey = GeneratePartitionKey(),
                RowKey = GenerateRowKey(),
                Settings = settings
            };
    }
}