using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurritoPOS.exception
{
    class InsufficientInventoryException : Exception
    {
        public InsufficientInventoryException(string errorMessage)
                             : base(errorMessage) {}

        public InsufficientInventoryException(string errorMessage, Exception innerEx)
                             : base(errorMessage, innerEx) {}
    }
}
