namespace EventService.Models
{
    public class EventPerson
    {
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public bool Performance { get; set; }
        public bool Bus { get; set; }
        public bool Diet { get; set; }
        public bool Vegan { get; set; }
    }
}