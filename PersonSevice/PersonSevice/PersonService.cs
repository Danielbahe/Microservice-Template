using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PersonSevice.Models;
using RabbitCommunications.Models;
using RabbitCommunications.Senders;

namespace PersonSevice
{
    class PersonService: IPersonService
    {
        private IPersonRepository PersonRepository;
        public PersonService(IPersonRepository personRepository)
        {
            PersonRepository = personRepository;
        }
        public Response<Person> SavePerson(Person model)
        {
            model.Id = 5;
            
            var data = this.PersonRepository.SavePerson(model);
            var response = new Response<Person>
            {
                Succes = true,
                Data = data
            };
            
            return response;
        }

        public Response<Person> GetPersonById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
