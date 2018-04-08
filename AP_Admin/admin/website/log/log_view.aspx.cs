using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Model.Dex;

public partial class log_log_view : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = this.Request.QueryString["oper_type"];
            ViewState["action"] = action;

            switch (action)
            {
                case "1":
                    {
                        ShowVisiblePage();
                    } break;
                default:
                    break;
            }
        }

    }
    protected void btnExportClick(object sender,EventArgs e)
    {
        string log_id = this.Request.QueryString["log_id"];
        if (!string.IsNullOrEmpty(log_id))
        {
            try
            {
                var logInfo = this.GetDexLogService().GetLog(LoggingSession, log_id);
                PageHelper.ExportToXml<LogInfo>(this, new LogInfo[] { logInfo }, obj =>
                {
                    var cells = new string[] {"<log_id>"+obj.log_id+"</log_id>",
                "<biz_id>"+obj.biz_id+"</biz_id>",
                "<biz_name>"+obj.biz_name+"</biz_name>",
                "<log_type_id>"+obj.log_type_id+"</log_type_id>",
                "<log_type_code>"+obj.log_type_code+"</log_type_code>",
                "<log_code>"+obj.log_code+"</log_code>",
                "<log_body><![CDATA["+obj.log_body+"]]></log_body>",
                "<create_time>"+obj.create_time+"</create_time>",
                "<create_user_id>"+obj.create_user_id+"</create_user_id>",
                "<modify_time>"+obj.modify_time+"</modify_time>",
                "<modify_user_id>"+obj.modify_user_id+"</modify_user_id>",
                "<customer_code>"+obj.customer_code+"</customer_code>",
                "<customer_id>"+obj.customer_id+"</customer_id>",
                "<unit_code>"+obj.unit_code+"</unit_code>",
                "<unit_id>"+obj.unit_id+"</unit_id>",
                "<user_code>"+obj.user_code+"</user_code>",
                "<user_id>"+obj.user_id+"</user_id>",
                "<if_code>"+obj.if_code+"</if_code>",
                "<app_code>"+obj.app_code+"</app_code>"
            };
                    return cells;
                });
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                this.InfoBox.ShowPopError("加载数据出错！");
            }
        }
    }
    //显示详细页
    private void ShowVisiblePage()
    {

        string log_id = this.Request.QueryString["log_id"];
        if (!string.IsNullOrEmpty(log_id))
        {
            try
            {
                var logInfo = this.GetDexLogService().GetLog(LoggingSession,log_id);
                
                this.lblAppType.Text=logInfo.app_code;
                this.lblIfCode.Text=logInfo.if_code;
                this.lblLogCode.Text=logInfo.log_code;
               this.lblBizId.Text=logInfo.biz_id;
               this.lblLogId.Text=logInfo.log_id;
                this.lblLogType.Text=logInfo.log_type_code;
               this.lblCreateTime.Text=logInfo.create_time;
                this.lblModifyTime.Text=logInfo.modify_time;
               this.lblCreateUserId.Text=logInfo.create_user_id;
                this.lblModifyUserId.Text=logInfo.modify_user_id;
                this.lblCustomerId.Text=logInfo.customer_id;
                this.lblUnitCode.Text=logInfo.unit_code;
                this.lblUnitId.Text=logInfo.unit_id;
                this.lblUserCode.Text=logInfo.user_code;
                this.lblUserId.Text = logInfo.user_id;
                this.lblBizCode.Text = logInfo.biz_name;
               this.tbLogBody.InnerText=logInfo.log_body;
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                this.InfoBox.ShowPopError("加载数据出错！");
            }
        
        }
        
    }
    protected void btnCloseClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.QueryString["from"] ?? "log_query.aspx");
    }
}