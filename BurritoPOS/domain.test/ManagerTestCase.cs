using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Numerics;
using BurritoPOS.domain;

namespace BurritoPOS.domain.test
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    class ManagerTestCase
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
        public void testValidateManager() {
		    try {
			    Manager m = new Manager();
			    m.employeeID = 1;
			    m.firstName = "Jim";
			    m.lastName = "Bloom";

                Assert.True(m.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testValidateManager: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testInvalidManager() {
		    try {
			    Manager m = new Manager();

                Assert.False(m.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testInvalidManager: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testEqualsManager() {
		    try {
			    Manager m = new Manager("Jim","Bloom",1);
			    Manager n = m;

                Assert.True(m.Equals(n));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testEqualsManager: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testNotEqualsManager() {
		    try {
			    Manager m = new Manager("Jim","Bloom",1);
			    Manager n = new Manager();

                Assert.False(m.Equals(n));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testNotEqualsManager: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
