using System.Collections.Generic;

namespace EventService.Models
{
    public class User   
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public int Colla { get; set; }
        public List<Person> PersonList { get; set; }
        public int Status { get; set; }
    }

    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public int Colla { get; set; }
        public int State { get; set; }
    }
}