using System.Collections.Generic;
using RabbitCommunications.Models;
using UserService.Models;

namespace UserService.Service.Interfaces
{
    public interface IUserService
    {
        Response<User> GetUserById(int id);
        Response<User> RegisterUser(User user);
        Response<User> Login(User user);
        Response<User> GetUserPersons(User user);
        Response<User> ActivateUser(User user);
        Response<User> RetrievePassword(User user);
        Response<User> ChangePassword(User user);
        Response<User> AddPerson(Person user);
        Response<User> DeletePerson(Person user);
        Response<User> UpdatePerson(Person user);
        Response<List<User>> GetChangedPasswordUsers(int collaId);
        Response<List<User>> GetAllUsers(int collaId);
        //Response<User> UpdateUser(Person user);

    }
}