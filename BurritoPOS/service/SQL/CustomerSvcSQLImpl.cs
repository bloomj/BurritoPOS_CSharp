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
    /// This service implementation uses ADO.NET libraries to do basic CRUD operations with MS SQL Server 2008 for Customer objects.
    /// </summary>
    class CustomerSvcSQLImpl : ICustomerSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // if production, we would want to change this from SA authentication to Integrated Windows Authentication with service account
        private String connString = Properties.Settings.Default.NeatoBurritoDB;
        String ICustomerSvc.NAME
        {
            get { return "ICustomerSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CustomerSvcSQLImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            dLog.Debug("connString: " + connString);         
        }

        /// <summary>
        /// This method retrieves a customer.
        /// </summary>
        /// <param name="id">Unique ID of customer to retrieve</param>
        /// <returns>customer object</returns>
        public Customer getCustomer(Int32 id)
        {
            dLog.Info("Entering method getCustomer | ID: " + id);
            Customer c = new Customer();
            SqlDataReader rs = null;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT EmailAddress, FirstName, LastName FROM Customer WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));
                rs = stmt.ExecuteReader();

                while (rs.Read())
                {
                    dLog.Info("Got the " + rs.FieldCount + " fields of the record");
                    c.id = id;

                    #region Read Fields
                    if (!rs.IsDBNull(0))
                        c.emailAddress = rs.GetString(0);
                    if(!rs.IsDBNull(1))
                        c.firstName = rs.GetString(1);
                    if(!rs.IsDBNull(2))
                        c.lastName = rs.GetString(2);
                    #endregion
                }
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in getCustomer: " + e1.Message + "\n" + e1.StackTrace);
                c = new Customer();
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getCustomer: " + e2.Message + "\n" + e2.StackTrace);
                c = new Customer();
            }
            finally
            {
                if (rs != null && !rs.IsClosed)
                    rs.Close();
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT COUNT(1) FROM Customer WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", c.id));

                if (Int32.Parse(stmt.ExecuteScalar().ToString()) > 0)
                {
                    //if first is a valid row, then we need to do an update
                    dLog.Info("Updating customer in database");

                    sqlStr = "UPDATE Customer SET EmailAddress=@EmailAddress, FirstName=@FirstName, LastName=@LastName WHERE id=@id";
                }
                else
                {
                    //if first is null, then we need to do an insert
                    dLog.Info("Inserting customer into database");

                    sqlStr = "INSERT INTO Customer (EmailAddress, FirstName, LastName, id) ";
                    sqlStr += "VALUES (@EmailAddress, @FirstName, @LastName, @id)";
                }

                dLog.Info("SQL Statement: " + sqlStr);
                stmt = new SqlCommand(sqlStr, conn);

                #region Add SQL Parameters
                stmt.Parameters.Add(new SqlParameter("@id", c.id));
                stmt.Parameters.Add(new SqlParameter("@EmailAddress", c.emailAddress));
                stmt.Parameters.Add(new SqlParameter("@FirstName", c.firstName));
                stmt.Parameters.Add(new SqlParameter("@LastName", c.lastName));
                #endregion

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in storeCustomer: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeCustomer: " + e2.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            dLog.Info("Entering method deleteCustomer | ID:" + id);
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "DELETE FROM Customer WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in deleteCustomer: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteCustomer: " + e2.Message);
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
