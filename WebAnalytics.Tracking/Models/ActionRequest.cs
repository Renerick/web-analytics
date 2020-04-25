using System;
using System.Collections.Generic;

namespace WebAnalytics.Tracking.Models
{
    public class ActionRequest
    {
        public Guid VisitorId { get; set; }

        public Guid SiteId { get; set; }

        public DateTimeOffset Time { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}