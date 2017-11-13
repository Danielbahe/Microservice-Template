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
    }
}