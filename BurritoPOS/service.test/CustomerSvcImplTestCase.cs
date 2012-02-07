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
    /// Unit test fixture for customer service implementation unit tests
    /// </summary>
    [TestFixture]
    class CustomerSvcImplTestCase
    {
        private Factory factory;
        private Customer c;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        protected void SetUp()
        {
            factory = Factory.getInstance();
            c = new Customer(1, "Jim", "Bloom", "jim@gmail.com");
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        protected void tearDown()
        {

        }

        /// <summary>
        /// Unit test for customer service implementation
        /// </summary>
        [Test]
	    public void testCustomerSvc() {
		    try {
			    //week 3
			    //ICustomerSvc ics = factory.getCustomerSvc();
			
			    //week 4
			    ICustomerSvc ics = (ICustomerSvc) factory.getService("ICustomerSvc"); 
			
			    // First let's store the Customer
                Assert.True(ics.storeCustomer(c));
			
			    // Then let's read it back in
			    c = ics.getCustomer(c.id);
                Assert.True(c.validate());
			
			    // Finally, let's cleanup the file that was created
                Assert.True(ics.deleteCustomer(c.id));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testStoreCustomer: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
	
        /// <summary>
        /// Unit test for customer service implementation (invalid customer)
        /// </summary>
	    [Test]
        public void testInvalidGetCustomer() {
		    try {
			    //week 3
			    //ICustomerSvc ics = factory.getCustomerSvc();
			
			    //week 4
			    ICustomerSvc ics = (ICustomerSvc) factory.getService("ICustomerSvc");
			    c = ics.getCustomer(1234);
                        
                if(c != null)
                    Assert.False(c.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testInvalidGetCustomer: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);			
		    }
	    }
    }
}
