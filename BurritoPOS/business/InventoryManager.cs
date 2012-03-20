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
    public class InventoryManager : FactoryManager
    {
        //private Factory factory = Factory.getInstance();
        private IInventorySvc inventorySvc;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        public InventoryManager()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));

            //will comment out after Spring.NET implementation
            inventorySvc = (IInventorySvc)factory.getService("IInventorySvc");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventorySvc"></param>
        public void setInventorySvc(IInventorySvc inventorySvc)
        {
            this.inventorySvc = inventorySvc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Boolean createInventory(Inventory i)
        {
            dLog.Debug("In createInventory");
            Boolean result = false;

            try
            {
                dLog.Debug("Validating inventory object");
                if (i.validate())
                {
                    if (inventorySvc.storeInventory(i))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in createInventory: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("createInventory result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Boolean updateInventory(Inventory i)
        {
            dLog.Debug("In updateInventory");
            Boolean result = false;

            try
            {
                if (i.validate())
                {
                    if (inventorySvc.storeInventory(i))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in updateInventory: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("updateInventory result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Boolean deleteInventory(Inventory i)
        {
            dLog.Debug("In deleteInventory");
            Boolean result = false;

            try
            {
                if (i.validate())
                {
                    if (inventorySvc.deleteInventory(i.id))
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in deleteInventory: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("deleteInventory result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public Boolean removeFromInventory(Inventory i, Order o)
        {
            dLog.Debug("In removeFromInventory");
            Boolean result = false;

            try
            {
                if (i.validate() && o.validate())
                {
                    // loop through all burritos on the order and remove from inventory
                    foreach (Burrito b in o.burritos)
                    {
                        if (b.Beef) i.setBeefQty(i.BeefQty - 1);
                        if (b.Chicken) i.setChickenQty(i.ChickenQty - 1);
                        if (b.Hummus) i.setHummusQty(i.HummusQty - 1);

                        //calculate remaining extras
                        if (b.ChiliTortilla) i.setChiliTortillaQty(i.ChiliTortillaQty - 1);
                        if (b.HerbGarlicTortilla) i.setHerbGarlicTortillaQty(i.HerbGarlicTortillaQty - 1);
                        if (b.JalapenoCheddarTortilla) i.setJalapenoCheddarTortillaQty(i.JalapenoCheddarTortillaQty - 1);
                        if (b.TomatoBasilTortilla) i.setTomatoBasilTortillaQty(i.TomatoBasilTortillaQty - 1);
                        if (b.WheatTortilla) i.setWheatTortillaQty(i.WheatTortillaQty - 1);
                        if (b.FlourTortilla) i.setFlourTortillaQty(i.FlourTortillaQty - 1);

                        if (b.WhiteRice) i.setWhiteRiceQty(i.WhiteRiceQty - 1);
                        if (b.BrownRice) i.setBrownRiceQty(i.BrownRiceQty - 1);

                        if (b.BlackBeans) i.setBlackBeansQty(i.BlackBeansQty - 1);
                        if (b.PintoBeans) i.setPintoBeansQty(i.PintoBeansQty - 1);

                        if (b.SalsaPico) i.setSalsaPicoQty(i.SalsaPicoQty - 1);
                        if (b.SalsaSpecial) i.setSalsaSpecialQty(i.SalsaSpecialQty - 1);
                        if (b.SalsaVerde) i.setSalsaVerdeQty(i.SalsaVerdeQty - 1);

                        if (b.Guacamole) i.setGuacamoleQty(i.GuacamoleQty - 1);

                        if (b.Cucumber) i.setCucumberQty(i.CucumberQty - 1);
                        if (b.Jalapenos) i.setJalapenosQty(i.JalapenosQty - 1);
                        if (b.Lettuce) i.setLettuceQty(i.LettuceQty - 1);
                        if (b.Onion) i.setOnionQty(i.OnionQty - 1);
                        if (b.Tomatoes) i.setTomatoesQty(i.TomatoesQty - 1);
                    }

                    // ensure the inventory gets updated
                    if (inventorySvc.storeInventory(i))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in removeFromInventory: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("removeFromInventory result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public Boolean returnToInventory(Inventory i, Order o)
        {
            dLog.Debug("In returnToInventory");
            Boolean result = false;

            try
            {
                if (i.validate() && o.validate())
                {
                    // loop through all burritos on the order and add to inventory
                    foreach (Burrito b in o.burritos)
                    {
                        if (b.Beef) i.setBeefQty(i.BeefQty + 1);
                        if (b.Chicken) i.setChickenQty(i.ChickenQty + 1);
                        if (b.Hummus) i.setHummusQty(i.HummusQty + 1);

                        //calculate remaining extras
                        if (b.ChiliTortilla) i.setChiliTortillaQty(i.ChiliTortillaQty + 1);
                        if (b.HerbGarlicTortilla) i.setHerbGarlicTortillaQty(i.HerbGarlicTortillaQty + 1);
                        if (b.JalapenoCheddarTortilla) i.setJalapenoCheddarTortillaQty(i.JalapenoCheddarTortillaQty + 1);
                        if (b.TomatoBasilTortilla) i.setTomatoBasilTortillaQty(i.TomatoBasilTortillaQty + 1);
                        if (b.WheatTortilla) i.setWheatTortillaQty(i.WheatTortillaQty + 1);
                        if (b.FlourTortilla) i.setFlourTortillaQty(i.FlourTortillaQty + 1);

                        if (b.WhiteRice) i.setWhiteRiceQty(i.WhiteRiceQty + 1);
                        if (b.BrownRice) i.setBrownRiceQty(i.BrownRiceQty + 1);

                        if (b.BlackBeans) i.setBlackBeansQty(i.BlackBeansQty + 1);
                        if (b.PintoBeans) i.setPintoBeansQty(i.PintoBeansQty + 1);

                        if (b.SalsaPico) i.setSalsaPicoQty(i.SalsaPicoQty + 1);
                        if (b.SalsaSpecial) i.setSalsaSpecialQty(i.SalsaSpecialQty + 1);
                        if (b.SalsaVerde) i.setSalsaVerdeQty(i.SalsaVerdeQty + 1);

                        if (b.Guacamole) i.setGuacamoleQty(i.GuacamoleQty + 1);

                        if (b.Cucumber) i.setCucumberQty(i.CucumberQty + 1);
                        if (b.Jalapenos) i.setJalapenosQty(i.JalapenosQty + 1);
                        if (b.Lettuce) i.setLettuceQty(i.LettuceQty + 1);
                        if (b.Onion) i.setOnionQty(i.OnionQty + 1);
                        if (b.Tomatoes) i.setTomatoesQty(i.TomatoesQty + 1);
                    }

                    // ensure the inventory gets updated
                    if (inventorySvc.storeInventory(i))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in returnToInventory: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("returnToInventory result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Boolean removeFromInventory(Inventory i, Burrito b)
        {
            dLog.Debug("In removeFromInventory");
            Boolean result = false;

            try
            {
                if (i.validate() && b.validate())
                {
                    // remove burrito ingredients from inventory
                    if (b.Beef) i.setBeefQty(i.BeefQty - 1);
                    if (b.Chicken) i.setChickenQty(i.ChickenQty - 1);
                    if (b.Hummus) i.setHummusQty(i.HummusQty - 1);

                    //calculate remaining extras
                    if (b.ChiliTortilla) i.setChiliTortillaQty(i.ChiliTortillaQty - 1);
                    if (b.HerbGarlicTortilla) i.setHerbGarlicTortillaQty(i.HerbGarlicTortillaQty - 1);
                    if (b.JalapenoCheddarTortilla) i.setJalapenoCheddarTortillaQty(i.JalapenoCheddarTortillaQty - 1);
                    if (b.TomatoBasilTortilla) i.setTomatoBasilTortillaQty(i.TomatoBasilTortillaQty - 1);
                    if (b.WheatTortilla) i.setWheatTortillaQty(i.WheatTortillaQty - 1);
                    if (b.FlourTortilla) i.setFlourTortillaQty(i.FlourTortillaQty - 1);

                    if (b.WhiteRice) i.setWhiteRiceQty(i.WhiteRiceQty - 1);
                    if (b.BrownRice) i.setBrownRiceQty(i.BrownRiceQty - 1);

                    if (b.BlackBeans) i.setBlackBeansQty(i.BlackBeansQty - 1);
                    if (b.PintoBeans) i.setPintoBeansQty(i.PintoBeansQty - 1);

                    if (b.SalsaPico) i.setSalsaPicoQty(i.SalsaPicoQty - 1);
                    if (b.SalsaSpecial) i.setSalsaSpecialQty(i.SalsaSpecialQty - 1);
                    if (b.SalsaVerde) i.setSalsaVerdeQty(i.SalsaVerdeQty - 1);

                    if (b.Guacamole) i.setGuacamoleQty(i.GuacamoleQty - 1);

                    if (b.Cucumber) i.setCucumberQty(i.CucumberQty - 1);
                    if (b.Jalapenos) i.setJalapenosQty(i.JalapenosQty - 1);
                    if (b.Lettuce) i.setLettuceQty(i.LettuceQty - 1);
                    if (b.Onion) i.setOnionQty(i.OnionQty - 1);
                    if (b.Tomatoes) i.setTomatoesQty(i.TomatoesQty - 1);

                    // ensure the inventory gets updated
                    if (inventorySvc.storeInventory(i))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in removeFromInventory: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("removeFromInventory result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Boolean returnToInventory(Inventory i, Burrito b)
        {
            dLog.Debug("In returnToInventory");
            Boolean result = false;

            try
            {
                if (i.validate() && b.validate())
                {
                    // add burrito ingredients to inventory
                    if (b.Beef) i.setBeefQty(i.BeefQty + 1);
                    if (b.Chicken) i.setChickenQty(i.ChickenQty + 1);
                    if (b.Hummus) i.setHummusQty(i.HummusQty + 1);

                    //calculate remaining extras
                    if (b.ChiliTortilla) i.setChiliTortillaQty(i.ChiliTortillaQty + 1);
                    if (b.HerbGarlicTortilla) i.setHerbGarlicTortillaQty(i.HerbGarlicTortillaQty + 1);
                    if (b.JalapenoCheddarTortilla) i.setJalapenoCheddarTortillaQty(i.JalapenoCheddarTortillaQty + 1);
                    if (b.TomatoBasilTortilla) i.setTomatoBasilTortillaQty(i.TomatoBasilTortillaQty + 1);
                    if (b.WheatTortilla) i.setWheatTortillaQty(i.WheatTortillaQty + 1);
                    if (b.FlourTortilla) i.setFlourTortillaQty(i.FlourTortillaQty + 1);

                    if (b.WhiteRice) i.setWhiteRiceQty(i.WhiteRiceQty + 1);
                    if (b.BrownRice) i.setBrownRiceQty(i.BrownRiceQty + 1);

                    if (b.BlackBeans) i.setBlackBeansQty(i.BlackBeansQty + 1);
                    if (b.PintoBeans) i.setPintoBeansQty(i.PintoBeansQty + 1);

                    if (b.SalsaPico) i.setSalsaPicoQty(i.SalsaPicoQty + 1);
                    if (b.SalsaSpecial) i.setSalsaSpecialQty(i.SalsaSpecialQty + 1);
                    if (b.SalsaVerde) i.setSalsaVerdeQty(i.SalsaVerdeQty + 1);

                    if (b.Guacamole) i.setGuacamoleQty(i.GuacamoleQty + 1);

                    if (b.Cucumber) i.setCucumberQty(i.CucumberQty + 1);
                    if (b.Jalapenos) i.setJalapenosQty(i.JalapenosQty + 1);
                    if (b.Lettuce) i.setLettuceQty(i.LettuceQty + 1);
                    if (b.Onion) i.setOnionQty(i.OnionQty + 1);
                    if (b.Tomatoes) i.setTomatoesQty(i.TomatoesQty + 1);

                    // ensure the inventory gets updated
                    if (inventorySvc.storeInventory(i))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in returnToInventory: " + e.Message + "\n" + e.StackTrace);
                result = false;
            }

            dLog.Debug("returnToInventory result: " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Inventory getInventory(Int32 id)
        {
            dLog.Debug("In getInventory");
            Inventory result;

            try
            {
                result = inventorySvc.getInventory(id);
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in getInventory: " + e.Message + "\n" + e.StackTrace);
                result = null;
            }

            dLog.Debug("getInventory result: " + result);
            return result;
        }
    }
}
