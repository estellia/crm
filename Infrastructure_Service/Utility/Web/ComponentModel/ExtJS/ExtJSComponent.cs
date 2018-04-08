/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/8 17:31:54
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

using JIT.Utility.Web.ComponentModel.ExtJS.Layout.Container;

namespace JIT.Utility.Web.ComponentModel.ExtJS
{
    /// <summary>
    /// Ext JS组件基类
    /// </summary>
    public class ExtJSComponent:ExtJSClass
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ExtJSComponent()
        {
        }
        #endregion

        #region 属性集

        /// <summary>
        /// ExtJS的xtype类名
        /// </summary>
        public string XType { get;set; }

        /// <summary>
        /// 组件的ID
        /// </summary>
        public string ID
        {
            get { return this.GetInitConfigValue<string>("id"); }
            set { this.SetInitConfigValue("id", value); }
        }

        /// <summary>
        /// 组件的宽度(单位px)
        /// </summary>
        public int Width
        {
            get { return this.GetInitConfigValue<int>("width"); }
            set { this.SetInitConfigValue("width", value); }
        }

        /// <summary>
        /// 组件的高度(单位px)
        /// </summary>
        public int Height
        {
            get { return this.GetInitConfigValue<int>("height"); }
            set { this.SetInitConfigValue("height", value); }
        }

        /// <summary>
        /// 组件是否隐藏
        /// </summary>
        public bool? IsHidden
        {
            get { return this.GetInitConfigValue<bool?>("hidden"); }
            set { this.SetInitConfigValue("hidden", value); }
        }

        /// <summary>
        /// 组件是否处于不启用状态
        /// </summary>
        public bool? IsDisabled
        {
            get { return this.GetInitConfigValue<bool?>("disabled"); }
            set { this.SetInitConfigValue("disabled", value); }
        }

        /// <summary>
        /// 组件停靠的方式
        /// </summary>
        public DockStyles? Dock
        {
            get { return this.GetInitConfigValue<DockStyles?>("dock"); }
            set { this.SetInitConfigValue("dock", value); }
        }

        /// <summary>
        /// HTML片段，该片段在组件呈现之后在插入
        /// </summary>
        public IJavascriptObject HTML
        {
            get { return this.GetInitConfigValue<IJavascriptObject>("html"); }
            set { this.SetInitConfigValue("html", value); }
        }

        /// <summary>
        /// 事件监听
        /// </summary>
        public ExtJSListeners Listeners
        {
            get { return this.GetInitConfigValue<ExtJSListeners>("listeners"); }
            set { this.SetInitConfigValue("listeners", value); }
        }
        #endregion

        #region 如果父容器的布局为Table布局时才有效的属性
        /// <summary>
        /// 行合并
        /// <remarks>
        /// <para>1.如果父容器的布局为Table布局时才有效的属性</para>
        /// </remarks>
        /// </summary>
        public int? Rowspan
        {
            get { return this.GetInitConfigValue<int?>("rowspan"); }
            set { this.SetInitConfigValue("rowspan", value); }
        }

        /// <summary>
        /// 列合并
        /// <remarks>
        /// <para>1.如果父容器的布局为Table布局时才有效的属性</para>
        /// </remarks>
        /// </summary>
        public int? Colspan
        {
            get { return this.GetInitConfigValue<int?>("colspan"); }
            set { this.SetInitConfigValue("colspan", value); }
        }

        /// <summary>
        /// 单元格的css类
        /// <remarks>
        /// <para>1.如果父容器的布局为Table布局时才有效的属性</para>
        /// </remarks>
        /// </summary>
        public string CellCSSClasses
        {
            get { return this.GetInitConfigValue<string>("cellCls"); }
            set { this.SetInitConfigValue("cellCls", value); }
        }
        #endregion

        #region 生成类实例创建配置项的脚本
        /// <summary>
        /// 生成类实例创建配置项的脚本
        /// <remarks>
        /// <para>1.此种方式为采用xtype来创建.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns>Ext JS使用xtype创建类实例的语句</returns>
        public virtual string ToCreationConfigScript(int pPrevTabs)
        {
            if (string.IsNullOrEmpty(this.XType))
            {
                throw new ArgumentNullException(this.XType);
            }
            Dictionary<string, object> configs = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(this.XType))
            {
                configs.Add("xtype", this.XType);
            }
            if (this.InitConfigs != null)
            {
                foreach (var config in this.InitConfigs)
                {
                    configs.Add(config.Key, config.Value);
                }
            }
            //
            return ExtJSClass.ToConfigScript(pPrevTabs, configs);
        }
        #endregion

        #region 重写ToScriptBlock的实现
        /// <summary>
        /// 重写ToScriptBlock的实现
        /// <remarks>
        /// <para>组件的语句脚本采用xtype的方式来生成</para>
        /// </remarks>
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns></returns>
        public override string ToScriptBlock(int pPrevTabs)
        {
            return this.ToCreationConfigScript(pPrevTabs);
        }
        #endregion
    }
}
