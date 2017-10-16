using System.Collections.Generic;

namespace GatewayApi.ServiceStrategy.Conditions
{
    public interface ICondition
    {
        bool ValidateConditions(List<string> keys);
    }
}
