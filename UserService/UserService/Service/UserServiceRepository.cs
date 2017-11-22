using System;
using System.Collections.Generic;
using RabbitCommunications.Models;
using UserService.Models;
using UserService.Service.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace UserService.Service
{
    public class UserRepository : IUserRepository
    {
        private const string ConnectionStrings =
            "Server=localhost;Database=userdbtest;Uid=admin;Pwd=admin;Allow User Variables=True;";

        private MySqlConnection Client;

        public Response<User> GetUserById(int id)
        {
            var response = new Response<User>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users where Id = " + id, Client);
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
        }

        public Response<User> RegisterUser(User user)
        {
            var response = new Response<User>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                var query =
                    "INSERT INTO users VALUES (NULL,@Colla , @Title, @Date, @InitialHour, @EndHour, @Square, @Address, @Description, @Type)";
                MySqlCommand command = new MySqlCommand(query, Client);
                command.Parameters.AddWithValue("@UserName", user.UserName).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Password", user.Password).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Role", user.Role).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Colla", user.Colla).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@State", 0).Direction = ParameterDirection.Input;
                using (var reader = command.ExecuteReader())
                {
                }
                Client.Close();
            }
            response.Succes = true;

            return response;
        }

        public Response<User> Login(User user)
        {
            var response = new Response<User>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd =
                    new MySqlCommand(
                        "select * from users where UserName = '" + user.UserName + "' AND Password = '" +
                        user.Password + "'", Client);
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

        public Response<User> GetUserPersons(User user)
        {
            var response = new Response<User>();

            response.Data = GetPersons(user);
            response.Succes = true;

            return response;
        }

        public Response<User> ActivateUser(User user)
        {
            var response = new Response<User>();

            var query = "UPDATE users SET State = @State where Id = " + user.Id;
            var cmd = new MySqlCommand(query, Client);
            cmd.Parameters.AddWithValue("@State", 1).Direction = ParameterDirection.Input;
            using (var reader = cmd.ExecuteReader())
            {
            }
            response.Succes = true;
            return response;
        }

        public Response<User> RetrievePassword(User user, string randomPass)
        {
            var response = new Response<User>();
            
            var query = "UPDATE users SET State = @State, Password = @Password where Id = " + user.Id;
            var cmd = new MySqlCommand(query, Client);
            cmd.Parameters.AddWithValue("@State", 2).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Password", randomPass).Direction = ParameterDirection.Input;
            using (var reader = cmd.ExecuteReader())
            {
            }
            response.Succes = true;
            return response;
        }

        public Response<User> ChangePassword(User user)
        {
            var response = new Response<User>();
            string ranpass = string.Empty;

            var query = "UPDATE users SET Password = @Password where Id = " + user.Id;
            var cmd = new MySqlCommand(query, Client);
            cmd.Parameters.AddWithValue("@Password", user.Password).Direction = ParameterDirection.Input;
            using (var reader = cmd.ExecuteReader())
            {
            }
            response.Succes = true;
            return response;
        }

        public Response<User> AddPerson(Person person)
        {
            var response = new Response<User>();

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

            response.Succes = true;
            return response;
        }

        public Response<User> DeletePerson(Person person)
        {
            var response = new Response<User>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd =
                    new MySqlCommand(
                        "delete from persons e where e.Id = " + person.Id,
                        Client);
                using (var reader = cmd.ExecuteReader())
                {
                }
                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<User> UpdatePerson(Person person)
        {
            var response = new Response<User>();

            var query =
                "UPDATE persons SET Id = @Id ,Alias = @Alias, BirthDate = @BirthDate, Colla = @Colla, Height = @Height, Name = @Name, SurName = @SurName, Weight = @Weight where Id = " +
                person.Id;
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

            response.Succes = true;
            return response;
        }

        public Response<List<User>> GetChangedPasswordUsers(int collaId)
        {
            var response = new Response<List<User>>();
            var userList = new List<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users where Colla = " + collaId + " AND State = 2", Client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userList.Add(MapUser(reader));
                    }
                }

                Client.Close();
            }
            response.Data = userList;
            response.Succes = true;

            return response;
        }

        public Response<List<User>> GetAllUsers(int collaId)
        {
            var response = new Response<List<User>>();
            var userList = new List<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users where Colla = " + collaId , Client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userList.Add(MapUser(reader));
                    }
                }

                Client.Close();
            }
            response.Data = userList;
            response.Succes = true;

            return response;
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