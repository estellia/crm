/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/7 13:15:29
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

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Web.ComponentModel.ExtJS.Data.Field;

namespace JIT.Utility.Web.ComponentModel.ExtJS.Data
{
    /// <summary>
    /// Ext.data.Model
    /// </summary>
    public class Model:ExtJSClass
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Model()
        {
            this.ClassFullName = string.Empty;
            this.SetInitConfigValue("extend", "Ext.data.Model");
        }
        #endregion

        #region 属性集

        /// <summary>
        /// 所有的字段
        /// </summary>
        public List<Field.Field> Fields
        {
            get { return this.GetInitConfigValue<List<Field.Field>>("fields"); }
            set { this.SetInitConfigValue("fields", value); }
        }
        #endregion

        /// <summary>
        /// 重写ToCreateScript脚本,Model不支持手动创建实例
        /// </summary>
        /// <param name="pPrevTabs"></param>
        /// <returns></returns>
        public override string ToCreationScript(int pPrevTabs)
        {
            throw new NotSupportedException("Model的实例化通常由Ext JS自身来实现");
        }

        /// <summary>
        /// 生成Model定义的脚本
        /// </summary>
        /// <param name="pPrevTabs"></param>
        /// <returns></returns>
        public string ToDefineScript(int pPrevTabs)
        {
            if (string.IsNullOrEmpty(this.ClassFullName))
                throw new ArgumentNullException("this.ClassFullName");
            if (this.ClassFullName == "Ext.data.Model")
                throw new ArgumentException("要重新命名新的类.");
            return ExtJSClass.ToDefineScript(this.ClassFullName, this.InitConfigs, pPrevTabs);
        }
    }
}
