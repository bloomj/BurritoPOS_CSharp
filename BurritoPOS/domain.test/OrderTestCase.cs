using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections;
using BurritoPOS.domain;

namespace BurritoPOS.domain.test
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    class OrderTestCase
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testValidateOrder() {
		    try {
			    Order o = new Order();
			    o.burritos = new ArrayList();
                o.burritos.Add(new Burrito(1, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, Decimal.Parse("0.00")));
			    o.orderDate = DateTime.Now;
			    o.isComplete = true;
			    o.isSubmitted = true;
			    o.orderID = 1;
			    o.totalCost = Decimal.Parse("17.00");

                Assert.True(o.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testValidateOrder: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        /// <summary>
        /// 
        /// </summary>
        [Test]
	    public void testInvalidOrder() {
		    try {
			    Order o = new Order();

                Assert.False(o.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testInvalidOrder: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        /// <summary>
        /// 
        /// </summary>
        [Test]
	    public void testEqualsOrder() {
		    try {
			    Order o = new Order(1,new ArrayList(),DateTime.Now, false,false,Decimal.Parse("17.00"));
			    Order p = o;

                Assert.True(o.Equals(p));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testEqualsOrder: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        /// <summary>
        /// 
        /// </summary>
        [Test]
	    public void testNotEqualsOrder() {
		    try {
			    Order o = new Order(1,new ArrayList(),DateTime.Now, false,false,Decimal.Parse("17.00"));
			    Order p= new Order();

                Assert.False(o.Equals(p));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testNotEqualsOrder: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
