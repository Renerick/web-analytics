using System.Threading.Tasks;
using WebAnalytics.Abstraction.Db;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Db.RabbitMqWriter
{
    public class RabbitMqWriter : IEventWriter
    {
        public void WriteSession(Session session) => throw new System.NotImplementedException();

        public Task WriteSessionAsync(Session session) => throw new System.NotImplementedException();

        public void WriteUiEvent(UiEvent @event) => throw new System.NotImplementedException();

        public Task WriteUiEventAsync(UiEvent @event) => throw new System.NotImplementedException();

        public void WriteError(Error error) => throw new System.NotImplementedException();

        public Task WriteErrorAsync(Error error) => throw new System.NotImplementedException();

        public void WriteAction(Action action) => throw new System.NotImplementedException();

        public Task WriteActionAsync(Action action) => throw new System.NotImplementedException();
    }
}