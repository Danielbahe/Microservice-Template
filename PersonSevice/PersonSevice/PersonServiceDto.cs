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
            var person = new Person
            {
                Age = personDto.Age,
                Name = personDto.Name,
                SurName = personDto.SurName
            };


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

