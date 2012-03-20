using System;
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
    public class BurritoManager : FactoryManager
    {
        //private Factory factory = Factory.getInstance();
        private IBurritoSvc burritoSvc;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        public BurritoManager()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));

            //will comment out after Spring.NET implementation
            burritoSvc = (IBurritoSvc) factory.getService("IBurritoSvc");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="burritoSvc"></param>
        public void setBurritoSvc(IBurritoSvc burritoSvc)
        {
            this.burritoSvc = burritoSvc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Boolean createBurrito(Burrito b)
        {
            dLog.Debug("In createBurrito");
            Boolean result = false;

            try
            {
                dLog.Debug("Validating burrito object");
                if (b.validate())
                {
                    if (burritoSvc.storeBurrito(b))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in createBurrito: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("createBurrito result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Boolean updateBurrito(Burrito b)
        {
            Boolean result = false;

            try
            {
                if (b.validate())
                {
                    if (burritoSvc.storeBurrito(b))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in updateBurrito: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Boolean deleteBurrito(Burrito b)
        {
            Boolean result = false;

            try
            {
                if (b.validate())
                {
                    if (burritoSvc.deleteBurrito(b.id))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in deleteBurrito: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Decimal calculatePrice(Burrito b)
        {
            dLog.Debug("In calculatePrice");
            Decimal result = new Decimal(-1);

            try
            {
                //check for valid burrito
                if (b.validate())
                {
                    result = new Decimal(0);

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
            catch (Exception e)
            {
                dLog.Debug("Exception in calculatePrice: " + e.Message + "\n" + e.StackTrace);
                result = new Decimal(-1);
            }

            dLog.Debug("calculatePrice result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public String getBurritoType(Burrito b)
        {
            String result = "Unknown";

            try
            {
                if (b.Beef)
                {
                    result = "Beef";
                }
                else if (b.Chicken)
                {
                    result = "Chicken";
                }
                else if (b.Hummus)
                {
                    result = "Hummus";
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in getBurritoType: " + e.Message + "\n" + e.StackTrace);
                result = "Unknown";
            }

            return result;
        }
    }
}
