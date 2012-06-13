using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Xml;
using log4net;
using log4net.Config;
using BurritoPOS.service;
using BurritoPOS.domain;

namespace BurritoPOS.business
{
    /// <summary>
    /// 
    /// </summary>
    public static class Prices
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Decimal getItemPrice(String name)
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            Decimal result = new Decimal(-1);

            try
            {
                XmlTextReader reader = new XmlTextReader("config/prices.xml");

                while (reader.Read())
                { 
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "entry" && reader.HasAttributes && reader.GetAttribute("key") == name)
                    {
                        dLog.Debug("Got " + reader.NodeType + " node | Name: " + reader.Name + " | HasAttributes: " + reader.HasAttributes);
                        // move one to read element value
                        reader.Read();
                        dLog.Debug("Node Value: " + reader.Value);
                        result = Decimal.Parse(reader.Value);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in getItemPrice: " + e.Message + "\n" + e.StackTrace);
                result = new Decimal(-1);
            }

            return result;
        }
    }
}
