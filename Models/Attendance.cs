namespace SmartBabySitter.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        public string Location { get; set; }
    }
}
