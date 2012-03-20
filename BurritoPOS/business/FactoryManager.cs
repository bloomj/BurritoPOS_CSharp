using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BurritoPOS.service;
using BurritoPOS.exception;

namespace BurritoPOS.business
{
    /// <summary>
    /// Factory Manager for service objects
    /// </summary>
    public abstract class FactoryManager
    {
        /// <summary>
        /// Factory service object
        /// </summary>
        protected Factory factory = Factory.getInstance();

        /// <summary>
        /// This method get service implementation based on name.
        /// </summary>
        /// <param name="name">Name of service object</param>
        /// <returns>service object</returns>
        protected IService getService(String name)
        {
            return factory.getService(name);
        }
    }
}
