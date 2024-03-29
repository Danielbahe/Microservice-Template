﻿using System;

namespace EventService.Models
{
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
}