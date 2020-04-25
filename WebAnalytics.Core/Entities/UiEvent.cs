using System;

namespace WebAnalytics.Core.Entities
{
    public class UiEvent
    {
        public string SiteId { get; set; }
        public string Group { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public string B { get; set; }
        public int C { get; set; }
    }
}