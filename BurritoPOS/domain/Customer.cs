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
    public class Customer
    {
        #region Properties
        /// <summary>
        /// Unique ID of Customer
        /// </summary>
        public virtual Int32 id { get; set; }
        
        /// <summary>
        /// Customer's first name
        /// </summary>
        public virtual String firstName { get; set; }
        
        /// <summary>
        /// Customer's last name
        /// </summary>
        public virtual String lastName { get; set; }
        
        /// <summary>
        /// Customer's email address
        /// </summary>
        public virtual String emailAddress { get; set; }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public Customer()
        {
            id = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        public Customer(Int32 id, String firstName, String lastName, String emailAddress)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
        }

        /// <summary>
        /// validates the object
        /// </summary>
        /// <returns>success or failure</returns>
        public virtual Boolean validate()
        {
            if (this.id > -1 && this.firstName != "" && this.lastName != "" && this.emailAddress != "")
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
		
		    if(obj == null || !this.GetType().Equals(obj.GetType()))
			    return false;
		
	        Customer other = (Customer) obj;
	        if (this.id != other.id || this.firstName != other.firstName || this.lastName != other.lastName || this.emailAddress != other.emailAddress)
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
