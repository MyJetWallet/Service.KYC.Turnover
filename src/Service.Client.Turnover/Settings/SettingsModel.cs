using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.Client.Turnover.Settings
{
    public class SettingsModel
    {
        [YamlProperty("KYCTurnover.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("KYCTurnover.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("KYCTurnover.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
        
        [YamlProperty("KYCTurnover.PostgresConnectionString")]
        public string PostgresConnectionString { get; set; }
        
        [YamlProperty("KYCTurnover.MyNoSqlWriterUrl")]
        public string MyNoSqlWriterUrl { get; set; }
        
        [YamlProperty("KYCTurnover.SpotServiceBusHostPort")]
        public string SpotServiceBusHostPort { get; set; }
        
        [YamlProperty("KYCTurnover.MyNoSqlReaderHostPort")]
        public string MyNoSqlReaderHostPort { get; set; }
    }
}
