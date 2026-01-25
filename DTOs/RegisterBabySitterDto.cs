namespace SmartBabySitter.DTOs
{
    public class RegisterBabySitterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Skills { get; set; }
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        public string Location { get; set; }
    }
}
