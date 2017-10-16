using System.Collections.Generic;

namespace GatewayApi.ServiceStrategy.Conditions
{
    public class MicroServiceConditions : ICondition
    {
        public bool ValidateConditions(List<string> keys)
        {
            if (!keys.Contains(QueuesNames.MicroServiceQueue)) return false;
            //Comprobar si modulo activado para este cliente concreto. En caso de no activado lanzar excepción para saber que es por esa es la razón.
            //para no duplicar codigo ponerlo como validación despues o en ConditionBase.
            return true;
        }
    }
}
