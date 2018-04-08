/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/9 13:20:41
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Data.Reader
{
    /// <summary>
    /// Ext.data.reader.Reader 
    /// </summary>
    public class Reader:ExtJSClass
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Reader()
        {
            this.ClassFullName = "Ext.data.reader.Reader ";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// reader的类别
        /// </summary>
        public string Type
        {
            get { return this.GetInitConfigValue<string>("type"); }
            set { this.SetInitConfigValue("type", value); }
        }
        /// <summary>
        /// 数据的根
        /// </summary>
        public string Root
        {
            get { return this.GetInitConfigValue<string>("root"); }
            set { this.SetInitConfigValue("root", value); }
        }
        #endregion
    }
}
