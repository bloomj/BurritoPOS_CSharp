using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurritoPOS.domain
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Manager
    {
        /// <summary>
        /// 
        /// </summary>
        public String firstName {get; set;}

        /// <summary>
        /// 
        /// </summary>
	    public String lastName {get; set;}

        /// <summary>
        /// 
        /// </summary>
	    public Int32 employeeID {get; set;}

        /// <summary>
        /// 
        /// </summary>
        public Boolean isManager {get; set;}

        /// <summary>
        /// Default constructor
        /// </summary>
	    public Manager() {

	    }
	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="employeeID"></param>
	    public Manager(String firstName, String lastName, Int32 employeeID) {
		    this.firstName = firstName;
		    this.lastName = lastName;
		    this.employeeID = employeeID;
		    this.isManager = true;
	    }

        /// <summary>
        /// validates the object
        /// </summary>
        /// <returns>success or failure</returns>
	    public Boolean validate() {
		    if(this.firstName != null && this.lastName != null && !this.employeeID.Equals(null))
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
		
	        Manager other = (Manager) obj;
	        if (this.firstName != other.firstName || this.lastName != other.lastName || this.employeeID != other.employeeID)
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
