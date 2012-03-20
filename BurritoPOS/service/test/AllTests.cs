using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BurritoPOS.service.test
{
    class AllTests
    {
        [Suite]
        public static IEnumerable Suite
        {
            get
            {
                ArrayList suite = new ArrayList();
                suite.Add(new BurritoSvcImplTestCase());
                suite.Add(new CustomerSvcImplTestCase());
                suite.Add(new EmployeeSvcImplTestCase());
                suite.Add(new InventorySvcImplTestCase());
                suite.Add(new ManagerSvcImplTestCase());
                suite.Add(new OrderSvcImplTestCase());
                return suite;
            }
        }
    }
}
