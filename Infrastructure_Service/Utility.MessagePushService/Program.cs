using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace JIT.Utility.MessagePushService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new MessagePushService() 
            };
            ServiceBase.Run(ServicesToRun);

            //if(args.Length==0)//这是服务启动的条件
            //{
            //    ServiceBase[] ServicesToRun;
            //    ServicesToRun = new ServiceBase[] 
            //    { 
            //        new MessagePushService() 
            //    };
            //    ServiceBase.Run(ServicesToRun);
            //}
            //else//即有启动选项的命令行参数DEBUG了
            //{
            //        //将服务中的类当作一个普通类来使用即可，如
            //    MessagePushService srv = new MessagePushService();
            //    ServiceBase.Run(ServicesToRun);
            //        //srv..DoSomeThing();
            //        Console.Read();
            //}
        }
    }
}
