using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.Configuration;

namespace cPos.Admin.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            string logFolderPath = @"C:\cPos\log\admin_task\";
            try
            {
                int cycleTime = Convert.ToInt32(ConfigurationManager.AppSettings["cycleTime"].Trim()) * 1000;
                string batId = string.Empty;
                var apCustomerService = new cPos.Admin.Service.Implements.CustomerService();

                while (true)
                {
                    Console.WriteLine(string.Format("[{0}]导入数据任务批次开始...", Utils.GetNow()));

                    // 获取客户列表
                    Hashtable htCustomer = new Hashtable();
                    htCustomer["Status"] = "1";
                    IList<cPos.Admin.Model.Customer.CustomerInfo> customers =
                        apCustomerService.GetAllCustomerList(htCustomer);
                    WorkerType workerType;
                    Hashtable result = new Hashtable();
                    string taskBeginFormat = "[{0}] {2} {1}任务开始...";
                    string taskEndFormat = "[{0}] {2} {1}任务结束";
                    foreach (var customer in customers)
                    {
                        if (customer.Connect == null || customer.Connect.WsUrl.Trim().Length == 0)
                        {
                            customer.Connect = apCustomerService.GetCustomerConnectByID(customer.ID);
                        }

                        // MonitorLog
                        if (true)
                        {
                            workerType = WorkerType.MonitorLog;
                            batId = Utils.NewGuid();
                            Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                            var worker = new MonitorLogTask();
                            result = worker.Run(batId, customer);
                            Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                        }
                        // PosOrder
                        if (true)
                        {
                            workerType = WorkerType.PosOrder;
                            batId = Utils.NewGuid();
                            Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                            var worker = new PosOrderTask();
                            result = worker.Run(batId, customer);
                            Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                        }
                        // Ad
                        if (true)
                        {
                            workerType = WorkerType.AdOrder;
                            batId = Utils.NewGuid();
                            Console.WriteLine(string.Format(taskBeginFormat, Utils.GetNow(), workerType, customer.Code));
                            var worker = new AdOrderTask();
                            result = worker.Run(batId, customer);
                            Console.WriteLine(string.Format(taskEndFormat, Utils.GetNow(), workerType, customer.Code));
                        }
                    }

                    Console.WriteLine(string.Format("[{0}]导入数据任务批次结束", Utils.GetNow()));
                    Console.WriteLine(string.Format("".PadLeft(50, '=')));
                    Thread.Sleep(cycleTime);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveFile(logFolderPath, Utils.GetNowString() + ".log", ex.ToString());
                Console.Write(ex.ToString());
                Console.Read();
            }
        }
    }

    public enum WorkerType
    {
        PosOrder, MonitorLog, AdOrder
    }

}
