using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using log4net;
using log4net.Config;
using NHibernate;
using NHibernate.Cfg;
using BurritoPOS.domain;

namespace BurritoPOS.service.Hibernate
{
    /// <summary>
    /// This service implementation uses NHibernate framework to do basic CRUD operations with MS SQL Server 2008 for Inventory objects.
    /// </summary>
    class InventorySvcHibernateImpl : BaseSvcHibernateImpl, IInventorySvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IInventorySvc.NAME
        {
            get { return "IInventorySvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public InventorySvcHibernateImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
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
            ISession session = null;

            try {
                using (session = getSession()) {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Inventory WHERE id = :id");
                        query.SetParameter("id", id);

                        i = query.List<Inventory>()[0];
                    }

                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getInventory: " + e2.Message + "\n" + e2.StackTrace);
                i = new Inventory();
            }
            finally
            {
                //ensure that session is close regardless of the errors in try/catch
                if (session != null && session.IsOpen)
                    session.Close();
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
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(i);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeInventory: " + e2.Message);
            }
            finally
            {
                //ensure that session is close regardless of the errors in try/catch
                if (session != null && session.IsOpen)
                    session.Close();
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
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Inventory WHERE id = :id");
                        query.SetParameter("id", id);
                        Inventory i = query.List<Inventory>()[0];
                        session.Delete(i);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteInventory: " + e2.Message);
            }
            finally
            {
                //ensure that session is close regardless of the errors in try/catch
                if (session != null && session.IsOpen)
                    session.Close();
            }

            return result;
        }
    }
}
