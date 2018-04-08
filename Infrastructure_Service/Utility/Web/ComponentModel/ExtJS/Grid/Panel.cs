/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/9 13:46:12
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

using JIT.Utility.Web.ComponentModel.ExtJS.Container;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Web.ComponentModel.ExtJS.Grid
{
    /// <summary>
    /// Ext.grid.Panel 
    /// </summary>
    public class Panel:Container.Container
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public Panel()
        {
            this.ClassFullName = "Ext.grid.Panel";
            this.XType = "gridpanel";
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 表格的标题
        /// </summary>
        public string Title
        {
            get { return this.GetInitConfigValue<string>("title"); }
            set { this.SetInitConfigValue("title", value); }
        }

        /// <summary>
        /// 表格的数据集
        /// <remarks>
        /// <para>1.采用IJavascriptObject使得可以更灵活的对该属性赋值</para>
        /// </remarks>
        /// </summary>
        public IJavascriptObject Store
        {
            get { return this.GetInitConfigValue<IJavascriptObject>("store"); }
            set { this.SetInitConfigValue("store", value); }
        }

        /// <summary>
        /// 表格的所有列
        /// </summary>
        public List<Column.Column> Columns
        {
            get { return this.GetInitConfigValue<List<Column.Column>>("columns"); }
            set { this.SetInitConfigValue("columns", value); }
        }

        /// <summary>
        /// 表格呈现到哪里
        /// </summary>
        public IJavascriptObject RenderTo
        {
            get { return this.GetInitConfigValue<IJavascriptObject>("renderTo"); }
            set { this.SetInitConfigValue("renderTo", value); }
        }

        /// <summary>
        /// 表格的特性
        /// </summary>
        public List<IJavascriptObject> Features
        {
            get { return this.GetInitConfigValue<List<IJavascriptObject>>("features"); }
            set { this.SetInitConfigValue("features", value); }
        }

        /// <summary>
        /// 表格周边停靠的项
        /// </summary>
        public List<ExtJSComponent> DockedItems 
        {
            get { return this.GetInitConfigValue<List<ExtJSComponent>>("dockedItems"); }
            set { this.SetInitConfigValue("dockedItems", value); }
        }

        /// <summary>
        /// 表格顶部的工具条的项
        /// </summary>
        public List<ExtJSComponent> TopBarItems
        {
            get { return this.GetInitConfigValue<List<ExtJSComponent>>("tbar"); }
            set { this.SetInitConfigValue("tbar", value); }
        }

        /// <summary>
        /// 表格右边的工具条的项
        /// </summary>
        public List<ExtJSComponent> RightBarItems
        {
            get { return this.GetInitConfigValue<List<ExtJSComponent>>("rbar"); }
            set { this.SetInitConfigValue("rbar", value); }
        }

        /// <summary>
        /// 表格左边的工具条的项
        /// </summary>
        public List<ExtJSComponent> LeftBarItems
        {
            get { return this.GetInitConfigValue<List<ExtJSComponent>>("lbar"); }
            set { this.SetInitConfigValue("lbar", value); }
        }


        /// <summary>
        /// 表格底部的工具条的项
        /// </summary>
        public List<ExtJSComponent> BottomBarItems
        {
            get { return this.GetInitConfigValue<List<ExtJSComponent>>("bbar"); }
            set { this.SetInitConfigValue("bbar", value); }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="pEventName">事件名</param>
        /// <param name="pFunction">事件处理函数</param>
        /// <returns></returns>
        public IJavascriptObject AddListener(string pEventName, JSFunction pFunction)
        {
            if (string.IsNullOrWhiteSpace(this.ID))
                throw new ArgumentException("组件的ID不能为空或null.");
            JavascriptBlock script = new JavascriptBlock();
            script.AddSentence("Ext.getCmp({0}{1}{0}).addListener({0}{2}{0},{3});", JSONConst.STRING_WRAPPER, this.ID, pEventName, pFunction.FunctionName);
            return script;
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="pEventName">事件名</param>
        /// <param name="pFunction">事件处理函数</param>
        /// <returns></returns>
        public IJavascriptObject RemoveListener(string pEventName, JSFunction pFunction)
        {
            if (string.IsNullOrWhiteSpace(this.ID))
                throw new ArgumentException("组件的ID不能为空或null.");
            JavascriptBlock script = new JavascriptBlock();
            script.AddSentence("Ext.getCmp({0}{1}{0}).removeListener({0}{2}{0},{3});",JSONConst.STRING_WRAPPER,this.ID,pEventName,pFunction.FunctionName);
            return script;
        }
        #endregion
    }
}
