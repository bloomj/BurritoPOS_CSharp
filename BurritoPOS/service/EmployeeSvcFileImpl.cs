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
    /// This service implementation uses Serializable to do basic CRUD operations with files for Employee objects.
    /// </summary>
    class EmployeeSvcFileImpl : IEmployeeSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IEmployeeSvc.NAME
        {
            get { return "IEmployeeSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmployeeSvcFileImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a employee.
        /// </summary>
        /// <param name="id">Unique ID of employee to retrieve</param>
        /// <returns>Success/Failure</returns>
        public Employee getEmployee(Int32 id)
        {
            dLog.Info("Entering method getEmployee | ID: " + id);
            Employee e = null;
            Stream input = null;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (File.Exists("Employee_" + id + ".txt"))
                {
                    input = File.Open("Employee_" + id + ".txt", FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    e = (Employee)bFormatter.Deserialize(input);
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in getEmployee: " + e1.Message);
            }
            catch (TypeLoadException e2)
            {
                dLog.Error("TypeLoadException in getEmployee: " + e2.Message);
            }
            catch (Exception e3)
            {
                dLog.Error("Exception in getEmployee: " + e3.Message);
            }
            finally
            {
                //ensure that input is close regardless of the errors in try/catch
                if (input != null)
                {
                    input.Close();
                }
            }

            return e;
        }

        /// <summary>
        /// This method stores a employee.
        /// </summary>
        /// <param name="e">The employee object to store</param>
        /// <returns>Success/Failure</returns>
        public Boolean storeEmployee(Employee e)
        {
            dLog.Info("Entering method storeEmployee | ID: " + e.employeeID);
            Stream output = null;
            Boolean result = false;

            try
            {
                //ensure we were passed a valid object before attempting to write
                if (e.validate())
                {
                    output = File.Open("Employee_" + e.employeeID + ".txt", FileMode.Create);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(output, e);
                    result = true;
                }
            }
            catch (IOException e1)
            {
                dLog.Error("IOException in storeEmployee: " + e1.Message);
                result = false;
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeEmployee: " + e2.Message);
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
        /// This method deletes a employee.
        /// </summary>
        /// <param name="id">Unique ID of the employee to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteEmployee(Int32 id)
        {
            dLog.Info("Entering method deleteEmployee | ID: " + id);
            Boolean result = false;

            try
            {
                // Ensure the file exists
                if (!File.Exists("Employee_" + id + ".txt"))
                    throw new FileNotFoundException("deleteEmployee: no such file or directory: Employee_" + id + ".txt");

                // Ensure the file is not locked
                //if (!f.canWrite())
                //throw new UnauthorizedAccessException("deleteEmployee: write protected: Employee_"+id+".txt");	

                // Attempt to delete it
                File.Delete("Employee_" + id + ".txt");
                result = true;
            }
            catch (Exception e)
            {
                dLog.Error("Exception in deleteEmployee: " + e.Message);
                result = false;
            }

            return result;
        }
    }
}
