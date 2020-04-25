using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAnalytics.Abstraction;
using WebAnalytics.Core.Entities;
using WebAnalytics.Tracking.Models;

namespace WebAnalytics.Tracking.Controllers
{
    [Route("track")]
    public class TrackingController : ControllerBase
    {
        protected readonly IEventWriter EventWriter;

        public TrackingController(IEventWriter eventWriter)
        {
            EventWriter = eventWriter;
        }

        [HttpGet("event")]
        public async Task<IActionResult> TrackEvent([FromQuery] EventRequest request)
        {
            var @event = new Event()
            {
                SiteId = request.S,
                SessionId = request.S,
                Type = request.T,
                Url = request.G,
                Visitor = request.V,
                Data = new Dictionary<string, object>() {["data"] = request.C, ["x"] = request.X, ["y"] = request.Y, ["width"] = request.W},
            };

            await EventWriter.WriteActionAsync(@event);

            return Ok();
        }
    }
}