using System;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using WebAnalytics.Abstraction;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Store.Postgres
{
    public class PostgresStore : IEventWriter, IRecordingWriter, IAnalyticsStore
    {
        private readonly IDbConnection _connection;

        private const string InsertSessionQuery =
            "INSERT INTO session(session_id, visitor_id, site_id, start, device_info) VALUES (@session_id, @visitor_id, @site_id, @start, @device_info::jsonb)";

        private const string InsertActionQuery =
            "INSERT INTO action(session_id, visitor_id, site_id, time, type,  url, data) VALUES (@session_id, @visitor_id, @site_id, @time, @type, @url, @data::jsonb)";

        private const string InsertRecordingFragmentQuery =
            "INSERT INTO session_recording(session_id, time, url, recording_data) VALUES (@session_id, @time, @url, @recording_data::jsonb)";

        public PostgresStore(IDbConnection connection)
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
                });

        public async Task WriteActionAsync(Event @event)
        {
            var lastEventFromVisitor = await
                _connection.QueryFirstOrDefaultAsync<string>(
                    "SELECT session_id FROM action WHERE visitor_id = @visitor_id AND @time - time < interval '00:20:00' ORDER BY time DESC",
                    new {visitor_id = @event.Visitor, time = @event.Time});

            @event.SessionId = lastEventFromVisitor ?? Guid.NewGuid().ToString();

            await _connection.ExecuteAsync(InsertActionQuery,
                new
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

        public async Task WriteFragmentAsync(RecordingFragment recordingFragment)
        {
            var lastEventFromVisitor = await
                _connection.QueryFirstOrDefaultAsync<string>(
                    "SELECT session_id FROM action WHERE visitor_id = @visitor_id AND @time - time < interval '00:20:00' ORDER BY time DESC",
                    new {visitor_id = recordingFragment.Visitor, time = recordingFragment.Time});

            recordingFragment.SessionId = lastEventFromVisitor ?? Guid.NewGuid().ToString();

            await _connection.ExecuteAsync(InsertRecordingFragmentQuery,
                new
                {
                    session_id = recordingFragment.SessionId,
                    time = recordingFragment.Time,
                    url = recordingFragment.Url,
                    recording_data = (string) JsonSerializer.Serialize<FrameSet>(recordingFragment.Frames,
                        new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true
                        })
                });
        }

        public async Task<Session[]> GetSessionsAsync(string siteId)
        {
            return (await _connection.QueryAsync<Session>(
                "SELECT session.session_id, session.site_id, session.visitor_id, session.start  FROM session WHERE session.site_id = @site_id",
                new {site_id = siteId})).ToArray();
        }

        public async Task<Site> CreateSiteAsync(Site site)
        {
            await _connection.ExecuteAsync("INSERT INTO site(site_id, name, url) VALUES (@site_id, @name, @url)",
                new {site_id = site.SiteId, name = site.Name, url = site.Url});
            return site;
        }

        public async Task<Site[]> GetSitesAsync()
        {
            return (await _connection.QueryAsync<Site>(
                "SELECT site_id as SiteId , site.name, site.url  FROM public.site")).ToArray();
        }

        public async Task<RecordingFragment[]> GetFragmentAsync(string siteId, string sessionId)
        {
            return (await _connection.QueryAsync<dynamic>(
                    "SELECT session_recording.recording_id as FragmentId , session_recording.session_id as SessionId, session_recording.time, session_recording.url, session_recording.recording_data FROM public.session_recording WHERE session_recording.session_id = @session_id",
                    new {session_id = sessionId})
                ).Select(d => new RecordingFragment()
                {
                    FragmentId = d.recording_id,
                    Frames = JsonSerializer.Deserialize<FrameSet>(d.recording_data,
                        new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true
                        }),
                    Url = d.url,
                    Time = d.time
                }).ToArray();
        }
    }
}