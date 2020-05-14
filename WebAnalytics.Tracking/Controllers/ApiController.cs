using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAnalytics.Abstraction;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Tracking.Controllers
{
    public class ApiController : ControllerBase
    {
        private IAnalyticsStore _store;

        public ApiController(IAnalyticsStore store)
        {
            _store = store;
        }


        [HttpPost("/api/v1/sites")]
        public async Task<IActionResult> CreateSite([FromBody] Site site)
        {
            return Ok(await _store.CreateSiteAsync(site));
        }

        [HttpGet("/api/v1/sites")]
        public async Task<IActionResult> GetSite()
        {
            return Ok(await _store.GetSitesAsync());
        }


        [HttpGet("/api/v1/site/{siteId}/sessions")]
        public async Task<IActionResult> GetSessions(string siteId)
        {
            return Ok(await _store.GetSessionsAsync(siteId));
        }

        [HttpGet("/api/v1/site/{siteId}/session/{sessionId}/recording")]
        public async Task<IActionResult> GetSessionRecording(string siteId,string sessionId)
        {
            return Ok(await _store.GetFragmentAsync(siteId, sessionId));
        }
    }
}