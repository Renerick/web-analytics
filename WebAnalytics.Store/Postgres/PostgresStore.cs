using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using WebAnalytics.Abstraction;
using WebAnalytics.Core.Entities;
using WebAnalytics.Core.ValueTypes;

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
            "INSERT INTO session_recording(session_id, time, url, recording_data) VALUES (@session_id, @time, @url, @recording_data)";

        public PostgresStore(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task WriteActionAsync(Event @event, DeviceInfo deviceInfo)
        {
            @event.SessionId = await InitSession(@event.Visitor, @event.SiteId, deviceInfo);

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
            recordingFragment.SessionId = await InitSession(recordingFragment.Visitor, recordingFragment.Site, null);

            await _connection.ExecuteAsync(InsertRecordingFragmentQuery,
                new
                {
                    session_id = recordingFragment.SessionId,
                    time = recordingFragment.Time,
                    url = recordingFragment.Url,
                    recording_data = recordingFragment.Frames
                });
        }

        public async Task<Session[]> GetSessionsAsync(string siteId)
        {
            var lookup = new Dictionary<string, Session>();
            var result = new List<Session>();
            await _connection.QueryAsync<Session, RecordingFragment, Session>(
                "SELECT * FROM (SELECT session.session_id, session.site_id, session.visitor_id, session.start, session.device_info, session_recording.recording_id as FragmentId , session_recording.session_id as SessionId, session_recording.time, session_recording.url, session_recording.recording_data as Frames FROM public.session JOIN session_recording  ON public.session_recording.session_id = public.session.session_id  WHERE session.site_id = @site_id) AS session_info ORDER BY session_info.time",
                (s, a) =>
                {
                    if (!lookup.TryGetValue(s.SessionId, out var session))
                    {
                        lookup.Add(s.SessionId, session = s);
                        result.Add(session);
                    }

                    session.RecordingFragments.Add(a);
                    return s;
                }, new {site_id = siteId}, splitOn: "FragmentId");
            return result.ToArray();
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
            return (await _connection.QueryAsync<RecordingFragment>(
                    "SELECT session_recording.recording_id as FragmentId , session_recording.session_id as SessionId, session_recording.time, session_recording.url, session_recording.recording_data as Frames FROM public.session_recording WHERE session_recording.session_id = @session_id",
                    new {session_id = sessionId})
                ).ToArray();
        }

        public async Task<Page[]> GetPagesAsync(string siteId)
        {
            return (await _connection.QueryAsync<Page>(
                    "SELECT count(session_recording.session_id) as visits, session_recording.url FROM public.session_recording JOIN public.session ON public.session_recording.session_id = public.session.session_id WHERE public.session.site_id = @site_id GROUP BY session_recording.url ",
                    new {site_id = siteId})
                ).ToArray();
        }

        public async Task<RecordingFragment[]> GetPageViews(string siteId, string pageUri)
        {
            return (await _connection.QueryAsync<RecordingFragment>(
                    "SELECT session_recording.recording_id as FragmentId , session_recording.session_id as SessionId, session_recording.time, session_recording.url, session_recording.recording_data as Frames FROM public.session_recording WHERE public.session_recording.url = @url",
                    new {url = pageUri})
                ).ToArray();
        }

        private async Task<string> InitSession(string visitorId, string siteId, DeviceInfo deviceInfo)
        {
            var sessionId = await
                _connection.QueryFirstOrDefaultAsync<string>(
                    "SELECT session_id FROM session WHERE visitor_id = @visitor_id AND @time - start < interval '00:20:00' ORDER BY start DESC",
                    new {visitor_id = visitorId, time = DateTimeOffset.Now});
            if (sessionId != null) return sessionId;

            sessionId = Guid.NewGuid().ToString();

            await _connection.ExecuteAsync(InsertSessionQuery,
                new
                {
                    session_id = sessionId,
                    visitor_id = visitorId,
                    site_id = siteId,
                    start = DateTimeOffset.Now,
                    device_info = deviceInfo
                });

            return sessionId;
        }
    }
}