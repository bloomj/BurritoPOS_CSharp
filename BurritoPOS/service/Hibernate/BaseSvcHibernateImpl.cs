using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using log4net;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using FluentNHibernate.MappingModel;

namespace BurritoPOS.service.Hibernate
{
    /// <summary>
    /// This is the base implementation for the Hibernate service classes;
    /// </summary>
    class BaseSvcHibernateImpl
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Configuration config = new Configuration();
        private static ISessionFactory sessionFactory = null;

        //Hibernate via XML
        /*private static ISessionFactory getSessionFactory() {
            try {
				if (sessionFactory == null) {
                    config.Configure("config/Hibernate/hibernate.cfg.xml");
                    //config.AddDirectory(new DirectoryInfo("config/hibernate"));
                    //config.AddAssembly(Assembly.GetCallingAssembly());
                    
                    sessionFactory = config.BuildSessionFactory();
				}
            }
            catch(Exception e) {
                dLog.Error("Exception in getSessionFactory: "+e.Message + "\n" + e.StackTrace);
            }

            return sessionFactory;
        }*/

        protected static ISession getSession()
        {
            //ISessionFactory factory = getSessionFactory();
            ISessionFactory factory = BuildSessionFactory();
            return (factory != null) ? factory.OpenSession() : null;
        }

        //Hibernate via Fluent Hibernate
        private static ISessionFactory BuildSessionFactory()
        {
            try {
                if (sessionFactory == null)
                {
                    dLog.Debug("sessionFactory is null | Going to create AutoPersistance model for: " + System.Reflection.Assembly.GetCallingAssembly().ToString());
                    AutoPersistenceModel model = CreateMappings();

                    if (model != null)
                    {
                        dLog.Info("Got autoMapped model");
 
                        sessionFactory = Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2008
                            .ConnectionString(c => c
                                .Server(".\\JIMTEST")
                                .Database("NeatoBurrito")
                                .Username("root")
                                .Password("admin")))
                            .Mappings(m => m
                                .AutoMappings.Add(model))
                            .ExposeConfiguration(BuildSchema)
                            .BuildSessionFactory();
                    }
                    else
                    {
                        dLog.Info("Unable to autoMap model");
                        throw new Exception("Unable to autoMap model");
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Error("Exception in BuildSessionFactory: " + e.Message + "\n" + e.StackTrace + "\n" + e.InnerException);
            }

            return sessionFactory;
        }

        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap
                .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(t => t.Namespace == "BurritoPOS.domain")
                .UseOverridesFromAssemblyOf<ManagerMappingOverride>();
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true);
        }
    }
}
