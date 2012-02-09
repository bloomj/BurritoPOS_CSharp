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
    /// Unit test fixture for manager service implementation unit tests
    /// </summary>
    [TestFixture]
    class ManagerSvcImplTestCase
    {
        private Factory factory;
        private Manager m;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        protected void SetUp()
        {
            factory = Factory.getInstance();
            m = new Manager("Jim", "Bloom", 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        protected void tearDown()
        {

        }

        /// <summary>
        /// Unit test for manager service implementation
        /// </summary>
        [Test]
        public void testStoreManager() {
		    try {
			    //week 3
			    //IManagerSvc ics = factory.getManagerSvc();
			
			    //week 4
			    IManagerSvc ics = (IManagerSvc) factory.getService("IManagerSvc");
			
			    // First let's store the Inventory
			    Assert.True(ics.storeManager(m));
			
			    // Then let's read it back in
                m = ics.getManager(m.employeeID);
			    Assert.True(m.validate());

                // Update Manager
                m.lastName = "Smith";
                Assert.True(ics.storeManager(m));
			
			    // Finally, let's cleanup the file that was created
			    Assert.True(ics.deleteManager(m.employeeID));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testStoreManager: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
