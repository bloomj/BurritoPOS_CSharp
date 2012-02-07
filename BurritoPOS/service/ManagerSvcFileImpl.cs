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

namespace BurritoPOS.service
{
    /// <summary>
    /// This service implementation uses Serializable to do basic CRUD operations with files for Manager objects.
    /// </summary>
    class ManagerSvcFileImpl : IManagerSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IManagerSvc.NAME
        {
            get { return "IManagerSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ManagerSvcFileImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a manager.
        /// </summary>
        /// <param name="id">Unique ID of manager to retrieve</param>
        /// <returns>Success/Failure</returns>
        public Manager getManager(Int32 id)
        {
            dLog.Info("Entering method getManager | ID: " + id);
            Manager m = null;
            Stream input = null;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (File.Exists("Manager_" + id + ".txt"))
                {
                    input = File.Open("Manager_" + id + ".txt", FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    m = (Manager)bFormatter.Deserialize(input);
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in getManager: " + e1.Message);
            }
            catch (TypeLoadException e2)
            {
                dLog.Error("TypeLoadException in getManager: " + e2.Message);
            }
            catch (Exception e3)
            {
                dLog.Error("Exception in getManager: " + e3.Message);
            }
            finally
            {
                //ensure that input is close regardless of the errors in try/catch
                if (input != null)
                {
                    input.Close();
                }
            }

            return m;
        }

        /// <summary>
        /// This method stores a manager.
        /// </summary>
        /// <param name="m">The manager object to store</param>
        /// <returns>Success/Failure</returns>
        public Boolean storeManager(Manager m)
        {
            dLog.Info("Entering method storeManager | ID: " + m.employeeID);
            Stream output = null;
            Boolean result = false;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (m.validate())
                {
                    output = File.Open("Manager_" + m.employeeID + ".txt", FileMode.Create);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(output, m);
                    result = true;
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in storeManager: " + e1.Message);
                result = false;
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeManager: " + e2.Message);
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
        /// This method deletes a manager.
        /// </summary>
        /// <param name="id">Unique ID of the manager to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteManager(Int32 id)
        {
            dLog.Info("Entering method deleteManager | ID:" + id);
            Boolean result = false;

            try
            {
                // Ensure the file exists
                if (!File.Exists("Manager_" + id + ".txt"))
                    throw new FileNotFoundException("deleteManager: no such file or directory: Manager_" + id + ".txt");

                // Ensure the file is not locked
                //if (!f.canWrite())
                //throw new UnauthorizedAccessException("deleteManager: write protected: Manager_"+id+".txt");	

                // Attempt to delete it
                File.Delete("Manager_" + id + ".txt");
                result = true;
            }
            catch (Exception e)
            {
                dLog.Error("Exception in deleteManager: " + e.Message);
                result = false;
            }

            return result;
        }
    }
}
