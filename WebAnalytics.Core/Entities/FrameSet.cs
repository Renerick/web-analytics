using System.Collections.Generic;

namespace WebAnalytics.Core.Entities
{
    public class FrameSet
    {
        public int Fps { get; set; }

        public List<Frame> Frames { get; set; }
    }
}