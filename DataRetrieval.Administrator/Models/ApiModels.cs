namespace DataRetrieval.Administrator.Models
{
    public class ScreenRecording
    {
        public required string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required string FileName { get; set; }
        public long FileSizeBytes { get; set; }
        public int DurationSeconds { get; set; }
        public required string Resolution { get; set; }
        public required string Status { get; set; }
    }

    public class EnvironmentRecording
    {
        public required string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required string AudioFileName { get; set; }
        public long AudioFileSizeBytes { get; set; }
        public int DurationSeconds { get; set; }
        public required string AudioQuality { get; set; }
        public double VolumeLevel { get; set; }
        public required string DeviceName { get; set; }
    }

    public class UsbActivity
    {
        public required string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required string DeviceName { get; set; }
        public required string DeviceId { get; set; }
        public required string Action { get; set; }
        public required string VendorId { get; set; }
        public required string ProductId { get; set; }
        public required string SerialNumber { get; set; }
        public long DataTransferredBytes { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}