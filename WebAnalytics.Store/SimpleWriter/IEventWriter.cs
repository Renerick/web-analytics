using System.Threading.Tasks;
using WebAnalytics.Abstraction.Db;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Db.SimpleWriter
{
    public class SimpleEventWriter : IEventWriter
    {
        private readonly IEventStore _eventStore;

        public SimpleEventWriter(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void WriteSession(Session session) => _eventStore.WriteSession(session);

        public Task WriteSessionAsync(Session session) => _eventStore.WriteSessionAsync(session);

        public void WriteUiEvent(UiEvent @event) => _eventStore.WriteUiEvent(@event);

        public Task WriteUiEventAsync(UiEvent @event) => _eventStore.WriteUiEventAsync(@event);

        public void WriteError(Error error) => _eventStore.WriteError(error);

        public Task WriteErrorAsync(Error error) => _eventStore.WriteErrorAsync(error);

        public void WriteAction(Action action) => _eventStore.WriteAction(action);

        public Task WriteActionAsync(Action action) => _eventStore.WriteActionAsync(action);
    }
}