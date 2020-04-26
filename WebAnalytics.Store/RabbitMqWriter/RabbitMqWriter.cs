using System.Threading.Tasks;
using WebAnalytics.Abstraction;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Store.RabbitMqWriter
{
    public class RabbitMqWriter : IEventWriter
    {
        public void WriteSession(Session session) => throw new System.NotImplementedException();

        public Task WriteSessionAsync(Session session) => throw new System.NotImplementedException();

        public void WriteAction(Event @event) => throw new System.NotImplementedException();

        public Task WriteActionAsync(Event @event) => throw new System.NotImplementedException();
    }
}