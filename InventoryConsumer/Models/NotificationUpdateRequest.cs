namespace NotificationConsumer.Models
{
    public class NotificationUpdateRequest
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}