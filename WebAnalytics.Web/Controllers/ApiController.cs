using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAnalytics.Abstraction;
using WebAnalytics.Core.Entities;
using WebAnalytics.Core.Entities.Ontology;
using WebAnalytics.HeatMaps;

namespace WebAnalytics.Web.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly IAnalyticsStore _store;
        private readonly IRdfMapper _rdfMapper;
        private readonly IHeatmapDrawer _heatmapDrawer;

        public ApiController(IAnalyticsStore store, IRdfMapper rdfMapper, IHeatmapDrawer heatmapDrawer)
        {
            _store = store;
            _rdfMapper = rdfMapper;
            _heatmapDrawer = heatmapDrawer;
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

        [HttpGet("/api/v1/site/{siteId}/pages")]
        public async Task<IActionResult> GetPages(string siteId)
        {
            return Ok(await _store.GetPagesAsync(siteId));
        }

        [HttpGet("/api/v1/site/{siteId}/pages/{pageUri}/heatmap")]
        public async Task<IActionResult> GetHeatmap(string siteId, string pageUri)
        {
            var sessions = await _store.GetPageViews(siteId, WebUtility.UrlDecode(pageUri));
            var rdfSessions = _rdfMapper.MapFragmentsToRdf(sessions);
            var map = _heatmapDrawer.CreateHeatmap(rdfSessions);
            return new FileContentResult(map, "img/png");
        }
    }
}