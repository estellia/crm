using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using JIT.MessageService.DataAccess;
//using JIT.MessageService.Entity;
//using JIT.MessageService;
using JIT.Utility;
using JIT.Utility.Log;
using JIT.Utility.SMS;
using JIT.Utility.SMS.Entity;
using JIT.Utility.SMS.DataAccess;
using HuYiMessageService;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public SMSSendEntity Current { get { return this.dataGridView1.CurrentRow.DataBoundItem as SMSSendEntity; } }
        //MessageHandler _handler = new MessageHandler();
        private void button1_Click(object sender, EventArgs e)
        {
            SMSSendDAO dao = new SMSSendDAO(new JIT.Utility.BasicUserInfo());
            var temp = dao.GetNoSend();
            this.dataGridView1.DataSource = temp.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 3000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //SMSSendDAO Dao = new SMSSendDAO(new BasicUserInfo());
            //var entities = Dao.GetNoSend().ToList().FindAll(t => PhoneNOHelper.IsPhoneNo(t.MobileNO));
            //foreach (var item in entities)
            //{
            //    var NO = item.MobileNO;
            //    if (PhoneNOHelper.IsPhoneNo(NO))
            //    {
            //        var result = _handler.Process(item);
            //        if (IsSuccess(result))
            //        {
            //            item.Status = 1;
            //            item.SendCount = (item.SendCount ?? 0) + 1;
            //            item.SendTime = DateTime.Now;
            //        }
            //        else
            //        {
            //            item.SendCount = (item.SendCount ?? 0) + 1;
            //        }
            //        Dao.Update(item);
            //    }

            //}
        }

        //private bool IsSuccess(string result)
        //{
        //    if (result.EndsWith("000"))
        //        return true;
        //    else
        //        return false;
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            //BaiYunSMS sms = new BaiYunSMS();
            //sms.Mobile = "18302159648";
            //sms.Message = "你好";
            //var result = sms.Send(JIT.Utility.SMS.Base.SendType.Get);
            //var blance = sms.GetBalance();

            SMSSendDAO Dao = new SMSSendDAO(new BasicUserInfo());
            Loggers.Debug(new DebugLogInfo() { Message = "开始查询数据库" });
            var entities = Dao.GetNoSend();
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("获取{0}条数据", entities.Length) });
            foreach (var item in entities)
            {
                var NO = item.MobileNO;
                var result = GetResult(item.GetSMS().Send2(JIT.Utility.SMS.Base.SendType.Get));
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
