using System;

namespace UserService.Models
{
    public class Person : EntityBase
    {
        public int Colla { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Alias { get; set; }
        public int Height { get; set; }
        public decimal Weight { get; set; }
        public DateTime BirthDate { get; set; }
    }
}