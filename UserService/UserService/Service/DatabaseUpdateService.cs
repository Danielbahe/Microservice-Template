using System.Collections.Generic;
using Newtonsoft.Json;
using RabbitCommunications.Models;
using RabbitCommunications.Senders;
using UserService.Models;
using UserService.Service.Interfaces;

namespace UserService.Service
{
    public class DatabaseUpdateService : IDatabaseUpdateService
    {
        public bool UpdatePersons(Person person, string method)
        {
            var request = new RequestModel();
            request.Body = JsonConvert.SerializeObject(person);
            request.Headers = new Dictionary<string, string>();
            request.Headers.Add("method", method);
            var json = JsonConvert.SerializeObject(request);
            var rpcClient = new ResponseSender();
            var responseJson = rpcClient.Call(json, "persondbqueue");
            rpcClient.Close();

            var response = JsonConvert.DeserializeObject<Response<Person>>(responseJson);
            return response.Succes;
        }

        public bool AddPersons(Person person, string method)
        {
            var request = new RequestModel();
            request.Body = JsonConvert.SerializeObject(person);
            request.Headers = new Dictionary<string, string>();
            request.Headers.Add("method", method);
            var json = JsonConvert.SerializeObject(request);
            var rpcClient = new ResponseSender();
            var responseJson = rpcClient.Call(json, "persondbqueue");
            rpcClient.Close();

            var response = JsonConvert.DeserializeObject<Response<Person>>(responseJson);
            return response.Succes;
        }

        public bool DeletePersons(Person person, string method)
        {
            var request = new RequestModel();
            request.Body = JsonConvert.SerializeObject(person);
            request.Headers = new Dictionary<string, string>();
            request.Headers.Add("method", method);
            var json = JsonConvert.SerializeObject(request);
            var rpcClient = new ResponseSender();
            var responseJson = rpcClient.Call(json, "persondbqueue");
            rpcClient.Close();

            var response = JsonConvert.DeserializeObject<Response<Person>>(responseJson);
            return response.Succes;
        }
    }
}