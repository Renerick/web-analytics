namespace WebAnalytics.Core.Entities
{
    public class Frame
    {
        public string Target { get; set; }
        public int? Width { get; set; }

        public int? ClickX { get; set; }
        public int? ClickY { get; set; }

        public int? ContextMenuX { get; set; }
        public int? ContextMenuY { get; set; }

        public int? MouseX { get; set; }
        public int? MouseY { get; set; }

        public int? ScrollX { get; set; }
        public int? ScrollY { get; set; }
    }
}