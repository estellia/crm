using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.Model.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace cPos.Admin.Component
{
    public class BusinessServiceProxyLocator
    {
        //castle在webconfig里配置的
        //   <component id="CustomerService" service="cPos.Admin.Service.Interfaces.ICustomerService, cPos.Admin.Service" type="cPos.Admin.Service.Implements.CustomerService, cPos.Admin.Service" lifestyle="singleton" />
 
        private static IWindsorContainer container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

        public static object GetService(Type serviceType)
        {
            return container[serviceType];
        }
    }
}
