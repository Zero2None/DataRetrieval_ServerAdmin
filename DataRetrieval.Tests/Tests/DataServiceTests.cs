using DataRetrieval.Client.Services;
using FluentAssertions;
using Xunit;

namespace DataRetrieval.Tests
{
    public class DataServiceTests
    {
        private readonly DataService _dataService;

        public DataServiceTests()
        {
            _dataService = new DataService();
        }

        [Fact]
        public async Task GetScreenRecordingsAsync_ShouldReturnRecordings()
        {
            var result = await _dataService.GetScreenRecordingsAsync();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count().Should().Be(15);
        }

        [Fact]
        public async Task GetEnvironmentRecordingsAsync_ShouldReturnRecordings()
        {
            var result = await _dataService.GetEnvironmentRecordingsAsync();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count().Should().Be(12);
        }

        [Fact]
        public async Task GetUsbActivitiesAsync_ShouldReturnActivities()
        {
            var result = await _dataService.GetUsbActivitiesAsync();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count().Should().Be(20);
        }

        [Fact]
        public async Task GetScreenRecordingByIdAsync_WithValidId_ShouldReturnRecording()
        {
            var allRecordings = await _dataService.GetScreenRecordingsAsync();
            var firstRecording = allRecordings.First();

            var result = await _dataService.GetScreenRecordingByIdAsync(firstRecording.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(firstRecording.Id);
            result.FileName.Should().Be(firstRecording.FileName);
        }

        [Fact]
        public async Task GetScreenRecordingByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            var result = await _dataService.GetScreenRecordingByIdAsync("invalid_id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetEnvironmentRecordingByIdAsync_WithValidId_ShouldReturnRecording()
        {
            var allRecordings = await _dataService.GetEnvironmentRecordingsAsync();
            var firstRecording = allRecordings.First();

            var result = await _dataService.GetEnvironmentRecordingByIdAsync(firstRecording.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(firstRecording.Id);
            result.AudioFileName.Should().Be(firstRecording.AudioFileName);
        }

        [Fact]
        public async Task GetEnvironmentRecordingByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            var result = await _dataService.GetEnvironmentRecordingByIdAsync("invalid_id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetUsbActivityByIdAsync_WithValidId_ShouldReturnActivity()
        {
            var allActivities = await _dataService.GetUsbActivitiesAsync();
            var firstActivity = allActivities.First();

            var result = await _dataService.GetUsbActivityByIdAsync(firstActivity.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(firstActivity.Id);
            result.DeviceName.Should().Be(firstActivity.DeviceName);
        }

        [Fact]
        public async Task GetUsbActivityByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            var result = await _dataService.GetUsbActivityByIdAsync("invalid_id");

            result.Should().BeNull();
        }

        [Fact]
        public async Task ScreenRecordings_ShouldHaveValidData()
        {
            var recordings = await _dataService.GetScreenRecordingsAsync();

            foreach (var recording in recordings)
            {
                recording.Id.Should().NotBeNullOrEmpty();
                recording.FileName.Should().NotBeNullOrEmpty();
                recording.Resolution.Should().NotBeNullOrEmpty();
                recording.Status.Should().NotBeNullOrEmpty();
                recording.FileSizeBytes.Should().BeGreaterThan(0);
                recording.DurationSeconds.Should().BeGreaterThan(0);
                recording.Timestamp.Should().BeBefore(DateTime.UtcNow);
            }
        }

        [Fact]
        public async Task EnvironmentRecordings_ShouldHaveValidData()
        {
            var recordings = await _dataService.GetEnvironmentRecordingsAsync();

            foreach (var recording in recordings)
            {
                recording.Id.Should().NotBeNullOrEmpty();
                recording.AudioFileName.Should().NotBeNullOrEmpty();
                recording.AudioQuality.Should().NotBeNullOrEmpty();
                recording.DeviceName.Should().NotBeNullOrEmpty();
                recording.AudioFileSizeBytes.Should().BeGreaterThan(0);
                recording.DurationSeconds.Should().BeGreaterThan(0);
                recording.VolumeLevel.Should().BeInRange(0, 100);
                recording.Timestamp.Should().BeBefore(DateTime.UtcNow);
            }
        }

        [Fact]
        public async Task UsbActivities_ShouldHaveValidData()
        {
            var activities = await _dataService.GetUsbActivitiesAsync();

            foreach (var activity in activities)
            {
                activity.Id.Should().NotBeNullOrEmpty();
                activity.DeviceName.Should().NotBeNullOrEmpty();
                activity.DeviceId.Should().NotBeNullOrEmpty();
                activity.Action.Should().NotBeNullOrEmpty();
                activity.VendorId.Should().NotBeNullOrEmpty();
                activity.ProductId.Should().NotBeNullOrEmpty();
                activity.SerialNumber.Should().NotBeNullOrEmpty();
                activity.DataTransferredBytes.Should().BeGreaterThanOrEqualTo(0);
                activity.Timestamp.Should().BeBefore(DateTime.UtcNow);
            }
        }
    }
}