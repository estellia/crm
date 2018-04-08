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
using JIT.Utility.Notification;
using JIT.Utility.SMS.DataAccess;
using JIT.Utility;
using JIT.Utility.SMS.Util;
using System.Threading;
using JIT.Utility.SMS;
using JIT.Utility.ExtensionMethod;

namespace HuYiMessageService
{
    partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += (s, e) => DoWork();
          //  DoWork();
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

        /// <summary>
        /// 签名,收邮件的人
        /// </summary>
        private Dictionary<string, string> _dict;

        protected override void OnStart(string[] args)
        {
            bw.RunWorkerAsync(); 
            LoadConfig();
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
                        var result = GetResult(item.GetSMS().Send2(JIT.Utility.SMS.Base.SendType.Get));
                        if (result.IsSuccess)
                        {
                            item.Status = 1;
                            item.SendCount = (item.SendCount ?? 0) + 1;
                            item.Mtmsgid = result.SMSID;
                            item.RegularlySendTime = DateTime.Now;
                            Loggers.Debug(new DebugLogInfo() { Message = "【发送成功】{0}【手机号】:{1}{0}【内容】:{2}{0}【返回码】：{3}".Fmt(Environment.NewLine, item.MobileNO, item.SMSContent, result.ToJSON()) });
                        }
                        else
                        {

                            var msg = "【发送失败】{0}【手机号】:{1}{0}【内容】:{2}{0}【错误信息】:{3}".Fmt(Environment.NewLine, item.MobileNO, item.SMSContent, result.ToJSON());
                            item.SendCount = (item.SendCount ?? 0) + 1;
                            Loggers.Debug(new DebugLogInfo { Message = msg });
                            if (item.SendCount > 2)
                            {
                                Loggers.Debug(new DebugLogInfo
                                {
                                    Message = item.SendCount.ToString() + _sendMailForFailMessage == null ? "空" : "非空"
                                });
                                if (!string.IsNullOrEmpty(item.Sign))
                                {
                                    string mailto = string.Empty;
                                    if (_dict.TryGetValue(item.Sign, out mailto))
                                    {
                                        _sendMailForFailMessage.BeginInvoke(msg, item.Sign, mailto, null, null);
                                    }
                                }
                                else
                                {
                                    Loggers.Debug(new DebugLogInfo
                                    {
                                        Message = "没有签名"
                                    });
                                }
                            }
                        }
                        Dao.Update(item);
                        //由于平台对发送频率有限制,同一个号码一分钟内不能发送2次以上,所以对4081的代码作延迟1分钟处理.
                        if (result.Code == "4081")
                            Thread.Sleep(TimeSpan.FromMinutes(1));
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(CycleInterval ?? 5));
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    Thread.Sleep(TimeSpan.FromMinutes(2));
                }
            }

            #endregion
        }

        private Result GetResult(string pResult)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "平台接口返回：" + pResult });
            try
            {
                var dic = Util.GetDic(pResult);
                Result result = new Result();
                result.Load(dic);
                return result;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                return new Result() { Code = "****", MSG = ex.Message, SMSID = "----" };
            }
        }

        private void LoadConfig()
        {
            _dict = new Dictionary<string, string>();
            var mailto = ConfigurationManager.AppSettings["MailTo"];
            if (!string.IsNullOrEmpty(mailto))
            {
                var items = mailto.Split('$');
                foreach (string item in items)
                {
                    var kv = item.Split('|');
                    if (kv.Length > 1)
                    {
                        _dict.Add(kv[0], kv[1]);
                    }
                }
            }
        }

                /// <summary>
        /// 短信发送失败时邮件提醒
        /// </summary>
        /// <param name="pResult">错误消息</param>
        /// <param name="sign">签名</param>
        /// <param name="mailTo">收件人</param>
        private Action<string, string, string> _sendMailForFailMessage = (pResult, sign, mailTo) =>
        {
            Loggers.Debug(new DebugLogInfo { Message = "Body:" + pResult });

            if (string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(mailTo))
            {
                Loggers.Debug(new DebugLogInfo { Message = "Return" });
                return;
            }
           
            var isSuccess = Mail.SendMail(mailTo, string.Format("{0}短信发送失败", sign), pResult);
            Loggers.Debug(new DebugLogInfo
            {
                Message = string.Format("{1}短信发送失败邮件提醒发送{0}", isSuccess ? "成功" : "失败", sign)
            });
        };
    }
}
