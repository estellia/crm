using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace Xgx.SyncData.PublishService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                
                x.Service<ServiceRunner>();
              
                x.RunAsLocalSystem();
                
                x.SetDescription("Xgx.SyncData.PublishService");
                
                x.SetDisplayName("Xgx.SyncData.PublishService");
               
                x.SetServiceName("Xgx.SyncData.PublishService");
                
            });
        }
    }
}
