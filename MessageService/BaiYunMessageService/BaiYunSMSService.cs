using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using JIT.Utility.SMS.DataAccess;
using JIT.Utility.Log;
using System.Threading;
using JIT.Utility.SMS;
using JIT.Utility;
using System.Configuration;

namespace BaiYunMessageService
{
    public partial class BaiYunSMSService : ServiceBase
    {
        public BaiYunSMSService()
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
                        var NO = item.MobileNO;
                        if(PhoneNOHelper.IsCTCC(NO))
                        {
                            item.Status = 2;
                        }
                        else if (PhoneNOHelper.IsCMCC(NO))
                        {
                            var result = item.GetSMS().Send(JIT.Utility.SMS.Base.SendType.Get);
                            if (IsSuccess(result))
                            {
                                item.Status = 1;
                                item.SendCount = (item.SendCount ?? 0) + 1;
                                item.Mtmsgid = result;
                                item.RegularlySendTime = DateTime.Now;
                                Loggers.Debug(new DebugLogInfo() { Message = "【发送成功】{0}【手机号】:{1}{0}【内容】:{2}{0}【返回码】：{3}".Fmt(Environment.NewLine, item.MobileNO, item.SMSContent, result) });
                            }
                            else
                            {
                                item.Status = 3;
                                item.SendCount = (item.SendCount ?? 0) + 1;
                                Loggers.Debug(new DebugLogInfo() { Message = "【发送失败】{0}【手机号】:{1}{0}【内容】:{2}{0}【错误信息】:{3}".Fmt(Environment.NewLine, item.MobileNO, item.SMSContent, result) });
                            }
                        }
                        else
                        {
                            var result = item.GetSMS().Send(JIT.Utility.SMS.Base.SendType.Get);
                            if (IsSuccess(result))
                            {
                                item.Status = 1;
                                item.SendCount = (item.SendCount ?? 0) + 1;
                                item.Mtmsgid = result;
                                item.RegularlySendTime = DateTime.Now;
                                Loggers.Debug(new DebugLogInfo() { Message = "【发送成功】{0}【手机号】:{1}{0}【内容】:{2}{0}【返回码】：{3}".Fmt(Environment.NewLine, item.MobileNO, item.SMSContent, result) });
                            }
                            else
                            {
                                item.SendCount = (item.SendCount ?? 0) + 1;
                                Loggers.Debug(new DebugLogInfo() { Message = "【发送失败】{0}【手机号】:{1}{0}【内容】:{2}{0}【错误信息】:{3}".Fmt(Environment.NewLine, item.MobileNO, item.SMSContent, result) });
                            }
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

        private bool IsSuccess(string result)
        {
            int k;
            return result.Length == 20 && result.All(t => int.TryParse(t.ToString(), out k));
        }
    }
}
