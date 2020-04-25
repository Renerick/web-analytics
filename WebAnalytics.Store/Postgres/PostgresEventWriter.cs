using System;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using WebAnalytics.Abstraction;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Store.Postgres
{
    public class PostgresEventWriter : IEventWriter
    {
        private readonly IDbConnection _connection;

        private const string InsertSessionQuery =
            "INSERT INTO session(session_id, visitor_id, site_id, start, device_info) VALUES (@session_id, @visitor_id, @site_id, @start, @device_info::jsonb)";

        private const string InsertActionQuery =
            "INSERT INTO action(session_id, visitor_id, site_id, time, type,  url, data) VALUES (@session_id, @visitor_id, @site_id, @time, @type, @url, @data::jsonb)";

        public PostgresEventWriter(IDbConnection connection)
        {
            _connection = connection;
        }

        public Task WriteSessionAsync(Session session) =>
            _connection.ExecuteAsync(InsertSessionQuery,
                                     new
                                     {
                                         session_id = session.SessionId,
                                         visitor_id = session.VisitorId,
                                         site_id = session.SiteId,
                                         start = session.Start,
                                         device_info = JsonSerializer.Serialize(session.DeviceInfo),
                                         referrer = JsonSerializer.Serialize(session.Referrer)
                                     });

        public async Task WriteActionAsync(Event @event)
        {
            var lastEventFromVisitor = await
                _connection.QueryFirstOrDefaultAsync<string>("SELECT session_id FROM action WHERE visitor_id = @visitor_id AND @time - time < interval '00:20:00' ORDER BY time DESC", new
                {
                    visitor_id = @event.Visitor,
                    time = @event.Time
                });

            @event.SessionId = lastEventFromVisitor ?? Guid.NewGuid().ToString();

            await _connection.ExecuteAsync(InsertActionQuery, new
            {
                session_id = @event.SessionId,
                visitor_id = @event.Visitor,
                site_id = @event.SiteId,
                time = @event.Time,
                type = @event.Type,
                url = @event.Url,
                data = (string) JsonSerializer.Serialize<dynamic>(@event.Data)
            });
        }
    }
}