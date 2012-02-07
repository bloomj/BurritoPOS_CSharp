using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    interface ICustomerSvc : IService
    {
        String NAME { get; }

        Boolean storeCustomer(Customer c);
	    Customer getCustomer(Int32 id);
	    Boolean deleteCustomer(Int32 id);
    }
}
