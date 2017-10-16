using System;
using Newtonsoft.Json;
using PersonSevice.Models;
using RabbitCommunications.Models;

namespace PersonSevice
{
    public class PersonServiceDto : IPersonServiceDto
    {
        public Response<PersonDto> SavePerson(string model)
        {
            var personDto = JsonConvert.DeserializeObject<PersonDto>(model);

            //todo automapper
            var person = personDto as Person;
            Console.WriteLine("saved person: " + JsonConvert.SerializeObject(person));

            person.Id = 5;

            var response = new Response<PersonDto>
            {
                Succes = true,
                Data = personDto
            };
            return response;
        }
    }
}

