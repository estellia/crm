using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.SMS.Base;

namespace HuYiMessageService
{
    public static class ExtensionMethod
    {
        public static BaseSMS GetSMS(this JIT.Utility.SMS.Entity.SMSSendEntity entity)
        {
            HuYiSMS sms = new HuYiSMS();
            sms.Mobile = entity.MobileNO;
            sms.Content = entity.SMSContent;
            sms.Sign = entity.Sign;
            sms.Account = entity.Account;
            sms.Password = entity.Password;
            return sms;
        }
    }
}
