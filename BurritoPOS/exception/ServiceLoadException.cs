using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurritoPOS.exception
{
    class ServiceLoadException : Exception
    {
        public ServiceLoadException(string errorMessage)
                             : base(errorMessage) {}

        public ServiceLoadException(string errorMessage, Exception innerEx)
                             : base(errorMessage, innerEx) {}
    }
}
