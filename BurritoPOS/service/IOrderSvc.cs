using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderSvc : IService
    {
        /// <summary>
        /// 
        /// </summary>
        String NAME { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        Boolean storeOrder(Order o);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order getOrder(Int32 id);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean deleteOrder(Int32 id);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ArrayList getAllOrders();
    }
}
