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
    public interface IManagerSvc : IService
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
        Boolean storeManager(Manager i);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Manager getManager(Int32 id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean deleteManager(Int32 id);
    }
}
