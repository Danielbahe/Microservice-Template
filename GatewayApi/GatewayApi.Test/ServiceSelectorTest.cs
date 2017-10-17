using System.Collections.Generic;
using GatewayApi.ServiceStrategy;
using GatewayApi.ServiceStrategy.Actions;
using NUnit.Framework;

namespace GatewayApi.Test
{
    [TestFixture]
    public class ServiceSelectorTest
    {
        private ServiceSelector ServiceSelector { get; set; }

        [Test]
        public void ValidKeyTest()
        {
            ServiceSelector = new ServiceSelector();
            var keyList = new List<string>();
            keyList.Add("xamarin");
            keyList.Add("invalidKey");

            var action = ServiceSelector.Validate(keyList);

            Assert.IsInstanceOf(typeof(IAction), action);
        }

        [Test]
        public void InValidKeyTest()
        {
            ServiceSelector = new ServiceSelector();
            var keyList = new List<string>();
            keyList.Add("invalidKey");

            var action = ServiceSelector.Validate(keyList);

            
            Assert.IsInstanceOf<IAction>(action);
            Assert.IsInstanceOf<IAction>(action);
        }
    }
}
