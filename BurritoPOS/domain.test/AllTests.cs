using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BurritoPOS.domain.test
{
    class AllTests
    {
        [Suite]
        public static IEnumerable Suite
        {
            get
            {
                ArrayList suite = new ArrayList();
                suite.Add(new BurritoTestCase());
                suite.Add(new CustomerTestCase());
                suite.Add(new EmployeeTestCase());
                suite.Add(new InventoryTestCase());
                suite.Add(new ManagerTestCase());
                suite.Add(new OrderTestCase());
                return suite;
            }
        }
    }
}
