using PersonSevice.Models;
using RabbitCommunications.Models;

namespace PersonSevice
{
    public interface IPersonService
    {
        Response<Person> SavePerson(Person model);
        Response<Person> GetPersonById(int id);

    }
}