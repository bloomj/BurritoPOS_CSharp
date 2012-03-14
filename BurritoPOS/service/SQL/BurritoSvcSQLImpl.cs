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
    /// This service implementation uses ADO.NET libraries to do basic CRUD operations with MS SQL Server 2008 for Burrito objects.
    /// </summary>
    class BurritoSvcSQLImpl : IBurritoSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // if production, we would want to change this from SA authentication to Integrated Windows Authentication with service account
        private String connString = Properties.Settings.Default.NeatoBurritoDB;
        String IBurritoSvc.NAME
        {
            get { return "IBurritoSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BurritoSvcSQLImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            dLog.Debug("connString: " + connString);
            try
            {
                dLog.Debug("app.config connString: " + ConfigurationManager.ConnectionStrings["NeatoBurrito"].ConnectionString);
            }
            catch (Exception e)
            {
                dLog.Debug("Exception getting app.config: " + e.Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// This method retrieves a burrito.
        /// </summary>
        /// <param name="id">Unique ID of burrito to retrieve</param>
        /// <returns>burrito object</returns>
        public Burrito getBurrito(Int32 id)
        {
            dLog.Info("Entering method getBurrito | ID: " + id);
            Burrito b = new Burrito();
            SqlDataReader rs = null;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT Beef, BlackBeans, BrownRice, Chicken, ChiliTortilla, Cucumber, FlourTortilla, Guacamole, HerbGarlicTortilla, Hummus, ";
                sqlStr += "JalapenoCheddarTortilla, Jalapenos, Lettuce, Onion, orderID, PintoBeans, Price, SalsaPico, SalsaSpecial, SalsaVerde, ";
                sqlStr += "TomatoBasilTortilla, Tomatoes, WheatTortilla, WhiteRice FROM Burrito WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));
                rs = stmt.ExecuteReader();

                while (rs.Read())
                {
                    Int32 tOrderID;
                    dLog.Info("Got the " + rs.FieldCount + " fields of the record");
                    b.id = id;

                    #region Read Fields
                    if (!rs.IsDBNull(0))
                        b.Beef = rs.GetBoolean(0);
                    if(!rs.IsDBNull(1))
                        b.BlackBeans = rs.GetBoolean(1);
                    if(!rs.IsDBNull(2))
                        b.BrownRice = rs.GetBoolean(2);
                    if(!rs.IsDBNull(3))
                        b.Chicken = rs.GetBoolean(3);
                    if(!rs.IsDBNull(4))
                        b.ChiliTortilla = rs.GetBoolean(4);
                    if(!rs.IsDBNull(5))
                        b.Cucumber = rs.GetBoolean(5);
                    if(!rs.IsDBNull(6))
                        b.FlourTortilla = rs.GetBoolean(6);
                    if(!rs.IsDBNull(7))
                        b.Guacamole = rs.GetBoolean(7);
                    if(!rs.IsDBNull(8))
                        b.HerbGarlicTortilla = rs.GetBoolean(8);
                    if(!rs.IsDBNull(9))
                        b.Hummus = rs.GetBoolean(9);
                    if(!rs.IsDBNull(10))
                        b.JalapenoCheddarTortilla = rs.GetBoolean(10);
                    if(!rs.IsDBNull(11))
                        b.Jalapenos = rs.GetBoolean(11);
                    if(!rs.IsDBNull(12))
                        b.Lettuce = rs.GetBoolean(12);
                    if(!rs.IsDBNull(13))
                        b.Onion = rs.GetBoolean(13);
                    if (!rs.IsDBNull(14) && Int32.TryParse(rs.GetString(14), out tOrderID))
                        b.orderID = tOrderID;
                    if(!rs.IsDBNull(15))
                        b.PintoBeans = rs.GetBoolean(15);
                    if(!rs.IsDBNull(16))
                        b.Price = rs.GetDecimal(16);
                    if(!rs.IsDBNull(17))
                        b.SalsaPico = rs.GetBoolean(17);
                    if(!rs.IsDBNull(18))
                        b.SalsaSpecial = rs.GetBoolean(18);
                    if(!rs.IsDBNull(19))
                        b.SalsaVerde = rs.GetBoolean(19);
                    if(!rs.IsDBNull(20))
                        b.TomatoBasilTortilla = rs.GetBoolean(20);
                    if(!rs.IsDBNull(21))
                        b.Tomatoes = rs.GetBoolean(21);
                    if(!rs.IsDBNull(22))
                        b.WheatTortilla = rs.GetBoolean(22);
                    if(!rs.IsDBNull(23))
                        b.WhiteRice = rs.GetBoolean(23);
                    #endregion
                }
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in getBurrito: " + e1.Message + "\n" + e1.StackTrace);
                b = new Burrito();
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getBurrito: " + e2.Message + "\n" + e2.StackTrace);
                b = new Burrito();
            }
            finally
            {
                if (rs != null && !rs.IsClosed)
                    rs.Close();
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return b;
        }

        /// <summary>
        /// This method stores a burrito.
        /// </summary>
        /// <param name="b">The burrito object to store</param>
        /// <returns>Success/Failure</returns>
        public Boolean storeBurrito(Burrito b)
        {
            dLog.Info("Entering method storeBurrito | ID: " + b.id);
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT COUNT(1) FROM Burrito WHERE id = @id";

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
            catch (SqlException e1)
            {
                dLog.Error("SqlException in storeBurrito: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeBurrito: " + e2.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return result;
        }

        /// <summary>
        /// This method deletes a burrito.
        /// </summary>
        /// <param name="id">Unique ID of the burrito to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteBurrito(Int32 id)
        {
            dLog.Info("Entering method deleteBurrito | ID:" + id);
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "DELETE FROM Burrito WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in deleteBurrito: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteBurrito: " + e2.Message);
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
