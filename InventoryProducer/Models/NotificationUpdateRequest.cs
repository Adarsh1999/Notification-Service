namespace NotificationProducer.Models
{
    public class NotificationUpdateRequest
	{
		public int Id { get; set; }
		public string DeviceId { get; set; }
		public string WarningLevel { get; set; }
		public string NotificationMessage { get; set; }
	}
}