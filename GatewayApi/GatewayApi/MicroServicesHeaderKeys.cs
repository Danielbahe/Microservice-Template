using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi
{
    public class MicroServicesHeaderKeys : IMicroServicesHeaderKeys
    {
        private readonly IEnumerable<string> HeadersKeyList;

        /// <summary>
        /// Default header keys provided by developer handmade.
        /// </summary>
        public MicroServicesHeaderKeys()
        {
            HeadersKeyList = new List<string> {
                "xamarin",
                "microservice",
                "bla"
            };
        }

        /// <summary>
        /// Get a list of header keys privded by an external site.
        /// </summary>
        /// <param name="list of headers keys"></param>
        public MicroServicesHeaderKeys(IEnumerable<string> keyList)
        {
            HeadersKeyList = keyList;
        }

        /// <summary>
        /// Return true if contains one of the keys
        /// </summary>
        /// <param name="listKey">list of header keys</param>
        /// <returns></returns>
        public bool ContainsAnyKey(List<string> listKey)
        {
            foreach (var key in listKey)
            {
                if (HeadersKeyList.Contains(key)) return true;
            }
            return false;
        }
    }

    public interface IMicroServicesHeaderKeys
    {
        bool ContainsAnyKey(List<string> listKey);
        
    }
}
