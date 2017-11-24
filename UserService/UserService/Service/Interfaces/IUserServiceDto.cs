using System.Collections.Generic;
using RabbitCommunications.Models;
using UserService.Models.Dto;
using UserService.Models;

namespace UserDtoService.Service.Interfaces
{
    public interface IUserServiceDto
    {
        Response<UserDto> GetUserById(string userJson);
        Response<UserDto> RegisterUser(string UserDto);
        Response<UserDto> Login(string UserDto);
        Response<UserDto> GetUserPersons(string UserDto);
        Response<UserDto> ActivateUser(string UserDto);
        Response<UserDto> RetrievePassword(string UserDto);
        Response<UserDto> ChangePassword(string UserDto);
        Response<UserDto> AddPerson(string UserDto);
        Response<UserDto> DeletePerson(string UserDto);
        Response<UserDto> UpdatePerson(string UserDto);
        Response<List<UserDto>> GetChangedPasswordUsers(string userJson);
        Response<List<UserDto>> GetAllUsers(string userJson);
        Response<List<UserDto>> GetAllNewUsers(string userJson);
        Response<UserDto> UpdateRole(string userJson);
        Response<UserDto> ChangeSuperAdmin(string userJson);
        Response<UserDto> UpdateState(string userJson);
        //Response<UserDto> UpdateUserDto(Person UserDto);
    }
}