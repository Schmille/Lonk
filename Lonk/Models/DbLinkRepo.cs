
using Microsoft.EntityFrameworkCore;

namespace Lonk.Models
{
    public class DbLinkRepo : ILinkRepository
    {
        private readonly LinkCtx Ctx;

        public DbLinkRepo(LinkCtx ctx)
        {
            Ctx = ctx;
            Ctx.Database.EnsureCreated();
        }

        public async Task DeleteAsync(string longId)
        {
            await Ctx.Links
                .Where(l => l.LongLinkId == longId)
                .ExecuteDeleteAsync();
        }

        public async Task<LongLink?> GetByLongIdAsync(string longId)
        {
            return await Ctx.Links
                .Where(l => l.LongLinkId == longId)
                .SingleOrDefaultAsync();
        }

        public async Task SaveLinkAsync(LongLink link)
        {
            Ctx.Links.Add(link);
            await Ctx.SaveChangesAsync();
        }
    }
}
