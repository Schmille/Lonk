using Microsoft.EntityFrameworkCore;

namespace Lonk.Models
{
    public class LinkCtx : DbContext
    {
        public DbSet<LongLink> Links { get; init; }
        public DbSet<BlocklistEntry> BlockedHosts { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={Environ.DbPath}");
        }

        public static void Init()
        {
            using LinkCtx ctx = new LinkCtx();
            ctx.Database.EnsureCreated();
        }
    }
}
