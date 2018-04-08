using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.SMS.Base
{
    public abstract class BaseResult
    {
        public BaseResult()
        {
            DataPara = new Dictionary<string, string>();
        }

        protected Dictionary<string, string> DataPara { get; set; }

        protected string GetPara(string pKey)
        {
            if (this.DataPara.ContainsKey(pKey))
                return DataPara[pKey];
            else
                return default(string);
        }

        protected void SetPara(string pKey, string pValue)
        {
            this.DataPara[pKey] = pValue;
        }

        public void Load(Dictionary<string, string> pPara)
        {
            this.DataPara = pPara;
        }
    }
}
