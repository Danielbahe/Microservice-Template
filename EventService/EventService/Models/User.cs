using System.Collections.Generic;

namespace EventService.Models
{
    public class User   
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int CollaId { get; set; }
        public List<Person> PersonList { get; set; }
    }
}