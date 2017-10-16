﻿using RabbitCommunications.Senders;

namespace GatewayApi.ServiceStrategy.Actions
{
    public class MicroServiceActions : IAction
    {
        public string ExecuteActions(string json)
        {
            var rpcClient = new ResponseSender();
            var responseJson = rpcClient.Call(json, QueuesNames.MicroServiceQueue);
            rpcClient.Close();

            return responseJson;
        }
    }
}
