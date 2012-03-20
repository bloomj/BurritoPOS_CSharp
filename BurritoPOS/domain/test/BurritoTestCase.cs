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
    class BurritoTestCase
    {
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        protected void SetUp()
        {
            
        }

        /// <summary>
        /// 
        /// </summary> 
        [TearDown]
        protected void tearDown()
        {

        }

        [Test]
        public void testValidBurrito()
        {
            try {
			    Burrito b = new Burrito();
			    b.id = 1;
			    b.Beef = true;
			    b.BlackBeans = true;
			    b.BrownRice = true;
			    b.Chicken = false;
			    b.ChiliTortilla = false;
			    b.Cucumber = true;
			    b.FlourTortilla = false;
			    b.Guacamole = true;
			    b.HerbGarlicTortilla = true;
			    b.Hummus = false;
			    b.JalapenoCheddarTortilla = false;
			    b.Jalapenos = true;
			    b.Lettuce = true;
			    b.Onion = false;
			    b.PintoBeans = false;
			    b.SalsaPico = true;
			    b.SalsaSpecial = false;
			    b.SalsaVerde = false;
			    b.TomatoBasilTortilla = false;
			    b.Tomatoes = false;
			    b.WheatTortilla = false;
			    b.WhiteRice = false;
                b.Price = Decimal.Parse("3.00");
			
			    Assert.True(b.validate());
		    }
		    catch(Exception e) {
			    Console.WriteLine("Exception in testValidBurrito: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
        }

        [Test]
        public void testInvalidBurrito()
        {
            try {
			    Burrito b = new Burrito();
			
			    Assert.False(b.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testInvalidBurrito: " + e.Message + "\n" + e.StackTrace);
			    Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
        }

        [Test]
        public void testEqualsObject() {
		    try {
                Burrito b = new Burrito(1, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, Decimal.Parse("0.00"));
			    Burrito c = b;

                Assert.True(b.Equals(c));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testEqualsObject: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testNotEqualsBurrito() {
		    try {
                Burrito b = new Burrito(1, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, Decimal.Parse("0.00"));
			    Burrito c = new Burrito();

                Assert.False(b.Equals(c));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testNotEqualsBurrito: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
