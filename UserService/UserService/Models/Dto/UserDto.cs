using System.Collections.Generic;

namespace UserService.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public int Colla { get; set; }
        public List<Person> PersonList { get; set; }
        public int State { get; set; }
    }
}