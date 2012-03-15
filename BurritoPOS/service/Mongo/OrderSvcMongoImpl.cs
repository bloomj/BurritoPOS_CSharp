using System;
using System.Collections;
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
    /// This service implementation uses MongoDB Driver to do basic CRUD operations with MongoDB for Order objects.
    /// </summary>
    class OrderSvcMongoImpl: IOrderSvc
    {
        private Factory factory;
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IBurritoSvc burritoSvc;
        String IOrderSvc.NAME
        {
            get { return "IOrderSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public OrderSvcMongoImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            factory = Factory.getInstance();
            burritoSvc = (IBurritoSvc) factory.getService("IBurritoSvc");
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("order");
                    var query = new QueryDocument("id", id);

                    BsonDocument myDoc = coll.FindOne(query);

                    //ensure we were passed a valid object before attempting to read
                    if (myDoc != null)
                    {
                        dLog.Debug("myDoc: " + myDoc.ToString());

                        #region Read Order Fields
                        o.id = id;
                        o.isComplete = myDoc["isComplete"].AsBoolean;
                        o.isSubmitted = myDoc["isSubmitted"].AsBoolean;
                        o.orderDate = myDoc["orderDate"].AsDateTime;
                        o.totalCost = Decimal.Parse(myDoc["totalCost"].AsString);
                        #endregion

                        //get burritos
                        dLog.Debug("Getting burritos");
                        coll = db.GetCollection("burrito");
                        query.Clear();
                        query.Add("orderID", id);
                        MongoCursor cur = coll.Find(query);

                        o.burritos = new List<Burrito>();

                        foreach (var doc in cur)
                        {
                            myDoc = (BsonDocument)doc;
                            Burrito b = new Burrito();

                            //cleaner but too many extra queries this way
                            //o.burritos.Add(burritoSvc.getBurrito(myDoc["id"].AsInt32));

                            #region Read burrito Fields
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

                            o.burritos.Add(b);
                        }
                    }
                    dLog.Debug("Finishing setting order");
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getOrder: " + e2.Message + "\n" + e2.StackTrace);
                o = new Order();
            }
            finally
            {
                //using statement above already calls RequestDone()
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

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("order");
                    var query = new QueryDocument("id", o.id);

                    dLog.Debug("Finding if order exists");
                    BsonDocument myDoc = coll.FindOne(query);

                    query.Add("isComplete", o.isComplete);
                    query.Add("isSubmitted", o.isSubmitted);
                    query.Add("orderDate", o.orderDate);
                    query.Add("totalCost", o.totalCost.ToString());
                    
                    //ensure we were passed a valid object before attempting to write
                    if (myDoc == null)
                    {
                        dLog.Debug("Inserting order");
                        coll.Insert(query);

                        result = true;
                    }
                    else
                    {
                        var update = new UpdateDocument();
                        update.Add(query.ToBsonDocument());
                        dLog.Debug("Updating order");
                        dLog.Debug("myDoc: " + myDoc.ToString());
                        dLog.Debug("update Query: " + update.ToString());

                        SafeModeResult wr = coll.Update(new QueryDocument("id", o.id), update, SafeMode.True);

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

                    //now insert the burritos
                    if (result)
                    {
                        dLog.Debug("Trying to insert " + o.burritos.Count + " burritos");

                        var index = 0;
                        foreach (Burrito b in o.burritos)
                        {
                            b.orderID = o.id;

                            dLog.Debug("Set order ID " + o.id + " for burrito: " + index);
                            if (b.validate())
                            {
                                dLog.Debug("Storing burrito: " + index);
                                result = burritoSvc.storeBurrito(b);
                            }
                            index++;
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
        /// This method deletes a order.
        /// </summary>
        /// <param name="id">Unique ID of the order to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteOrder(Int32 id)
        {
            dLog.Info("Entering method deleteOrder | ID:" + id);
            Boolean result = false;

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("order");
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
                dLog.Error("Exception in deleteOrder: " + e2.Message);
            }
            finally
            {
                //using statement above already calls RequestDone()
            }

            return result;
        }

        //TODO: come back and reduce number of reads on DB
        /// <summary>
        /// This method returns all historical orders.
        /// </summary>
        /// <returns>ArrayList of order objects</returns>
        public ArrayList getAllOrders()
        {
            dLog.Info("Entering method getAllOrders");
            ArrayList result = new ArrayList();

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("order");

                    MongoCursor cur = coll.FindAll();

                    foreach (var doc in cur)
                    {
                        result.Add(getOrder(((BsonDocument)doc)["id"].AsInt32));
                    }
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getAllOrders: " + e2.Message);
            }
            finally
            {
                //using statement above already calls RequestDone()
            }

            return result;
        }
    }
}
