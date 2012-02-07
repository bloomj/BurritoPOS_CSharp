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
    class InventoryTestCase
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
        public void testValidateInventory() {
		    try {
			    Inventory i = new Inventory();
                i.id = 1;
			    i.setBeefQty(50);
			    i.setBlackBeansQty(50);
			    i.setBrownRiceQty(50);
			    i.setChickenQty(50);
			    i.setChiliTortillaQty(50);
			    i.setCucumberQty(50);
			    i.setFlourTortillaQty(50);
			    i.setGuacamoleQty(20);
			    i.setHerbGarlicTortillaQty(50);
			    i.setHummusQty(50);
			    i.setJalapenoCheddarTortillaQty(50);
			    i.setLettuceQty(50);
			    i.setOnionQty(50);
			    i.setPintoBeansQty(50);
			    i.setSalsaPicoQty(50);
			    i.setSalsaSpecialQty(50);
			    i.setSalsaVerdeQty(50);
			    i.setTomatoBasilTortillaQty(50);
			    i.setTomatoesQty(50);
			    i.setWheatTortillaQty(50);
			    i.setWhiteRiceQty(50);
			    i.setJalapenosQty(50);

                Assert.True(i.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testValidateInventory: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testInvalidInventory() {
		    try {
			    Inventory i = new Inventory();

                Assert.False(i.validate());
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testInvalidInventory: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testEqualsInventory() {
		    try {
                Inventory i = new Inventory(1, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50);
			    Inventory j = i;

                Assert.True(i.Equals(j));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testEqualsInventory: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }

        [Test]
	    public void testNotEqualsInventory() {
		    try {
                Inventory i = new Inventory(1, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50);
                Inventory j = new Inventory(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                Assert.False(i.Equals(j));
		    }
		    catch(Exception e) {
                Console.WriteLine("Exception in testNotEqualsInventory: " + e.Message + "\n" + e.StackTrace);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
		    }
	    }
    }
}
