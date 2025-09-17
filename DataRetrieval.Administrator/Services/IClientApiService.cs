using DataRetrieval.Administrator.Models;

namespace DataRetrieval.Administrator.Services
{
    public interface IClientApiService
    {
        Task<IEnumerable<ScreenRecording>> GetScreenRecordingsAsync();
        Task<IEnumerable<EnvironmentRecording>> GetEnvironmentRecordingsAsync();
        Task<IEnumerable<UsbActivity>> GetUsbActivitiesAsync();
        Task<ScreenRecording?> GetScreenRecordingByIdAsync(string id);
        Task<EnvironmentRecording?> GetEnvironmentRecordingByIdAsync(string id);
        Task<UsbActivity?> GetUsbActivityByIdAsync(string id);
    }
}