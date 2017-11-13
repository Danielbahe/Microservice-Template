using System.Collections.Generic;

namespace GatewayApi.ServiceStrategy.Conditions
{
    public class EventServiceConditions : ICondition
    {
        public bool ValidateConditions(List<string> keys)
        {
            if (!keys.Contains(QueuesNames.EventServiceQueue)) return false;

            return true;
        }
    }

    public class EventServiceListConditions : ICondition
    {
        public bool ValidateConditions(List<string> keys)
        {
            if (!keys.Contains(QueuesNames.EventServiceListQueue)) return false;

            return true;
        }

    }
}