using Lonk.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lonk.Pages
{
    public class IndexModel : PageModel
    {
        public string? Elongated { get; set; }

        [BindProperty]
        public string? LongLink { get; set; }

        [BindProperty]
        public int? Delay { get; set; }

        private readonly LinkManager Manager;

        public IndexModel(LinkManager manager)
        {
            Manager = manager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (LongLink == null)
                return Page();

            if (Delay == null)
                Delay = 0;

            Uri link = new Uri(LongLink);

            Delay *= 1000;
            // Make sure input is 0 <= x <= 10000 (ms)
            int elongation = Math.Clamp((int)Delay, 0, 10_000);

            if (Manager.IsBlockedHost(link))
                return Page();

            string linkId = await Manager.CreateLinkAsync(link, elongation);

            Uri uri = new Uri($"https://{Environ.Host}/l/{linkId}");

            Elongated = uri.AbsoluteUri;
            return Page();
        }

    }
}
