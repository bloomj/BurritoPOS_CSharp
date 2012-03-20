using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using log4net.Config;
using NUnit.Framework;
using System.Numerics;
using BurritoPOS.business;
using BurritoPOS.domain;

namespace BurritoPOS.business.test
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    class InventoryManagerTestCase
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private InventoryManager iManager;
        private OrderManager oManager;
        private Inventory i;
        private Order o;
        private Burrito b;
        private Random rand;

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        protected void SetUp()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            dLog.Info("Beginning InventoryManagerTestCase Setup");
            rand = new Random();

            try {
                iManager = new InventoryManager();
                oManager = new OrderManager();
                i = new Inventory(rand.Next(), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250));
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
            }
            catch (Exception e)
            {
                dLog.Error("Unable to initialize service/domain objects: " + e.Message + "\n" + e.StackTrace);
            }
            dLog.Info("Finishing InventoryManagerTestCase Setup");
        }

        /// <summary>
        /// 
        /// </summary> 
        [TearDown]
        protected void tearDown()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testCreateInventory()
        {
            try
            {
                dLog.Info("Start testCreateInventory");
                i = new Inventory(rand.Next(), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250));

                Assert.True(iManager.createInventory(i));

                Assert.True(iManager.deleteInventory(i));

                dLog.Info("Finish testCreateInventory");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testCreateInventory: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testUpdateInventory()
        {
            try
            {
                dLog.Info("Start testUpdateInventory");
                i = new Inventory(rand.Next(), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250));

                Assert.True(iManager.createInventory(i));

                i.setBeefQty(i.BeefQty - 5);
                i.setChickenQty(i.ChickenQty + 23);
                i.setHerbGarlicTortillaQty(i.HerbGarlicTortillaQty - 3);

                Assert.True(iManager.updateInventory(i));

                Assert.True(iManager.deleteInventory(i));

                dLog.Info("Finish testUpdateInventory");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testUpdateInventory: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testRemoveFromInventoryOrder()
        {
            try
            {
                dLog.Info("Start testRemoveFromInventoryOrder");
                i = new Inventory(rand.Next(), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250));
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));

                Assert.True(iManager.createInventory(i));

                //randomly create burritos to remove from inventory
                for(int n=0; n< rand.Next(2,7); n++)
                    oManager.addBurritoToOrder(o, new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble())));

                Assert.True(iManager.removeFromInventory(i, o));

                Assert.True(iManager.deleteInventory(i));

                dLog.Info("Finish testRemoveFromInventoryOrder");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testRemoveFromInventoryOrder: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testReturnToInventoryOrder()
        {
            try
            {
                dLog.Info("Start testReturnToInventoryOrder");
                i = new Inventory(rand.Next(), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250));
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));

                Assert.True(iManager.createInventory(i));

                //randomly create burritos to remove from inventory
                for (int n = 0; n < rand.Next(2, 7); n++)
                    oManager.addBurritoToOrder(o, new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble())));

                Assert.True(iManager.returnToInventory(i, o));

                Assert.True(iManager.deleteInventory(i));

                dLog.Info("Finish testReturnToInventoryOrder");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testReturnToInventoryOrder: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testRemoveFromInventoryBurrito()
        {
            try
            {
                dLog.Info("Start testRemoveFromInventoryBurrito");
                i = new Inventory(rand.Next(), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));

                Assert.True(iManager.createInventory(i));

                Assert.True(iManager.removeFromInventory(i, b));

                Assert.True(iManager.deleteInventory(i));

                dLog.Info("Finish testRemoveFromInventoryBurrito");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testRemoveFromInventoryBurrito: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testReturnToInventoryBurrito()
        {
            try
            {
                dLog.Info("Start testReturnToInventoryBurrito");
                i = new Inventory(rand.Next(), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));

                Assert.True(iManager.createInventory(i));

                Assert.True(iManager.returnToInventory(i, b));

                Assert.True(iManager.deleteInventory(i));

                dLog.Info("Finish testReturnToInventoryBurrito");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testReturnToInventoryBurrito: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        private Boolean nextBool()
        {
            return (rand.Next(0, 2) == 1) ? true : false;
        }
    }
}
