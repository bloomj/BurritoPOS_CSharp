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
    /// This service implementation uses MongoDB Driver to do basic CRUD operations with MongoDB for Customer objects.
    /// </summary>
    class CustomerSvcMongoImpl: ICustomerSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String ICustomerSvc.NAME
        {
            get { return "ICustomerSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CustomerSvcMongoImpl()
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("customer");
                    var query = new QueryDocument("id", id);

                    BsonDocument myDoc = coll.FindOne(query);

                    //ensure we were passed a valid object before attempting to read
                    if (myDoc != null)
                    {
                        dLog.Debug("myDoc: " + myDoc.ToString());

                        #region Read Fields
                        c.id = id;
                        c.emailAddress = myDoc["emailAddress"].AsString;
                        c.firstName = myDoc["firstName"].AsString;
                        c.lastName = myDoc["lastName"].AsString;
                        #endregion
                    }
                    dLog.Debug("Finishing setting customer");
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getCustomer: " + e2.Message + "\n" + e2.StackTrace);
                c = new Customer();
            }
            finally
            {
                //using statement above already calls RequestDone()
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("customer");
                    var query = new QueryDocument("id", c.id);

                    dLog.Debug("Finding if customer exists");
                    BsonDocument myDoc = coll.FindOne(query);

                    query.Add("emailAddress", c.emailAddress);
                    query.Add("lastName", c.lastName);
                    query.Add("firstName", c.firstName);
                    
                    //ensure we were passed a valid object before attempting to write
                    if (myDoc == null)
                    {
                        dLog.Debug("Inserting customer");
                        coll.Insert(query);

                        result = true;
                    }
                    else
                    {
                        var update = new UpdateDocument();
                        update.Add(query.ToBsonDocument());
                        dLog.Debug("Updating customer");
                        dLog.Debug("myDoc: " + myDoc.ToString());
                        dLog.Debug("update Query: " + update.ToString());

                        SafeModeResult wr = coll.Update(new QueryDocument("id", c.id), update, SafeMode.True);

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
                dLog.Error("Exception in storeCustomer: " + e2.Message);
            }
            finally
            {
                //using statement above already calls RequestDone()
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("customer");
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
                dLog.Error("Exception in deleteBurrito: " + e2.Message);
            }
            finally
            {
                //using statement above already calls RequestDone()
            }

            return result;
        }
    }
}
