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

        #region Properties
        /// <summary>
        /// Unique ID of Order
        /// </summary>
        public virtual Int32 id { get; set; }

	    //don't need Vector because Orders will not need to by synchronized (i.e. only one person should be modifying at a time)
        /// <summary>
        /// Burritos in order
        /// </summary>
        public virtual List<Burrito> burritos { get; set; }
        
        /// <summary>
        /// Date of Order
        /// </summary>
        public virtual DateTime orderDate { get; set; }

        /// <summary>
        /// Has order been submitted to kitchen?
        /// </summary>
        public virtual Boolean isSubmitted { get; set; }

        /// <summary>
        /// Has order been completed?
        /// </summary>
        public virtual Boolean isComplete { get; set; }

        /// <summary>
        /// Total cost of order
        /// </summary>
        public virtual Decimal totalCost { get; set; }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
	    public Order() {
            id = -1;
            burritos = new List<Burrito>();
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
	    public Order(Int32 orderID, List<Burrito> burritos, DateTime orderDate, Boolean isSubmitted, Boolean isComplete, Decimal totalCost) {
		    this.id = orderID;
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
        public virtual Boolean validate()
        {
            dLog.Debug("orderID: " + this.id + " | burritos: " + this.burritos.Count + " | isSubmitted: " + this.isSubmitted + " | isComplete: " + this.isComplete + " | totalCost: " + this.totalCost);
            if (this.id > -1 && this.burritos.Count > 0 && Decimal.Floor(this.totalCost) > 0)
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
	        if (this.id != other.id || this.burritos != other.burritos || this.orderDate != other.orderDate || this.isSubmitted != other.isSubmitted || this.isComplete != other.isComplete || this.totalCost != other.totalCost)
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
