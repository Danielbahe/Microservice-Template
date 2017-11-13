using RabbitCommunications.Models;
using UserService.Models;

namespace UserService.Service.Interfaces
{
    public interface IUserService
    {
        Response<User> GetUserById(int id);
        Response<User> RegisterUser(User user);
        Response<User> Login(User user);

    }
}