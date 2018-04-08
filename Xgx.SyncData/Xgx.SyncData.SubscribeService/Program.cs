using Topshelf;

namespace Xgx.SyncData.SubscribeService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceRunner>();
                x.RunAsLocalSystem();
                x.SetDescription("Xgx.SyncData.ProductSubscribeService");
                x.SetDisplayName("Xgx.SyncData.ProductSubscribeService");//显示的名字
                x.SetServiceName("Xgx.SyncData.ProductSubscribeService");//在这里定义服务名
            });
        }
    }
}
