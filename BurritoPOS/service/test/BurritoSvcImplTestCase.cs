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
    /// Unit test fixture for burrito service implementation unit tests
    /// </summary>
    [TestFixture]
    class BurritoSvcImplTestCase
    {
        private Factory factory;
	    private Burrito b;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        protected void SetUp()
        {
            factory = Factory.getInstance();
            b = new Burrito(1, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, Decimal.Parse("3.00"));
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        protected void tearDown()
        {

        }

        /// <summary>
        /// Unit test for burrito service implementation
        /// </summary>
        [Test]
        public void testBurritoSvc()
        {
            try {
			    //week 3
			    //IBurritoSvc ics = factory.getBurritoSvc();
			
			    //week 4
                dLog.Debug("Going to get the service implementation");
			    IBurritoSvc ics = (IBurritoSvc) factory.getService("IBurritoSvc");

                dLog.Debug("Going to create burrito");
			    // First let's store the Burrito
                Assert.True(ics.storeBurrito(b));

                dLog.Debug("Going to read burrito");
			    // Then let's read it back in
			    b = ics.getBurrito(b.id);
                Assert.True(b.validate());

                // Update burrito
                dLog.Debug("Going to update burrito");
                b.Beef = false;
                b.Hummus = true;
                Assert.True(ics.storeBurrito(b));

                dLog.Debug("Going to delete burrito");
			    // Finally, let's cleanup the file that was created
                Assert.True(ics.deleteBurrito(b.id));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testBurritoSvc: " + e.Message + "\n" + e.StackTrace);
			    Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
