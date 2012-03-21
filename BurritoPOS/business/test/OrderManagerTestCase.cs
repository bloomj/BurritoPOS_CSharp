using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using log4net.Config;
using NUnit.Framework;
using System.Numerics;
using Spring;
using Spring.Context;
using Spring.Context.Support;
using BurritoPOS.business;
using BurritoPOS.domain;

namespace BurritoPOS.business.test
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    class OrderManagerTestCase
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private OrderManager oManager;
        private BurritoManager bManager;
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
            dLog.Info("Beginning OrderManagerTestCase Setup");
            rand = new Random();

            try {
                //oManager = new OrderManager();
                //bManager = new BurritoManager();

                //Spring.NET
                XmlApplicationContext ctx = new XmlApplicationContext("config/spring.cfg.xml");
                oManager = (OrderManager)ctx.GetObject("OrderManager");
                bManager = (BurritoManager)ctx.GetObject("BurritoManager");

                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
                b.Price = bManager.calculatePrice(b);
            }
            catch (Exception e)
            {
                dLog.Error("Unable to initialize service/domain objects: " + e.Message + "\n" + e.StackTrace);
            }
            dLog.Info("Finishing OrderManagerTestCase Setup");
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
        public void testCreateOrder()
        {
            try
            {
                dLog.Info("Start testCreateOrder");
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                oManager.addBurritoToOrder(o, b);
                o.totalCost = oManager.calculateTotal(o);

                Assert.True(oManager.createOrder(o));

                Assert.True(oManager.deleteOrder(o));

                dLog.Info("Finish testCreateOrder");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testCreateOrder: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testAddBurritoToOrder()
        {
            try
            {
                dLog.Info("Start testAddBurritoToOrder");
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
                b.Price = bManager.calculatePrice(b);
                oManager.addBurritoToOrder(o, b);
                o.totalCost = oManager.calculateTotal(o);

                Assert.True(oManager.createOrder(o));

                Assert.True(oManager.addBurritoToOrder(o, b));

                Assert.True(oManager.deleteOrder(o));

                dLog.Info("Finish testAddBurritoToOrder");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testAddBurritoToOrder: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testEditBurritoToOrder()
        {
            try
            {
                dLog.Info("Start testEditBurritoToOrder");
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
                b.Price = bManager.calculatePrice(b);
                oManager.addBurritoToOrder(o, b);
                o.totalCost = oManager.calculateTotal(o);

                Assert.True(oManager.createOrder(o));

                Assert.True(oManager.addBurritoToOrder(o, b));

                b.BlackBeans = nextBool();
                b.Guacamole = nextBool();
                b.WhiteRice = nextBool();

                Assert.True(oManager.updateBurritoInOrder(o, b));

                Assert.True(oManager.deleteOrder(o));

                dLog.Info("Finish testEditBurritoToOrder");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testEditBurritoToOrder: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testRemoveBurritoFromOrder()
        {
            try
            {
                dLog.Info("Start testRemoveBurritoFromOrder");
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
                b.Price = bManager.calculatePrice(b);
                oManager.addBurritoToOrder(o, b);
                o.totalCost = oManager.calculateTotal(o);

                Assert.True(oManager.createOrder(o));

                Assert.True(oManager.addBurritoToOrder(o, b));

                Assert.True(oManager.removeBurritoFromOrder(o, b));

                Assert.True(oManager.deleteOrder(o));

                dLog.Info("Finish testRemoveBurritoFromOrder");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testRemoveBurritoFromOrder: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testInvalidRemoveBurritoFromOrder()
        {
            try
            {
                dLog.Info("Start testInvalidRemoveBurritoFromOrder");
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
                b.Price = bManager.calculatePrice(b);
                oManager.addBurritoToOrder(o, b);

                Assert.True(oManager.createOrder(o));

                b = new Burrito();

                Assert.False(oManager.removeBurritoFromOrder(o, b));

                Assert.True(oManager.deleteOrder(o));

                dLog.Info("Finish testInvalidRemoveBurritoFromOrder");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testInvalidRemoveBurritoFromOrder: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testSubmitOrder()
        {
            try
            {
                dLog.Info("Start testSubmitOrder");
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
                b.Price = bManager.calculatePrice(b);
                oManager.addBurritoToOrder(o, b);
                o.totalCost = oManager.calculateTotal(o);

                Assert.True(oManager.createOrder(o));

                dLog.Debug("Order ID: " + o.id);

                Assert.True(oManager.addBurritoToOrder(o, b));

                o.totalCost = oManager.calculateTotal(o);
                if (o.totalCost == -1)
                    Assert.Fail("Invalid total calculated for order");

                Assert.True(oManager.submitOrder(o));

                Assert.True(oManager.cancelOrder(o));

                dLog.Info("Finish testSubmitOrder");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testSubmitOrder: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testInvalidSubmitOrder()
        {
            try
            {
                dLog.Info("Start testInvalidSubmitOrder");
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
                b.Price = bManager.calculatePrice(b);
                oManager.addBurritoToOrder(o, b);

                Assert.True(oManager.createOrder(o));

                dLog.Debug("Order ID: " + o.id);

                Assert.True(oManager.addBurritoToOrder(o, b));

                Assert.False(oManager.submitOrder(o));

                Assert.True(oManager.cancelOrder(o));

                dLog.Info("Finish testInvalidSubmitOrder");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testInvalidSubmitOrder: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testCalculateTotal()
        {
            try
            {
                dLog.Info("Start testCalculateTotal");
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                oManager.addBurritoToOrder(o, b);
                o.totalCost = oManager.calculateTotal(o);

                Assert.True(oManager.createOrder(o));

                dLog.Debug("Order ID: " + o.id);

                //randomly create some burritos to add to order
                for (int n = 0; n < rand.Next(2, 7); n++)
                {
                    b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
                    b.Price = bManager.calculatePrice(b);
                    Assert.True(oManager.addBurritoToOrder(o, b));
                }

                o.totalCost = oManager.calculateTotal(o);
                dLog.Debug("Total cost of order: " + o.totalCost + " | Number of burritos: " + o.burritos.Count);
                if (o.totalCost == -1)
                    Assert.Fail("Invalid total calculated for order");

                Assert.True(oManager.submitOrder(o));

                Assert.True(oManager.cancelOrder(o));

                dLog.Info("Finish testCalculateTotal");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testCalculateTotal: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testgetOrderHistories()
        {
            try
            {
                dLog.Info("Start testgetOrderHistories");
                o = new Order(rand.Next(), new List<Burrito>(), new DateTime(), false, false, new Decimal(0));
                oManager.addBurritoToOrder(o, b);
                o.totalCost = oManager.calculateTotal(o);

                Assert.True(oManager.createOrder(o));

                Assert.NotNull(oManager.getOrderHistories());

                Assert.True(oManager.cancelOrder(o));

                dLog.Info("Finish testgetOrderHistories");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testgetOrderHistories: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        private Boolean nextBool()
        {
            return (rand.Next(0, 2) == 1) ? true : false;
        }
    }
}
