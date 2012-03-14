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
    /// This service implementation uses NHibernate framework to do basic CRUD operations with MS SQL Server 2008 for Employee objects.
    /// </summary>
    class EmployeeSvcHibernateImpl : BaseSvcHibernateImpl, IEmployeeSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IEmployeeSvc.NAME
        {
            get { return "IEmployeeSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmployeeSvcHibernateImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a employee.
        /// </summary>
        /// <param name="id">Unique ID of employee to retrieve</param>
        /// <returns>burrito object</returns>
        public Employee getEmployee(Int32 id)
        {
            dLog.Info("Entering method getEmployee | ID: " + id);
            Employee e = new Employee();
            ISession session = null;

            try {
                using (session = getSession()) {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Employee WHERE id = :id");
                        query.SetParameter("id", id);

                        e = query.List<Employee>()[0];
                    }

                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getBurrito: " + e2.Message + "\n" + e2.StackTrace);
                e = new Employee();
            }
            finally
            {
                //ensure that session is close regardless of the errors in try/catch
                if (session != null && session.IsOpen)
                    session.Close();
            }

            return e;
        }

        /// <summary>
        /// This method stores a employee.
        /// </summary>
        /// <param name="e">The employee object to store</param>
        /// <returns>Success/Failure</returns>
        public Boolean storeEmployee(Employee e)
        {
            dLog.Info("Entering method storeEmployee | ID: " + e.id);
            Boolean result = false;
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(e);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeEmployee: " + e2.Message);
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
        /// This method deletes a employee.
        /// </summary>
        /// <param name="id">Unique ID of the employee to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteEmployee(Int32 id)
        {
            dLog.Info("Entering method deleteEmployee | ID:" + id);
            Boolean result = false;
            ISession session = null;

            try
            {
                using (session = getSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        IQuery query = session.CreateQuery(@"FROM Employee WHERE id = :id");
                        query.SetParameter("id", id);
                        Employee e = query.List<Employee>()[0];
                        session.Delete(e);
                        transaction.Commit();

                        if (transaction.WasCommitted)
                            result = true;
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteEmployee: " + e2.Message);
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
