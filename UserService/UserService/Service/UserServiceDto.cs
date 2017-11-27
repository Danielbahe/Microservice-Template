using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;
using RabbitCommunications.Models;
using UserDtoService.Service.Interfaces;
using UserService.Models;
using UserService.Models.Dto;
using UserService.Service.Interfaces;

namespace UserService.Service
{
    public class UserServiceDto : IUserServiceDto
    {
        private IUserService userService;

        public UserServiceDto(IUserService userService)
        {
            this.userService = userService;
        }

        public Response<UserDto> GetUserById(string idJson)
        {
            var id = JsonConvert.DeserializeObject<int>(idJson);

            var response = this.userService.GetUserById(id);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<UserDto> RegisterUser(string userJson)
        {
            var userDto = JsonConvert.DeserializeObject<UserDto>(userJson);
            var user = Mapper.Map<User>(userDto);

            var response = this.userService.RegisterUser(user);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<UserDto> Login(string userJson)
        {
            var userDto = JsonConvert.DeserializeObject<UserDto>(userJson);
            var user = Mapper.Map<User>(userDto);

            var response = this.userService.Login(user);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }
        
        public Response<UserDto> GetUserPersons(string userJson)
        {
            var userDto = JsonConvert.DeserializeObject<UserDto>(userJson);
            var user = Mapper.Map<User>(userDto);

            var response = this.userService.GetUserPersons(user);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<UserDto> ActivateUser(string userJson)
        {
            var userDto = JsonConvert.DeserializeObject<UserDto>(userJson);
            var user = Mapper.Map<User>(userDto);

            var response = this.userService.ActivateUser(user);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<UserDto> RetrievePassword(string userJson)
        {
            var userDto = JsonConvert.DeserializeObject<UserDto>(userJson);
            var user = Mapper.Map<User>(userDto);

            var response = this.userService.RetrievePassword(user);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<UserDto> ChangePassword(string userJson)
        {
            var userDto = JsonConvert.DeserializeObject<UserDto>(userJson);
            var user = Mapper.Map<User>(userDto);

            var response = this.userService.ChangePassword(user);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<UserDto> AddPerson(string userJson)
        {
            var person = JsonConvert.DeserializeObject<Person>(userJson);

            var response = this.userService.AddPerson(person);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<UserDto> DeletePerson(string userJson)
        {
            var person = JsonConvert.DeserializeObject<Person>(userJson);

            var response = this.userService.DeletePerson(person);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<UserDto> UpdatePerson(string userJson)
        {
            var person = JsonConvert.DeserializeObject<Person>(userJson);

            var response = this.userService.UpdatePerson(person);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<List<UserDto>> GetChangedPasswordUsers(string userjson)
        {
            var user = JsonConvert.DeserializeObject<User>(userjson);

            var response = this.userService.GetChangedPasswordUsers(user.Colla);

            var responseDto = Mapper.Map<Response<List<UserDto>>>(response);
            return responseDto;
        }

        public Response<List<UserDto>> GetAllUsers(string userjson)
        {
            var user = JsonConvert.DeserializeObject<User>(userjson);

            var response = this.userService.GetAllUsers(user.Colla);

            var responseDto = Mapper.Map<Response<List<UserDto>>>(response);
            return responseDto;
        }

        public Response<List<UserDto>> GetAllNewUsers(string userjson)
        {
            var user = JsonConvert.DeserializeObject<User>(userjson);

            var response = this.userService.GetAllNewUsers(user.Colla);

            var responseDto = Mapper.Map<Response<List<UserDto>>>(response);
            return responseDto;
        }

        public Response<UserDto> UpdateRole(string userJson)
        {
            var user = JsonConvert.DeserializeObject<User>(userJson);

            var response = this.userService.UpdateRole(user);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }
        public Response<UserDto> ChangeSuperAdmin(string userJson)
        {
            var user = JsonConvert.DeserializeObject<User>(userJson);

            var response = this.userService.UpdateRole(user);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }

        public Response<UserDto> UpdateState(string userJson)
        {
            var user = JsonConvert.DeserializeObject<User>(userJson);

            var response = this.userService.UpdateState(user);

            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }
    }
}