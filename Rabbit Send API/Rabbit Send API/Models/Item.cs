namespace Rabbit_Send_API.Models
{
    public class Item
    {
        public string? Message { get; set; }

        public DateTime Timestamp { get; set; }

        public int Priority { get; set; }
    }
}
