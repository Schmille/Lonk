using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lonk.Models
{
    [Index(nameof(Hostname), IsUnique = true, Name = "IDX_BLOCKLIST_HOSTNAME")]
    public class BlocklistEntry
    {
        [Key]
        public int Id { get; set; }
        public string Hostname { get; set; }

        public BlocklistEntry(string hostname)
        {
            Hostname = hostname;
        }
    }
}
