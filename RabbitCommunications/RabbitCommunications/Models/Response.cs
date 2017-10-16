using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitCommunications.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succes { get; set; }
        public List<Exception> ExceptionList { get; set; }
        public bool HaveException
        {
            get
            {
                if (ExceptionList != null)
                {
                    return ExceptionList.Count != 0;
                }
                return false;
            }
        }

        public Exception GetError()
        {
            if (!HaveException) return null;

            return ExceptionList.First();
        }
    }
}
