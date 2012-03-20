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
    public interface IBurritoSvc : IService
    {
        /// <summary>
        /// 
        /// </summary>
        String NAME {get;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        Boolean storeBurrito(Burrito b);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Burrito getBurrito(Int32 id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Boolean deleteBurrito(Int32 id);
    }
}
