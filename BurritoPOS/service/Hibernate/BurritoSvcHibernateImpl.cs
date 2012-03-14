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
    /// This service implementation uses NHibernate framework to do basic CRUD operations with MS SQL Server 2008 for Burrito objects.
    /// </summary>
    class BurritoSvcHibernateImpl : BaseSvcHibernateImpl, IBurritoSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IBurritoSvc.NAME
        {
            get { return "IBurritoSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BurritoSvcHibernateImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
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
            ISession session = null;

            try {
                using (session = getSession()) {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Burrito WHERE id = :id");
                        query.SetParameter("id", id);
                        
                        b = query.List<Burrito>()[0];
                    }

                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getBurrito: " + e2.Message + "\n" + e2.StackTrace);
                b = new Burrito();
            }
            finally
            {
                //ensure that session is close regardless of the errors in try/catch
                if (session != null && session.IsOpen)
                    session.Close();
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
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(b);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeBurrito: " + e2.Message);
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
        /// This method deletes a burrito.
        /// </summary>
        /// <param name="id">Unique ID of the burrito to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteBurrito(Int32 id)
        {
            dLog.Info("Entering method deleteBurrito | ID:" + id);
            Boolean result = false;
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Burrito WHERE id = :id");
                        query.SetParameter("id", id);
                        Burrito b = query.List<Burrito>()[0];
                        session.Delete(b);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteBurrito: " + e2.Message);
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
