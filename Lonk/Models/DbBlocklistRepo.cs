using Microsoft.EntityFrameworkCore;

namespace Lonk.Models
{
    public class DbBlocklistRepo : IBlocklistRepo
    {
        private readonly LinkCtx Ctx;

        public DbBlocklistRepo(LinkCtx ctx)
        {
            Ctx = ctx;
        }

        public async Task CreateAsync(string hostname)
        {
            Ctx.BlockedHosts.Add(new(hostname));
            await Ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(string hostname)
        {
            await Ctx.BlockedHosts
                .Where(b => b.Hostname == hostname)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> ExistsAsync(string hostname)
        {
            BlocklistEntry? entry = await Ctx.BlockedHosts
                .FirstOrDefaultAsync(b => b.Hostname == hostname);

            return entry != null;
        }

        public async Task<List<string>> GetAllAsync()
        {
            return await Ctx.BlockedHosts
                .Select(b => b.Hostname)
                .ToListAsync();
        }
    }
}
