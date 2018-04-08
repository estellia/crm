using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zmind.EventBus.Contract;

namespace Xgx.SyncData.DomainPublishService
{
    public interface IPublish
    {
        void Deal(EventContract msg);
    }
}
