using System.Threading.Tasks;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Abstraction
{
    public interface IAnalyticsStore
    {
        Task<Session[]> GetSessionsAsync(string siteId);
        Task<Site> CreateSiteAsync(Site site);
        Task<Site[]> GetSitesAsync();
        Task<RecordingFragment[]> GetFragmentAsync(string siteId, string sessionId);
        Task<Page[]> GetPagesAsync(string siteId);
        Task<RecordingFragment[]> GetPageViews(string siteId, string pageUri);
    }
}