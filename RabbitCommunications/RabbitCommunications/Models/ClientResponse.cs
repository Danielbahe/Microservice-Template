namespace RabbitCommunications.Models
{
    public class ClientResponse<T>
    {
        public T Data { get; set; }
        public bool Succes { get; set; }
        public string ErrorMessage { get; set; }
        public bool HaveException { get; set; }
        public string ErrorCode { get; set; }

    }
}