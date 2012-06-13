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
    public interface IAuthenticationSvc : IService
    {
        /// <summary>
        /// 
        /// </summary>
        String NAME { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Boolean login(Employee e, String password);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Boolean login(Manager m, String password);
    }
}
