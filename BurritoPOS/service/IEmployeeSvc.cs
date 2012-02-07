using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    interface IEmployeeSvc : IService
    {
        String NAME { get; }

        Boolean storeEmployee(Employee e);
	    Employee getEmployee(Int32 id);
	    Boolean deleteEmployee(Int32 id);
    }
}
