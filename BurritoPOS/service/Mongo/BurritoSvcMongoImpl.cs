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
    /// This service implementation uses MongoDB Driver to do basic CRUD operations with MongoDB for Burrito objects.
    /// </summary>
    class BurritoSvcMongoImpl : IBurritoSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IBurritoSvc.NAME
        {
            get { return "IBurritoSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BurritoSvcMongoImpl()
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("burrito");
                    var query = new QueryDocument("id", id);

                    BsonDocument myDoc = coll.FindOne(query);

                    //ensure we were passed a valid object before attempting to read
                    if (myDoc != null)
                    {
                        dLog.Debug("myDoc: " + myDoc.ToString());

                        #region Read Fields
                        b.id = id;
                        b.Beef = myDoc["beef"].AsBoolean;
                        b.BlackBeans = myDoc["blackBeans"].AsBoolean;
                        b.BrownRice = myDoc["brownRice"].AsBoolean;
                        b.Chicken = myDoc["chicken"].AsBoolean;
                        b.ChiliTortilla = myDoc["chiliTortilla"].AsBoolean;
                        b.Cucumber = myDoc["cucumber"].AsBoolean;
                        b.FlourTortilla = myDoc["flourTortilla"].AsBoolean;
                        b.Guacamole = myDoc["guacamole"].AsBoolean;
                        b.HerbGarlicTortilla = myDoc["herbGarlicTortilla"].AsBoolean;
                        b.Hummus = myDoc["hummus"].AsBoolean;
                        b.JalapenoCheddarTortilla = myDoc["jalapenoCheddarTortilla"].AsBoolean;
                        b.Jalapenos = myDoc["jalapenos"].AsBoolean;
                        b.Lettuce = myDoc["lettuce"].AsBoolean;
                        b.Onion = myDoc["onion"].AsBoolean;
                        b.orderID = myDoc["orderID"].AsInt32;
                        b.PintoBeans = myDoc["pintoBeans"].AsBoolean;
                        b.Price = Decimal.Parse(myDoc["price"].AsString);
                        b.SalsaPico = myDoc["salsaPico"].AsBoolean;
                        b.SalsaSpecial = myDoc["salsaSpecial"].AsBoolean;
                        b.SalsaVerde = myDoc["salsaVerde"].AsBoolean;
                        b.TomatoBasilTortilla = myDoc["tomatoBasilTortilla"].AsBoolean;
                        b.Tomatoes = myDoc["tomatoes"].AsBoolean;
                        b.WheatTortilla = myDoc["wheatTortilla"].AsBoolean;
                        b.WhiteRice = myDoc["whiteRice"].AsBoolean;
                        #endregion
                    }
                    dLog.Debug("Finishing setting burrito");
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getBurrito: " + e2.Message + "\n" + e2.StackTrace);
                b = new Burrito();
            }
            finally
            {
                //using statement above already calls RequestDone()
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("burrito");
                    var query = new QueryDocument("id", b.id);

                    dLog.Debug("Finding if burrito exists");
                    BsonDocument myDoc = coll.FindOne(query);

                    query.Add("beef", b.Beef);
                    query.Add("blackBeans", b.BlackBeans);
                    query.Add("brownRice", b.BrownRice);
                    query.Add("chicken", b.Chicken);
                    query.Add("chiliTortilla", b.ChiliTortilla);
                    query.Add("cucumber", b.Cucumber);
                    query.Add("flourTortilla", b.FlourTortilla);
                    query.Add("guacamole", b.Guacamole);
                    query.Add("herbGarlicTortilla", b.HerbGarlicTortilla);
                    query.Add("hummus", b.Hummus);
                    query.Add("jalapenoCheddarTortilla", b.JalapenoCheddarTortilla);
                    query.Add("jalapenos", b.Jalapenos);
                    query.Add("lettuce", b.Lettuce);
                    query.Add("onion", b.Onion);
                    query.Add("orderID", b.orderID);
                    query.Add("pintoBeans", b.PintoBeans);
                    query.Add("price", b.Price.ToString());
                    query.Add("salsaPico", b.SalsaPico);
                    query.Add("salsaSpecial", b.SalsaSpecial);
                    query.Add("salsaVerde", b.SalsaVerde);
                    query.Add("tomatoBasilTortilla", b.TomatoBasilTortilla);
                    query.Add("tomatoes", b.Tomatoes);
                    query.Add("wheatTortilla", b.WheatTortilla);
                    query.Add("whiteRice", b.WhiteRice);
                    
                    //ensure we were passed a valid object before attempting to write
                    if (myDoc == null)
                    {
                        dLog.Debug("Inserting burrito");
                        coll.Insert(query);

                        result = true;
                    }
                    else
                    {
                        var update = new UpdateDocument();
                        update.Add(query.ToBsonDocument());
                        dLog.Debug("Updating burrito");
                        dLog.Debug("myDoc: " + myDoc.ToString());
                        dLog.Debug("update Query: " + update.ToString());

                        SafeModeResult wr = coll.Update(new QueryDocument("id", b.id), update, SafeMode.True);

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
                dLog.Error("Exception in storeBurrito: " + e2.Message);
            }
            finally
            {
                //using statement above already calls RequestDone()
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("burrito");
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
