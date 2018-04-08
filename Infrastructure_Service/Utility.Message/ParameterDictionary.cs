/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 12:45:23
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.Utility.Message
{
    /// <summary>
    /// 参数字典
    /// </summary>
    public class ParameterDictionary
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ParameterDictionary()
        {
            this.InnerDictionary = new Dictionary<string, object>();
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 参数字典
        /// </summary>
        public Dictionary<string, object> InnerDictionary { get; set; }
        #endregion

        #region 工具方法
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="pParameterName"></param>
        /// <param name="pParameterValue"></param>
        public void SetParam(string pParameterName, object pParameterValue)
        {
            if (string.IsNullOrWhiteSpace(pParameterName))
                throw new ArgumentNullException("pParameterName");
            if (pParameterValue == null)
                this.InnerDictionary.Remove(pParameterName);
            else
                this.InnerDictionary[pParameterName] = pParameterValue;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="pParameterName"></param>
        /// <returns></returns>
        public T GetParam<T>(string pParameterName)
        {
            var val = default(T);
            if (this.InnerDictionary.ContainsKey(pParameterName))
            {
                var temp = this.InnerDictionary[pParameterName];
                if (temp != null)
                {
                    val = (T)temp;
                }
            }
            return val;
        }
        #endregion
    }
}
