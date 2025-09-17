namespace DataRetrieval.Client.Models
{
    public class EnvironmentRecording
    {
        public required string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required  string AudioFileName { get; set; }
        public long AudioFileSizeBytes { get; set; }
        public int DurationSeconds { get; set; }
        public required  string AudioQuality { get; set; }
        public double VolumeLevel { get; set; }
        public required  string DeviceName { get; set; }
    }
}