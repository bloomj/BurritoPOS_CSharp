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
    class EmployeeTestCase
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
        public void testValidateEmployee() {
		    try {
			    Employee e = new Employee();
                e.id = 1;
			    e.firstName = "Jim";
			    e.lastName = "Bloom";

                Assert.True(e.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testValidateEmployee: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testInvalidEmployee() {
		    try {
			    Employee e = new Employee();

                Assert.False(e.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testInvalidEmployee: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testEqualsEmployee() {
		    try {
                Employee e = new Employee("Jim", "Bloom", 1);
			    Employee d = e;

                Assert.True(e.Equals(d));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testEqualsEmployee: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testNotEqualsEmployee() {
		    try {
			    Employee e = new Employee("Jim","Bloom",1);
			    Employee d = new Employee();

                Assert.False(e.Equals(d));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testNotEqualsEmployee: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
