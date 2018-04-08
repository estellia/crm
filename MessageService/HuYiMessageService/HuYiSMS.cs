using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.SMS.Base;
using System.Configuration;
using JIT.Utility.SMS.BLL;
using JIT.Utility.SMS.DataAccess;
using JIT.Utility;
using JIT.Utility.Cache;
using JIT.ManagementPlatform.Web.Module.BLL;
using JIT.Utility.SMS.Entity;
using JIT.ManagementPlatform.Web.Module.Entity;

namespace HuYiMessageService
{
    public class HuYiSMS : BaseSMS
    {
        #region 构造函数
        public HuYiSMS()
        {
            //wen.wu 20141017 update 根据短信信息表SMS_send 与客户账号表关联获取账号和用户名
            //this.Account = ConfigurationManager.AppSettings["HuYiAccount"];
            //this.Password = ConfigurationManager.AppSettings["HuYiPassword"];
        }
        #endregion

        #region 属性
        public string Account
        {
            get { return this.GetPara("account"); }
            set { this.SetPara("account", value); }
        }

        public string Password
        {
            get { return this.GetPara("password"); }
            set { this.SetPara("password", value); }
        }

        public string Mobile
        {
            get { return this.GetPara("mobile"); }
            set { this.SetPara("mobile", value); }
        }

        public string Content
        {
            get { return this.GetPara("content"); }
            set { this.SetPara("content", value); }
        }

        public string Sign
        {
            get { return this.GetPara("sign"); }
            set { this.SetPara("sign", value); }
        }
        #endregion


        public override string GetURL()
        {
            return ConfigurationManager.AppSettings["SMSSendURL"];
        }

        public override string GetParamStr()
        {
            throw new NotImplementedException();
        }

        public override byte[] GetData()
        {
            throw new NotImplementedException();
        }
    }
}
