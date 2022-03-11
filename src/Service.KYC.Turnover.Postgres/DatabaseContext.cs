using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;
using Service.Bitgo.DepositDetector.Domain.Models;
using Service.KYC.Turnover.Domain.Models;

namespace Service.KYC.Turnover.Postgres
{
    public class DatabaseContext : MyDbContext
    {
        public const string Schema = "kycturnover";

        private const string SettingsTableName = "settings";
        private const string DepositsTableName = "deposits";

        public DbSet<TurnoverSettings> Settings { get; set; }
        public DbSet<Deposit> Deposits { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<TurnoverSettings>().ToTable(SettingsTableName);
            modelBuilder.Entity<TurnoverSettings>().HasKey(e =>e.SettingsKey);
            
            modelBuilder.Entity<Deposit>().ToTable(DepositsTableName);
            modelBuilder.Entity<Deposit>().HasKey(e =>e.Id);
            
            modelBuilder.Entity<Deposit>().Property(e =>e.LastError).IsRequired(false);
            modelBuilder.Entity<Deposit>().Property(e =>e.CardLast4).IsRequired(false);
            modelBuilder.Entity<Deposit>().Property(e =>e.Network).IsRequired(false);

            modelBuilder.Entity<Deposit>().HasIndex(e => e.ClientId);
            modelBuilder.Entity<Deposit>().HasIndex(e => e.WalletId);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> UpsertAsync(IEnumerable<TurnoverSettings> entities)
        {
            var result = await Settings.UpsertRange(entities).AllowIdentityMatch().RunAsync();
            return result;
        }
        
        public async Task<int> UpsertAsync(IEnumerable<Deposit> entities)
        {
            var result = await Deposits.UpsertRange(entities).AllowIdentityMatch().RunAsync();
            return result;
        }
    }
}
