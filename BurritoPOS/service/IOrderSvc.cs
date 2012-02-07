using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    interface IOrderSvc : IService
    {
        String NAME { get; }

        Boolean storeOrder(Order o);
	    Order getOrder(Int32 id);
	    Boolean deleteOrder(Int32 id);
	    ArrayList getAllOrders();
    }
}
