using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model.Pos;
using cPos.Model;
using System.Collections;
using System.Text;
using cPos.Model.Exchange;

public partial class exchange_announce_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //var temp = this.Request.QueryString["from"];
            string action = this.Request.QueryString["strDo"] ?? "2";
            ViewState["action"] = action;

            switch (action)
            {
                case "2":
                    {
                        ShowCreatePage();
                    } break;
                case "3":
                    {
                        ShowModifyPage();
                    } break;
                case "1":
                    {
                        ShowVisiblePage();
                    } break;
                default:
                    break;
            }
        }
    }

    //生成类实例的属性
    protected ExchangeService exchangeService
    {
        get { return new ExchangeService(); }
    }

    protected UnitService UnitService
    {
        get { return new UnitService(); }
    }



    protected void btnOK_Click(object sender, EventArgs e)
    {
        var announce = new AnnounceInfo();
        announce.Title = this.tbTitle.Text.Trim();
        announce.Content = this.tbContent.Text.Trim();
        announce.Publisher = this.tbPublisher.Text.Trim();
        announce.BeginDate = this.tbBeginDate.Value;
        announce.EndDate = this.tbEndDate.Value;
        //announce.ModifyTime = Convert.ToDateTime(this.tbEditTime.Text);
        foreach (var item in this.tvUnit.SelectValues)
        {
            AnnounceUnitInfo info = new AnnounceUnitInfo(new UnitInfo { Id = item });
            announce.AnnounceUnits.Add(info);
        }
        //announce.AnnounceUnits
        var opertype = this.Request.QueryString["strDo"];
        if (opertype == "2")
        {
            try
            {
                announce.SystemModifyStamp = DateTime.Now;
                announce.AllowDownload = 0;
                announce.CreateTime = DateTime.Now;
                announce.CreateUserID = loggingSessionInfo.CurrentUser.User_Id;
                if (exchangeService.InsertAnnounce(loggingSessionInfo, announce))
                {
                    this.Redirect("新建通告成功", InfoType.Info, this.Request.QueryString["from"] ?? "announce_query.aspx");
                }
                else
                {
                    this.InfoBox.ShowPopError("新建通告失败");
                }
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                this.InfoBox.ShowPopError("新建通告失败！");
            }

        }
        else
        {
            try
            {
                string announce_id = this.Request.QueryString["announce_id"];
                announce.ModifyTime = DateTime.Now;
                if (this.cbAllowDownload.Text == "否")
                {
                    announce.AllowDownload = 0;
                }
                else
                {
                    announce.AllowDownload = 1;
                }
                announce.ID = announce_id;
                announce.No = Convert.ToInt32(this.tbNo.Text);
                //announce.CreateTime = Convert.ToDateTime(this.tbCreateTime.Text);
                announce.CreateUserID = loggingSessionInfo.CurrentUser.User_Id;
                if (exchangeService.ModifyAnnounce(loggingSessionInfo, announce))
                {
                    this.Redirect("保存成功", InfoType.Info, this.Request.QueryString["from"] ?? "announce_query.aspx");
                }
                else
                {
                    this.InfoBox.ShowPopError("保存失败");
                }
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                this.InfoBox.ShowPopError("保存失败！");
            }

        }

    }


    //加载通告单位
    private void LoadUnitsInfo()
    {
        try
        {
            var service = new UnitService();
            this.tvUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
            {
                CheckState = false,
                Complete = false,
                ShowCheck = false,
                Text = item.Name,
                Value = item.Id,
            }));
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

    //显示详细页
    private void ShowVisiblePage()
    {

        string announce_id = this.Request.QueryString["announce_id"];
        this.btnOK.Visible = false;
        if (string.IsNullOrEmpty(announce_id))
        {
            ShowCreatePage();
            this.btnOK.Visible = false;
        }
        else
        {
            LoadUnitsInfo();
            var rult = UnitService.GetRootUnitsByDefaultRelationMode(loggingSessionInfo);
            this.tvUnit.SelectValues = rult.Select(obj => obj.Id).ToArray();
            disabledAllInput(this);
            this.btnOK.Visible = false;
            LoadAnnounceInfo(announce_id);
        }
    }

    //禁用所有input、DropDownList
    protected void disabledAllInput(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is TextBox)
            {
                var temp = c as TextBox;
                temp.ReadOnly = true;
            }
            if (c is DropDownList)
            {
                var temp = c as DropDownList;
                temp.Enabled = false;
            }
            if (c.Controls.Count > 0)
                disabledAllInput(c);
        }

    }


    private void LoadAnnounceInfo(string announce_id)
    {
        var announce = exchangeService.GetAnnounceByID(loggingSessionInfo, announce_id);
        if (announce == null)
        {
            this.InfoBox.ShowPopError("数据加载失败");
            this.btnOK.Visible = false;
            return;
        }

        this.tbNo.Text = Convert.ToString(announce.No);
        this.tbTitle.Text = announce.Title;
        this.tvUnit.SelectedText = announce.Unit.DisplayName;
        this.tvUnit.SelectedValue = announce.Unit.Id;
        this.tbContent.Text = announce.Content;
        this.tbPublisher.Text = announce.Publisher;
        this.tbBeginDate.Value = (announce.BeginDate ?? "").ToString();
        //(source.PurchaseDate ?? "").ToString()
        this.tbEndDate.Value = (announce.EndDate ?? "").ToString();
        this.cbAllowDownload.Text = "否";
        this.tbCreater.Text = announce.CreateUserName;
        this.tbCreateTime.Text = announce.CreateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        this.tbEditTime.Text = announce.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        this.tbEditor.Text = announce.ModifyUserName ?? "";
       // this.tbSysModifyTime.Text = announce.SystemModifyStamp.ToString("yyyy-MM-dd-hh:mm:ss.fff");
    }
    //显示修改页面
    private void ShowModifyPage()
    {
        string announce_id = this.Request.QueryString["announce_id"];
        if (string.IsNullOrEmpty(announce_id))
        {
            ShowCreatePage();
        }
        else
        {
            LoadUnitsInfo();
            this.btnOK.Visible = true;
            LoadAnnounceInfo(announce_id);
        }
    }
    //显示新建页面
    private void ShowCreatePage()
    {
        //var today = DateTime.Now.ToShortDateString().Split('/');
        //if (today[1].Length < 2) {
        //    today[1] = "0" + today[1];
        //}
        //if (today[2].Length < 2) {
        //    today[2] = "0" + today[2];
        //}
        //this.tbBeginDate.Value = today[0] + "-" + today[1] + "-" + today[2];
        this.tbBeginDate.Value = DateTime.Now.ToString("yyyy-MM-dd ");
        //var tomm = DateTime.Now.AddDays(1).ToShortDateString().Split('/');
        //if (tomm[1].Length < 2) {
        //    tomm[1] = "0" + tomm[1];
        //}
        //if (tomm[2].Length < 2) {
        //    tomm[2] = "0" + tomm[2];
        //}
        //this.tbEndDate.Value = tomm[0] + "-" + tomm[1] + "-" + tomm[2];
        this.tbEndDate.Value = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd ");
        this.cbAllowDownload.Text = "否";
        LoadUnitsInfo();
        this.btnOK.Visible = true;
        //var rult = UnitService.GetRootUnitsByDefaultRelationMode(loggingSessionInfo);
        // this.tvUnit.SelectValues = rult.Select(obj => obj.Id).ToArray();
    }

}