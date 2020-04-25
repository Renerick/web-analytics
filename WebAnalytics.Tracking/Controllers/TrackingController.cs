using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAnalytics.Abstraction.Db;
using WebAnalytics.Core.Entities;
using WebAnalytics.Tracking.Models;
using Action = WebAnalytics.Core.Entities.Action;

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

        [HttpPost("Error")]
        public async Task<IActionResult> PostError(ErrorRequest request)
        {
            var error = new Error();

            await EventWriter.WriteErrorAsync(error);

            return Ok();
        }

        [HttpPost("Action")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> PostAction([FromForm]ActionRequest request)
        {
            var error = new Action()
            {
                VisitorId = request.VisitorId,
                SiteId = request.SiteId,
                Name = request.Name,
                Time = request.Time,
                Type = request.Type,
                Url = request.Url,
                Data = request.Data,
            };

            await EventWriter.WriteActionAsync(error);

            return Ok();
        }

        [HttpPost("UiEvent")]
        public async Task<IActionResult> PostUiEvent(UiEventRequest request)
        {
            var @event = new UiEvent();

            await EventWriter.WriteUiEventAsync(@event);

            return Ok();
        }
    }
}