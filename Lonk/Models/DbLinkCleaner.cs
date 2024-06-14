using Microsoft.EntityFrameworkCore;

namespace Lonk.Models
{
    public class DbLinkCleaner : ILinkCleaner
    {
        public async Task CleanAsync(DateTime before)
        {
            using LinkCtx ctx = new LinkCtx();

            await ctx.Links
                .Where(l => l.Created <= before)
                .ExecuteDeleteAsync();
        }

    }
}
