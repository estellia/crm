using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JIT.Utility.Pay
{
    public abstract class BasicPara
    {
        #region 构造函数
        public BasicPara()
        {
            Paras = new Dictionary<string, object>();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 数值
        /// </summary>
        [JsonIgnore]
        protected Dictionary<string, object> Paras { get; set; }

        #endregion

        #region 方法
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="pParaName"></param>
        /// <returns></returns>
        protected string GetPara(string pParaName)
        {
            if (this.Paras.ContainsKey(pParaName))
                return this.Paras[pParaName].ToString();
            else
                return default(string);
        }
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="pParaName"></param>
        /// <param name="pValue"></param>
        protected void SetPara(string pParaName, object pValue)
        {
            this.Paras[pParaName] = pValue;
        }
        #endregion

        #region 抽象方法
        public abstract bool IsValid(out string msg);
        #endregion
    }
}
