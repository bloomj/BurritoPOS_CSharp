using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NHibernate;
using NHibernate.Cfg;

namespace BurritoPOS.service
{
    /// <summary>
    /// 
    /// </summary>
    class BaseSvcHibernateImpl
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Configuration config = new Configuration();
        private static ISessionFactory sessionFactory = null;

        //Hibernate
        private static ISessionFactory getSessionFactory() {
            try {
				if (sessionFactory == null) {
				    config.AddAssembly("hibernate.cfg");
                    
                    sessionFactory = config.BuildSessionFactory();
				}
            }
            catch(Exception e) {
                dLog.Error("Exception in getSessionFactory: "+e.Message);
                Console.WriteLine("Exception in getSessionFactory: "+e.Message);
            }

            return sessionFactory;
        }

        protected static ISession getSession()
        {
            ISessionFactory factory = getSessionFactory();
            return (factory != null) ? factory.OpenSession() : null;
        }
    }
}
