using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NUnit.Framework;
using System.Collections;
using BurritoPOS.domain;
using BurritoPOS.service;

namespace BurritoPOS.service.test
{
    /// <summary>
    /// Unit test fixture for order service implementation unit tests
    /// </summary>
    [TestFixture]
    class OrderSvcImplTestCase
    {
        private Factory factory;
        private Order o;
        private Burrito b;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        protected void SetUp()
        {
            factory = Factory.getInstance();
            o = new Order(1, new List<Burrito>(), DateTime.Now, false, false, Decimal.Parse("17.00"));
            b = new Burrito(1, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, Decimal.Parse("3.00"));
            o.burritos.Add(b);
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        protected void tearDown()
        {

        }

        /// <summary>
        /// Unit test for order service implementation
        /// </summary>
        [Test]
        public void testStoreOrder()
        {
		    try {
                //week 3
                //IOrderSvc ics = factory.getOrderSvc();

                //week 4
                IOrderSvc ics = (IOrderSvc)factory.getService("IOrderSvc");

                // First let's store the Order
                Assert.True(ics.storeOrder(o));

                // Then let's read it back in
                o = ics.getOrder(o.id);
                Assert.True(o.validate());

                // Update the Order
                o.isComplete = true;
                o.isSubmitted = true;
                Assert.True(ics.storeOrder(o));

                // Finally, let's cleanup the file that was created
                Assert.True(ics.deleteOrder(o.id));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testStoreOrder: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
