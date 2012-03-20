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
    public interface IEmployeeSvc : IService
    {
        /// <summary>
        /// 
        /// </summary>
        String NAME { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Boolean storeEmployee(Employee e);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Employee getEmployee(Int32 id);
	    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean deleteEmployee(Int32 id);
    }
}
