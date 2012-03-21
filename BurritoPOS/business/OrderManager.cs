using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using log4net;
using log4net.Config;
using BurritoPOS.service;
using BurritoPOS.domain;

namespace BurritoPOS.business
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderManager : FactoryManager
    {
        //private Factory factory = Factory.getInstance();
        private IOrderSvc orderSvc;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        public OrderManager()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));

            //will comment out after Spring.NET implementation
            //orderSvc = (IOrderSvc)factory.getService("IOrderSvc");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderSvc"></param>
        public void setOrderSvc(IOrderSvc orderSvc)
        {
            this.orderSvc = orderSvc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ArrayList getOrderHistories()
        {
            return orderSvc.getAllOrders();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public Boolean createOrder(Order o)
        {
            dLog.Debug("In createOrder");
            Boolean result = false;

            try
            {
                dLog.Debug("Validating order object");
                if (o.validate())
                {
                    if (orderSvc.storeOrder(o))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in createOrder: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("createOrder result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public Boolean updateOrder(Order o)
        {
            dLog.Debug("In updateOrder");
            Boolean result = false;

            try
            {
                if (o.validate())
                {
                    if (orderSvc.storeOrder(o))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in updateOrder: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("updateOrder result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public Boolean deleteOrder(Order o)
        {
            dLog.Debug("In deleteOrder");
            Boolean result = false;

            try
            {
                if (o.validate())
                {
                    if (orderSvc.deleteOrder(o.id))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in deleteOrder: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("deleteOrder result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Boolean addBurritoToOrder(Order o, Burrito b)
        {
            dLog.Debug("In addBurritoToOrder");
            Boolean result = false;

            try
            {
                if (b.validate())
                {
                    o.burritos.Add(b);

                    if (orderSvc.storeOrder(o))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in addBurritoToOrder: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("addBurritoToOrder result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Boolean updateBurritoInOrder(Order o, Burrito b)
        {
            dLog.Debug("In updateBurritoInOrder");
            Boolean result = false;

            try
            {
                if (b.validate())
                {
                    var idx = 0;
                    foreach (Burrito b1 in o.burritos)
                    {
                        if (b1.id == b.id)
                        {
                            o.burritos[idx] = b;
                            result = true;
                            break;
                        }

                        idx++;
                    }

                    if (orderSvc.storeOrder(o))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in updateBurritoInOrder: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("updateBurritoInOrder result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Boolean removeBurritoFromOrder(Order o, Burrito b)
        {
            dLog.Debug("In removeBurritoFromOrder");
            Boolean result = false;

            try
            {
                if (b.validate())
                {
                    o.burritos.Remove(b);

                    if (orderSvc.storeOrder(o))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in updateBurritoInOrder: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("removeBurritoFromOrder result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public Boolean submitOrder(Order o)
        {
            dLog.Debug("In submitOrder");
            Boolean result = false;

            try
            {
                if (o.validate())
                {
                    dLog.Debug("Order ID: " + o.id + " | Size: " + o.burritos.Count + " | Total Cost: " + o.totalCost);

                    // ensure we have at least one burrito and the cost of the burritos has been calculated
                    if (o.burritos.Count > 0 && o.totalCost > 0)
                    {
                        // set order submitted flag
                        o.isSubmitted = true;

                        // store updated Order
                        if (orderSvc.storeOrder(o))
                            result = true;
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in submitOrder: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("submitOrder result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public Boolean cancelOrder(Order o)
        {
            dLog.Debug("In cancelOrder");
            Boolean result = false;

            try
            {
                if (o.validate())
                {
                    if (orderSvc.deleteOrder(o.id))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in cancelOrder: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("cancelOrder result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public Decimal calculateTotal(Order o)
        {
            dLog.Debug("In calculateTotal");
            Decimal result = new Decimal(-1);

            try
            {
                result = new Decimal(0);

                // loop through all burritos on the order
                foreach (Burrito b in o.burritos)
                {
                    //check for valid burrito
                    if (b.validate())
                    {
                        // first determine base type of Beef, Chicken, or Hummus by order of cost then determine extras
                        if (b.Beef)
                        {
                            result = result + Prices.getItemPrice("BeefBurrito");

                            //add extra main
                            if (b.Chicken)
                                result = result + Prices.getItemPrice("ExtraMeat");
                            if (b.Hummus)
                                result = result + Prices.getItemPrice("ExtraBeans");
                        }
                        else if (b.Chicken)
                        {
                            result = result + Prices.getItemPrice("BeefBurrito");

                            if (b.Hummus)
                                result = result + Prices.getItemPrice("ExtraBeans");
                        }
                        else if (b.Hummus)
                        {
                            result = result + Prices.getItemPrice("HummusBurrito");
                        }

                        // calculate remaining extras
                        if (b.ChiliTortilla || b.HerbGarlicTortilla || b.JalapenoCheddarTortilla || b.TomatoBasilTortilla || b.WheatTortilla)
                            result = result + Prices.getItemPrice("FlavoredTortilla");

                        if (b.WhiteRice && b.BrownRice)
                            result = result + Prices.getItemPrice("ExtraRice");

                        if (b.BlackBeans && b.PintoBeans)
                            result = result + Prices.getItemPrice("ExtraBeans");

                        if (b.SalsaPico && b.SalsaSpecial && b.SalsaVerde)
                            result = result + Prices.getItemPrice("ExtraSalsa");

                        if (b.Guacamole)
                            result = result + Prices.getItemPrice("Guacamole");
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in calculateTotal: " + e.Message + "\n" + e.StackTrace);
                result = new Decimal(-1);
            }

            dLog.Debug("calculateTotal result: " + result);
            return result;
        }
    }
}
