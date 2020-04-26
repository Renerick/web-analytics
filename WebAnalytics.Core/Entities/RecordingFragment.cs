using System;

namespace WebAnalytics.Core.Entities
{
    public class RecordingFragment
    {
        public string FragmentId { get; set; }
        public string SessionId { get; set; }
        public DateTimeOffset Time { get; set; }

        public string Url { get; set; }
        public FrameSet Frames { get; set; }
        public string Visitor { get; set; }
        public string Site { get; set; }
    }
}