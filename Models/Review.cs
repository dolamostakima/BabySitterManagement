namespace SmartBabySitter.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int BabySitterId { get; set; }
        public BabySitter BabySitter { get; set; }

        public int Rating { get; set; } // 1–5
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
