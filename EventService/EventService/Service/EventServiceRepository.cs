using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                using (var reader = cmd.ExecuteReader())
                {

                }


                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<Event> AsistToEvent(Event Event)
        {
            var response = new Response<Event>();

            foreach (var person in Event.PersonList)
            {
                using (Client = new MySqlConnection(ConnectionStrings))
                {
                    Client.Open();

                    MySqlCommand command;
                    if (ExistEvent(Client, Event, person))
                    {
                        var query =
                            "UPDATE eventperson SET Performance = @Performance where EventId = " + Event.Id + " AND PersonId = " + person.Id;
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@Performance", 1).Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        var query = "INSERT INTO eventperson VALUES (@EventId , @PersonId, @Performance)";
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@EventId", Event.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@PersonId", person.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@Performance", 1).Direction = ParameterDirection.Input;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                    }

                    Client.Close();
                }
            }
            response.Succes = true;

            return response;
        }

        public Response<Event> CancelAsistToEvent(Event Event)
        {
            var response = new Response<Event>();

            foreach (var person in Event.PersonList)
            {
                using (Client = new MySqlConnection(ConnectionStrings))
                {
                    Client.Open();

                    MySqlCommand command;
                    if (ExistEvent(Client, Event, person))
                    {
                        var query =
                            "UPDATE eventperson SET Performance = @Performance where EventId = " + Event.Id + " AND PersonId = " + person.Id;
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@Performance", 1).Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        var query = "INSERT INTO eventperson VALUES (@EventId , @PersonId, @Performance)";
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@EventId", Event.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@PersonId", person.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@Performance", 1).Direction = ParameterDirection.Input;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                    }

                    Client.Close();
                }
            }
            response.Succes = true;

            return response;
        }

        public Response<Event> AsistBusToEvent(Event Event)
        {
            var response = new Response<Event>();

            foreach (var person in Event.BusList)
            {
                using (Client = new MySqlConnection(ConnectionStrings))
                {
                    Client.Open();

                    MySqlCommand command;
                    if (ExistEvent(Client, Event, person))
                    {
                        var query =
                            "UPDATE eventperson SET EventId = @EventId, PersonId = @PersonId, Bus = @Bus where EventId = " + Event.Id + " AND PersonId = " + person.Id;
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@Bus", 1).Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        var query = "INSERT INTO eventperson VALUES (@EventId , @PersonId, @Bus)";
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@EventId", Event.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@PersonId", person.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@Bus", 1).Direction = ParameterDirection.Input;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                    }

                    Client.Close();
                }
            }
            response.Succes = true;

            return response;
        }

        public Response<Event> CancelAsistBusToEvent(Event Event)
        {
            var response = new Response<Event>();

            foreach (var person in Event.BusList)
            {
                using (Client = new MySqlConnection(ConnectionStrings))
                {
                    Client.Open();

                    MySqlCommand command;
                    if (ExistEvent(Client, Event, person))
                    {
                        var query =
                            "UPDATE eventperson SET EventId = @EventId, PersonId = @PersonId, Bus = @Bus where EventId = " + Event.Id + " AND PersonId = " + person.Id;
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@Bus", 1).Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        var query = "INSERT INTO eventperson VALUES (@EventId , @PersonId, @Bus)";
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@EventId", Event.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@PersonId", person.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@Bus", 1).Direction = ParameterDirection.Input;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                    }

                    Client.Close();
                }
            }
            response.Succes = true;

            return response;
        }

        public Response<Event> AsistDietToEvent(Event Event)
        {
            var response = new Response<Event>();

            foreach (var person in Event.DietList)
            {
                using (Client = new MySqlConnection(ConnectionStrings))
                {
                    Client.Open();

                    MySqlCommand command;
                    if (ExistEvent(Client, Event, person))
                    {
                        var query =
                            "UPDATE eventperson SET EventId = @EventId, PersonId = @PersonId, Diet = @Diet where EventId = " + Event.Id + " AND PersonId = " + person.Id;
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@Diet", 1).Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        var query = "INSERT INTO eventperson VALUES (@EventId , @PersonId, @Diet)";
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@EventId", Event.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@PersonId", person.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@Diet", 1).Direction = ParameterDirection.Input;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                    }

                    Client.Close();
                }
            }
            response.Succes = true;

            return response;
        }

        public Response<Event> CancelAsistDietToEvent(Event Event)
        {
            var response = new Response<Event>();

            foreach (var person in Event.DietList)
            {
                using (Client = new MySqlConnection(ConnectionStrings))
                {
                    Client.Open();

                    MySqlCommand command;
                    if (ExistEvent(Client, Event, person))
                    {
                        var query =
                            "UPDATE eventperson SET EventId = @EventId, PersonId = @PersonId, Diet = @Diet where EventId = " + Event.Id + " AND PersonId = " + person.Id;
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@Diet", 1).Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        var query = "INSERT INTO eventperson VALUES (@EventId , @PersonId, @Diet)";
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@EventId", Event.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@PersonId", person.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@Diet", 1).Direction = ParameterDirection.Input;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                    }

                    Client.Close();
                }
            }
            response.Succes = true;

            return response;
        }

        public Response<Event> AsistVeganToEvent(Event Event)
        {
            var response = new Response<Event>();

            foreach (var person in Event.VeganDietList)
            {
                using (Client = new MySqlConnection(ConnectionStrings))
                {
                    Client.Open();

                    MySqlCommand command;
                    if (ExistEvent(Client, Event, person))
                    {
                        var query =
                            "UPDATE eventperson SET EventId = @EventId, PersonId = @PersonId, Vegan = @Vegan where EventId = " + Event.Id + " AND PersonId = " + person.Id;
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@Vegan", 1).Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        var query = "INSERT INTO eventperson VALUES (@EventId , @PersonId, @Vegan)";
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@EventId", Event.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@PersonId", person.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@Vegan", 1).Direction = ParameterDirection.Input;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                    }

                    Client.Close();
                }
            }
            response.Succes = true;

            return response;
        }

        public Response<Event> CancelAsistVeganToEvent(Event Event)
        {
            var response = new Response<Event>();

            foreach (var person in Event.VeganDietList)
            {
                using (Client = new MySqlConnection(ConnectionStrings))
                {
                    Client.Open();

                    MySqlCommand command;
                    if (ExistEvent(Client, Event, person))
                    {
                        var query =
                            "UPDATE eventperson SET EventId = @EventId, PersonId = @PersonId, Vegan = @Vegan where EventId = " + Event.Id + " AND PersonId = " + person.Id;
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@Vegan", 0).Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        var query = "INSERT INTO eventperson VALUES (@EventId , @PersonId, @Vegan)";
                        command = new MySqlCommand(query, Client);
                        command.Parameters.AddWithValue("@EventId", Event.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@PersonId", person.Id).Direction = ParameterDirection.Input;
                        command.Parameters.AddWithValue("@Vegan", 0).Direction = ParameterDirection.Input;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                    }

                    Client.Close();
                }
            }
            response.Succes = true;

            return response;
        }

        public Response<Event> GetEventAssistance(Event eve)
        {
            var response = new Response<Event>();

            MySqlCommand cmd = new MySqlCommand(
                "SELECT * FROM eventdbtest.persons p JOIN eventdbtest.eventperson e on p.Id = e.PersonId " + eve.Id,
                Client);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var per = MapPerson(reader);
                    if (Convert.ToInt32(reader["Performance"]) == 1)
                    {
                        eve.PersonList.Add(per);
                    }

                    if (Convert.ToInt32(reader["Bus"]) == 1)
                    {
                        eve.BusList.Add(per);
                    }

                    if (Convert.ToInt32(reader["Diet"]) == 1)
                    {
                        eve.DietList.Add(per);
                    }

                    if (Convert.ToInt32(reader["Vegan"]) == 1)
                    {
                        eve.VeganDietList.Add(per);
                    }
                }
            }
            Client.Close();

            response.Data = eve;
            response.Succes = true;
            return response;
        }
            
        public Response<Event> InsertPersonsDatabase(Person person)
        {
            var response = new Response<Event>();

            var query =
                "INSERT INTO persons VALUES (@Id , @Alias, @BirthDate, @Colla, @Height, @Name, @SurName, @Weight)";
            MySqlCommand command = new MySqlCommand(query, Client);
            command.Parameters.AddWithValue("@Id", person.Id).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Alias", person.Alias).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@BirthDate", person.BirthDate).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Colla", person.Colla).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Height", person.Height).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Name", person.Name).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@SurName", person.SurName).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Weight", person.Weight).Direction = ParameterDirection.Input;

            using (var reader = command.ExecuteReader())
            {
            }
            Client.Close();

            response.Data = new Event();
            response.Succes = true;
            return response;
        }

        public Response<Event> UpdatePersonsDatabase(Person person)
        {
            var response = new Response<Event>();

            var query =
                "UPDATE events SET Id = @Id ,Alias = @Alias, BirthDate = @BirthDate, Colla = @Colla, Height = @Height, Name = @Name, SurName = @SurName, Weight = @Weight where Id = " + person.Id;
            MySqlCommand command = new MySqlCommand(query, Client);
            command.Parameters.AddWithValue("@Id", person.Id).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Alias", person.Alias).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@BirthDate", person.BirthDate).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Colla", person.Colla).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Height", person.Height).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Name", person.Name).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@SurName", person.SurName).Direction = ParameterDirection.Input;
            command.Parameters.AddWithValue("@Weight", person.Weight).Direction = ParameterDirection.Input;

            using (var reader = command.ExecuteReader())
            {
            }
            Client.Close();

            response.Data = new Event();
            response.Succes = true;
            return response;
        }

        public Response<Event> DeletePerson(Person person)
        {
            var response = new Response<Event>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd =
                    new MySqlCommand(
                        "delete from events e where e.Id = " + person.Id,
                        Client);
                using (var reader = cmd.ExecuteReader())
                {
                }
                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        private bool ExistEvent(MySqlConnection client, Event eve, Person person)
        {
            MySqlCommand cmd = new MySqlCommand(
                    "select * from eventperson e where e.EventId = " + eve.Id + " AND e.PersonId = " + person.Id,
                client);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return true;
                }
            }
            return false;
        }

        private Person MapPerson(MySqlDataReader reader)
        {
            var person = new Person()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Colla = Convert.ToInt32(reader["Colla"]),
                Name = Convert.ToString(reader["Name"]),
                SurName = Convert.ToString(reader["Surname"]),
                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
            };

            var height = reader["Height"];
            if (height.GetType() != typeof(DBNull)) person.Height = Convert.ToInt32(reader["Height"]);
            var alias = reader["Alias"];
            if (alias.GetType() != typeof(DBNull)) person.Alias = Convert.ToString(reader["Alias"]);
            var weight = reader["Weight"];
            if (weight.GetType() != typeof(DBNull)) person.Weight = Convert.ToDecimal(reader["Weight"]);

            return person;
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
                    "https://www.google.es",
                Date = new DateTime(),
                Title = "Assaig bla",
                Description = "bla bla bla",
                InitialHour = "20:00",
                EndHour = "22:00",
            };
        }
    }
}