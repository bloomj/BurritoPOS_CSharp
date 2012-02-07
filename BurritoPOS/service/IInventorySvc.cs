using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    interface IInventorySvc : IService
    {
        String NAME { get; }

        Boolean storeInventory(Inventory i);
	    Inventory getInventory(Int32 id);
	    Boolean deleteInventory(Int32 id);
    }
}
