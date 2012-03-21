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
    class BurritoManagerTestCase
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private BurritoManager bManager;
        private Burrito b;
        private Random rand;

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        protected void SetUp()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            dLog.Info("Beginning BurritoManagerTestCase Setup");
            rand = new Random();

            try
            {
                //bManager = new BurritoManager();
                
                //Spring.NET
                XmlApplicationContext ctx = new XmlApplicationContext("config/spring.cfg.xml");
                bManager = (BurritoManager)ctx.GetObject("BurritoManager");
                
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
            }
            catch (Exception e)
            {
                dLog.Error("Unable to initialize service/domain objects: " + e.Message + "\n" + e.InnerException + "\n" + e.StackTrace);
            }
            dLog.Info("Finishing BurritoManagerTestCase Setup");
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
        public void testCreateBurrito()
        {
            try
            {
                dLog.Info("Start testCreateBurrito");
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));
                b.Price = bManager.calculatePrice(b);
                dLog.Debug("Burrito Price: " + b.Price);

                Assert.True(bManager.createBurrito(b));

                Assert.True(bManager.deleteBurrito(b));

                dLog.Info("Finish testCreateBurrito");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testCreateBurrito: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testUpdateBurrito()
        {
            try
            {
                dLog.Info("Start testUpdateBurrito");
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));

                Assert.True(bManager.createBurrito(b));

                b.Beef = nextBool();
                b.ChiliTortilla = nextBool();
                b.BlackBeans = nextBool();
                b.Guacamole = nextBool();
                b.PintoBeans = nextBool();
                b.SalsaSpecial = nextBool();

                Assert.True(bManager.updateBurrito(b));

                Assert.True(bManager.deleteBurrito(b));

                dLog.Info("Finish testUpdateBurrito");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testUpdateBurrito: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testCalculatePrice()
        {
            try
            {
                dLog.Info("Start testCalculatePrice");
                b = new Burrito(rand.Next(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), nextBool(), new Decimal(rand.NextDouble()));

                Assert.True(bManager.createBurrito(b));

                b.Price = bManager.calculatePrice(b);
                if (b.Price == -1)
                    Assert.Fail("Invalid price calculated for Burrito");

                Assert.True(bManager.deleteBurrito(b));

                dLog.Info("Finish testCalculatePrice");
            }
            catch (Exception e)
            {
                dLog.Error("Exception in testCalculatePrice: " + e.Message);
                Assert.Fail(e.Message + "\n" + e.StackTrace);
            }
        }

        private Boolean nextBool()
        {
            return (rand.Next(0, 2) == 1) ? true : false;
        }
    }
}
