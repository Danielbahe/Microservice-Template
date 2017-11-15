using System;

namespace EventService.Models
{
    public class Person
    {
        public int Id { get; set; }
        public int CollaId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Alias { get; set; }
        public DateTime Birth { get; set; }
    }
}