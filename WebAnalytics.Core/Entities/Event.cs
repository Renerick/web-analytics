using System;
using System.Collections.Generic;

namespace WebAnalytics.Core.Entities
{
    public class Event
    {
        public long EventId { get; set; }

        public string SessionId { get; set; }

        public string SiteId { get; set; }

        public DateTimeOffset Time { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public string Visitor { get; set; }

        public Dictionary<string, object> Data { get; set; }
    }
}