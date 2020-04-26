using System;
using WebAnalytics.Core.ValueTypes;

namespace WebAnalytics.Core.Entities
{
    public class Session
    {
        public string SessionId { get; set; }
        public string VisitorId { get; set; }
        public string SiteId { get; set; }
        public DateTimeOffset Start { get; set; }
        public DeviceInfo DeviceInfo { get; set; }
    }
}