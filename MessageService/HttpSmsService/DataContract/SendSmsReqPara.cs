using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HttpSmsService.DataContract
{
    public class SendSmsReqPara
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public String MobileNO { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public String SMSContent { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public String Sign { get; set; }

        /// <summary>
        /// 模板code
        /// </summary>
        public String SmsTemplateCode { get; set; }

    }
}