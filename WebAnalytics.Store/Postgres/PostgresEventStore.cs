using System;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using WebAnalytics.Abstraction.Db;
using WebAnalytics.Core.Entities;
using Action = WebAnalytics.Core.Entities.Action;

namespace WebAnalytics.Db.Postgres
{
    public class PostgresEventStore : IEventStore
    {
        private readonly IDbConnection _connection;
        private readonly ILogger<PostgresEventStore> _logger;

        private const string InsertSessionQuery =
            "INSERT INTO session(session_id, visitor_id, site_id, start, device_info) VALUES (@session_id, @visitor_id, @site_id, @start, @device_info::jsonb)";

        private const string InsertActionQuery =
            "INSERT INTO action(session_id, visitor_id, site_id, time, type, name, url, data) VALUES (@session_id, @visitor_id, @site_id, @time, @type, @name, @url, @data::jsonb)";

        public PostgresEventStore(IDbConnection connection, ILogger<PostgresEventStore> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public void WriteSession(Session session) =>
            _connection.Execute(InsertSessionQuery,
                                new
                                {
                                    session_id = session.SessionId,
                                    visitor_id = session.VisitorId,
                                    site_id = session.SiteId,
                                    start = session.Start,
                                    device_info = JsonSerializer.Serialize(session.DeviceInfo),
                                    referrer = JsonSerializer.Serialize(session.Referrer)
                                });

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

        public void WriteUiEvent(UiEvent @event) => throw new NotImplementedException();
        public Task WriteUiEventAsync(UiEvent @event) => throw new NotImplementedException();

        public void WriteError(Error error) => throw new NotImplementedException();

        public Task WriteErrorAsync(Error error) => throw new NotImplementedException();

        public void WriteAction(Action action) =>
            _connection.Execute(InsertActionQuery, new
            {
                session_id = action.SessionId,
                visitor_id = action.VisitorId,
                site_id = action.SiteId,
                time = action.Time,
                type = action.Type,
                name = action.Name,
                url = action.Url,
                data = (string) JsonSerializer.Serialize<dynamic>(action.Data)
            });

        public async Task WriteActionAsync(Action action)
        {
            var lastEventFromVisitor = await
                _connection.QueryFirstOrDefaultAsync<Guid?>("SELECT session_id FROM action WHERE visitor_id = @visitor_id AND @time - time < interval '00:20:00' ORDER BY time DESC", new
                {
                    visitor_id = action.VisitorId,
                    time = action.Time
                });

            action.SessionId = lastEventFromVisitor ?? Guid.NewGuid();

            await _connection.ExecuteAsync(InsertActionQuery, new
            {
                session_id = action.SessionId,
                visitor_id = action.VisitorId,
                site_id = action.SiteId,
                time = action.Time,
                type = action.Type,
                name = action.Name,
                url = action.Url,
                data = (string) JsonSerializer.Serialize<dynamic>(action.Data)
            });
        }
    }
}