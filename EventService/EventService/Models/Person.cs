using System;

namespace EventService.Models
{
    public class Person
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
    public class PersonEntity
    {
        public int Colla { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Alias { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public DateTime BirthDate { get; set; }
        public int UserId { get; set; }
    }
}