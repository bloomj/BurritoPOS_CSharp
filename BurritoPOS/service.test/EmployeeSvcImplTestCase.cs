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
    /// Unit test fixture for employee service implementation unit tests
    /// </summary>
    [TestFixture]
    class EmployeeSvcImplTestCase
    {
        private Factory factory;
        private Employee e;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        protected void SetUp()
        {
            factory = Factory.getInstance();
            e = new Employee("Jim", "Bloom", 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        protected void tearDown()
        {

        }

        /// <summary>
        /// Unit test for employee service implementation
        /// </summary>
        [Test]
        public void testStoreEmployee()
        {
		    try {
                //week 3
                //IEmployeeSvc ics = factory.getEmployeeSvc();

                //week 4
                IEmployeeSvc ics = (IEmployeeSvc)factory.getService("IEmployeeSvc");

                // First let's store the Employee
                Assert.True(ics.storeEmployee(e));

                // Then let's read it back in
                e = ics.getEmployee(e.employeeID);
                Assert.True(e.validate());

                // Update Employee
                e.lastName = "Smith";
                Assert.True(ics.storeEmployee(e));

                // Finally, let's cleanup the file that was created
                Assert.True(ics.deleteEmployee(e.employeeID));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testStoreEmployee: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
