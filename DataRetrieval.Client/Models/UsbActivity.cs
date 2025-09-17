namespace DataRetrieval.Client.Models
{
    public class UsbActivity
    {
        public required  string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public required  string DeviceName { get; set; }
        public required  string DeviceId { get; set; }
        public required  string Action { get; set; }
        public required  string VendorId { get; set; }
        public required  string ProductId { get; set; }
        public required  string SerialNumber { get; set; }
        public long DataTransferredBytes { get; set; }
    }
}