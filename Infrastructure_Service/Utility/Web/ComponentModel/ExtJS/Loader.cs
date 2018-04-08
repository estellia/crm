using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.Utility.Web.ComponentModel.ExtJS
{
    /// <summary>
    /// Ext JS的动态JS加载器
    /// </summary>
    [ToolboxData("<{0}:Loader runat=server></{0}:Loader>")]
    public class Loader : System.Web.UI.Control
    {
        #region 属性集
        /// <summary>
        /// 是否启用动态加载,默认为true
        /// </summary>
        [Bindable(true)]
        [DefaultValue(true)]
        public bool Enable { get; set; }

        /// <summary>
        /// 是否禁用浏览器的缓存,默认为真
        /// </summary>
        [Bindable(true)]
        [DefaultValue(true)]
        public bool DisableCaching { get; set; }
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
            //启用并设置延迟加载
            pOutput.WriteLine("Ext.Loader.setConfig({");
            pOutput.WriteLine(" enabled: {0}",this.Enable.ToString().ToLower());
            pOutput.WriteLine(" ,disableCaching:{0}", this.DisableCaching.ToString().ToLower());
            pOutput.WriteLine("});");
            pOutput.WriteLine("</script>");
        }
        #endregion
    }
}
