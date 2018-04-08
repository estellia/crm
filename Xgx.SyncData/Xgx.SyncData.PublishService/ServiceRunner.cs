using System;
using EasyNetQ;
using log4net;
using Topshelf;
using Xgx.SyncData.Common;
using Xgx.SyncData.DomainPublishService;
using Zmind.EventBus.Contract;

namespace Xgx.SyncData.PublishService
{
    public class ServiceRunner : ServiceControl
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(ServiceRunner));
        private readonly IBus _bus = MqBusMgr.GetInstance();
        public bool Start(HostControl hostControl)
        {
            _bus.Subscribe<EventContract>("xgx", HandleEventContract);
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _bus.Dispose();
            return true;
        }

        private void HandleEventContract(EventContract msg)
        {
            try
            {
                var service = PublishFactory.GetInstance(msg);

                _log.Debug(new {
                    from="publish",
                    msg=msg
                });

                if (service != null)
                {
                    service.Deal(msg);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }
}
