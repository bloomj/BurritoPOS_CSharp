using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BurritoPOS.domain;

namespace BurritoPOS.service.Serializable
{
    /// <summary>
    /// This service implementation uses Serializable to do basic CRUD operations with files for Inventory objects.
    /// </summary>
    class InventorySvcFileImpl : IInventorySvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IInventorySvc.NAME
        {
            get { return "IInventorySvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public InventorySvcFileImpl() 
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a inventory.
        /// </summary>
        /// <param name="id">Unique ID of inventory to retrieve</param>
        /// <returns>Success/Failure</returns>
        public Inventory getInventory(Int32 id)
        {
            dLog.Info("Entering method getInventory | ID: " + id);
            Inventory i = null;
            Stream input = null;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (File.Exists("Inventory_" + id + ".txt"))
                {
                    input = File.Open("Inventory_" + id + ".txt", FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    i = (Inventory)bFormatter.Deserialize(input);
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in getInventory: " + e1.Message);
            }
            catch (TypeLoadException e2)
            {
                dLog.Error("TypeLoadException in getInventory: " + e2.Message);
            }
            catch (Exception e3)
            {
                dLog.Error("Exception in getInventory: " + e3.Message);
            }
            finally
            {
                //ensure that input is close regardless of the errors in try/catch
                if (input != null)
                {
                    input.Close();
                }
            }

            return i;
        }

        /// <summary>
        /// This method stores a inventory.
        /// </summary>
        /// <param name="i">The inventory object to store</param>
        /// <returns>Success/Failure</returns>
        public Boolean storeInventory(Inventory i)
        {
            dLog.Info("Entering method storeInventory | ID: " + i.id);
            Stream output = null;
            Boolean result = false;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (i.validate())
                {
                    output = File.Open("Inventory_" + i.id + ".txt", FileMode.Create);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(output, i);
                    result = true;
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in storeInventory: " + e1.Message);
                result = false;
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeInventory: " + e2.Message);
                result = false;
            }
            finally
            {
                //ensure that output is close regardless of the errors in try/catch
                if (output != null)
                {
                    output.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// This method deletes a inventory.
        /// </summary>
        /// <param name="id">Unique ID of the inventory to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteInventory(Int32 id)
        {
            dLog.Info("Entering method deleteInventory | ID: " + id);
            Boolean result = false;

            try
            {
                // Ensure the file exists
                if (!File.Exists("Inventory_" + id + ".txt"))
                    throw new FileNotFoundException("deleteInventory: no such file or directory: Inventory_" + id + ".txt");

                // Ensure the file is not locked
                //if (!f.canWrite())
                //throw new UnauthorizedAccessException("deleteInventory: write protected: Inventory_"+id+".txt");	

                // Attempt to delete it
                File.Delete("Inventory_" + id + ".txt");
                result = true;
            }
            catch (Exception e)
            {
                dLog.Error("Exception in deleteInventory: " + e.Message);
                result = false;
            }

            return result;
        }
    }
}
