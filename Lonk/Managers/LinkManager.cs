using Lonk.Models;
using System.Text;

namespace Lonk.Services
{
    public record class LongLinkData(Uri Link, int Delay);

    public class LinkManager
    {
        private readonly ILinkRepository LinkRepo;
        private readonly IBlocklistRepo BlocklistRepo;

        public LinkManager(ILinkRepository linkRepo, IBlocklistRepo blocklistRepo)
        {
            this.LinkRepo = linkRepo;
            BlocklistRepo = blocklistRepo;
        }

        public async Task<string> CreateLinkAsync(Uri linkUri, int elongation)
        {
            string longLinkId = GenRandomId();

            LongLink longLink = new LongLink(linkUri, longLinkId, elongation);

            await LinkRepo.SaveLinkAsync(longLink);

            return longLinkId;
        }

        public async Task<LongLinkData?> GetLinkUriAsync(string longLinkId)
        {
            LongLink? link = await LinkRepo.GetByLongIdAsync(longLinkId);
            if (link == null)
                return null;

            return new LongLinkData(link.RedirectUrl, link.ElongationTime);
        }

        public async Task<bool> IsBlockedHostAsync(Uri uri)
        {
            string host = uri.Host;

            return await BlocklistRepo.ExistsAsync(host);
        }

        private string GenRandomId()
        {
            const int length = 512;
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                char randChar = characters.ElementAt(random.Next(characters.Length));
                stringBuilder.Append(randChar);
            }

            return stringBuilder.ToString();
        }
    }
}
