using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using JIT.Utility.Log;
using JIT.Utility.SMS.DataAccess;
using JIT.Utility;
using JIT.Utility.SMS.Util;
using System.Threading;
using JIT.Utility.SMS;

namespace HuYiMessageService
{
    partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += (s, e) => DoWork();
            try
            {
                CycleInterval = Convert.ToInt32(ConfigurationManager.AppSettings["CycleInterval"]);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
            }
        }
        BackgroundWorker bw;
        int? CycleInterval;
        protected override void OnStart(string[] args)
        {
            bw.RunWorkerAsync();
        }

        protected override void OnStop()
        {
            bw.Dispose();
        }

        private void DoWork()
        {
            #region 一条一条发送

            while (true)
            {
                try
                {
                    SMSSendDAO Dao = new SMSSendDAO(new BasicUserInfo());
                    Loggers.Debug(new DebugLogInfo() { Message = "开始查询数据库" });
                    var entities = Dao.GetNoSend();
                    Loggers.Debug(new DebugLogInfo() { Message = string.Format("获取{0}条数据", entities.Length) });
                    foreach (var item in entities)
                    {
                        var result = GetResult(item.GetSMS().Send(JIT.Utility.SMS.Base.SendType.Get));
                        if (result.IsSuccess)
                        {
                            item.Status = 1;
                            item.SendCount = (item.SendCount ?? 0) + 1;
                            item.Mtmsgid = result.SMSID;
                            item.RegularlySendTime = DateTime.Now;
                            Loggers.Debug(new DebugLogInfo() { Message = "【发送成功】{0}【手机号】:{1}{0}【内容】:{2}{0}【返回码】：{3}".Fmt(Environment.NewLine, item.MobileNO, item.SMSContent, result) });
                        }
                        else
                        {
                            item.SendCount = (item.SendCount ?? 0) + 1;
                            Loggers.Debug(new DebugLogInfo() { Message = "【发送失败】{0}【手机号】:{1}{0}【内容】:{2}{0}【错误信息】:{3}".Fmt(Environment.NewLine, item.MobileNO, item.SMSContent, result) });
                        }
                        Dao.Update(item);
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(CycleInterval ?? 5));
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                }
            }

            #endregion
        }

        private Result GetResult(string pResult)
        {
            var dic = Util.GetDic(pResult);
            Result result = new Result();
            result.Load(dic);
            return result;
        }
    }
}
