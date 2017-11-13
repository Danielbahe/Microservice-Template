using System.Collections.Generic;

namespace EventService.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int CollaId { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string InitialHour { get; set; }
        public string EndHour { get; set; }
        public string Square { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public List<User> UserList { get; set; }
        public List<User> BusList { get; set; }
        public List<User> VeganDietList { get; set; }
        public List<User> DietList { get; set; }
        public int Day { get; set; }
    }
}