using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using log4net;
using log4net.Config;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Wrappers;
using BurritoPOS.domain;

namespace BurritoPOS.service.Mongo
{
    /// <summary>
    /// This service implementation uses MongoDB Driver to do basic CRUD operations with MongoDB for Employee objects.
    /// </summary>
    class EmployeeSvcMongoImpl: IEmployeeSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IEmployeeSvc.NAME
        {
            get { return "IEmployeeSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmployeeSvcMongoImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a employee.
        /// </summary>
        /// <param name="id">Unique ID of employee to retrieve</param>
        /// <returns>employee object</returns>
        public Employee getEmployee(Int32 id)
        {
            dLog.Info("Entering method getEmployee | ID: " + id);
            Employee e = new Employee();

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("employee");
                    var query = new QueryDocument("id", id);

                    BsonDocument myDoc = coll.FindOne(query);

                    //ensure we were passed a valid object before attempting to read
                    if (myDoc != null)
                    {
                        dLog.Debug("myDoc: " + myDoc.ToString());

                        #region Read Fields
                        e.id = id;
                        e.firstName = myDoc["firstName"].AsString;
                        e.isManager = myDoc["isManager"].AsBoolean;
                        e.lastName = myDoc["lastName"].AsString;
                        #endregion
                    }
                    dLog.Debug("Finishing setting employee");
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getEmployee: " + e2.Message + "\n" + e2.StackTrace);
                e = new Employee();
            }
            finally
            {
                //using statement above already calls RequestDone()
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("employee");
                    var query = new QueryDocument("id", e.id);

                    dLog.Debug("Finding if employee exists");
                    BsonDocument myDoc = coll.FindOne(query);

                    query.Add("firstName", e.firstName);
                    query.Add("isManager", e.isManager);
                    query.Add("lastName", e.lastName);
                    
                    //ensure we were passed a valid object before attempting to write
                    if (myDoc == null)
                    {
                        dLog.Debug("Inserting employee");
                        coll.Insert(query);

                        result = true;
                    }
                    else
                    {
                        var update = new UpdateDocument();
                        update.Add(query.ToBsonDocument());
                        dLog.Debug("Updating employee");
                        dLog.Debug("myDoc: " + myDoc.ToString());
                        dLog.Debug("update Query: " + update.ToString());

                        SafeModeResult wr = coll.Update(new QueryDocument("id", e.id), update, SafeMode.True);

                        dLog.Debug("SafeModeResult: " + wr.Ok);
                        if (wr.LastErrorMessage == null && wr.Ok)
                        {
                            result = true;
                        }
                        else
                        {
                            dLog.Debug("SafeModeResult: " + wr.LastErrorMessage);
                        }
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in storeEmployee: " + e2.Message);
            }
            finally
            {
                //using statement above already calls RequestDone()
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("employee");
                    var query = new QueryDocument("id", id);

                    SafeModeResult wr = coll.Remove(query, SafeMode.True);

                    dLog.Debug("SafeModeResult: " + wr.Ok);
                    if (wr.LastErrorMessage == null && wr.Ok)
                    {
                        result = true;
                    }
                    else
                    {
                        dLog.Debug("SafeModeResult: " + wr.LastErrorMessage);
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in deleteEmployee: " + e2.Message);
            }
            finally
            {
                //using statement above already calls RequestDone()
            }

            return result;
        }
    }
}
