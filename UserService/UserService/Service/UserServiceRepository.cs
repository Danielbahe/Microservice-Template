using System;
using RabbitCommunications.Models;
using UserService.Models;
using UserService.Service.Interfaces;

namespace UserService.Service
{
    public class UserRepository : IUserRepository
    {
        public Response<User> GetUserById(int id)
        {
            var response = new Response<User>();
            if (id == 1)
            {
                var user = new User
                {
                    Id = 1,
                    Birth = new DateTime(1992, 8, 22),
                    Name = "Dani",
                    SurName = "Red"
                };

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
                    Birth = new DateTime(1992, 8, 22),
                    Name = "Dani",
                    SurName = "Red",
                    Admin = true,
                    CollaId = 1
                };
                userBdd.UserName = user.UserName;
                response.Data = userBdd;
                response.Succes = true;
            }
            return response;
        }
    }
}