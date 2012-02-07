using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    interface IAuthenticationSvc : IService
    {
        String NAME { get; }

        Boolean login(Employee e, String password);
        Boolean login(Manager m, String password);
    }
}
