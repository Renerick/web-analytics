namespace WebAnalytics.Web.Models
{
    public class EventRequest
    {
        /// <summary>
        /// Site
        /// </summary>
        public string S { get; set; }

        /// <summary>
        /// Visitor
        /// </summary>
        public string V { get; set; }

        /// <summary>
        /// Group
        /// </summary>
        public string G { get; set; }

        /// <summary>
        /// User agent
        /// </summary>
        public string U { get; set; }

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