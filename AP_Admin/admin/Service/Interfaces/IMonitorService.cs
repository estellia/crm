using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Admin.Model;

namespace cPos.Admin.Service.Interfaces
{
    public interface IMonitorService
    {
        /// <summary>
        /// Monitor Log 保存
        /// </summary>
        Hashtable SaveMonitorLogList(bool IsTrans, IList<MonitorLogInfo> logs);
    }
}
