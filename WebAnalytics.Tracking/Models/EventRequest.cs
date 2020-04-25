namespace WebAnalytics.Tracking.Models
{
    public class EventRequest
    {
        /// <summary>
        /// Site
        /// </summary>
        public string S { get; set; }

        /// <summary>
        /// Group
        /// </summary>
        public string G { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Windows width
        /// </summary>
        public int W { get; set; }

        /// <summary>
        /// Visitor
        /// </summary>
        public string V { get; set; }

        /// <summary>
        /// Browser
        /// </summary>
        public string B { get; set; }

        /// <summary>
        /// Event type
        /// </summary>
        public string T { get; set; }

        /// <summary>
        /// Event info
        /// </summary>
        public string C { get; set; }
    }
}