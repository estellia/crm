/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/9 13:17:24
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

using JIT.Utility.Web.ComponentModel.ExtJS.Data.Reader;

namespace JIT.Utility.Web.ComponentModel.ExtJS.Data.Proxy
{
    /// <summary>
    /// Ext.data.proxy.Proxy 
    /// </summary>
    public class Proxy:ExtJSClass
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Proxy()
        {
            this.ClassFullName = "Ext.data.proxy.Proxy ";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// proxy的类别
        /// </summary>
        public string Type
        {
            get { return this.GetInitConfigValue<string>("type"); }
            set { this.SetInitConfigValue("type", value); }
        }

        /// <summary>
        /// proxy的数据读取器
        /// </summary>
        public Reader.Reader Reader
        {
            get { return this.GetInitConfigValue<Reader.Reader>("reader"); }
            set { this.SetInitConfigValue("reader", value); }
        }

        /// <summary>
        /// proxy读取数据的url
        /// </summary>
        public string URL
        {
            get { return this.GetInitConfigValue<string>("url"); }
            set { this.SetInitConfigValue("url", value); }
        }
        #endregion
    }
}
