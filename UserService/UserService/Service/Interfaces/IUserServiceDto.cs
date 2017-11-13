using RabbitCommunications.Models;
using UserService.Models.Dto;

namespace UserService.Service.Interfaces
{
    public interface IUserServiceDto
    {
        Response<UserDto> GetUserById(string id);
        Response<UserDto> RegisterUser(string user);
        Response<UserDto> Login(string user);
    }
}