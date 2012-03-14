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
    /// This service implementation uses NHibernate framework to do basic CRUD operations with MS SQL Server 2008 for Manager objects.
    /// </summary>
    class ManagerSvcHibernateImpl : BaseSvcHibernateImpl, IManagerSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IManagerSvc.NAME
        {
            get { return "IManagerSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ManagerSvcHibernateImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a manager.
        /// </summary>
        /// <param name="id">Unique ID of manager to retrieve</param>
        /// <returns>burrito object</returns>
        public Manager getManager(Int32 id)
        {
            dLog.Info("Entering method getManager | ID: " + id);
            Manager m = new Manager();
            ISession session = null;

            try {
                using (session = getSession()) {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Manager WHERE id = :id");
                        query.SetParameter("id", id);

                        m = query.List<Manager>()[0];
                    }

                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getManager: " + e2.Message + "\n" + e2.StackTrace);
                m = new Manager();
            }
            finally
            {
                //ensure that session is close regardless of the errors in try/catch
                if (session != null && session.IsOpen)
                    session.Close();
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
            dLog.Info("Entering method storeManager | ID: " + m.id);
            Boolean result = false;
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(m);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeManager: " + e2.Message);
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
        /// This method deletes a manager.
        /// </summary>
        /// <param name="id">Unique ID of the manager to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteManager(Int32 id)
        {
            dLog.Info("Entering method deleteManager | ID:" + id);
            Boolean result = false;
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Manager WHERE id = :id");
                        query.SetParameter("id", id);
                        Manager m = query.List<Manager>()[0];
                        session.Delete(m);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteManager: " + e2.Message);
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
