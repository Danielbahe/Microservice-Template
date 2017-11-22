using UserService.Models;

namespace UserService.Service.Interfaces
{
    public interface IDatabaseUpdateService
    {
        bool UpdatePersons(Person person, string method);
        bool AddPersons(Person person, string method);
        bool DeletePersons(Person person, string method);
    }
}