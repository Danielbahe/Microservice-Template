using AutoMapper;
using Newtonsoft.Json;
using RabbitCommunications.Models;
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

        public Response<UserDto> GetUserById(string userJson)
        {
            var user = JsonConvert.DeserializeObject<User>(userJson);

            var response = this.userService.GetUserById(user.Id);

            return MapResponse(response);

        }

        public Response<UserDto> RegisterUser(string userjson)
        {
            var user = MapDto(userjson);
            var response = this.userService.RegisterUser(user);

            return MapResponse(response);

        }

        public Response<UserDto> Login(string userjson)
        {
            var user = MapDto(userjson);
            var response = this.userService.Login(user);

            return MapResponse(response);
        }

        private User MapDto(string dto)
        {
            var userDto = JsonConvert.DeserializeObject<UserDto>(dto);
            var user = Mapper.Map<User>(userDto);
            return user;

        }

        private Response<UserDto> MapResponse(Response<User> response)
        {
            var responseDto = Mapper.Map<Response<UserDto>>(response);
            return responseDto;
        }
    }

}