using GatewayApi.ServiceStrategy.Actions;
using GatewayApi.ServiceStrategy.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GatewayApi.ServiceStrategy
{
    public class ServiceSelector
    {
        private static List<ICondition> ConditionList { get; set; }

        public static void Initialize()
        {
            if (ConditionList != null) return;
            ConditionList = new List<ICondition>();

            var conditionList = Assembly.GetEntryAssembly().DefinedTypes.Where(x => x.ImplementedInterfaces.Contains(typeof(ICondition)));

            foreach (var conditionType in conditionList)
            {
                ConditionList.Add((ICondition)Activator.CreateInstance(conditionType.AsType()));                    
            }
        }

        public static IAction Validate (List<string> headers)
        {
            foreach (var condition in ConditionList)
            {
                if (condition.ValidateConditions(headers))
                {
                    var actionList = Assembly.GetEntryAssembly().DefinedTypes.Where(miau => miau.ImplementedInterfaces.Contains(typeof(IAction)));

                    var selectedAction = actionList.FirstOrDefault(m => m.FullName.StartsWith(condition.GetType().FullName.Replace("Condition", "Action")));

                    if (selectedAction != null)
                    {
                        var action = (IAction) Activator.CreateInstance(selectedAction.AsType());

                        return action;
                    }
                    // error
                }
            }      

            return new StranglerActions();            
        }
    }
}
