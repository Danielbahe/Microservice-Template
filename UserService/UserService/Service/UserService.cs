using System;
using System.Collections.Generic;
using RabbitCommunications.Models;
using UserService.Models;
using UserService.Service.Interfaces;

namespace UserService.Service
{
    public class UserService: IUserService
    {
        private IUserRepository userServiceRepository;

        public UserService(IUserRepository userServiceRepository)
        {
            this.userServiceRepository = userServiceRepository;
        }

        public Response<User> GetUserById(int id)
        {
            var response = userServiceRepository.GetUserById(id);
            return response;
        }

        public Response<User> RegisterUser(User user)
        {
            var response = userServiceRepository.RegisterUser(user);
            return response;
        }

        public Response<User> Login(User user)
        {
            var response = userServiceRepository.Login(user);
            return response;
        }

        public Response<User> GetUserPersons(User user)
        {
            var response = userServiceRepository.GetUserPersons(user);
            return response;
        }

        public Response<User> ActivateUser(User user)
        {
            var response = userServiceRepository.ActivateUser(user);
            return response;
        }

        public Response<User> RetrievePassword(User user)
        {
            string ranpass = string.Empty;
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                ranpass = ranpass + rnd.Next(0, 10);
            }

            var response = userServiceRepository.RetrievePassword(user, ranpass);
            return response;
        }

        public Response<User> ChangePassword(User user)
        {
            var response = userServiceRepository.ChangePassword(user);
            return response;
        }

        public Response<User> AddPerson(Person person)
        {
            var response = userServiceRepository.AddPerson(person);
            //todo update event service
            return response;
        }

        public Response<User> DeletePerson(Person person)
        {
            var response = userServiceRepository.DeletePerson(person);
            //todo update event service
            return response;
        }

        public Response<User> UpdatePerson(Person person)
        {
            var response = userServiceRepository.UpdatePerson(person);
            //todo update event service
            return response;
        }

        public Response<List<User>> GetChangedPasswordUsers(int collaId)
        {
            var response = userServiceRepository.GetChangedPasswordUsers(collaId);
            return response;
        }

        public Response<List<User>> GetAllUsers(int collaId)
        {
            var response = userServiceRepository.GetAllUsers(collaId);
            return response;
        }
    }
}