using DataRetrieval.Client.Models;

namespace DataRetrieval.Client.Services
{
    public interface IDataService
    {
        Task<IEnumerable<ScreenRecording>> GetScreenRecordingsAsync();
        Task<IEnumerable<EnvironmentRecording>> GetEnvironmentRecordingsAsync();
        Task<IEnumerable<UsbActivity>> GetUsbActivitiesAsync();
        Task<ScreenRecording?> GetScreenRecordingByIdAsync(string id);
        Task<EnvironmentRecording?> GetEnvironmentRecordingByIdAsync(string id);
        Task<UsbActivity?> GetUsbActivityByIdAsync(string id);
    }
}