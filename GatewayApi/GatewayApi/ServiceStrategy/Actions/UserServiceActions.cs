using RabbitCommunications.Senders;

namespace GatewayApi.ServiceStrategy.Actions
{
    public class UserServiceActions : IAction
    {
        public string ExecuteActions(string json)
        {
            var rpcClient = new ResponseSender();
            var responseJson = rpcClient.Call(json, QueuesNames.UserServiceQueue);
            rpcClient.Close();

            return responseJson;
        }
    }

    public class UserServiceListActions : IAction
    {
        public string ExecuteActions(string json)
        {
            var rpcClient = new ResponseSender();
            var responseJson = rpcClient.Call(json, QueuesNames.UserServiceListQueue);
            rpcClient.Close();

            return responseJson;
        }
    }
}
