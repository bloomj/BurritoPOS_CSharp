using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    /// <summary>
    /// This service implementation uses Serializable to do basic CRUD operations with files for Burrito objects.
    /// </summary>
    class BurritoSvcFileImpl : IBurritoSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String IBurritoSvc.NAME
        {
            get { return "IBurritoSvc"; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BurritoSvcFileImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        /// <summary>
        /// This method retrieves a burrito.
        /// </summary>
        /// <param name="id">Unique ID of burrito to retrieve</param>
        /// <returns>Success/Failure</returns>
	    public Burrito getBurrito(Int32 id) {
		    dLog.Info("Entering method getBurrito | ID: " + id);
		    Burrito b = null;
            Stream input = null;

		    try {
			    //ensure we were passed a valid object before attempting to write
                if (File.Exists("Burrito_" + id + ".txt"))
                {
                    input = File.Open("Burrito_" + id + ".txt", FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    b = (Burrito)bFormatter.Deserialize(input); 
			    }
		    } 
		    catch (IOException e1) {
                dLog.Error("IOException in getBurrito: " + e1.Message);
		    }
		    catch (TypeLoadException e2) {
                dLog.Error("TypeLoadException in getBurrito: " + e2.Message);
		    }
		    catch(Exception e3) {
                dLog.Error("Exception in getBurrito: " + e3.Message);
		    }
		    finally {
			    //ensure that input is close regardless of the errors in try/catch
			    if(input != null) {
                    input.Close();
			    }
		    }

		    return b;
	    }

        /// <summary>
        /// This method stores a burrito.
        /// </summary>
        /// <param name="b">The burrito object to store</param>
        /// <returns>Success/Failure</returns>
	    public Boolean storeBurrito(Burrito b) {
		    dLog.Info("Entering method storeBurrito | ID: "+b.id);
		    Stream output = null;
            Boolean result = false;

		    try {
			    //ensure we were passed a valid object before attempting to write
                if (b.validate())
                {
                    dLog.Debug("Burrito object is valid, writing to file");
                    output = File.Open("Burrito_" + b.id + ".txt", FileMode.Create);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(output, b);
                    result = true;
                }
                else
                    dLog.Debug("Burrito object is not valid");
		    } 
		    catch (IOException e1) {
                dLog.Error("IOException in storeBurrito: " + e1.Message);
			    result = false;
		    }
		    catch(Exception e2) {
                dLog.Error("Exception in storeBurrito: " + e2.Message);
			    result = false;
		    }
		    finally {
			    //ensure that output is close regardless of the errors in try/catch
			    if(output != null) {
				    output.Close();
			    }
		    }

		    return result;
	    }

        /// <summary>
        /// This method deletes a burrito.
        /// </summary>
        /// <param name="id">Unique ID of the burrito to delete</param>
        /// <returns>Success/Failure</returns>
	    public Boolean deleteBurrito(Int32 id) {
            dLog.Info("Entering method deleteBurrito | ID:" + id);
            Boolean result = false;

		    try {
			    // Ensure the file exists
			    if (!File.Exists("Burrito_"+id+".txt"))
				    throw new FileNotFoundException("deleteBurrito: no such file or directory: Burrito_"+id+".txt");

			    // Ensure the file is not locked
			    //if (!f.canWrite())
				    //throw new UnauthorizedAccessException("deleteBurrito: write protected: Burrito_"+id+".txt");	

			    // Attempt to delete it
                File.Delete("Burrito_" + id + ".txt");
                result = true;
		    }
		    catch(Exception e) {
                dLog.Error("Exception in deleteBurrito: " + e.Message);
			    result = false;
		    }

		    return result;
	    }
    }
}
