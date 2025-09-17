namespace DataRetrieval.Client.Models
{
    public class ScreenRecording
    {
        public required  string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required  string FileName { get; set; }
        public long FileSizeBytes { get; set; }
        public int DurationSeconds { get; set; }
        public required  string Resolution { get; set; }
        public required  string Status { get; set; }
    }
}