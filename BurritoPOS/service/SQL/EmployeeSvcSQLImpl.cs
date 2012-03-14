using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using log4net;
using log4net.Config;
using System.Data.SqlClient;
using BurritoPOS.domain;

namespace BurritoPOS.service.SQL
{
    /// <summary>
    /// This service implementation uses ADO.NET libraries to do basic CRUD operations with MS SQL Server 2008 for Employee objects.
    /// </summary>
    class EmployeeSvcSQLImpl : IEmployeeSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // if production, we would want to change this from SA authentication to Integrated Windows Authentication with service account
        private String connString = Properties.Settings.Default.NeatoBurritoDB;
        String IEmployeeSvc.NAME
        {
            get { return "IEmployeeSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmployeeSvcSQLImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            dLog.Debug("connString: " + connString);         
        }

        /// <summary>
        /// This method retrieves a employee.
        /// </summary>
        /// <param name="id">Unique ID of employee to retrieve</param>
        /// <returns>customer object</returns>
        public Employee getEmployee(Int32 id)
        {
            dLog.Info("Entering method getEmployee | ID: " + id);
            Employee e = new Employee();
            SqlDataReader rs = null;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT FirstName, isManager, LastName FROM Employee WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));
                rs = stmt.ExecuteReader();

                while (rs.Read())
                {
                    dLog.Info("Got the " + rs.FieldCount + " fields of the record");
                    e.id = id;

                    #region Read Fields
                    if (!rs.IsDBNull(0))
                        e.firstName = rs.GetString(0);
                    if (!rs.IsDBNull(1))
                        e.isManager = rs.GetBoolean(1);
                    if (!rs.IsDBNull(2))
                        e.lastName = rs.GetString(2);
                    #endregion
                }
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in getEmployee: " + e1.Message + "\n" + e1.StackTrace);
                e = new Employee();
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getEmployee: " + e2.Message + "\n" + e2.StackTrace);
                e = new Employee();
            }
            finally
            {
                if (rs != null && !rs.IsClosed)
                    rs.Close();
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            dLog.Info("Entering method storeEmployee | ID: " + e.id);
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT COUNT(1) FROM Employee WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", e.id));

                if (Int32.Parse(stmt.ExecuteScalar().ToString()) > 0)
                {
                    //if first is a valid row, then we need to do an update
                    dLog.Info("Updating employee in database");

                    sqlStr = "UPDATE Employee SET FirstName=@FirstName, isManager=@isManager, LastName=@LastName WHERE id=@id";
                }
                else
                {
                    //if first is null, then we need to do an insert
                    dLog.Info("Inserting employee into database");

                    sqlStr = "INSERT INTO Employee (FirstName,isManager, LastName, id) ";
                    sqlStr += "VALUES (@FirstName, @isManager, @LastName, @id)";
                }

                dLog.Info("SQL Statement: " + sqlStr);
                stmt = new SqlCommand(sqlStr, conn);

                #region Add SQL Parameters
                stmt.Parameters.Add(new SqlParameter("@id", e.id));
                stmt.Parameters.Add(new SqlParameter("@FirstName", e.firstName));
                stmt.Parameters.Add(new SqlParameter("@isManager", e.isManager));
                stmt.Parameters.Add(new SqlParameter("@LastName", e.lastName));
                #endregion

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in storeEmployee: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeEmployee: " + e2.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            dLog.Info("Entering method deleteEmployee | ID:" + id);
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "DELETE FROM Employee WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in deleteEmployee: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteEmployee: " + e2.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }
    }
}
