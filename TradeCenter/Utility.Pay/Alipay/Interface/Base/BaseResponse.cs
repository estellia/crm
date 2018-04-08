using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Base
{
    public abstract class BaseResponse
    {
        public BaseResponse()
        {
            DataParas = new Dictionary<string, string>();
        }
        protected Dictionary<string, string> DataParas { get; set; }
        public abstract void Load(Dictionary<string, string> Paras);
        protected string GetDataPara(string pParaName)
        {
            if (this.DataParas.ContainsKey(pParaName))
                return this.DataParas[pParaName];
            else
                return default(string);
        }
        protected void SetDataPara(string pParaName, string pValue)
        {
            this.DataParas[pParaName] = pValue;
        }
    }
}
