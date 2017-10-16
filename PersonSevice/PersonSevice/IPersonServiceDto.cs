using PersonSevice.Models;
using RabbitCommunications.Models;

namespace PersonSevice
{
    public interface IPersonServiceDto
    {
        Response<PersonDto> SavePerson(string model);
    }
}
