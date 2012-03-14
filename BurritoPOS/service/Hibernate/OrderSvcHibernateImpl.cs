using System;
using System.Collections.Generic;
using System.Collections;
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
    /// This service implementation uses NHibernate framework to do basic CRUD operations with MS SQL Server 2008 for Order objects.
    /// </summary>
    class OrderSvcHibernateImpl : BaseSvcHibernateImpl, IOrderSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IOrderSvc.NAME
        {
            get { return "IOrderSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public OrderSvcHibernateImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
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
            ISession session = null;

            try {
                using (session = getSession()) {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Order WHERE id = :id");
                        query.SetParameter("id", id);

                        o = query.List<Order>()[0];
                    }

                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getOrder: " + e2.Message + "\n" + e2.StackTrace);
                o = new Order();
            }
            finally
            {
                //ensure that session is close regardless of the errors in try/catch
                if (session != null && session.IsOpen)
                    session.Close();
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
            Boolean result = false;
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(o);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeOrder: " + e2.Message);
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
        /// This method deletes a order.
        /// </summary>
        /// <param name="id">Unique ID of the order to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteOrder(Int32 id)
        {
            dLog.Info("Entering method deleteOrder | ID:" + id);
            Boolean result = false;
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Order WHERE id = :id");
                        query.SetParameter("id", id);
                        Order o = query.List<Order>()[0];
                        session.Delete(o);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteOrder: " + e2.Message);
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
        /// This method returns all historical orders.
        /// </summary>
        /// <returns>ArrayList of order objects</returns>
	    public ArrayList getAllOrders() {
		    dLog.Info("Entering method getAllOrders");
		    ArrayList result = new ArrayList();
		    ISession session = null;

		    try {
                using (session = getSession())
                {
                    session.Clear();

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Order");

                        foreach(Order o in query.List<Order>()) {
                            result.Add(o);
                        }

                        transaction.Commit();
                        session.Flush();
                        //session.evict(Order.class);
                    }
                }
		    } 
		    catch(Exception e2) {
                dLog.Error("Exception in getAllOrders: " + e2.Message);
		    }
		    finally {
			    //ensure that session is close regardless of the errors in try/catch
			    if (session != null && session.IsOpen) {
                    session.Close();
			    }
		    }

		    return result;
	    }
    }
}
