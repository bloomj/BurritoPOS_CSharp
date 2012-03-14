using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using BurritoPOS.domain;
using FluentNHibernate;
using FluentNHibernate.Mapping;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.MappingModel;

namespace BurritoPOS.service.Hibernate
{
    class ManagerMappingOverride : ClassMap<Manager>,  IAutoMappingOverride<Manager>
    {
        public void Override(AutoMapping<Manager> mapping)
        {
            Table("Employee");
        }
    }
}
