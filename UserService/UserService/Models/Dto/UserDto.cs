using System.Collections.Generic;

namespace UserService.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int CollaId { get; set; }
        public List<Person> PersonList { get; set; }
    }
}