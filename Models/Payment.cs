namespace SmartBabySitter.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; }
        // Cash, bKash, Nagad, Card

        public string PaymentStatus { get; set; }
        // Pending, Paid, Failed

        public DateTime PaidAt { get; set; }
    }
}
