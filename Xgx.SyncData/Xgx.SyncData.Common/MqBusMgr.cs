using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Loggers;

namespace Xgx.SyncData.Common
{
    public static class MqBusMgr
    {
        private static IBus _bus;
        private static readonly object SyncObj = new object();
        static MqBusMgr()
        {
            var mqHost = ConfigMgr.RabbitMqHost;
            _bus = RabbitHutch.CreateBus(mqHost, reg => reg.Register<IEasyNetQLogger>(log => new NullLogger()));
        }

        public static IBus GetInstance()//单例模式
        {
            if (_bus == null)
            {
                lock (SyncObj)
                {
                    if (_bus == null)
                    {
                        var mqHost = ConfigMgr.RabbitMqHost;
                        _bus = RabbitHutch.CreateBus(mqHost, reg => reg.Register<IEasyNetQLogger>(log => new NullLogger()));
                    }
                }
            }
            return _bus;
        }
    }
}
