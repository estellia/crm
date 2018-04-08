using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using JIT.TradeCenter.BLL;
using System.Threading;
using JIT.TradeCenter.Entity;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;

namespace PayCenterNotifyService
{
    public partial class NotifyService : ServiceBase
    {
        public NotifyService()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            var tempIntval = ConfigurationManager.AppSettings["Intval"];
            if (string.IsNullOrEmpty(tempIntval))
                _intval = 5;
            else
            {
                int m;
                if (!int.TryParse(tempIntval, out m))
                {
                    m = 5;
                }
                _intval = m;
            }
        }

        private BackgroundWorker bw;
        private int _intval;
        private static int _runCount;
        protected override void OnStart(string[] args)
        {
            bw.RunWorkerAsync();
        }

        protected override void OnStop()
        {
            bw = null;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    var bll = new AppOrderBLL(new JIT.Utility.BasicUserInfo());
                    //获取未通知的订单信息
                    var entitys = bll.GetNotNodify();
                    Loggers.Debug(new DebugLogInfo() { Message = string.Format("找到{0}条待通知记录", entitys.Length) });
                    foreach (var item in entitys)
                    {
                        string msg;
                        if (NotifyHandler.Notify(item, out msg))
                        {
                            item.IsNotified = true;
                        }
                        else
                        {
                            //设定下次通知时间
                            item.NextNotifyTime = GetNextNotifyTime(item.NotifyCount ?? 0);
                        }
                        //NotifyCount++
                        item.NotifyCount++;
                        //更新数据
                        bll.Update(item);
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                }
                _runCount++;
                if (_runCount % 100 == 0)
                    Loggers.Debug(new DebugLogInfo() { Message = string.Format("轮循了{0}次", _runCount) });
                Thread.Sleep(TimeSpan.FromSeconds(_intval));
            }
        }



        private DateTime GetNextNotifyTime(int NotifyCount)
        {
            switch (NotifyCount)
            {
                case 0:
                    return DateTime.Now.AddSeconds(30);
                case 1:
                    return DateTime.Now.AddMinutes(3);
                case 2:
                    return DateTime.Now.AddMinutes(10);
                case 3:
                    return DateTime.Now.AddMinutes(30);
                case 4:
                    return DateTime.Now.AddHours(1);
                case 5:
                    return DateTime.Now.AddHours(6);
                default:
                    return DateTime.Now.AddDays(1);
            }
        }
    }
}
