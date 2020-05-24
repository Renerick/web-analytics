using System.Threading.Tasks;
using WebAnalytics.Core.Entities;
using WebAnalytics.Core.ValueTypes;

namespace WebAnalytics.Abstraction
{
    public interface IEventWriter
    {
        Task WriteActionAsync(Event @event, DeviceInfo deviceInfo);
    }
}