namespace Service.Client.Turnover.Domain.Models
{
    public class TurnoverSettings
    {
        public string SettingsKey { get; set; } = "Settings";
        public string LimitAsset { get; set; }
        public decimal LimitLevel2 { get; set; }
        public decimal LimitLevel3 { get; set; }
    }
}