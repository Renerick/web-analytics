using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAnalytics.Abstraction;
using WebAnalytics.Core.Entities;
using WebAnalytics.Core.ValueTypes;
using WebAnalytics.Web.Models;
using WebAnalytics.Web.Services;

namespace WebAnalytics.Web.Controllers
{
    [Route("track")]
    public class TrackingController : ControllerBase
    {
        private readonly IEventWriter _eventWriter;
        private readonly IRecordingWriter _recordingWriter;
        private readonly IDeviceService _deviceService;

        public TrackingController(IEventWriter eventWriter, IRecordingWriter recordingWriter, IDeviceService deviceService)
        {
            _eventWriter = eventWriter;
            _recordingWriter = recordingWriter;
            _deviceService = deviceService;
        }

        // S - site
        // V - visitor
        // B - browser
        // G - URL
        // W - width
        // C - additional event data

        [HttpPost("event")]
        public async Task<IActionResult> TrackEvent([FromQuery] EventRequest request)
        {
            var @event = new Event()
            {
                SiteId = request.S,
                Type = request.T,
                Url = request.G,
                Visitor = request.V,
                Time = DateTimeOffset.Now,
            };

            var deviceInfo = _deviceService.BuildFromEventData(request.U);

            await _eventWriter.WriteActionAsync(@event, deviceInfo);

            return Ok();
        }

        [HttpPost("rec")]
        [Consumes("text/plain")]
        public async Task<IActionResult> SaveRecording(string s, string v, string g)
        {
            string body;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
            }
            var frames = JsonSerializer.Deserialize<FrameSet>(body, new JsonSerializerOptions(){PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            var recordingFragment = new RecordingFragment()
            {
                Site = s,
                Visitor = v,
                Time = DateTimeOffset.Now,
                Url = g,
                Frames = frames
            };

            await _recordingWriter.WriteFragmentAsync(recordingFragment);

            return Ok();
        }
    }
}