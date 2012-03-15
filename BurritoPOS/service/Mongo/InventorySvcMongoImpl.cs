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
    /// This service implementation uses MongoDB Driver to do basic CRUD operations with MongoDB for Inventory objects.
    /// </summary>
    class InventorySvcMongoImpl: IInventorySvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IInventorySvc.NAME
        {
            get { return "IInventorySvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public InventorySvcMongoImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a inventory.
        /// </summary>
        /// <param name="id">Unique ID of inventory to retrieve</param>
        /// <returns>inventory object</returns>
        public Inventory getInventory(Int32 id)
        {
            dLog.Info("Entering method getInventory | ID: " + id);
            Inventory i = new Inventory();

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("inventory");
                    var query = new QueryDocument("id", id);

                    BsonDocument myDoc = coll.FindOne(query);

                    //ensure we were passed a valid object before attempting to read
                    if (myDoc != null)
                    {
                        dLog.Debug("myDoc: " + myDoc.ToString());

                        #region Read Fields
                        i.id = id;
                        i.BeefQty = myDoc["beefQty"].AsInt32;
                        i.BlackBeansQty = myDoc["blackBeansQty"].AsInt32;
                        i.BrownRiceQty = myDoc["brownRiceQty"].AsInt32;
                        i.ChickenQty = myDoc["chickenQty"].AsInt32;
                        i.ChiliTortillaQty = myDoc["chiliTortillaQty"].AsInt32;
                        i.CucumberQty = myDoc["cucumberQty"].AsInt32;
                        i.FlourTortillaQty = myDoc["flourTortillaQty"].AsInt32;
                        i.GuacamoleQty = myDoc["guacamoleQty"].AsInt32;
                        i.HerbGarlicTortillaQty = myDoc["herbGarlicTortillaQty"].AsInt32;
                        i.HummusQty = myDoc["hummusQty"].AsInt32;
                        i.JalapenoCheddarTortillaQty = myDoc["jalapenoCheddarTortillaQty"].AsInt32;
                        i.JalapenosQty = myDoc["jalapenosQty"].AsInt32;
                        i.LettuceQty = myDoc["lettuceQty"].AsInt32;
                        i.OnionQty = myDoc["onionQty"].AsInt32;
                        i.PintoBeansQty = myDoc["pintoBeansQty"].AsInt32;
                        i.SalsaPicoQty = myDoc["salsaPicoQty"].AsInt32;
                        i.SalsaSpecialQty = myDoc["salsaSpecialQty"].AsInt32;
                        i.SalsaVerdeQty = myDoc["salsaVerdeQty"].AsInt32;
                        i.TomatoBasilTortillaQty = myDoc["tomatoBasilTortillaQty"].AsInt32;
                        i.TomatoesQty = myDoc["tomatoesQty"].AsInt32;
                        i.WheatTortillaQty = myDoc["wheatTortillaQty"].AsInt32;
                        i.WhiteRiceQty = myDoc["whiteRiceQty"].AsInt32;
                        #endregion
                    }
                    dLog.Debug("Finishing setting inventory");
                }
            }
            catch (Exception e2)
            {
                dLog.Error("Exception in getInventory: " + e2.Message + "\n" + e2.StackTrace);
                i = new Inventory();
            }
            finally
            {
                //using statement above already calls RequestDone()
            }

            return i;
        }

        /// <summary>
        /// This method stores a inventory.
        /// </summary>
        /// <param name="i">The inventory object to store</param>
        /// <returns>Success/Failure</returns>
        public Boolean storeInventory(Inventory i)
        {
            dLog.Info("Entering method storeInventory | ID: " + i.id);
            Boolean result = false;

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("inventory");
                    var query = new QueryDocument("id", i.id);

                    dLog.Debug("Finding if inventory exists");
                    BsonDocument myDoc = coll.FindOne(query);

                    query.Add("beefQty", i.BeefQty);
                    query.Add("blackBeansQty", i.BlackBeansQty);
                    query.Add("brownRiceQty", i.BrownRiceQty);
                    query.Add("chickenQty", i.ChickenQty);
                    query.Add("chiliTortillaQty", i.ChiliTortillaQty);
                    query.Add("cucumberQty", i.CucumberQty);
                    query.Add("flourTortillaQty", i.FlourTortillaQty);
                    query.Add("guacamoleQty", i.GuacamoleQty);
                    query.Add("herbGarlicTortillaQty", i.HerbGarlicTortillaQty);
                    query.Add("hummusQty", i.HummusQty);
                    query.Add("jalapenoCheddarTortillaQty", i.JalapenoCheddarTortillaQty);
                    query.Add("jalapenosQty", i.JalapenosQty);
                    query.Add("lettuceQty", i.LettuceQty);
                    query.Add("onionQty", i.OnionQty);
                    query.Add("pintoBeansQty", i.PintoBeansQty);
                    query.Add("salsaPicoQty", i.SalsaPicoQty);
                    query.Add("salsaSpecialQty", i.SalsaSpecialQty);
                    query.Add("salsaVerdeQty", i.SalsaVerdeQty);
                    query.Add("tomatoBasilTortillaQty", i.TomatoBasilTortillaQty);
                    query.Add("tomatoesQty", i.TomatoesQty);
                    query.Add("wheatTortillaQty", i.WheatTortillaQty);
                    query.Add("whiteRiceQty", i.WhiteRiceQty);
                    
                    //ensure we were passed a valid object before attempting to write
                    if (myDoc == null)
                    {
                        dLog.Debug("Inserting inventory");
                        coll.Insert(query);

                        result = true;
                    }
                    else
                    {
                        var update = new UpdateDocument();
                        update.Add(query.ToBsonDocument());
                        dLog.Debug("Updating inventory");
                        dLog.Debug("myDoc: " + myDoc.ToString());
                        dLog.Debug("update Query: " + update.ToString());

                        SafeModeResult wr = coll.Update(new QueryDocument("id", i.id), update, SafeMode.True);

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
                dLog.Error("Exception in storeInventory: " + e2.Message);
            }
            finally
            {
                //using statement above already calls RequestDone()
            }

            return result;
        }

        /// <summary>
        /// This method deletes a inventory.
        /// </summary>
        /// <param name="id">Unique ID of the inventory to delete</param>
        /// <returns>Success/Failure</returns>
        public Boolean deleteInventory(Int32 id)
        {
            dLog.Info("Entering method deleteInventory | ID:" + id);
            Boolean result = false;

            try
            {
                MongoServer server = MongoServer.Create();
                MongoDatabase db = server.GetDatabase("neatoBurrito");
                //MongoCredentials credentials = new MongoCredentials("username", "password");
                //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

                using (server.RequestStart(db))
                {
                    MongoCollection<BsonDocument> coll = db.GetCollection("inventory");
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
                dLog.Error("Exception in deleteInventory: " + e2.Message);
            }
            finally
            {
                //using statement above already calls RequestDone()
            }

            return result;
        }
    }
}
