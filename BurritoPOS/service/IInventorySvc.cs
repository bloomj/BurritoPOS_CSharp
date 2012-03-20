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
    public interface IInventorySvc : IService
    {
        /// <summary>
        /// 
        /// </summary>
        String NAME { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        Boolean storeInventory(Inventory i);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Inventory getInventory(Int32 id);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean deleteInventory(Int32 id);
    }
}
