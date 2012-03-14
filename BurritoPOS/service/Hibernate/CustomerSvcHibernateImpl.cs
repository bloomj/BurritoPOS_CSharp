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
    /// This service implementation uses NHibernate framework to do basic CRUD operations with MS SQL Server 2008 for Customer objects.
    /// </summary>
    class CustomerSvcHibernateImpl : BaseSvcHibernateImpl, ICustomerSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String ICustomerSvc.NAME
        {
            get { return "ICustomerSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CustomerSvcHibernateImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
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
            ISession session = null;

            try {
                using (session = getSession()) {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Customer WHERE id = :id");
                        query.SetParameter("id", id);

                        c = query.List<Customer>()[0];
                    }

                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getCustomer: " + e2.Message + "\n" + e2.StackTrace);
                c = new Customer();
            }
            finally
            {
                //ensure that session is close regardless of the errors in try/catch
                if (session != null && session.IsOpen)
                    session.Close();
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
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(c);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeCustomer: " + e2.Message);
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
        /// This method deletes a customer.
        /// </summary>
        /// <param name="id">Unique ID of the customer to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteCustomer(Int32 id)
        {
            dLog.Info("Entering method deleteCustomer | ID:" + id);
            Boolean result = false;
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Customer WHERE id = :id");
                        query.SetParameter("id", id);
                        Customer c = query.List<Customer>()[0];
                        session.Delete(c);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteCustomer: " + e2.Message);
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
