using System.Threading.Tasks;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Abstraction
{
    public interface IEventWriter
    {
        Task WriteSessionAsync(Session session);

        Task WriteActionAsync(Event @event);
    }
}