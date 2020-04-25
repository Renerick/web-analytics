using System.Collections.Generic;

namespace WebAnalytics.Core.ValueTypes
{
    public class Referrer
    {
        public string ReferrerHeader { get; set; }
        public Dictionary<string,string> Utm { get; set; }
    }
}