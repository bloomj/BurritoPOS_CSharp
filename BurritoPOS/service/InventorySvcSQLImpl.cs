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
    /// This service implementation uses ADO.NET libraries to do basic CRUD operations with MS SQL Server 2008 for Inventory objects.
    /// </summary>
    class InventorySvcSQLImpl : IInventorySvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // if production, we would want to change this from SA authentication to Integrated Windows Authentication with service account
        private String connString = Properties.Settings.Default.NeatoBurritoDB;
        String IInventorySvc.NAME
        {
            get { return "IInventorySvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public InventorySvcSQLImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            dLog.Debug("connString: " + connString);         
        }

        /// <summary>
        /// This method retrieves a inventory.
        /// </summary>
        /// <param name="id">Unique ID of inventory to retrieve</param>
        /// <returns>inventory object</returns>
        public Inventory getInventory(Int32 id)
        {
            dLog.Info("Entering method getInventory | ID: " + id);
            Inventory i = new Inventory();
            SqlDataReader rs = null;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT BeefQty, BlackBeansQty, BrownRiceQty, ChickenQty, ChiliTortillaQty, CucumberQty, FlourTortillaQty, GuacamoleQty, HerbGarlicTortillaQty, HummusQty, ";
                sqlStr += "JalapenoCheddarTortillaQty, JalapenosQty, LettuceQty, OnionQty, PintoBeansQty, SalsaPicoQty, SalsaSpecialQty, SalsaVerdeQty, ";
                sqlStr += "TomatoBasilTortillaQty, TomatoesQty, WheatTortillaQty, WhiteRiceQty FROM Inventory WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));
                rs = stmt.ExecuteReader();

                while (rs.Read())
                {
                    dLog.Info("Got the " + rs.FieldCount + " fields of the record");
                    i.id = id;

                    #region Read Fields
                    if (!rs.IsDBNull(0))
                        i.BeefQty = rs.GetInt32(0);
                    if (!rs.IsDBNull(1))
                        i.BlackBeansQty = rs.GetInt32(1);
                    if (!rs.IsDBNull(2))
                        i.BrownRiceQty = rs.GetInt32(2);
                    if (!rs.IsDBNull(3))
                        i.ChickenQty = rs.GetInt32(3);
                    if (!rs.IsDBNull(4))
                        i.ChiliTortillaQty = rs.GetInt32(4);
                    if (!rs.IsDBNull(5))
                        i.CucumberQty = rs.GetInt32(5);
                    if (!rs.IsDBNull(6))
                        i.FlourTortillaQty = rs.GetInt32(6);
                    if (!rs.IsDBNull(7))
                        i.GuacamoleQty = rs.GetInt32(7);
                    if (!rs.IsDBNull(8))
                        i.HerbGarlicTortillaQty = rs.GetInt32(8);
                    if (!rs.IsDBNull(9))
                        i.HummusQty = rs.GetInt32(9);
                    if (!rs.IsDBNull(10))
                        i.JalapenoCheddarTortillaQty = rs.GetInt32(10);
                    if (!rs.IsDBNull(11))
                        i.JalapenosQty = rs.GetInt32(11);
                    if (!rs.IsDBNull(12))
                        i.LettuceQty = rs.GetInt32(12);
                    if (!rs.IsDBNull(13))
                        i.OnionQty = rs.GetInt32(13);
                    if (!rs.IsDBNull(14))
                        i.PintoBeansQty = rs.GetInt32(14);
                    if (!rs.IsDBNull(15))
                        i.SalsaPicoQty = rs.GetInt32(15);
                    if (!rs.IsDBNull(16))
                        i.SalsaSpecialQty = rs.GetInt32(16);
                    if (!rs.IsDBNull(17))
                        i.SalsaVerdeQty = rs.GetInt32(17);
                    if (!rs.IsDBNull(18))
                        i.TomatoBasilTortillaQty = rs.GetInt32(18);
                    if (!rs.IsDBNull(19))
                        i.TomatoesQty = rs.GetInt32(19);
                    if (!rs.IsDBNull(20))
                        i.WheatTortillaQty = rs.GetInt32(20);
                    if (!rs.IsDBNull(21))
                        i.WhiteRiceQty = rs.GetInt32(21);
                    #endregion
                }
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in getInventory: " + e1.Message + "\n" + e1.StackTrace);
                i = new Inventory();
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getInventory: " + e2.Message + "\n" + e2.StackTrace);
                i = new Inventory();
            }
            finally
            {
                if (rs != null && !rs.IsClosed)
                    rs.Close();
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "SELECT COUNT(1) FROM Inventory WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", i.id));

                if (Int32.Parse(stmt.ExecuteScalar().ToString()) > 0)
                {
                    //if first is a valid row, then we need to do an update
                    dLog.Info("Updating inventory in database");

                    sqlStr = "UPDATE Inventory SET BeefQty=@Beef, BlackBeansQty=@BlackBeans, BrownRiceQty=@BrownRice, ChickenQty=@Chicken, ChiliTortillaQty=@ChiliTortilla, ";
                    sqlStr += "CucumberQty=@Cucumber, FlourTortillaQty=@FlourTortilla, GuacamoleQty=@Guacamole, HerbGarlicTortillaQty=@HerbGarlicTortilla, HummusQty=@Hummus, ";
                    sqlStr += "JalapenoCheddarTortillaQty=@JalapenoCheddarTortilla, JalapenosQty=@Jalapenos, LettuceQty=@Lettuce, OnionQty=@Onion, PintoBeansQty=@PintoBeans, ";
                    sqlStr += "SalsaPicoQty=@SalsaPico, SalsaSpecialQty=@SalsaSpecial, SalsaVerdeQty=@SalsaVerde, TomatoBasilTortillaQty=@TomatoBasilTortilla, ";
                    sqlStr += "TomatoesQty=@Tomatoes, WheatTortillaQty=@WheatTortilla, WhiteRiceQty=@WhiteRice WHERE id=@id";
                }
                else
                {
                    //if first is null, then we need to do an insert
                    dLog.Info("Inserting inventory into database");

                    sqlStr = "INSERT INTO Inventory (BeefQty, BlackBeansQty, BrownRiceQty, ChickenQty, ChiliTortillaQty, CucumberQty,";
                    sqlStr += "FlourTortillaQty, GuacamoleQty, HerbGarlicTortillaQty, HummusQty, JalapenoCheddarTortillaQty, JalapenosQty,";
                    sqlStr += "LettuceQty, OnionQty, PintoBeansQty, SalsaPicoQty, SalsaSpecialQty, SalsaVerdeQty, TomatoBasilTortillaQty,";
                    sqlStr += "TomatoesQty, WheatTortillaQty, WhiteRiceQty, id) VALUES (@Beef, @BlackBeans, @BrownRice, @Chicken, @ChiliTortilla, @Cucumber,";
                    sqlStr += "@FlourTortilla, @Guacamole, @HerbGarlicTortilla, @Hummus, @JalapenoCheddarTortilla, @Jalapenos,";
                    sqlStr += "@Lettuce, @Onion, @PintoBeans, @SalsaPico, @SalsaSpecial, @SalsaVerde, @TomatoBasilTortilla,";
                    sqlStr += "@Tomatoes, @WheatTortilla, @WhiteRice, @id)";
                }

                dLog.Info("SQL Statement: " + sqlStr);
                stmt = new SqlCommand(sqlStr, conn);

                #region Add SQL Parameters
                stmt.Parameters.Add(new SqlParameter("@id", i.id));
                stmt.Parameters.Add(new SqlParameter("@Beef", i.BeefQty));
                stmt.Parameters.Add(new SqlParameter("@BlackBeans", i.BlackBeansQty));
                stmt.Parameters.Add(new SqlParameter("@BrownRice", i.BrownRiceQty));
                stmt.Parameters.Add(new SqlParameter("@Chicken", i.ChickenQty));
                stmt.Parameters.Add(new SqlParameter("@ChiliTortilla", i.ChiliTortillaQty));
                stmt.Parameters.Add(new SqlParameter("@Cucumber", i.CucumberQty));
                stmt.Parameters.Add(new SqlParameter("@FlourTortilla", i.FlourTortillaQty));
                stmt.Parameters.Add(new SqlParameter("@Guacamole", i.GuacamoleQty));
                stmt.Parameters.Add(new SqlParameter("@HerbGarlicTortilla", i.HerbGarlicTortillaQty));
                stmt.Parameters.Add(new SqlParameter("@Hummus", i.HummusQty));
                stmt.Parameters.Add(new SqlParameter("@JalapenoCheddarTortilla", i.JalapenoCheddarTortillaQty));
                stmt.Parameters.Add(new SqlParameter("@Jalapenos", i.JalapenosQty));
                stmt.Parameters.Add(new SqlParameter("@Lettuce", i.LettuceQty));
                stmt.Parameters.Add(new SqlParameter("@Onion", i.OnionQty));
                stmt.Parameters.Add(new SqlParameter("@PintoBeans", i.PintoBeansQty));
                stmt.Parameters.Add(new SqlParameter("@SalsaPico", i.SalsaPicoQty));
                stmt.Parameters.Add(new SqlParameter("@SalsaSpecial", i.SalsaSpecialQty));
                stmt.Parameters.Add(new SqlParameter("@SalsaVerde", i.SalsaVerdeQty));
                stmt.Parameters.Add(new SqlParameter("@TomatoBasilTortilla", i.TomatoBasilTortillaQty));
                stmt.Parameters.Add(new SqlParameter("@Tomatoes", i.TomatoesQty));
                stmt.Parameters.Add(new SqlParameter("@WheatTortilla", i.WheatTortillaQty));
                stmt.Parameters.Add(new SqlParameter("@WhiteRice", i.WhiteRiceQty));
                #endregion

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in storeInventory: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeInventory: " + e2.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
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
            dLog.Info("Entering method deleteInventory | ID:" + id);
            Boolean result = false;
            SqlConnection conn = null;
            SqlCommand stmt = null;

            try
            {
                String sqlStr = "DELETE FROM Inventory WHERE id = @id";

                conn = new SqlConnection(connString);
                conn.Open();
                stmt = new SqlCommand(sqlStr, conn);
                stmt.Parameters.Add(new SqlParameter("@id", id));

                if (stmt.ExecuteNonQuery() > 0)
                    result = true;
            }
            catch (SqlException e1)
            {
                dLog.Error("SqlException in deleteInventory: " + e1.Message);
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteInventory: " + e2.Message);
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
