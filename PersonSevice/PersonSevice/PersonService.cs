using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PersonSevice.Models;
using RabbitCommunications.Models;

namespace PersonSevice
{
    class PersonService: IPersonService
    {
        public PersonService()
        {
            
        }
        public Response<Person> SavePerson(Person model)
        {
            model.Id = 5;

            Console.WriteLine("saved person: " + JsonConvert.SerializeObject(model));

            var response = new Response<Person>
            {
                Succes = true,
                Data = model
            };

            return response;
        }

        public Response<Person> GetPersonById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
