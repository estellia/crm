using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_Pager : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 记录总数
    /// </summary>
    public int RecordCount
    { get; set; }

    private event EventHandler _GoPage;

    public event EventHandler GoPage
    {
        add
        {
            _GoPage += new EventHandler(value);
        }
        remove
        {
            _GoPage -= new EventHandler(value);
        }
    }

    protected void dllPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.Control c = this.Parent;
        while (true)
        {
            if (c is GridView)
            {
                break;
            }
            c = c.Parent;
        }
        (c as GridView).PageSize = int.Parse(this.ddlPageSize.SelectedValue);
    }

    protected void btnGoPage_Click(object sender, EventArgs e)
    {
        System.Web.UI.Control c = this.Parent;
        while (true)
        {
            if (c is GridView)
            {
                break;
            }
            c = c.Parent;
        }


        int i = 0;
        if (tbGoPage != null)
        {
            try
            {
                i = int.Parse(tbGoPage.Text.Trim());

                if (i > 0 && i <= (c as GridView).PageCount)
                {
                    (c as GridView).PageIndex = i - 1;
                    if (_GoPage != null)
                    {
                        _GoPage(sender, e);
                    }
                    else
                        (c as GridView).DataBind();
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
}