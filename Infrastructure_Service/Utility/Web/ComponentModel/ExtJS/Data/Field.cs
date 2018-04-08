/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/7 13:12:11
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Data.Field
{
    /// <summary>
    /// Ext.data.Field 
    /// </summary>
    public class Field:ExtJSClass
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Field()
        {
            this.ClassFullName = "Ext.data.Field";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 字段的名称
        /// </summary>
        public string Name
        {
            get { return this.GetInitConfigValue<string>("name"); }
            set { this.SetInitConfigValue("name", value); }
        }

        /// <summary>
        /// 字段的数据类型
        /// </summary>
        public string Type
        {
            get { return this.GetInitConfigValue<string>("type"); }
            set { this.SetInitConfigValue("type", value); }
        }
        #endregion
    }
}
