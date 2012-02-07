using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    interface IManagerSvc : IService
    {
        String NAME { get; }

        Boolean storeManager(Manager i);
	    Manager getManager(Int32 id);
        Boolean deleteManager(Int32 id);
    }
}
