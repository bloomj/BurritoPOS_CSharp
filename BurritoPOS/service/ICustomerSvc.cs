using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerSvc : IService
    {
        /// <summary>
        /// 
        /// </summary>
        String NAME { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        Boolean storeCustomer(Customer c);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customer getCustomer(Int32 id);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean deleteCustomer(Int32 id);
    }
}
