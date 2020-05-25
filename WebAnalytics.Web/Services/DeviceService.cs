using System.Linq;
using DeviceDetectorNET;
using DeviceDetectorNET.Parser;
using DeviceDetectorNET.Parser.Client;
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
            if (!BrowserParser.GetBrowserFamily(client.ShortName, out var browserFamily))
            {
                browserFamily = client.ShortName;
            }

            if (!OperatingSystemParser.GetOsFamily(os.Match.ShortName, out var osFamily) || osFamily == "GNU/Linux" )
            {
                osFamily = os.Match.ShortName;
            }
            return new DeviceInfo
            {
                Browser = new BrowserInfo()
                {
                    Name = client.Name + " " + client.Version,
                    Type = browserFamily
                },
                DeviceType = dd.GetDeviceName(),
                OperatingSystem = new OsInfo()
                {
                    Name = os.Match.Platform + " " + os.Match.Name + " " + os.Match.Version,
                    Type = osFamily
                },
                UserAgent = userAgent
            };
        }
    }
}