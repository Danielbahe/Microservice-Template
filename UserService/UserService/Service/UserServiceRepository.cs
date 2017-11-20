using System;
using System.Collections.Generic;
using RabbitCommunications.Models;
using UserService.Models;
using UserService.Service.Interfaces;
using MySql.Data.MySqlClient;

namespace UserService.Service
{
    public class UserRepository : IUserRepository
    {
        private const string ConnectionStrings = "Server=localhost;Database=userdbtest;Uid=admin;Pwd=admin;Allow User Variables=True;";
        private MySqlConnection Client;

        public Response<User> GetUserById(int id)
        {
            var response = new Response<User>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd =
                    new MySqlCommand(
                        "select * from users where Id = " + id, Client);
                //MySqlCommand cmd = new MySqlCommand("select * from events", Client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        response.Data = MapUser(reader);
                    }
                }

                response.Data = GetPersons(response.Data);

                Client.Close();
            }
            response.Succes = true;

            return response;

            //var response = new Response<User>();
            //if (id == 1)
            //{
            //    var user = new User
            //    {
            //        Id = 1,
            //        PersonList = new List<Person>(),
            //    };
            //    user.PersonList.Add(new Person{Name = "Dani", SurName = "red"});
            //    response.Data = user;
            //    response.Succes = true;
            //}
            //return response;
        }

        

        public Response<User> RegisterUser(User user)
        {
            var response = new Response<User>();

            user.Id = 1;
            response.Data = user;
            response.Succes = true;

            return response;
        }

        public Response<User> Login(User user)
        {
            
            var response = new Response<User>();
            if (user.UserName == "admin" && user.Password == "admin")
            {
                var userBdd = new User
                {
                    Id = 1,
                    
                    Role = 1,
                    Colla = 1
                };
                user.PersonList.Add(new Person { Name = "Dani", SurName = "red" });
                userBdd.UserName = user.UserName;
                response.Data = userBdd;
                response.Succes = true;
            }
            return response;
        }

        private User MapUser(MySqlDataReader reader)
        {
            var user = new User()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Role = Convert.ToInt32(reader["Role"]),
                State = Convert.ToInt32(reader["State"]),
                UserName = Convert.ToString(reader["UserName"]),
                Colla = Convert.ToInt32(reader["Colla"]),
                Password = Convert.ToString(reader["Password"])
            };

            return user;
        }
        private User GetPersons(User user)
        {
            var list = new List<Person>();
            MySqlCommand cmd =
                new MySqlCommand(
                    "select * from persons where UserId = " + user.Id, Client);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(MapPerson(reader));
                }
            }
            user.PersonList = list;

            return user;
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
    }
}