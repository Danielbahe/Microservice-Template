using System.Collections.Generic;

namespace GatewayApi.ServiceStrategy.Conditions
{
    public class UserServiceConditions : ICondition
    {
        public bool ValidateConditions(List<string> keys)
        {
            if (!keys.Contains(QueuesNames.UserServiceQueue)) return false;
            
            return true;
        }
    }

    public class UserServiceListConditions : ICondition
    {
        public bool ValidateConditions(List<string> keys)
        {
            if (!keys.Contains(QueuesNames.EventServiceListQueue)) return false;

            return true;
        }
    }
}