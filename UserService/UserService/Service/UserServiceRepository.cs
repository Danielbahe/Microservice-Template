using System;
using System.Collections.Generic;
using RabbitCommunications.Models;
using UserService.Models;
using UserService.Service.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;

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

                if (response.Data.State != 4 && response.Data.State != 0)
                {
                    response.Data = GetPersons(response.Data);
                    response.Succes = true;
                }
                else
                {
                    response.Succes = false;
                }

                Client.Close();
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

        public Response<User> GetUserPersons(User user)
        {
            var response = new Response<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                response.Data = GetPersons(user);
                response.Succes = true;
                Client.Close();
            }

            return response;
        }

        public Response<User> ActivateUser(User user)
        {
            var response = new Response<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                var query = "UPDATE users SET State = @State where Id = " + user.Id;
                var cmd = new MySqlCommand(query, Client);
                cmd.Parameters.AddWithValue("@State", 1).Direction = ParameterDirection.Input;
                using (var reader = cmd.ExecuteReader())
                {
                }
                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<User> RetrievePassword(User user, string randomPass)
        {
            var response = new Response<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                var query = "UPDATE users SET State = @State, Password = @Password where Id = " + user.Id;
                var cmd = new MySqlCommand(query, Client);
                cmd.Parameters.AddWithValue("@State", 2).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Password", randomPass).Direction = ParameterDirection.Input;
                using (var reader = cmd.ExecuteReader())
                {
                }
                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<User> ChangePassword(User user)
        {
            var response = new Response<User>();
            string ranpass = string.Empty;
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                var query = "UPDATE users SET State = @State, Password = @Password where Id = " + user.Id;
                var cmd = new MySqlCommand(query, Client);
                cmd.Parameters.AddWithValue("@Password", user.Password).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@State", 1).Direction = ParameterDirection.Input;
                using (var reader = cmd.ExecuteReader())
                {
                }
                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<User> AddPerson(Person person)
        {
            var response = new Response<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                var query =
                    "INSERT INTO persons (Alias, BirthDate, Colla, Height, Name, Surname, Weight, UserId) VALUES (@Alias, @BirthDate, @Colla, @Height, @Name, @SurName, @Weight, @UserId)";
                MySqlCommand command = new MySqlCommand(query, Client);
                command.Parameters.AddWithValue("@Alias", person.Alias).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@BirthDate", person.BirthDate).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Colla", person.Colla).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Height", person.Height).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Name", person.Name).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@SurName", person.SurName).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Weight", person.Weight).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@UserId", person.Id).Direction = ParameterDirection.Input;

                using (var reader = command.ExecuteReader())
                {
                }
                response.Data = new User();
                response.Data.PersonList = new List<Person>();
                response.Data.PersonList.Add(GetLastPersonInserted());

                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<User> ReinsertDeletedPerson(Person person)
        {
            var response = new Response<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                var query =
                    "INSERT INTO persons (Id, Alias, BirthDate, Colla, Height, Name, Surname, Weight, UserId) VALUES (@Id, @Alias, @BirthDate, @Colla, @Height, @Name, @SurName, @Weight, @UserId)";
                MySqlCommand command = new MySqlCommand(query, Client);
                command.Parameters.AddWithValue("@Id", person.Id).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Alias", person.Alias).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@BirthDate", person.BirthDate).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Colla", person.Colla).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Height", person.Height).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Name", person.Name).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@SurName", person.SurName).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Weight", person.Weight).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@UserId", person.Id).Direction = ParameterDirection.Input;

                using (var reader = command.ExecuteReader())
                {
                }
                response.Data = new User();
                response.Data.PersonList = new List<Person>();
                response.Data.PersonList.Add(GetLastPersonInserted());

                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<User> DeletePerson(Person person)
        {
            var response = new Response<User>();

            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();

                MySqlCommand countcmd =
                    new MySqlCommand(
                        "select COUNT(*) count from persons e where e.UserId = " + person.UserId,
                        Client);
                int count;
                using (var reader = countcmd.ExecuteReader())
                {
                    count = Convert.ToInt32(reader["count"]);
                }
                if (count == 1)
                {
                    Client.Close();
                    response.Succes = false;
                    return response;
                }

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
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
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

                response.Data = new User();
                response.Data.PersonList = new List<Person>();
                response.Data.PersonList.Add(GetUserById(person.UserId).Data.PersonList.First());
                Client.Close();
            }
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
                MySqlCommand cmd = new MySqlCommand("select * from users where Colla = " + collaId + " AND State = 2",
                    Client);
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
                MySqlCommand cmd =
                    new MySqlCommand("select * from users where Role != 2 AND Colla = " + collaId, Client);
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

        public Response<List<User>> GetAllNewUsers(int collaId)
        {
            var response = new Response<List<User>>();
            var userList = new List<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users where Colla = " + collaId + " AND State = 0",
                    Client);
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

        private User GetLastUserInserted()
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

            var getByIdQuery = "select * from users where Id = " + id;
            var getByIdCmd = new MySqlCommand(getByIdQuery, Client);
            User eve = new User();
            using (var reader = getByIdCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    eve = MapUser(reader);
                }
            }
            return eve;
        }

        private Person GetLastPersonInserted()
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

            var getByIdQuery = "select * from persons where Id = " + id;
            var getByIdCmd = new MySqlCommand(getByIdQuery, Client);
            Person eve = new Person();
            using (var reader = getByIdCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    eve = MapPerson(reader);
                }
            }
            return eve;
        }

        public Response<User> UpdateRole(User user)
        {
            var response = new Response<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                var query = "UPDATE users SET Role = @Role where Id = " + user.Id;

                MySqlCommand command = new MySqlCommand(query, Client);
                command.Parameters.AddWithValue("@Role", user.Role).Direction = ParameterDirection.Input;

                using (var reader = command.ExecuteReader())
                {
                }
                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<User> ChangeSuperAdmin(User user)
        {
            var response = new Response<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                User currentSA = new User();
                Client.Open();
                var getSAQuery = "select * from users where Role = 2 AND Colla = " + user.Colla;

                MySqlCommand command = new MySqlCommand(getSAQuery, Client);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currentSA = MapUser(reader);
                    }
                }
                var downGradeSAQuery = "UPDATE users SET Role = 1 where Id = " + currentSA.Id;

                MySqlCommand cmd = new MySqlCommand(downGradeSAQuery, Client);
                //command.Parameters.AddWithValue("@Role", 1).Direction = ParameterDirection.Input;
                using (var reader = cmd.ExecuteReader())
                {
                }

                var updateSAQuery = "UPDATE users SET Role = 2 where Id = " + user.Id;

                MySqlCommand comm = new MySqlCommand(updateSAQuery, Client);
                //comm.Parameters.AddWithValue("@Role", 2).Direction = ParameterDirection.Input;
                using (var reader = comm.ExecuteReader())
                {
                }
                Client.Close();
            }
            response.Succes = true;
            return response;
        }

        public Response<User> UpdateState(User user)
        {
            var response = new Response<User>();
            using (Client = new MySqlConnection(ConnectionStrings))
            {
                Client.Open();
                var query = "UPDATE users SET State = @State where Id = " + user.Id;

                MySqlCommand command = new MySqlCommand(query, Client);
                command.Parameters.AddWithValue("@State", user.State).Direction = ParameterDirection.Input;

                using (var reader = command.ExecuteReader())
                {
                }
                Client.Close();
            }
            response.Succes = true;
            return response;
        }
    }
}