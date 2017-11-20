using System;
using System.Collections.Generic;
using System.Linq;
using EventService.Database;
using EventService.Models;
using EventService.Service.Interfaces;
using RabbitCommunications.Models;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

//using System.Data.SqlClient;

namespace EventService.Service
{
    public class EventServiceRepository : IEventServiceRepository
    {
        private MySqlConnection Client;
        private EventContext db;
        public EventServiceRepository()
        {
            db = new EventContext();
            Client = new MySqlConnection("Server=localhost;Database=eventdbtest;Uid=admin;Pwd=admin;");
        }

        public Response<List<Event>> GetWeekEvents(DateTime date)
        {
            var response = new Response<List<Event>>();

            using (Client)
            {
                var list = new List<EventEntity>();
                Client.Open();
                MySqlCommand cmd = new MySqlCommand("select * from events e where e.Date >= "+ date + "AND e.Date <=" + date.AddDays(7), Client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new EventEntity()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            EndHour = Convert.ToString(reader["EndHour"]),
                            InitialHour = Convert.ToString(reader["InitialHour"]),
                            Date = Convert.ToDateTime(reader["InitialHour"]),
                            Description = Convert.ToString(reader["InitialHour"]),
                            Title = Convert.ToString(reader["InitialHour"]),
                            Address = Convert.ToString(reader["InitialHour"]),
                            Colla = Convert.ToInt32(reader["InitialHour"]),
                            EventPerson = Convert.ToInt32(reader["InitialHour"]),
                            EventType = Convert.ToInt32(reader["InitialHour"]),
                            Square = Convert.ToString(reader["InitialHour"]),
                        });
                    }
                }
                Client.Close();
            }
            return response;

            //var weekEvents = new List<Event>();
            //for (int i = 0; i < 7; i++)
            //{
            //    weekEvents.Add(CreateMockEvent());
            //}

            //var response = new Response<List<Event>>
            //{
            //    Data = weekEvents,
            //    Succes = true
            //};
            //return response;
        }

        public Response<Event> SaveEvent(Event Event)
        {
            throw new NotImplementedException();
        }

        public Response<Event> CreateEvent(Event Event)
        {
            var response = new Response<Event>();

            using (Client)
            {
                List<City> list = new List<City>();
                Client.Open();
                MySqlCommand cmd = new MySqlCommand("select * from city", Client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new City()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                        });
                    }
                }
                Client.Close();
            }
            return response;
        }

        public Response<Event> DeleteEvent(Event Event)
        {
            throw new System.NotImplementedException();
        }

        private Event CreateMockEvent()
        {
            return new Event
            {
                Colla = 1,
                Address = "https://www.google.es/maps/place/Pla%C3%A7a+dOctavi%C3%A0,+08172+Sant+Cugat+del+Vall%C3%A8s,+Barcelona/data=!4m2!3m1!1s0x12a496c2dcbe74a7:0x4c5ea5ef7e711774?sa=X&ved=0ahUKEwipqsLk2LHXAhUJvRQKHcxeB2kQ8gEIezAO",
                Date = new DateTime(),
                Title = "Assaig bla",
                Description = "bla bla bla",
                InitialHour = "20:00",
                EndHour = "22:00",
            };
        }
    }

    

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}