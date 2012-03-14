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
        #region Properties
        /// <summary>
        /// Manager's first name
        /// </summary>
        public virtual String firstName { get; set; }

        /// <summary>
        /// Manager's last name
        /// </summary>
        public virtual String lastName { get; set; }

        /// <summary>
        /// Unique ID of Manager
        /// </summary>
        public virtual Int32 id { get; set; }

        /// <summary>
        /// Is manager a manager?
        /// Will always be true
        /// </summary>
        public virtual Boolean isManager { get; set; }
        #endregion

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
		    this.id = employeeID;
		    this.isManager = true;
	    }

        /// <summary>
        /// validates the object
        /// </summary>
        /// <returns>success or failure</returns>
        public virtual Boolean validate()
        {
		    if(this.firstName != null && this.lastName != null && !this.id.Equals(null))
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
	        if (this.firstName != other.firstName || this.lastName != other.lastName || this.id != other.id)
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
