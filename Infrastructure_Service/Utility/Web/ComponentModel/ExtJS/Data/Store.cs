/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/9 13:10:41
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

using JIT.Utility.Web.ComponentModel.ExtJS.Data;
using JIT.Utility.Web.ComponentModel.ExtJS.Data.Proxy;

namespace JIT.Utility.Web.ComponentModel.ExtJS.Data
{
    /// <summary>
    /// Ext.data.Store 
    /// </summary>
    public class Store:ExtJSClass
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Store()
        {
            this.ClassFullName = "Ext.data.Store";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 数据集所使用的数据模型类的类全名
        /// </summary>
        public string ModelClassFullName
        {
            get { return this.GetInitConfigValue<string>("model"); }
            set { this.SetInitConfigValue("model", value); }
        }

        /// <summary>
        /// 数据集中所有记录的字段(与ModelClassFullName属性只要指定一个就可以)
        /// </summary>
        public List<Field.Field> Fields
        {
            get { return this.GetInitConfigValue<List<Field.Field>>("fields"); }
            set { this.SetInitConfigValue("fields", value); }
        }

        /// <summary>
        /// 数据集的代理（主要作用是读取&写入数据）
        /// </summary>
        public Proxy.Proxy Proxy
        {
            get { return this.GetInitConfigValue<Proxy.Proxy>("proxy"); }
            set { this.SetInitConfigValue("proxy", value); }
        }

        /// <summary>
        /// 数据集的ID
        /// </summary>
        public string StoreID
        {
            get { return this.GetInitConfigValue<string>("storeId"); }
            set { this.SetInitConfigValue("storeId", value); }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data
        {
            get { return this.GetInitConfigValue<object>("data"); }
            set { this.SetInitConfigValue("data", value); }
        }
        #endregion
    }
}
