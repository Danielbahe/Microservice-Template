using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PersonSevice.Models;

namespace PersonSevice
{
    class PersonRepository : IPersonRepository
    {
        private MongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<Person> PersonCollection;

        public PersonRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("Danitest");
            PersonCollection = _db.GetCollection<Person>("Persons");
        }

        public Person SavePerson(Person person)
        {

            var response = CreateCol(person);
            
            return response;
        }

        private Person CreateCol(Person person)
        {
            ////await _db.CreateCollectionAsync("Persons");
            //var collection = _db.GetCollection<Person>("Persons");

            //var document = new BsonDocument
            //{
            //    {"name", BsonValue.Create(person.Name)},
            //    {"surname", new BsonString(person.SurName)},
            //    { "age", person.Age },
            //    { "id", person.Id }
            //};

            //var doc = person.ToBsonDocument();

            //var equal = doc == document;

            //var personList = new List<Person> { person };

            //await collection.InsertOneAsync(person);
            //await collection.InsertManyAsync(personList);

            //var collection = _db.GetCollection<BsonDocument>("Persons");
            //var document = new BsonDocument
            //{
            //    {"name", BsonValue.Create(person.Name)},
            //    {"surname", new BsonString(person.SurName)},
            //    { "age", person.Age }
            //};
            //await collection.Find(FilterDefinition<BsonDocument>.Empty)
            //    .ForEachAsync(doc => Console.WriteLine(doc));

            //var filter = new BsonDocument("name", "dani");

            //await collection.Find(filter)
            //    .ForEachAsync(doc => Console.WriteLine(doc + "second"));



            var response = PersonCollection.Find(p => p.Name != "Peter" && p.Id == 5).FirstOrDefault();
            return response;
        }
    }

    internal interface IPersonRepository
    {
        Person SavePerson(Person person);
    }
}