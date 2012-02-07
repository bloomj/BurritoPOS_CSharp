using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BurritoPOS.domain;

namespace BurritoPOS.service
{
    interface IBurritoSvc : IService
    {
        String NAME {get;}

        Boolean storeBurrito(Burrito b);
        Burrito getBurrito(Int32 id);
        Boolean deleteBurrito(Int32 id);
    }
}
