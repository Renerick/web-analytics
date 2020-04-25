using System.Threading.Tasks;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Abstraction.Db
{
    public interface IEventStore
    {
        void WriteSession(Session session);

        Task WriteSessionAsync(Session session);

        void WriteUiEvent(UiEvent @event);

        Task WriteUiEventAsync(UiEvent @event);

        void WriteError(Error error);

        Task WriteErrorAsync(Error error);

        void WriteAction(Action action);

        Task WriteActionAsync(Action action);
    }
}