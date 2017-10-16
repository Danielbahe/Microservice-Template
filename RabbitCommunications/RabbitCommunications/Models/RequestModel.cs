using System.Collections.Generic;

namespace RabbitCommunications.Models
{
    public class RequestModel
    {
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
    }
}
