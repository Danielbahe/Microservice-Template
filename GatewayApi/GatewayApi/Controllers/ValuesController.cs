using System.Collections.Generic;
using RabbitCommunications.Models;
using Microsoft.AspNetCore.Mvc;
using GatewayApi.ServiceStrategy;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace GatewayApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public ValuesController()
        {
            ServiceSelector.Initialize();
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var a = new Response<Person>();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var keyList = new List<string>();
            foreach (var pair in this.Request.Headers)
            {
                keyList.Add(pair.Key);
            }

            var serializedBody = JsonConvert.SerializeObject(id);

            //todo TEST
            var per = new Person
            {
                Name = "dani",
                Age = 3,
                SurName = "Shurmano"
            };
            serializedBody = JsonConvert.SerializeObject(per);

            var model = new RequestModel
            {
                Body = serializedBody,
                Headers = castHeadersToDictionary(HttpContext.Request.Headers)
            };
            var jsonModel = JsonConvert.SerializeObject(model);

            var action = ServiceSelector.Validate(keyList);

            var response = action.ExecuteActions(jsonModel);

            var jsonResponse = JsonConvert.SerializeObject(response);

            return jsonResponse;
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]JObject value)
        {
            var keyList = new List<string>();
            foreach (var pair in this.Request.Headers)
            {
                keyList.Add(pair.Key);
            }

            var serializedBody = JsonConvert.SerializeObject(value);

            var model = new RequestModel
            {
                Body = serializedBody,
                Headers = castHeadersToDictionary(HttpContext.Request.Headers)
            };
            var jsonModel = JsonConvert.SerializeObject(model);

            var action = ServiceSelector.Validate(keyList);

            var response = action.ExecuteActions(jsonModel);

            var jsonResponse = JsonConvert.SerializeObject(response);

            return jsonResponse;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private Dictionary<string, string> castHeadersToDictionary(IHeaderDictionary headers)
        {
            var headerDictionary = new Dictionary<string, string>();

            foreach (var pair in headers)
            {
                headerDictionary.Add(pair.Key, pair.Value);
            }

            return headerDictionary;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
    }
}
