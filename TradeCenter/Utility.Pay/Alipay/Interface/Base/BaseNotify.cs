using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Base
{
    public abstract class BaseNotify
    {
        public BaseNotify()
        {
            DataParas = new Dictionary<string, string>();
        }

        public Dictionary<string, string> DataParas { get; set; }

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

        public void Load(Dictionary<string, string> pParas)
        {
            this.DataParas = pParas;
        }
    }
}
