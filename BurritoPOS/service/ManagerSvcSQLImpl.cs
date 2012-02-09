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

namespace BurritoPOS.service
{
    /// <summary>
    /// This service implementation uses ADO.NET libraries to do basic CRUD operations with MS SQL Server 2008 for Manager objects.
    /// </summary>
    class ManagerSvcSQLImpl : IManagerSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // if production, we would want to change this from SA authentication to Integrated Windows Authentication with service account
        private String connString = Properties.Settings.Default.NeatoBurritoDB;
        String IManagerSvc.NAME
        {
            get { return "IManagerSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ManagerSvcSQLImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            dLog.Debug("connString: " + connString);         
        }

        /// <summary>
        /// This method retrieves a manager.
        /// </summary>
        /// <param name="id">Unique ID of manager to retrieve</param>
        /// <returns>customer object</returns>
        public Manager getManager(Int32 id)
        {
            dLog.Info("Entering method getManager | ID: " + id);
            Manager m = new Manager();
            SqlDataReader rs = null;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT FirstName, isManager, LastName FROM Employee WHERE employeeID = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));
                rs = stmt.ExecuteReader();

                while (rs.Read())
                {
                    dLog.Info("Got the " + rs.FieldCount + " fields of the record");
                    m.employeeID = id;

                    #region Read Fields
                    if (!rs.IsDBNull(0))
                        m.firstName = rs.GetString(0);
                    if (!rs.IsDBNull(1))
                        m.isManager = rs.GetBoolean(1);
                    if (!rs.IsDBNull(2))
                        m.lastName = rs.GetString(2);
                    #endregion
                }
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in getManager: " + e1.Message + "\n" + e1.StackTrace);
                m = new Manager();
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getManager: " + e2.Message + "\n" + e2.StackTrace);
                m = new Manager();
            }
            finally
            {
                if (rs != null && !rs.IsClosed)
                    rs.Close();
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT COUNT(1) FROM Employee WHERE employeeID = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", m.employeeID));

                if (Int32.Parse(stmt.ExecuteScalar().ToString()) > 0)
                {
                    //if first is a valid row, then we need to do an update
                    dLog.Info("Updating manager in database");

                    sqlStr = "UPDATE Employee SET FirstName=@FirstName, isManager=@isManager, LastName=@LastName WHERE employeeID=@id";
                }
                else
                {
                    //if first is null, then we need to do an insert
                    dLog.Info("Inserting manager into database");

                    sqlStr = "INSERT INTO Employee (FirstName,isManager, LastName, employeeID) ";
                    sqlStr += "VALUES (@FirstName, @isManager, @LastName, @id)";
                }

                dLog.Info("SQL Statement: " + sqlStr);
                stmt = new SqlCommand(sqlStr, conn);

                #region Add SQL Parameters
                stmt.Parameters.Add(new SqlParameter("@id", m.employeeID));
                stmt.Parameters.Add(new SqlParameter("@FirstName", m.firstName));
                stmt.Parameters.Add(new SqlParameter("@isManager", m.isManager));
                stmt.Parameters.Add(new SqlParameter("@LastName", m.lastName));
                #endregion

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in storeManager: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeManager: " + e2.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "DELETE FROM Employee WHERE employeeID = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in deleteManager: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteManager: " + e2.Message);
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
