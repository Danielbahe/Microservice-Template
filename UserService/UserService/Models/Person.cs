﻿using System;

namespace UserService.Models
{
    public class Person
    {
        public int CollaId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime Birth { get; set; }
    }
}