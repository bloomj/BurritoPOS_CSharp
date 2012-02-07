using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using log4net;
using log4net.Config;
using System.IO;

namespace BurritoPOS.domain
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Order
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        public Int32 orderID { get; set; }

	    //changing a generic array out for an ArrayList here to dynamically and easily add/subtract the burritos
	    //don't need Vector because Orders will not need to by synchronized (i.e. only one person should be modifying at a time)
	    //private Burrito[] burritos;
        /// <summary>
        /// 
        /// </summary>
        public ArrayList burritos { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime orderDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Boolean isSubmitted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Boolean isComplete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal totalCost { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
	    public Order() {
            orderID = -1;
            burritos = new ArrayList();
	    }
	
	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="orderID"></param>
	    /// <param name="burritos"></param>
	    /// <param name="orderDate"></param>
	    /// <param name="isSubmitted"></param>
	    /// <param name="isComplete"></param>
	    /// <param name="totalCost"></param>
	    public Order(Int32 orderID, ArrayList burritos, DateTime orderDate, Boolean isSubmitted, Boolean isComplete, Decimal totalCost) {
		    this.orderID = orderID;
		    this.burritos = burritos;
		    this.orderDate = orderDate;
		    this.isSubmitted = isSubmitted;
		    this.isComplete = isComplete;
		    this.totalCost = totalCost;
	    }

        /// <summary>
        /// validates the object
        /// </summary>
        /// <returns>success or failure</returns>
	    public Boolean validate() {
            dLog.Debug("orderID: " + this.orderID + " | burritos: " + this.burritos.Count + " | isSubmitted: " + this.isSubmitted + " | isComplete: " + this.isComplete + " | totalCost: " + this.totalCost);
            if (this.orderID > -1 && this.burritos.Count > 0 && Decimal.Floor(this.totalCost) > 0)
			    return true;
		    else
			    return false;
	    }

        /// <summary>
        /// Checks if the objects are equal
        /// </summary>
        /// <returns>success or failure</returns>
        public override Boolean Equals(Object obj)
        {
		    if(this == obj)
			    return true;

            if (obj == null || !this.GetType().Equals(obj.GetType()))
                return false;
		
	        Order other = (Order) obj;
	        if (this.orderID != other.orderID || this.burritos != other.burritos || this.orderDate != other.orderDate || this.isSubmitted != other.isSubmitted || this.isComplete != other.isComplete || this.totalCost != other.totalCost)
	    	    return false;
	    
	        return true;
	    }

        /// <summary>
        /// Returns base object GetHashCode
        /// </summary>
        /// <returns>Unique Hash of Object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
