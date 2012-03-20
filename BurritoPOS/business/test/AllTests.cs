using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BurritoPOS.business.test
{
    class AllTests
    {
        [Suite]
        public static IEnumerable Suite
        {
            get
            {
                ArrayList suite = new ArrayList();
                suite.Add(new BurritoManagerTestCase());
                suite.Add(new InventoryManagerTestCase());
                suite.Add(new OrderManagerTestCase());
                return suite;
            }
        }
    }
}
