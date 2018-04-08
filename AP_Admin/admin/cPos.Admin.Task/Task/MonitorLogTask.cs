using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Admin.Model;
using cPos.Admin.Model.Customer;

namespace cPos.Admin.Task
{
    public class MonitorLogTask
    {
        public Hashtable Run(string batId, CustomerInfo customer)
        {
            string bizId = Utils.NewGuid();
            var data = new Hashtable();
            try
            {
                string customerId = customer.ID;
                int count = 0; // 总数量
                int rowsCount = 10; // 每页数量
                int startRow = 0;
                string unitId = null;

                var apMonitorService = new Service.MonitorService();
                var bsService = BsWebService.CreateMonitorLogService(customer);
                count = bsService.GetMonitorLogNotPackagedCount(customerId, unitId);

                IList<MonitorLogInfo> logList = new List<MonitorLogInfo>();
                string dataBatId = string.Empty;
                while (count > 0 && startRow < count)
                {
                    dataBatId = Utils.NewGuid();
                    logList.Clear();
                    var tmpList = bsService.GetMonitorLogListPackaged(customerId, unitId, 0, rowsCount, dataBatId);
                    if (tmpList != null && tmpList.Length > 0)
                    {
                        foreach (var tmpObj in tmpList)
                        {
                            var logObj = new MonitorLogInfo();
                            logObj.monitor_log_id = tmpObj.monitor_log_id;
                            logObj.customer_id = tmpObj.customer_id;
                            logObj.unit_id = tmpObj.unit_id;
                            logObj.pos_id = tmpObj.pos_id;
                            logObj.upload_time = tmpObj.upload_time;
                            logObj.remark = tmpObj.remark;
                            logObj.create_time = tmpObj.create_time;
                            logObj.create_user_id = tmpObj.create_user_id;
                            logList.Add(logObj);
                        }
                        // 保存
                        data = apMonitorService.SaveMonitorLogList(true, logList);

                        // 更新标记
                        bsService.SetMonitorLogIfFlagInfo(customerId, dataBatId);
                    }
                    startRow += logList.Count;
                }
                data["status"] = Utils.GetStatus(true);
                return data;
            }
            catch (Exception ex)
            {
                data["status"] = Utils.GetStatus(false);
                data["error"] = ex.ToString();
                Console.WriteLine(ex.ToString());
            }
            return data;
        }
    }
}
