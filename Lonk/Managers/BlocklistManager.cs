using Lonk.Models;
using System.Text.RegularExpressions;

namespace Lonk.Managers
{
    public class BlocklistManager
    {
        private readonly IBlocklistRepo BlocklistRepo;
        private List<Regex> RegexList;
        private Timer Timer;

        public BlocklistManager()
        {
            BlocklistRepo = new DbBlocklistRepo(new LinkCtx());
            RegexList = new List<Regex>();
            Timer = new Timer(RebuildRegexList, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
        }

        public bool IsInBlocklist(string uri)
        {
            lock (RegexList)
            {
                foreach (Regex regex in RegexList)
                    if (regex.IsMatch(uri))
                        return true;
            }

            return false;
        }

        private async void RebuildRegexList(object? obj)
        {
            List<string> patterns = await BlocklistRepo.GetAllAsync();
            lock (RegexList)
            {
                RegexList.Clear();

                foreach (string pattern in patterns)
                {
                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.NonBacktracking | RegexOptions.Compiled);
                    RegexList.Add(regex);
                }
            }
        }
    }
}
