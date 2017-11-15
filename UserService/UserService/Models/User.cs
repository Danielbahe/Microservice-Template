using System.Collections.Generic;

namespace UserService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int CollaId { get; set; }
        public List<Person> PersonList { get; set; }
    }
}