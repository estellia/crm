using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class controls_DropDownTree : System.Web.UI.UserControl
{
    public controls_DropDownTree()
    {
        this.MultiSelect = false;
    }
        

    protected void Page_Load(object sender, EventArgs e)
    {
         //tvItems.TreeNodeDataBound
       // tvItems.
    }

    #region 属性
    /// <summary>
    /// 是否多选。如果否，则“选择树”中只能选择一个节点；否则“选择树”中可以选择多个点 
    /// </summary>
    public bool MultiSelect { get { return System.Convert.ToBoolean(this.ViewState["MultiSelect"]); } set { this.ViewState["MultiSelect"] = value.ToString(); } }
    public string DropdownWidth { get { return (this.ViewState["DropdownWidth"] ??"168px") as string; } set { this.ViewState["DropdownWidth"] = value; } }
    public string DropdownHeight { get { return (this.ViewState["DropdownHeight"] ?? "200px") as string; } set { this.ViewState["DropdownHeight"] = value; } }
    public string Url { get { return (this.ViewState["Url"] ?? "false") as string; } set { this.ViewState["Url"] = value; } }
    /// <summary>
    /// 是否为只读
    /// </summary>
    public bool ReadOnly { get { return (bool)(this.ViewState["ReadOnly"] ?? false); } set { this.ViewState["ReadOnly"] = value; } }

    public string SelectedValue { get { return this.SelectValues.FirstOrDefault(); } set { this.SelectValues = new string[]{ value}; } }
    public string SelectedText { get { return this.SelectTexts.FirstOrDefault(); } set { this.SelectTexts = new string[] { value }; } }

    public string[] SelectTexts { get {
        return StringToArray(tbSelectTexts.Value); 
    }
        set {
            tbSelectTexts.Value = ArrayToString(value);
            tbItemText.Value = value.FirstOrDefault();
        }
    }

    public string[] SelectValues
    {
        get
        {    
            return StringToArray(tbSelectValues.Value); 
        }
        set {
            tbSelectValues.Value = ArrayToString(value);
        }
    }

    private string ArrayToString(string [] arr)
    {
        return string.Join("|", (arr ?? new string[0]).Select(obj => this.Server.UrlEncode(obj)).ToArray());
    }

    private string[] StringToArray(string urlEncodeStr)
    {
        if (string.IsNullOrEmpty(urlEncodeStr))
        {
            return new string[0];
        }
        else
        {
            return (urlEncodeStr ?? "").Split('|').Select(obj => this.Server.UrlDecode(obj)).ToArray();
        }
    }

    public string Width { get { return (this.ViewState["Width"] ?? "none") as string; } set { this.ViewState["Width"] = value; } }
    public string Height { get { return (this.ViewState["Height"]??"none") as string; } set { this.ViewState["Height"] = value; } }
    
    [Obsolete("该重载方法已被移弃")]
    public void DataBind<T>(IEnumerable<T>items,Func<T,T,bool>t1_is_t2_parnet, Func<T, string> get_text, Func<T, string> get_value)
    {    
        var tns = QueryCreateTreeNode<T>(items,default (T), t1_is_t2_parnet, get_text, get_value);

        this.DataBind(tns);
    }
   
    public void DataBind<T>(IEnumerable<T> firstLevelItems, Func<T, IEnumerable<T>> get_childres, Func<T, tvNode> convertTvNode)
    {
        var tns = CreateTvNodes(firstLevelItems, get_childres, convertTvNode);
        this.DataBind(tns);
    }

    private IEnumerable<tvNode> CreateTvNodes<T>(IEnumerable<T> items,Func<T, IEnumerable<T>> get_childres, Func<T, tvNode> convertTvNode)
    { 
        //if(items != null )
        return (items??new T[0]) .Select (obj=>{var tn = convertTvNode(obj);tn.Childrens = CreateTvNodes<T>(get_childres(obj),get_childres,convertTvNode);return tn; });
    }

    public void DataBind(IEnumerable<tvNode> tns)
    {
        _data_json = tvNode.ToJsonData(tns); 
    }

    private string _data_json; 

    protected override void OnInit(EventArgs e)
    {
        _data_json = this.Request.Form[this.ClientID + "_" + "data"];
        base.OnInit(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        this.Page.ClientScript.RegisterHiddenField(this.ClientID + "_" + "data", _data_json);
    }

    public class tvNode {
        public tvNode() { this.Id = this.GetHashCode().ToString(); }
        public string Id { get;set; }
        public string Text {get;set;}
        public string Value {get;set;}
        public string Tag { get; set; }
        public bool ShowCheck {get;set;}
        public bool Complete {get;set;}
        public bool IsExpand {get;set;}
        public bool? CheckState {get;set;}
        public bool HasChildren{get{
                return !Complete || (Childrens != null ? Childrens.FirstOrDefault() != null : false);
        }}
        public IEnumerable<tvNode> Childrens { get; set; }
        public override string ToString()
        {
            return string.Format("{{id:{0},text:{1},value:{2},showcheck:{3},complete:{4},isexpand:{5},checkstate:{6},hasChildren:{7},ChildNodes:[{8}],tag:{9} }}"
                                                    , Util_toJs(Id)
                                                    , Util_toJs(Text)
                                                    , Util_toJs(Value)
                                                    ,Util_toJs(ShowCheck)
                                                    ,Util_toJs(Complete)
                                                    , Util_toJs(IsExpand)
                                                    ,CheckState.HasValue ? (CheckState==true?1:2) : 0
                                                    , Util_toJs(HasChildren)
                                                    ,string.Join<tvNode>(",", Childrens??new tvNode[0])
                                                    , Util_toJs(Tag)
            );
        }

        public static string ToJsonData(IEnumerable<tvNode> tns)
        {
            return string.Format("[{0}]", string.Join(",", tns.Select(obj => obj.ToString())));
        }

        //to js 常量
        private static string Util_toJs(string str)
        {
            if (str == null)
            {
                return "null";
            }
            else
            {
                return string.Format ("'{0}'",
                    str.Replace("\n", @"\n")
                    .Replace("\r", @"\r")
                    .Replace("\t", @"\t")
                    .Replace("\b", @"\b")
                    .Replace("'", @"\'")
                    );
            }
        }
        //to js 常量
        private static string Util_toJs(bool bl)
        {
            return bl.ToString().ToLower();
        }
    }


    private IEnumerable<TreeNode> QueryTreeNodes(TreeView tv)
    {
        foreach (TreeNode tn in tv.Nodes)
        {
            yield return tn;
            foreach (var sub_tn in QueryTreeNodes(tn))
            {
                yield return sub_tn;
            }
        }
    }

    private IEnumerable<TreeNode> QueryTreeNodes(TreeNode tn)
    { 
         foreach (TreeNode item in tn.ChildNodes)
        {
            yield return item;
            foreach (var sub_tn in QueryTreeNodes(item))
            {
                yield return item;
            }
        }
    }
     
    private IEnumerable<tvNode> QueryCreateTreeNode<T>(IEnumerable<T> items, T parent_item, Func<T, T, bool> t1_is_t2_parnet, Func<T, string> get_text, Func<T, string> get_value)
    {
        foreach (var item in items.Where(obj=>t1_is_t2_parnet(parent_item,obj))) 
        {
            tvNode tn = new tvNode();
            tn.Text = get_text(item);
            tn.Value = get_value(item);
            tn.Complete = true;
            tn.ShowCheck = true; 
            tn.Childrens = QueryCreateTreeNode<T>(items, item, t1_is_t2_parnet, get_text, get_value);
            yield return tn;
        }
    }
     
    #endregion
}