using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebAnalytics.Core.ValueTypes;
using System.Text.Json.Serialization;

namespace WebAnalytics.Core.Entities
{
    public class Session
    {
        public string SessionId { get; set; }
        public string VisitorId { get; set; }
        public string SiteId { get; set; }

        public TimeSpan Duration => RecordingFragments[0].Time - this.Start;

        public double Activity => RecordingFragments
                                  .SelectMany(f => f.Frames.Frames)
                                  .Count(f => f.ClickX.HasValue || f.ContextMenuX.HasValue) /
            Duration.TotalMinutes;

        public DateTimeOffset Start { get; set; }

        public DeviceInfo DeviceInfo { get; set; }

        public List<RecordingFragment> RecordingFragments { get; set; } = new List<RecordingFragment>();
    }
}