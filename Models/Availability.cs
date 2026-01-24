namespace SmartBabySitter.Models
{
    public class Availability
    {
        public int Id { get; set; }

        public int BabySitterId { get; set; }
        public BabySitter BabySitter { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
