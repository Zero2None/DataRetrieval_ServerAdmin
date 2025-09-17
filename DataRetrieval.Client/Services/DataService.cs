using DataRetrieval.Client.Models;

namespace DataRetrieval.Client.Services
{
    public class DataService : IDataService
    {
        private readonly List<ScreenRecording> _screenRecordings;
        private readonly List<EnvironmentRecording> _environmentRecordings;
        private readonly List<UsbActivity> _usbActivities;

        public DataService()
        {
            _screenRecordings = GenerateScreenRecordings();
            _environmentRecordings = GenerateEnvironmentRecordings();
            _usbActivities = GenerateUsbActivities();
        }

        public async Task<IEnumerable<ScreenRecording>> GetScreenRecordingsAsync()
        {
            await Task.Delay(50);
            return _screenRecordings;
        }

        public async Task<IEnumerable<EnvironmentRecording>> GetEnvironmentRecordingsAsync()
        {
            await Task.Delay(50);
            return _environmentRecordings;
        }

        public async Task<IEnumerable<UsbActivity>> GetUsbActivitiesAsync()
        {
            await Task.Delay(50);
            return _usbActivities;
        }

        public async Task<ScreenRecording?> GetScreenRecordingByIdAsync(string id)
        {
            await Task.Delay(25);
            return _screenRecordings.FirstOrDefault(x => x.Id == id);
        }

        public async Task<EnvironmentRecording?> GetEnvironmentRecordingByIdAsync(string id)
        {
            await Task.Delay(25);
            return _environmentRecordings.FirstOrDefault(x => x.Id == id);
        }

        public async Task<UsbActivity?> GetUsbActivityByIdAsync(string id)
        {
            await Task.Delay(25);
            return _usbActivities.FirstOrDefault(x => x.Id == id);
        }

        private List<ScreenRecording> GenerateScreenRecordings()
        {
            var recordings = new List<ScreenRecording>();
            var statuses = new[] { "Active", "Completed", "Processing", "Failed" };
            var resolutions = new[] { "1920x1080", "1366x768", "2560x1440", "3840x2160" };

            for (int i = 1; i <= 15; i++)
            {
                recordings.Add(new ScreenRecording
                {
                    Id = $"screen_{i:D3}",
                    Timestamp = DateTime.UtcNow.AddMinutes(-Random.Shared.Next(1, 1440)),
                    FileName = $"screen_recording_{i:D3}.mp4",
                    FileSizeBytes = Random.Shared.Next(50000000, 500000000),
                    DurationSeconds = Random.Shared.Next(30, 3600),
                    Resolution = resolutions[Random.Shared.Next(resolutions.Length)],
                    Status = statuses[Random.Shared.Next(statuses.Length)]
                });
            }
            return recordings;
        }

        private List<EnvironmentRecording> GenerateEnvironmentRecordings()
        {
            var recordings = new List<EnvironmentRecording>();
            var qualities = new[] { "High", "Medium", "Low" };
            var devices = new[] { "Internal Microphone", "External USB Mic", "Bluetooth Headset", "Line In" };

            for (int i = 1; i <= 12; i++)
            {
                recordings.Add(new EnvironmentRecording
                {
                    Id = $"env_{i:D3}",
                    Timestamp = DateTime.UtcNow.AddMinutes(-Random.Shared.Next(1, 2160)),
                    AudioFileName = $"environment_audio_{i:D3}.wav",
                    AudioFileSizeBytes = Random.Shared.Next(10000000, 100000000),
                    DurationSeconds = Random.Shared.Next(60, 7200),
                    AudioQuality = qualities[Random.Shared.Next(qualities.Length)],
                    VolumeLevel = Math.Round(Random.Shared.NextDouble() * 100, 2),
                    DeviceName = devices[Random.Shared.Next(devices.Length)]
                });
            }
            return recordings;
        }

        private List<UsbActivity> GenerateUsbActivities()
        {
            var activities = new List<UsbActivity>();
            var actions = new[] { "Connected", "Disconnected", "Data Transfer", "Error", "Ejected" };
            var devices = new[] { "USB Drive", "External HDD", "Mouse", "Keyboard", "Webcam", "Printer" };

            for (int i = 1; i <= 20; i++)
            {
                activities.Add(new UsbActivity
                {
                    Id = $"usb_{i:D3}",
                    Timestamp = DateTime.UtcNow.AddMinutes(-Random.Shared.Next(1, 4320)),
                    DeviceName = devices[Random.Shared.Next(devices.Length)],
                    DeviceId = $"USB\\VID_{Random.Shared.Next(1000, 9999)}&PID_{Random.Shared.Next(1000, 9999)}",
                    Action = actions[Random.Shared.Next(actions.Length)],
                    VendorId = $"{Random.Shared.Next(1000, 9999):X4}",
                    ProductId = $"{Random.Shared.Next(1000, 9999):X4}",
                    SerialNumber = Guid.NewGuid().ToString("N")[..12].ToUpper(),
                    DataTransferredBytes = Random.Shared.Next(0, 1000000000)
                });
            }
            return activities;
        }
    }
}