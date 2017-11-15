using System;
using System.Collections.Generic;
using RabbitCommunications.Models;
using UserService.Models;
using UserService.Service.Interfaces;

namespace UserService.Service
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            
        }
        public Response<User> GetUserById(int id)
        {
            var response = new Response<User>();
            if (id == 1)
            {
                var user = new User
                {
                    Id = 1,
                    PersonList = new List<Person>(),
                };
                user.PersonList.Add(new Person{Name = "Dani", SurName = "red"});
                response.Data = user;
                response.Succes = true;
            }
            return response;
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
                    
                    Admin = true,
                    CollaId = 1
                };
                user.PersonList.Add(new Person { Name = "Dani", SurName = "red" });
                userBdd.UserName = user.UserName;
                response.Data = userBdd;
                response.Succes = true;
            }
            return response;
        }
    }
}