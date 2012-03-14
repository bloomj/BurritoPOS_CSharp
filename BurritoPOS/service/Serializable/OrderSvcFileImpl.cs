using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using BurritoPOS.domain;

namespace BurritoPOS.service.Serializable
{
    /// <summary>
    /// This service implementation uses Serializable to do basic CRUD operations with files for Order objects.
    /// </summary>
    class OrderSvcFileImpl : IOrderSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IOrderSvc.NAME
        {
            get { return "IOrderSvc"; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public OrderSvcFileImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a order.
        /// </summary>
        /// <param name="id">Unique ID of order to retrieve</param>
        /// <returns>Success/Failure</returns>
        public Order getOrder(Int32 id)
        {
            dLog.Info("Entering method getOrder | ID: " + id);
            Order o = null;
            Stream input = null;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (File.Exists("Order_" + id + ".txt"))
                {
                    input = File.Open("Order_" + id + ".txt", FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    o = (Order)bFormatter.Deserialize(input);
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in getOrder: " + e1.Message);
            }
            catch (TypeLoadException e2)
            {
                dLog.Error("TypeLoadException in getOrder: " + e2.Message);
            }
            catch (Exception e3)
            {
                dLog.Error("Exception in getOrder: " + e3.Message);
            }
            finally
            {
                //ensure that input is close regardless of the errors in try/catch
                if (input != null)
                {
                    input.Close();
                }
            }

            return o;
        }

        /// <summary>
        /// This method stores a order.
        /// </summary>
        /// <param name="o">The order object to store</param>
        /// <returns>Success/Failure</returns>
        public Boolean storeOrder(Order o)
        {
            dLog.Info("Entering method storeOrder | ID: " + o.id);
            Stream output = null;
            Boolean result = false;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (o.validate())
                {
                    output = File.Open("Order_" + o.id + ".txt", FileMode.Create);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(output, o);
                    result = true;
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in storeOrder: " + e1.Message);
                result = false;
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeOrder: " + e2.Message);
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
        /// This method deletes a order.
        /// </summary>
        /// <param name="id">Unique ID of the order to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteOrder(Int32 id)
        {
            dLog.Info("Entering method deleteOrder | ID:" + id);
            Boolean result = false;

            try
            {
                // Ensure the file exists
                if (!File.Exists("Order_" + id + ".txt"))
                    throw new FileNotFoundException("deleteOrder: no such file or directory: Order_" + id + ".txt");

                // Ensure the file is not locked
                //if (!f.canWrite())
                //throw new UnauthorizedAccessException("deleteOrder: write protected: Order_"+id+".txt");	

                // Attempt to delete it
                File.Delete("Order_" + id + ".txt");
                result = true;
            }
            catch (Exception e)
            {
                dLog.Error("Exception in deleteOrder: " + e.Message);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ArrayList getAllOrders() {
            dLog.Info("Entering method getAllOrders");
		    ArrayList result = new ArrayList();

		    try {
                dLog.Debug("Current diretory: " + Directory.GetCurrentDirectory());

                DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());
				foreach(FileInfo child in d.EnumerateFiles()) { 
				    // Get filename of file or directory 
                    String filename = child.Name;
                    dLog.Debug("  - On file: " + filename);
                    char[] separator = { '_' };
                    String[] fileTokens = filename.Split(separator);
                    String firstTok = fileTokens[0];
                    dLog.Debug("  - First Token: " + firstTok);
					if(firstTok.IndexOf("Object") > -1 && fileTokens.Length > 1) {
                        String secondTok = fileTokens[1];
                        dLog.Debug("  - Second Token: " + secondTok);
                        char[] separator2 = { '.' };
                        String[] parts = secondTok.Split(separator2);
                        dLog.Debug("  - Parts Length: " + parts.Length);
						if(parts.Length > 0) {
                            Int32 tOrderID = Int32.Parse(parts[0]);
                            dLog.Debug("  - Found order: " + tOrderID);

							//add this file 
							result.Add(getOrder(tOrderID));
						}
				    }
				}
		    }
		    catch(Exception e) {
                dLog.Error("Exception in getAllOrders: " + e.Message);
		    }
		
		    return result;
	    }
    }
}
