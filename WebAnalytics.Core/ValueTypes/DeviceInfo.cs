namespace WebAnalytics.Core.ValueTypes
{
    public class DeviceInfo
    {
        public string UserAgent { get; set; }
        public BrowserInfo Browser { get; set; }
        public OsInfo OperatingSystem { get; set; }
        public string DeviceType { get; set; }
    }

    public class BrowserInfo
    {
        public string Type { get; set; }

        public string Name { get; set; }
    }

    public class OsInfo
    {
        public string Type { get; set; }

        public string Name { get; set; }
    }
}