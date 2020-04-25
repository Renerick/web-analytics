using System;
using WebAnalytics.Core.ValueTypes;

namespace WebAnalytics.Core.Entities
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public Guid VisitorId { get; set; }
        public Guid SiteId { get; set; }
        public DateTimeOffset Start { get; set; }
        public DeviceInfo DeviceInfo { get; set; }
        public Referrer Referrer { get; set; }
    }
}