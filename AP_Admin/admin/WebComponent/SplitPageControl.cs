using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace WebComponent
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:SplitPageControl runat=server></{0}:SplitPageControl>")]
    public class SplitPageControl : WebControl, IPostBackEventHandler, IPostBackDataHandler
    {
        public SplitPageControl()
            : base(HtmlTextWriterTag.Div)
        {
            SplitPageCommand_Key = 0xff01;
            RequireUpdate_Key = 0xff02;
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        #region Define
        private readonly object SplitPageCommand_Key;
        private readonly object RequireUpdate_Key;
        #endregion

        #region Event
        [Category("Action")]
        public event SplitPageCommandEventHandler SplitPageCommand
        {
            add { base.Events.AddHandler(SplitPageCommand_Key, value); }
            remove { base.Events.RemoveHandler(SplitPageCommand_Key, value); }
        }
        [Category("Action")]
        public event EventHandler RequireUpdate
        {
            add { base.Events.AddHandler(RequireUpdate_Key, value); }
            remove { base.Events.RemoveHandler(RequireUpdate_Key, value); }
        }
        protected virtual void DoSplitPageCommandEvent(SplitPageCommandEventArgument e)
        {
            var del_command = (SplitPageCommandEventHandler)this.Events[SplitPageCommand_Key];
            if (del_command != null)
            {
                del_command(this, e);
            }
            if (!e.Cancel)
            {
                switch (e.Command)
                {
                    case SplitPageNavigateCommand.First:
                        this.PageIndex = 0;
                        break;
                    case SplitPageNavigateCommand.Previous:
                        this.PageIndex--;
                        break;
                    case SplitPageNavigateCommand.Next:
                        this.PageIndex++;
                        break;
                    case SplitPageNavigateCommand.Last:
                        this.PageIndex = this.PageCount;
                        break;
                    case SplitPageNavigateCommand.Go:
                        this.PageIndex = this.GoToPageNum - 1;
                        break;
                }

                var del_update = (EventHandler)this.Events[RequireUpdate_Key];
                if (del_update != null)
                {
                    del_update(this, new EventArgs());
                }
            }
        }
        #endregion

        #region Property
        [Category("分页属性")]
        public int PageIndex
        {
            get
            {
                if (this.ViewState["PageIndex"] == null)
                {
                    this.ViewState["PageIndex"] = 0;
                }
                return System.Convert.ToInt32(this.ViewState["PageIndex"]);
            }
            set
            {
                this.ViewState["PageIndex"] = Math.Max(0, Math.Min(value, this.PageCount - 1));
            }
        }
        [DefaultValue(typeof(int), "10")]
        [Category("分页属性")]
        public int PageSize
        {
            get
            {
                if (this.ViewState["PageSize"] == null)
                {
                    this.ViewState["PageSize"] = 10;
                }

                return (Math.Max(1, (int)this.ViewState["PageSize"]));
            }
            set
            {
                this.ViewState["PageSize"] = Math.Max(1, value);
                CheckPageIndex();
            }
        }
        [DefaultValue(typeof(long), "100")]
        [Category("分页属性")]
        public long RecoedCount
        {
            get
            {
                if (this.ViewState["RecoedCount"] == null)
                {
                    this.ViewState["RecoedCount"] = 0;
                }
                return System.Convert.ToInt64(this.ViewState["RecoedCount"]);
            }
            set
            {
                this.ViewState["RecoedCount"] = Math.Max(0, value);
                CheckPageIndex();
            }
        }
        [Category("分页属性")]
        public int GoToPageNum
        {
            get
            {
                if (this.ViewState["GoToPageNum"] == null)
                {
                    this.ViewState["GoToPageNum"] = 0;
                }
                return System.Convert.ToInt32(this.ViewState["GoToPageNum"]);
            }
            set
            {
                this.ViewState["GoToPageNum"] = Math.Max(1, Math.Min(value, this.PageCount));
            }
        }
        [Category("分页属性")]
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(((decimal)RecoedCount / PageSize));
            }
        }
        [Category("分页属性")]
        public bool IsFirst
        {
            get
            {
                return this.PageIndex == 0;
            }
        }
        [Category("分页属性")]
        public bool IsLast
        {
            get
            {
                return this.PageIndex >= this.PageCount - 1;
            }
        }
        private void CheckPageIndex()
        {
            this.PageIndex = Math.Max(0, Math.Min(this.PageIndex, this.PageCount - 1));
        }
        #endregion

        #region Style
        public bool ShowPart_Go
        {
            get
            {
                if (this.ViewState["ShowPart_Go"] == null)
                {
                    this.ViewState["ShowPart_Go"] = false;
                }
                return (bool)this.ViewState["ShowPart_Go"];
            }
            set
            {
                this.ViewState["ShowPart_Go"] = value;
            }
        }
        public bool ShowPart_FirstLast
        {
            get
            {
                if (this.ViewState["ShowPart_FirstLast"] == null)
                {
                    this.ViewState["ShowPart_FirstLast"] = true;
                }
                return (bool)this.ViewState["ShowPart_FirstLast"];
            }
            set
            {
                this.ViewState["ShowPart_FirstLast"] = value;
            }
        }
        public bool ShowPart_RecordTotal
        {
            get
            {
                if (this.ViewState["ShowPart_RecordTotal"] == null)
                {
                    this.ViewState["ShowPart_RecordTotal"] = true;
                }
                return (bool)this.ViewState["ShowPart_RecordTotal"];
            }
            set
            {
                this.ViewState["ShowPart_RecordTotal"] = value;
            }
        }
        public bool ShowPart_Refresh
        {
            get
            {
                if (this.ViewState["ShowPart_Refresh"] == null)
                {
                    this.ViewState["ShowPart_Refresh"] = false;
                }
                return (bool)this.ViewState["ShowPart_Refresh"];
            }
            set
            {
                this.ViewState["ShowPart_Refresh"] = value;
            }
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Page.RegisterRequiresPostBack(this); 
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID);
            if (this.PageCount <= 1)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none");
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            var cscript = this.Page.ClientScript;
            base.RenderContents(writer);
            writer.Write("<table border='0' cellspacing='0' cellpadding='0'>");
            writer.Write("    <tr>");
            writer.Write("        <td width='90' align='center'>");
            writer.Write("            <span>" + (this.PageIndex + 1) + "</span>");
            writer.Write("            /");
            writer.Write("            <span >" + this.PageCount + "</span>");
            writer.Write("            页");
            writer.Write("        </td>");
            writer.Write("        ");
            writer.Write("        <td width='60'>");
            writer.Write(string.Format("<select name='{0}' id='{0}' onchange= \"{1}\">", this.ClientID + "_txtPageSize", cscript.GetPostBackEventReference(this, "Refresh")));
            if (!(new int[] { 5, 10, 15, 20, 25, 30 }).Contains(this.PageSize)){
                writer.Write(string.Format("		<option {0} value='{1}'>{1}</option>", "selected='selected'", this.PageSize));
            }
            writer.Write(string.Format("		<option {0} value='5'>5</option>", this.PageSize == 5 ? "selected='selected'" : ""));
            writer.Write(string.Format("		<option {0} value='10'>10</option>", this.PageSize == 10 ? "selected='selected'" : ""));
            writer.Write(string.Format("		<option {0} value='15'>15</option>", this.PageSize == 15 ? "selected='selected'" : ""));
            writer.Write(string.Format("		<option {0} value='20'>20</option>", this.PageSize == 20 ? "selected='selected'" : ""));
            writer.Write(string.Format("		<option {0} value='25'>25</option>", this.PageSize == 25 ? "selected='selected'" : ""));
            writer.Write(string.Format("		<option {0} value='30'>30</option>", this.PageSize == 30 ? "selected='selected'" : ""));
            writer.Write("	</select>");
            writer.Write("        </td>");
            writer.Write("        <td width='96'>");
            writer.Write("            条/页");
            writer.Write("        </td>");
            writer.Write("        <td width='35' align='center'>");
            writer.Write(string.Format("<a href='javascript:' onclick=\"{0}\" {1}><input type='image'   src='{2}img/dot_lb.png' style=' width:9px; height:6px;' onclick='return false;' /></a>", (this.IsFirst ? "" : cscript.GetPostBackEventReference(this, "First")), (this.IsFirst ? "disable='disable'" : ""), this.ResolveUrl("~")));
            writer.Write("        </td>");
            writer.Write("        <td width='25'>");
            writer.Write(string.Format("<a href='javascript:' onclick=\"{0}\" {1}><input type='image'   src='{2}img/dot_ls.png' style=' width:9px; height:6px;' onclick='return false;' /></a>", (this.IsFirst ? "" : cscript.GetPostBackEventReference(this, "Previous")), (this.IsFirst ? "disable='disable'" : ""), this.ResolveUrl("~")));
            writer.Write("        </td>");
            writer.Write("        <td width='30' align='right'>");
            writer.Write(string.Format("<a href='javascript:' onclick=\"{0}\" {1}><input type='image'   src='{2}img/dot_rs.png' style=' width:9px; height:6px;' onclick='return false;' /></a>", (this.IsLast ? "" : cscript.GetPostBackEventReference(this, "Next")), (this.IsLast ? "disable='disable'" : ""), this.ResolveUrl("~")));
            writer.Write("        </td>");
            writer.Write("        <td width='35' align='center'>");
            writer.Write(string.Format("<a href='javascript:' onclick=\"{0}\" {1}><input type='image'   src='{2}img/dot_rb.png' style=' width:9px; height:6px;' onclick='return false;' /></a>", (this.IsLast ? "" : cscript.GetPostBackEventReference(this, "Last")), (this.IsLast ? "disable='disable'" : ""), this.ResolveUrl("~")));
            writer.Write("        </td>");
            writer.Write("        <td width='50'>");
            writer.Write(string.Format("            <input name='{0}' type='text' maxlength='5' id='{0}' style='width:20px;' />", this.ClientID + "_txtGoPage"));
            writer.Write("        </td>");
            writer.Write("        <td align='center'>");
            writer.Write("            <a href='#'><input type='button'  value='转到' class='go' onclick=\"" + cscript.GetPostBackEventReference(this, "Go") + "\"  /></a>");
            writer.Write("        </td>");
            writer.Write("    </tr>");
            writer.Write("</table>");

            return;
            //原始代码
            writer.Write(string.Format("第{0}/{1}页 &nbsp;", this.PageIndex + 1, this.PageCount));
            var script = this.Page.ClientScript;
            if (!this.IsFirst)
            {
                if (this.ShowPart_FirstLast)
                {
                    writer.Write(" <a onclick=\"" + script.GetPostBackEventReference(this, "First") + "\">第一页</a>");
                }
                writer.Write(" &nbsp; <a onclick=\"" + script.GetPostBackEventReference(this, "Previous") + "\">上一页</a>");
            }
            else
            {
                if (this.ShowPart_FirstLast)
                {
                    writer.Write("<span style=\"color:#ccc\">第一页</span>");
                }
                writer.Write(" &nbsp; <span style=\"color:#ccc\">上一页</span>");
            }
            if (!IsLast)
            {
                ;
                writer.Write(" &nbsp; <a onclick=\"" + script.GetPostBackEventReference(this, "Next") + "\">下一页</a>");
                if (this.ShowPart_FirstLast)
                {
                    writer.Write(" &nbsp; <a onclick=\"" + script.GetPostBackEventReference(this, "Last") + "\">最后页</a>");
                }
            }
            else
            {
                writer.Write(" &nbsp; <span style=\"color:#ccc\">下一页</span>");
                if (this.ShowPart_FirstLast)
                {
                    writer.Write(" &nbsp; <span style=\"color:#ccc\">最后页</span>");
                }
            }
            if (this.ShowPart_Go)
            {
                writer.Write(" &nbsp; <input type='text' id=\"" + this.ClientID + "_txtGoPage\" name=\"" + this.ClientID + "_txtGoPage\" style='width:60px;' value='" + GoToPageNum.ToString() + "' />");
                writer.Write(" <a onclick=\"" + script.GetPostBackEventReference(this, "Go") + "\">Go</a>");
            }
            if (ShowPart_Refresh)
            {
                writer.Write(" &nbsp; <a onclick=\"" + script.GetPostBackEventReference(this, "Refresh") + "\">刷新</a>");
            }
            if (ShowPart_RecordTotal)
            {
                writer.Write(string.Format(" &nbsp;{0}条", this.RecoedCount));
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            var cmd = (SplitPageNavigateCommand)Enum.Parse(typeof(SplitPageNavigateCommand), eventArgument);
            var e = new SplitPageCommandEventArgument(cmd);
            DoSplitPageCommandEvent(e);
        }

        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            string goPageNum = postCollection[this.ClientID + "_txtGoPage"];
            string txtPageSize = postCollection[this.ClientID + "_txtPageSize"];
            bool isChanged = false; 
            if (!string.IsNullOrEmpty(goPageNum))
            {
                int num = 0;
                int old = this.GoToPageNum;
                if (int.TryParse(goPageNum, out num) && old != num)
                {
                    this.GoToPageNum = num;
                    isChanged =  true;
                }
            }
            if (!string.IsNullOrEmpty(txtPageSize))
            {
                int num = 0;
                int old = this.PageSize;
                if (int.TryParse(txtPageSize, out num) && old != num)
                {
                    this.PageSize = num;
                    isChanged =  true;
                }
            } 
            return isChanged;
        }

        public void RaisePostDataChangedEvent()
        {
            //throw new NotImplementedException();
        }
    }
    public delegate void SplitPageCommandEventHandler(object sender, SplitPageCommandEventArgument e);
    public class SplitPageCommandEventArgument : EventArgs
    {
        public SplitPageCommandEventArgument(SplitPageNavigateCommand cmd)
        {
            this.Command = cmd;
        }

        public SplitPageNavigateCommand Command { get; private set; }
        public bool Cancel { get; set; }
    }
    public enum SplitPageNavigateCommand : int
    {
        First,
        Previous,
        Next,
        Last,
        Go,
        Refresh,
    }
}