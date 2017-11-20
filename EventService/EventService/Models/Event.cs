using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventService.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int Colla { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string InitialHour { get; set; }
        public string EndHour { get; set; }
        public string Square { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int EventType { get; set; }
        public List<Person> PersonList { get; set; }
        public List<Person> BusList { get; set; }
        public List<Person> VeganDietList { get; set; }
        public List<Person> DietList { get; set; }
        //public List<string> Pinyes { get; set; }
    }

    public class EventEntity
    {
        public int Id { get; set; }
        public int Colla { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string InitialHour { get; set; }
        public string EndHour { get; set; }
        public string Square { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int EventType { get; set; }
        public int EventPerson { get; set; }
        //public List<Person> PersonList { get; set; }
        //public List<Person> BusList { get; set; }
        //public List<Person> VeganDietList { get; set; }
        //public List<Person> DietList { get; set; }
        //public int Pinyes { get; set; }
    }

    public class EventPerson
    {
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public bool Performance { get; set; }
        public bool Bus { get; set; }
        public bool Diet { get; set; }
        public bool Vegan { get; set; }
    }

}