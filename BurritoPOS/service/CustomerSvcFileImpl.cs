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
    /// This service implementation uses Serializable to do basic CRUD operations with files for Customer objects.
    /// </summary>
    class CustomerSvcFileImpl : ICustomerSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String ICustomerSvc.NAME
        {
            get { return "ICustomerSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CustomerSvcFileImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a customer.
        /// </summary>
        /// <param name="id">Unique ID of customer to retrieve</param>
        /// <returns>Success/Failure</returns>
        public Customer getCustomer(Int32 id)
        {
            dLog.Info("Entering method getCustomer | ID: " + id);
            Customer c = null;
            Stream input = null;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (File.Exists("Customer_" + id + ".txt"))
                {
                    input = File.Open("Customer_" + id + ".txt", FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    c = (Customer)bFormatter.Deserialize(input);
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in getCustomer: " + e1.Message);
            }
            catch (TypeLoadException e2)
            {
                dLog.Error("TypeLoadException in getCustomer: " + e2.Message);
            }
            catch (Exception e3)
            {
                dLog.Error("Exception in getCustomer: " + e3.Message);
            }
            finally
            {
                //ensure that input is close regardless of the errors in try/catch
                if (input != null)
                {
                    input.Close();
                }
            }

            return c;
        }

        /// <summary>
        /// This method stores a customer.
        /// </summary>
        /// <param name="c">The customer object to store</param>
        /// <returns>Success/Failure</returns>
        public Boolean storeCustomer(Customer c)
        {
            dLog.Info("Entering method storeCustomer | ID: " + c.id);
            Stream output = null;
            Boolean result = false;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (c.validate())
                {
                    output = File.Open("Customer_" + c.id + ".txt", FileMode.Create);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(output, c);
                    result = true;
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in storeCustomer: " + e1.Message);
                result = false;
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeCustomer: " + e2.Message);
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
        /// This method deletes a customer.
        /// </summary>
        /// <param name="id">Unique ID of the customer to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteCustomer(Int32 id)
        {
            dLog.Info("Entering method deleteCustomer | ID: " + id);
            Boolean result = false;

            try
            {
                // Ensure the file exists
                if (!File.Exists("Customer_" + id + ".txt"))
                    throw new FileNotFoundException("deleteCustomer: no such file or directory: Customer_" + id + ".txt");

                // Ensure the file is not locked
                //if (!f.canWrite())
                //throw new UnauthorizedAccessException("deleteCustomer: write protected: Customer_"+id+".txt");	

                // Attempt to delete it
                File.Delete("Customer_" + id + ".txt");
                result = true;
            }
            catch (Exception e)
            {
                dLog.Error("Exception in deleteCustomer: " + e.Message);
                result = false;
            }

            return result;
        }
    }
}
