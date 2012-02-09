using System;
using System.Collections.Generic;
using System.Collections;
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
    /// This service implementation uses ADO.NET libraries to do basic CRUD operations with MS SQL Server 2008 for Order objects.
    /// </summary>
    class OrderSvcSQLImpl : IOrderSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // if production, we would want to change this from SA authentication to Integrated Windows Authentication with service account
        private String connString = Properties.Settings.Default.NeatoBurritoDB;
        String IOrderSvc.NAME
        {
            get { return "IOrderSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public OrderSvcSQLImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            dLog.Debug("connString: " + connString);         
        }

        /// <summary>
        /// This method retrieves a order.
        /// </summary>
        /// <param name="id">Unique ID of order to retrieve</param>
        /// <returns>order object</returns>
        public Order getOrder(Int32 id)
        {
            dLog.Info("Entering method getOrder | ID: " + id);
            Order o = new Order();
            SqlDataReader rs = null;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT isComplete, orderDate, isSubmitted, totalCost, Beef, BlackBeans, BrownRice, Chicken, ChiliTortilla, Cucumber, FlourTortilla, Guacamole, ";
                sqlStr += "HerbGarlicTortilla, Hummus, JalapenoCheddarTortilla, Jalapenos, Lettuce, Onion, b.orderID, PintoBeans, Price, SalsaPico, SalsaSpecial, SalsaVerde, ";
                sqlStr += "TomatoBasilTortilla, Tomatoes, WheatTortilla, WhiteRice, b.id FROM Orders o LEFT JOIN Burrito b on o.orderID = b.orderID WHERE o.orderID = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));
                rs = stmt.ExecuteReader();

                while (rs.Read())
                {
                    Int32 tID;
                    dLog.Info("Got the " + rs.FieldCount + " fields of the record ");
                    o.orderID = id;

                    #region Read Fields
                    if(!rs.IsDBNull(0))
                        o.isComplete = rs.GetBoolean(0);
                    if(!rs.IsDBNull(1))
                        o.orderDate = rs.GetDateTime(1);
                    if(!rs.IsDBNull(2))
                        o.isSubmitted = rs.GetBoolean(2);
                    if(!rs.IsDBNull(3))
                        o.totalCost = rs.GetDecimal(3);

                    Burrito b = new Burrito();
                    if (!rs.IsDBNull(4))
                        b.Beef = rs.GetBoolean(4);
                    if (!rs.IsDBNull(5))
                        b.BlackBeans = rs.GetBoolean(5);
                    if (!rs.IsDBNull(6))
                        b.BrownRice = rs.GetBoolean(6);
                    if (!rs.IsDBNull(7))
                        b.Chicken = rs.GetBoolean(7);
                    if (!rs.IsDBNull(8))
                        b.ChiliTortilla = rs.GetBoolean(8);
                    if (!rs.IsDBNull(9))
                        b.Cucumber = rs.GetBoolean(9);
                    if (!rs.IsDBNull(10))
                        b.FlourTortilla = rs.GetBoolean(10);
                    if (!rs.IsDBNull(11))
                        b.Guacamole = rs.GetBoolean(11);
                    if (!rs.IsDBNull(12))
                        b.HerbGarlicTortilla = rs.GetBoolean(12);
                    if (!rs.IsDBNull(13))
                        b.Hummus = rs.GetBoolean(13);
                    if (!rs.IsDBNull(14))
                        b.JalapenoCheddarTortilla = rs.GetBoolean(14);
                    if (!rs.IsDBNull(15))
                        b.Jalapenos = rs.GetBoolean(15);
                    if (!rs.IsDBNull(16))
                        b.Lettuce = rs.GetBoolean(16);
                    if (!rs.IsDBNull(17))
                        b.Onion = rs.GetBoolean(17);
                    if (!rs.IsDBNull(18))
                        b.orderID = rs.GetInt32(18);
                    if (!rs.IsDBNull(19))
                        b.PintoBeans = rs.GetBoolean(19);
                    if (!rs.IsDBNull(20))
                        b.Price = rs.GetDecimal(20);
                    if (!rs.IsDBNull(21))
                        b.SalsaPico = rs.GetBoolean(21);
                    if (!rs.IsDBNull(22))
                        b.SalsaSpecial = rs.GetBoolean(22);
                    if (!rs.IsDBNull(23))
                        b.SalsaVerde = rs.GetBoolean(23);
                    if (!rs.IsDBNull(24))
                        b.TomatoBasilTortilla = rs.GetBoolean(24);
                    if (!rs.IsDBNull(25))
                        b.Tomatoes = rs.GetBoolean(25);
                    if (!rs.IsDBNull(26))
                        b.WheatTortilla = rs.GetBoolean(26);
                    if (!rs.IsDBNull(27))
                        b.WhiteRice = rs.GetBoolean(27);
                    if (!rs.IsDBNull(28) && Int32.TryParse(rs.GetString(14), out tID))
                        b.id = tID;

                    o.burritos.Add(b);
                    #endregion
                }
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in getOrder: " + e1.Message + "\n" + e1.StackTrace);
                o = new Order();
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getOrder: " + e2.Message + "\n" + e2.StackTrace);
                o = new Order();
            }
            finally
            {
                if (rs != null && !rs.IsClosed)
                    rs.Close();
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            dLog.Info("Entering method storeOrder | ID: " + o.orderID);
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT COUNT(1) FROM Orders WHERE orderID = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", o.orderID));

                if (Int32.Parse(stmt.ExecuteScalar().ToString()) > 0)
                {
                    //if first is a valid row, then we need to do an update
                    dLog.Info("Updating order in database");

                    sqlStr = "UPDATE Orders SET isComplete=@isComplete, isSubmitted=@isSubmitted, orderDate=@orderDate, totalCost=@TotalCost WHERE orderID=@id";
                }
                else
                {
                    //if first is null, then we need to do an insert
                    dLog.Info("Inserting order into database");

                    sqlStr = "INSERT INTO Orders (orderID, isComplete, isSubmitted, orderDate, totalCost) ";
                    sqlStr += "VALUES (@id, @isComplete, @isSubmitted, @orderDate, @TotalCost)";
                }

                dLog.Info("SQL Statement: " + sqlStr);
                stmt = new SqlCommand(sqlStr, conn);

                #region Add SQL Parameters
                stmt.Parameters.Add(new SqlParameter("@id", o.orderID));
                stmt.Parameters.Add(new SqlParameter("@isComplete", o.isComplete));
                stmt.Parameters.Add(new SqlParameter("@isSubmitted", o.isSubmitted));
                stmt.Parameters.Add(new SqlParameter("@orderDate", o.orderDate));
                stmt.Parameters.Add(new SqlParameter("@TotalCost", o.totalCost));
                #endregion

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;

                dLog.Info("Trying to insert: " + o.burritos.Count + " burritos");
                foreach (Burrito b in o.burritos)
                {
                    dLog.Info("Validating burrito object");
                    if (b.validate())
                    {
                        sqlStr = "SELECT COUNT(1) FROM Burrito WHERE id = @id";

                        conn = new SqlConnection(connString);
                        conn.Open();
                        stmt = new SqlCommand(sqlStr, conn);
                        stmt.Parameters.Add(new SqlParameter("@id", b.id));

                        if (Int32.Parse(stmt.ExecuteScalar().ToString()) > 0)
                        {
                            //if first is a valid row, then we need to do an update
                            dLog.Info("Updating burrito in database");

                            sqlStr = "UPDATE Burrito SET Beef=@Beef, BlackBeans=@BlackBeans, BrownRice=@BrownRice, Chicken=@Chicken, ChiliTortilla=@ChiliTortilla, ";
                            sqlStr += "Cucumber=@Cucumber, FlourTortilla=@FlourTortilla, Guacamole=@Guacamole, HerbGarlicTortilla=@HerbGarlicTortilla, Hummus=@Hummus, ";
                            sqlStr += "JalapenoCheddarTortilla=@JalapenoCheddarTortilla, Jalapenos=@Jalapenos, Lettuce=@Lettuce, Onion=@Onion, OrderID=@OrderID, PintoBeans=@PintoBeans, ";
                            sqlStr += "Price=@Price, SalsaPico=@SalsaPico, SalsaSpecial=@SalsaSpecial, SalsaVerde=@SalsaVerde, TomatoBasilTortilla=@TomatoBasilTortilla, ";
                            sqlStr += "Tomatoes=@Tomatoes, WheatTortilla=@WheatTortilla, WhiteRice=@WhiteRice WHERE id=@id";
                        }
                        else
                        {
                            //if first is null, then we need to do an insert
                            dLog.Info("Inserting burrito into database");

                            sqlStr = "INSERT INTO Burrito (Beef, BlackBeans, BrownRice, Chicken, ChiliTortilla, Cucumber,";
                            sqlStr += "FlourTortilla, Guacamole, HerbGarlicTortilla, Hummus, JalapenoCheddarTortilla, Jalapenos,";
                            sqlStr += "Lettuce, Onion, OrderID, PintoBeans, Price, SalsaPico, SalsaSpecial, SalsaVerde, TomatoBasilTortilla,";
                            sqlStr += "Tomatoes, WheatTortilla, WhiteRice, id) VALUES (@Beef, @BlackBeans, @BrownRice, @Chicken, @ChiliTortilla, @Cucumber,";
                            sqlStr += "@FlourTortilla, @Guacamole, @HerbGarlicTortilla, @Hummus, @JalapenoCheddarTortilla, @Jalapenos,";
                            sqlStr += "@Lettuce, @Onion, @OrderID, @PintoBeans, @Price, @SalsaPico, @SalsaSpecial, @SalsaVerde, @TomatoBasilTortilla,";
                            sqlStr += "@Tomatoes, @WheatTortilla, @WhiteRice, @id)";
                        }

                        dLog.Info("SQL Statement: " + sqlStr);
                        stmt = new SqlCommand(sqlStr, conn);

                        #region Add SQL Parameters
                        stmt.Parameters.Add(new SqlParameter("@id", b.id));
                        stmt.Parameters.Add(new SqlParameter("@Beef", b.Beef));
                        stmt.Parameters.Add(new SqlParameter("@BlackBeans", b.BlackBeans));
                        stmt.Parameters.Add(new SqlParameter("@BrownRice", b.BrownRice));
                        stmt.Parameters.Add(new SqlParameter("@Chicken", b.Chicken));
                        stmt.Parameters.Add(new SqlParameter("@ChiliTortilla", b.ChiliTortilla));
                        stmt.Parameters.Add(new SqlParameter("@Cucumber", b.Cucumber));
                        stmt.Parameters.Add(new SqlParameter("@FlourTortilla", b.FlourTortilla));
                        stmt.Parameters.Add(new SqlParameter("@Guacamole", b.Guacamole));
                        stmt.Parameters.Add(new SqlParameter("@HerbGarlicTortilla", b.HerbGarlicTortilla));
                        stmt.Parameters.Add(new SqlParameter("@Hummus", b.Hummus));
                        stmt.Parameters.Add(new SqlParameter("@JalapenoCheddarTortilla", b.JalapenoCheddarTortilla));
                        stmt.Parameters.Add(new SqlParameter("@Jalapenos", b.Jalapenos));
                        stmt.Parameters.Add(new SqlParameter("@Lettuce", b.Lettuce));
                        stmt.Parameters.Add(new SqlParameter("@Onion", b.Onion));
                        stmt.Parameters.Add(new SqlParameter("@OrderID", b.orderID));
                        stmt.Parameters.Add(new SqlParameter("@PintoBeans", b.PintoBeans));
                        stmt.Parameters.Add(new SqlParameter("@Price", b.Price));
                        stmt.Parameters.Add(new SqlParameter("@SalsaPico", b.SalsaPico));
                        stmt.Parameters.Add(new SqlParameter("@SalsaSpecial", b.SalsaSpecial));
                        stmt.Parameters.Add(new SqlParameter("@SalsaVerde", b.SalsaVerde));
                        stmt.Parameters.Add(new SqlParameter("@TomatoBasilTortilla", b.TomatoBasilTortilla));
                        stmt.Parameters.Add(new SqlParameter("@Tomatoes", b.Tomatoes));
                        stmt.Parameters.Add(new SqlParameter("@WheatTortilla", b.WheatTortilla));
                        stmt.Parameters.Add(new SqlParameter("@WhiteRice", b.WhiteRice));
                        #endregion

                        if (stmt.ExecuteNonQuery() > 0)
                            result = true;
                    }
                }
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in storeOrder: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeOrder: " + e2.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "DELETE FROM Orders WHERE orderID = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in deleteOrder: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteOrder: " + e2.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }

        //TODO: come back and reduce number of reads on DB
        /// <summary>
        /// This method returns all historical orders.
        /// </summary>
        /// <returns>ArrayList of order objects</returns>
        public ArrayList getAllOrders()
        {
            dLog.Info("Entering method getAllOrders");
            ArrayList result = new ArrayList();
            SqlDataReader rs = null;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try {
                String sqlStr = "SELECT orderID from Orders";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                rs = stmt.ExecuteReader();

                if (rs.Read())
                {
                    if(!rs.IsDBNull(0))
                        result.Add(getOrder(rs.GetInt32(0)));
                }
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in getAllOrders: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getAllOrders: " + e2.Message);
            }
            finally
            {
                if (rs != null && !rs.IsClosed)
                    rs.Close();
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }
    }
}
