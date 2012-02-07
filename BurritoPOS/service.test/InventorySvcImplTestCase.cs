using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NUnit.Framework;
using System.Numerics;
using BurritoPOS.domain;
using BurritoPOS.service;

namespace BurritoPOS.service.test
{
    /// <summary>
    /// Unit test fixture for inventory service implementation unit tests
    /// </summary>
    [TestFixture]
    class InventorySvcImplTestCase
    {
        private Factory factory;
        private Inventory i;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        protected void SetUp()
        {
            factory = Factory.getInstance();
            i = new Inventory(1, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50);
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        protected void tearDown()
        {

        }

        /// <summary>
        /// Unit test for inventory service implementation
        /// </summary>
        [Test]
        public void testStoreInventory()
        {
		    try {
                //week 3
                //IInventorySvc ics = factory.getInventorySvc();

                //week 4
                IInventorySvc ics = (IInventorySvc)factory.getService("IInventorySvc");

                // First let's store the Inventory
                Assert.True(ics.storeInventory(i));

                // Then let's read it back in
                i = ics.getInventory(i.id);
                Assert.True(i.validate());

                // Finally, let's cleanup the file that was created
                Assert.True(ics.deleteInventory(i.id));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testStoreInventory: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
