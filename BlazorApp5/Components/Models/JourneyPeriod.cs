namespace BlazorApp5.Components.Models
{
    public class JourneyPeriod
    {
        public int Sequence { get; set; }
        public string? Name { get; set; }
        public string? ATD { get; set; }
        public string? ATA { get; set; }
        public TimeSpan? Duration { get; set; } 
    }

}
