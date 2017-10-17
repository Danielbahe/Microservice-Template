using System;
using Newtonsoft.Json;
using PersonSevice.Models;
using RabbitCommunications.Models;

namespace PersonSevice
{
    public class PersonServiceDto : IPersonServiceDto
    {
        public IPersonService PersonService { get; set; }   
        public PersonServiceDto(IPersonService personService)
        {
            PersonService = personService;
        }
        public Response<PersonDto> SavePerson(string model)
        {
            var personDto = JsonConvert.DeserializeObject<PersonDto>(model);

            //todo automapper
            var person = personDto as Person;

            var personResponse = PersonService.SavePerson(person);

            //todo map responses
            
            var response = new Response<PersonDto>
            {
                Succes = true,
                Data = personDto
            };
            return response;
        }
    }
}

