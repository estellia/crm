using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.SMS.Base;

namespace HuYiMessageService
{
    public class Result : BaseResult
    {
        public string Code
        {
            get { return this.GetPara("code"); }
            set { this.SetPara("code", value); }
        }

        public string SMSID
        {
            get { return this.GetPara("smsid"); }
            set { this.SetPara("smsid", value); }
        }

        public string MSG
        {
            get { return this.GetPara("msg"); }
            set { this.SetPara("msg", value); }
        }

        public bool IsSuccess
        {
            get { return this.Code == "2"; }
        }
    }
}
