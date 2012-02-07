using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Numerics;
using BurritoPOS.domain;

namespace BurritoPOS.domain.test
{
    [TestFixture]
    class CustomerTestCase
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [SetUp]
        protected void SetUp()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [TearDown]
        protected void tearDown()
        {

        }

        [Test]
        public void testValidate() {
		    try {
			    Customer c = new Customer();
                c.id = 1;
			    c.emailAddress = "jim@gmail.com";
			    c.firstName = "Jim";
			    c.lastName = "Bloom";

                Assert.True(c.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testValidate: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testInvalidCustomer() {
		    try {
			    Customer c = new Customer();

                Assert.False(c.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testInvalidCustomer: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testEqualsObject() {
		    try {
                Customer c = new Customer(1, "Jim", "Bloom", "jim@gmail.com");
			    Customer d = c;

                Assert.True(c.Equals(d));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testEqualsObject: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testNotEqualsCustomer() {
		    try {
                Customer c = new Customer(1, "Jim", "Bloom", "jim@gmail.com");
			    Customer d = new Customer();

                Assert.False(c.Equals(d));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testNotEqualsCustomer: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
