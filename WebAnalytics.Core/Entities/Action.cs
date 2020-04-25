using System;
using System.Collections.Generic;

namespace WebAnalytics.Core.Entities
{
    public class Action
    {
        public long ActionId { get; set; }

        public Guid SessionId { get; set; }

        public Guid VisitorId { get; set; }

        public Guid SiteId { get; set; }

        public DateTimeOffset Time { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}