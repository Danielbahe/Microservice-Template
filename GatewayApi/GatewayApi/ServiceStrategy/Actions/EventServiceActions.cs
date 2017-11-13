using RabbitCommunications.Senders;

namespace GatewayApi.ServiceStrategy.Actions
{
    public class EventServiceActions : IAction
    {
        public string ExecuteActions(string json)
        {
            var rpcClient = new ResponseSender();
            var responseJson = rpcClient.Call(json, QueuesNames.EventServiceQueue);
            rpcClient.Close();

            return responseJson;
        }
    }

    public class EventServiceListActions : IAction
    {
        public string ExecuteActions(string json)
        {
            var rpcClient = new ResponseSender();
            var responseJson = rpcClient.Call(json, QueuesNames.EventServiceListQueue);
            rpcClient.Close();

            return responseJson;
        }
    }
}
