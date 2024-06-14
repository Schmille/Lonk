using Lonk.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lonk.Controllers
{
    [Route("/l")]
    [ApiController]
    public class LinkController : ControllerBase
    {

        private readonly LinkManager Manager;

        public LinkController(LinkManager manager)
        {
            Manager = manager;
        }

        [HttpGet]
        [Route("{longLink}")]
        public async Task<ActionResult> GetAsync(string longLink)
        {
            LongLinkData? redirectTarget = await Manager.GetLinkUriAsync(longLink);

            if (redirectTarget == null)
                return NotFound();

            await Task.Delay(redirectTarget.Delay);

            return Redirect(redirectTarget.Link.AbsoluteUri);
        }

        // Maybe re-enable in future, if I want to do link creation via api
        //[HttpPost]
        //[Route("create")]
        //public async Task<ActionResult<string>> Post([FromBody] LongLinkData linkData)
        //{
        //    // Make sure input is 0 <= x <= 10000 (ms)
        //    int elongation = Math.Clamp(linkData.Delay, 0, 10_000);

        //    if(await Manager.IsBlockedHost(linkData.Link))
        //        return BadRequest();

        //    string linkId = await Manager.CreateLinkAsync(linkData.Link, elongation);

        //    Uri uri = new Uri($"https://{Environ.Host}/l/{linkId}");

        //    return Ok(uri.AbsoluteUri);
        //}
    }
}
