using WebAnalytics.Core.ValueTypes;

namespace WebAnalytics.Web.Services
{
    public interface IDeviceService
    {
        DeviceInfo BuildFromEventData(string userAgent);
    }
}