using System;
using System.Collections.Generic;
using System.Data;
using EventService.Models;
using EventService.Service.Interfaces;
using RabbitCommunications.Models;
using MySql.Data.MySqlClient;

namespace EventService.Service
{
    public class EventServiceRepository : IEventServiceRepository
    {
        private MySqlConnection Client;

        private const string ConnectionStrings =
            "Server=localhost;Database=eventdbtest;Uid=admin;Pwd=admin;Allow User Variables=True;";

        public Response<List<Event>> GetWeekEvents(DateTime date)
        {
            var response = new Response<List<Event>>();
            var list = new List<Event>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd =
                    new MySqlCommand(
                        "select * from events e where e.Date = '" + date + "' AND e.Date '<= " + date.AddDays(7) + "'",
                        Client);
                //MySqlCommand cmd = new MySqlCommand("select * from events", Client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapEvent(reader));
                    }
                }
                Client.Close();
            }
            response.Data = list;
            response.Succes = true;

            return response;
        }

        public Response<Event> SaveEvent(Event Event)
        {
            var response = new Response<Event>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();

                var query =
                    "UPDATE events SET Colla = @Colla ,Title = @Title, Date = @Date, InitialHour = @InitialHour,EndHour = @EndHour, Square = @Square, Address = @Address, Description = @Description, Type = @Type where Id = "+ Event.Id;
                MySqlCommand command = new MySqlCommand(query, Client);
                command.Parameters.AddWithValue("@Colla", Event.Colla);
                command.Parameters.AddWithValue("@Title", Event.Title);
                command.Parameters.AddWithValue("@Date", Event.Date);
                command.Parameters.AddWithValue("@InitialHour", Event.InitialHour);
                command.Parameters.AddWithValue("@EndHour", Event.EndHour);
                command.Parameters.AddWithValue("@Square", Event.Square);
                command.Parameters.AddWithValue("@Address", Event.Address);
                command.Parameters.AddWithValue("@Description", Event.Description);
                command.Parameters.AddWithValue("@Type", Event.EventType);

                using (var reader = command.ExecuteReader())
                {
                }
                MySqlCommand cmd =
                    new MySqlCommand(
                        "select * from events e where e.Id = " + Event.Id,
                        Client);
                //MySqlCommand cmd = new MySqlCommand("select * from events", Client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        response.Data = MapEvent(reader);
                    }
                }
                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<Event> CreateEvent(Event Event)
        {
            var response = new Response<Event>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();

                var query =
                    "INSERT INTO events VALUES (NULL,@Colla , @Title, @Date, @InitialHour, @EndHour, @Square, @Address, @Description, @Type)";
                MySqlCommand command = new MySqlCommand(query, Client);
                command.Parameters.AddWithValue("@Colla", Event.Colla).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Title", Event.Title).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Date", Event.Date).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@InitialHour", Event.InitialHour).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@EndHour", Event.EndHour).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Square", Event.Square).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Address", Event.Address).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Description", Event.Description).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Type", Event.EventType).Direction = ParameterDirection.Input;

                using (var reader = command.ExecuteReader())
                {
                }
                response.Data = GetLastInserted();

                Client.Close();
            }
            return response;
        }

        public Response<Event> DeleteEvent(Event Event)
        {
            var response = new Response<Event>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd =
                    new MySqlCommand(
                        "delete from events e where e.Id = " + Event.Id,
                        Client);
                //MySqlCommand cmd = new MySqlCommand("select * from events", Client);
                using (var reader = cmd.ExecuteReader())
                {

                }


                Client.Close();
            }
            response.Succes = true;
            response.Data = new Event();
            return response;
        }

        private Event MapEvent(MySqlDataReader reader)
        {
            var eve = new Event()
            {
                Id = Convert.ToInt32(reader["Id"]),
                InitialHour = Convert.ToString(reader["InitialHour"]),
                Date = Convert.ToDateTime(reader["Date"]),
                Title = Convert.ToString(reader["Title"]),
                Colla = Convert.ToInt32(reader["Colla"]),
                Square = Convert.ToString(reader["Square"]),
            };

            var type = reader["Type"];
            if (type.GetType() != typeof(DBNull)) eve.EventType = Convert.ToInt32(reader["Type"]);
            var address = reader["Type"];
            if (address.GetType() != typeof(DBNull)) eve.Address = Convert.ToString(reader["Address"]);
            var endHour = reader["Type"];
            if (endHour.GetType() != typeof(DBNull)) eve.EndHour = Convert.ToString(reader["EndHour"]);
            var description = reader["Type"];
            if (description.GetType() != typeof(DBNull)) eve.Description = Convert.ToString(reader["Description"]);

            return eve;
        }

        private Event GetLastInserted()
        {
            var query = "select last_insert_id()";
            var command = new MySqlCommand(query, Client);

            int id = 0;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["last_insert_id()"]);
                }
            }

            var getByIdQuery = "select * from events where Id = " + id;
            var getByIdCmd = new MySqlCommand(getByIdQuery, Client);
            Event eve = new Event();
            using (var reader = getByIdCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    eve = MapEvent(reader);
                }
            }
            return eve;
        }

        private Event CreateMockEvent()
        {
            return new Event
            {
                Colla = 1,
                Address =
                    "https://www.google.es/maps/place/Pla%C3%A7a+dOctavi%C3%A0,+08172+Sant+Cugat+del+Vall%C3%A8s,+Barcelona/data=!4m2!3m1!1s0x12a496c2dcbe74a7:0x4c5ea5ef7e711774?sa=X&ved=0ahUKEwipqsLk2LHXAhUJvRQKHcxeB2kQ8gEIezAO",
                Date = new DateTime(),
                Title = "Assaig bla",
                Description = "bla bla bla",
                InitialHour = "20:00",
                EndHour = "22:00",
            };
        }
    }
}