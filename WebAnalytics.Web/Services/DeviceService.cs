using System.Linq;
using DeviceDetectorNET;
using DeviceDetectorNET.Results.Client;
using WebAnalytics.Core.ValueTypes;

namespace WebAnalytics.Web.Services
{
    public class DeviceService : IDeviceService
    {
        public DeviceInfo BuildFromEventData(string userAgent)
        {
            var dd = new DeviceDetector(userAgent);
            dd.Parse();
            var client = (BrowserMatchResult) dd.GetClient().Matches.First(r => r is BrowserMatchResult);
            var os = dd.GetOs();
            return new DeviceInfo
            {
                Browser = new BrowserInfo()
                {
                    Name = client.Name + " " + client.Version,
                    Type = client.ShortName
                },
                DeviceType = dd.GetDeviceName(),
                OperatingSystem = new OsInfo()
                {
                    Name = os.Match.Platform + " " + os.Match.Name + " " + os.Match.Version,
                    Type = os.Match.ShortName
                },
                UserAgent = userAgent
            };
        }
    }
}