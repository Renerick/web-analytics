using System.Threading.Tasks;
using WebAnalytics.Core.Entities;

namespace WebAnalytics.Abstraction
{
    public interface IRecordingWriter
    {
        Task WriteFragmentAsync(RecordingFragment recordingFragment);
    }
}