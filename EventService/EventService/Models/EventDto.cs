using System.Collections.Generic;

namespace EventService.Models
{
    public class EventDto
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
        public List<UserDto> UserList { get; set; }
        public List<UserDto> BusList { get; set; }
        public List<UserDto> VeganDietList { get; set; }
        public List<UserDto> DietList { get; set; }
        public int Day { get; set; }

    }
}