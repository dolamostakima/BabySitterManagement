namespace SmartBabySitter.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Receiver { get; set; }
        public string Message { get; set; }

        public string Type { get; set; } // SMS / Email
        public DateTime SentAt { get; set; }
    }
}
