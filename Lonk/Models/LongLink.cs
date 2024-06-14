using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lonk.Models
{
    [Index(nameof(LongLinkId), IsUnique = true, Name = "IDX_LONG_LINK_ID")]
    [Index(nameof(LongLinkId), IsUnique = false, Name = "IDX_LONG_LINK_CREATED")]
    public class LongLink
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(512)]
        public Uri RedirectUrl { get; set; }
        [Required]
        [MaxLength(512)]
        public string LongLinkId { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public int ElongationTime { get; set; }

        public LongLink(Uri redirectUrl, string longLinkId, int elongationTime)
        {
            Id = Guid.NewGuid();
            RedirectUrl = redirectUrl;
            LongLinkId = longLinkId;
            ElongationTime = elongationTime;
            Created = DateTime.Now;
        }
    }
}
