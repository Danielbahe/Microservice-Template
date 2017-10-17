using System.Collections.Generic;
using System.Linq;
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
        public string Get(JObject id)
        {
            var response = SendToService(id);
            return response;
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]JObject value)
        {
            var response = SendToService(value);
            return response;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody]string value)
        {
            return "Use 'Post' method";
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public string Delete(JObject id)
        {
            var response = SendToService(id);
            return response;
        }

        private Dictionary<string, string> CastHeadersToDictionary(IHeaderDictionary headers)
        {
            var headerDictionary = new Dictionary<string, string>();

            foreach (var pair in headers)
            {
                headerDictionary.Add(pair.Key, pair.Value);
            }

            return headerDictionary;
        }

        private string SendToService(JObject value)
        {
            var keyList = this.Request.Headers.Select(pair => pair.Key).ToList();

            var serializedBody = JsonConvert.SerializeObject(value);

            var model = new RequestModel
            {
                Body = serializedBody,
                Headers = CastHeadersToDictionary(HttpContext.Request.Headers)
            };
            var jsonModel = JsonConvert.SerializeObject(model);

            var action = ServiceSelector.Validate(keyList);

            var response = action.ExecuteActions(jsonModel);

            var jsonResponse = JsonConvert.SerializeObject(response);

            return jsonResponse;
        }
    }
}