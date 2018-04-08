using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Web.ComponentModel.ExtJS.App
{
    /// <summary>
    /// EXT JS的MVC模式的Application
    /// </summary>
    [ToolboxData("<{0}:Application runat=\"server\"></{0}:Application>")]
    public class Application : System.Web.UI.Control
    {
        #region 属性集
        /// <summary>
        /// 应用的名称,默认为aspx文件的名称
        /// </summary>
        [Bindable(true)]
        public string Name { get; set; }

        /// <summary>
        /// 应用的文件夹
        /// </summary>
        [Bindable(true)]
        public string AppFolder { get; set; }

        /// <summary>
        /// 控制器的名称,默认为Name+'Ctl'后缀
        /// </summary>
        [Bindable(true)]
        public string[] Controllers { get; set; }

        /// <summary>
        /// 当前应用所属的菜单ID
        /// </summary>
        [Bindable(true)]
        public string MenuID { get; set; }

        /// <summary>
        /// 当前所属于的页面
        /// </summary>
        [Bindable(true)]
        public string View { get; set; }

        /// <summary>
        /// 用户在当前页面所能允许的操作
        /// </summary>
        [Bindable(true)]
        public List<Action> Actions { get; set; }

        /// <summary>
        /// 应用的一般处理程序的路径,默认为aspx页面目录下的/Handler/Handler.ashx
        /// </summary>
        [Bindable(true)]
        public string AjaxHandlerPath { get; set; }

        /// <summary>
        /// 多语言资源
        /// </summary>
        [Bindable(true)]
        public Dictionary<string, string> LanguageResources { get; set; }

        /// <summary>
        /// 应用的launch的脚本代码块
        /// </summary>
        public string LaunchJSBlock { get; set; }
        #endregion

        #region 初始化
        protected override void OnInit(EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                var fileName = this.Page.GetFileName();
                this.Name = fileName.Replace(".aspx",string.Empty);
            }
            if(!string.IsNullOrEmpty(this.View))
            {
                this.View = this.Page.GetAbsoluteUrl();
            }
            if (this.Controllers == null || this.Controllers.Length <= 0)
            {
                this.Controllers = new string[] { this.Name+"Ctl" };
            }
            if (this.Actions == null)
            {
                this.Actions = new List<Action>();
            }
            if (string.IsNullOrEmpty(this.AjaxHandlerPath))
            {
                var dir = this.Page.GetDirectory();
                this.AjaxHandlerPath = string.Format("{0}/Handler/Handler.ashx",dir);
            }
            //
            base.OnInit(e);
        }
        #endregion

        #region 呈现内容
        /// <summary>
        /// 呈现内容
        /// </summary>
        /// <param name="pOutput">输出</param>
        protected override void Render(HtmlTextWriter pOutput)
        {
            pOutput.WriteLine();
            pOutput.WriteLine("<script language=\"javascript\" type=\"text/javascript\">");
            //应用的入口
            //1.默认参数
            pOutput.WriteLine("Ext.application({");
            pOutput.WriteLine(" name:{1}{0}{1}",this.Name,JSONConst.STRING_WRAPPER);
            pOutput.WriteLine(" ,appFolder:{1}{0}{1}",this.AppFolder,JSONConst.STRING_WRAPPER);
            pOutput.WriteLine(" , controllers:{0}",this.Controllers.ToJoinString(',','\''));
            //2.自定义参数
            pOutput.WriteLine(" , params:{");
            pOutput.WriteLine("     ajaxHandlerPath: {1}{0}{1}", this.AjaxHandlerPath, JSONConst.STRING_WRAPPER);
            pOutput.WriteLine("     , menuID: {1}{0}{1}", this.MenuID, JSONConst.STRING_WRAPPER);
            pOutput.WriteLine("     , view: {1}{0}{1}", this.View, JSONConst.STRING_WRAPPER);
            pOutput.WriteLine("     , actions: {0}", this.Actions.ToArray().ToJSON());
            pOutput.WriteLine("     , languageResources: [");
            if (this.LanguageResources != null && this.LanguageResources.Count > 0)
            {
                bool isFirst = true;
                foreach (var item in this.LanguageResources)
                {
                    if (!isFirst)
                    {
                        pOutput.WriteLine("         ,{{ key: {2}{0}{2}, value: {2}{1}{2}}}", item.Key, item.Value,JSONConst.STRING_WRAPPER);
                    }
                    else
                    {
                        pOutput.WriteLine("         {{ key: {2}{0}{2}, value: {2}{1}{2}}}", item.Key, item.Value,JSONConst.STRING_WRAPPER);
                        isFirst = false;
                    }
                }
            }
            pOutput.WriteLine("     ]");
            pOutput.WriteLine(" }");
            pOutput.WriteLine(" , launch:function(){");
            if (!string.IsNullOrEmpty(this.LaunchJSBlock))
            {
                pOutput.WriteLine(this.LaunchJSBlock);
            }
            pOutput.WriteLine(" }");
            pOutput.WriteLine("});");
            pOutput.WriteLine("</script>");
        }
        #endregion
    }
}
