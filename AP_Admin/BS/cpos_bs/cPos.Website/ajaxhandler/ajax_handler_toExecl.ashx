﻿<%@ WebHandler Language="C#" Class="ajax_handler_toExecl" %>

using System;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
//using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;

public class ajax_handler_toExecl : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public string ExportDir { get { return HttpContext.Current.Server.MapPath("~/exceltemp/"); } } 
    private Random _random = new Random();
    cPos.Components.SessionManager sessionManage = new cPos.Components.SessionManager();
    public void ProcessRequest (HttpContext context) {
        try
        {
            var paras = context.Request.Params;
            context.Response.ContentType = "text/plain";
            string fileName = doDownLoadMethod(context, paras);
            var str = System.IO.Path.Combine(context.Request.ApplicationPath, "exceltemp/" + fileName);
            context.Response.Write(str);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            throw ex;
        } 
    }
    
    private string doDownLoadMethod(HttpContext context, System.Collections.Specialized.NameValueCollection paras)
    {
        var type = paras["type"].ToString();
        ClearTempFilesBeforeToday();
        switch (type)
        {
            case "pos":return downLoadPos(context,paras);
            case "saleMain": return downLoadSale();
            case "saleDetail": return downLoadSaleDetail();
            case "shift": return downLoadShift();
            case "shiftDetail": return downLoadShiftDetail();
            case "item": return downLoadItem();
            case "stock": LoadSkuProp(); return dowmLoadStock();
            case "clear": { HttpContext.Current.Session["_unit_detail"] = null; HttpContext.Current.Session["_shift_detail"] = null; } return null;
            default: return null;
        }
    }
    #region 加载Sku属性信息
    private System.Collections.Generic.IList<cPos.Model.SkuPropInfo> SkuPropInfos
    {
        get;
        set;
    }
    //加载Sku属性信息列表
    private void LoadSkuProp()
    {
        try
        {
            var source = new cPos.Service.SkuPropServer().GetSkuPropList(loggingSessionInfo);
            SkuPropInfos = source;
        }
        catch(Exception ex)
        {  
            PageLog.Current.Write(ex);
        }
    }
    #endregion
    private string dowmLoadStock()
    {
        try
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff")+_random.Next(1,100).ToString() + ".xls";
            var full_path = ExportDir + fileName;
            var sw = new FileStream(full_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var Writer = new StreamWriter(sw, System.Text.Encoding.UTF8);
            System.Collections.Generic.List<string> headers = new List<string>();
            string[] header_pre = new string[] { "单位", "仓库", "品质", "商品编码", "商品名称" };
            string[] header_nex = new string[] { "期初数", "入库数", "出库数", "调整入数", "调整出数", "期末数" };
            headers.AddRange(header_pre);
            for (int i = 1; i <= 5; i++)
            {
                var item = this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == i);
                if (item != null)
                {
                    headers.Add(item.prop_name);
                }
            }
            headers.AddRange(header_nex);
            var para = HttpContext.Current.Request.Params;
            var source = new cPos.Service.StockBalanceService().SearchStockBalance(loggingSessionInfo, para["unit_id"] ?? "", para["warehouse_id"] ?? "", para["item_code"] ?? "", para["item_name"] ?? "", para["stock_type"] ?? "", para["sel_date"] ?? "", 36500, 0);
            var data = source.StockBalanceInfoList;
            ExportHtmlExcel<cPos.Model.StockBalanceInfo>(Writer,headers.ToArray(), data, obj =>
            {
                var cells = new object[] { obj.unit_name??"", obj.warehouse_name??"", obj.item_label_type_name??"", obj.item_code??"", obj.item_name??"", propValue(obj, 1),  propValue(obj, 2) , propValue(obj, 3) ,  propValue(obj, 4) , propValue(obj, 5) ,obj.begin_qty, obj.in_qty, obj.out_qty, obj.adjust_in_qty, obj.adjust_out_qty,obj.end_qty };
                return cells;
            }, null);
            Writer.Close();
            sw.Close();
             return fileName;
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return null;
        }
    }
    private string propValue(cPos.Model.StockBalanceInfo info,int index)
    {
        switch (index)
        {
            case 1: return info.prop_1_detail_name;
            case 2: return info.prop_2_detail_name;
            case 3: return info.prop_3_detail_name;
            case 4: return info.prop_4_detail_name;
            case 5: return info.prop_5_detail_name;
            default: return null;
        }
    }
    private string downLoadItem()
    {
        try
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff")+_random.Next(1,100).ToString() + ".xls";
            var full_path = ExportDir + fileName;
            var sw = new FileStream(full_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var Writer = new StreamWriter(sw,System.Text.Encoding.UTF8);
            string[] headers = new string[] { "商品编码", "商品名称", "商品条形码", "销售单价", "销售数量", "销售金额"};
            var para = HttpContext.Current.Request.Params;
            var unit_ids = "";
            if(!string.IsNullOrEmpty(para["unit_id"]))
            {
                unit_ids = string.Join(",", para["unit_id"].Split(',').Select(o => "'" + o + "'").ToArray());
            }
            var source = new cPos.Service.ReportService().SearchItemSalesReport(loggingSessionInfo, para["item_code"] ?? "", para["item_name"] ?? "", para["barcode"] ?? "", unit_ids, para["order_date_begin"] ?? "", para["order_date_end"] ?? "", 36500, 0);
            var data = source.ItemSalesReportList;
            ExportHtmlExcel<cPos.Model.Report.ItemSalesReportInfo>(Writer,headers, data, obj =>
            {
                var cells = new object[] { obj.item_code ?? "", obj.item_name ?? "", obj.barcode ?? "", obj.std_price,obj.enter_qty, obj.enter_amount };
                return cells;
            }, null);
            Writer.Close();
            sw.Close();
            return fileName;
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return null;
        }
    }
    private string downLoadShiftDetail()
    {
        try
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _random.Next(1, 100).ToString() + ".xls";
            var full_path = ExportDir + fileName;
            var sw = new FileStream(full_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var Writer = new StreamWriter(sw, System.Text.Encoding.UTF8);
            string[] headers = new string[] { "单据时间", "销售人员", "收银员", "单据单号", "会员姓名", "支付方式", "单笔销售金额" };
            var para = HttpContext.Current.Request.Params;
            var source = (IEnumerable<UnitDetailDTO>)HttpContext.Current.Session["_shift_detail"];
            var data = source;
            ExportHtmlExcel<UnitDetailDTO>(Writer,headers, data.OrderByDescending(o=>o.total_amount), obj =>
            {
                var cells = new object[] { obj.create_time ?? "", obj.sales_user ?? "", obj.create_user_name ?? "", obj.order_no ?? "", obj.vip_no ?? "", obj.payment_name ?? "",  obj.total_amount };
                return cells;
            }, obj =>
            {
                decimal total = 0;
                foreach (var item in obj)
                {
                    total += item.total_amount;
                }
                var total_cells = new string[] { "总计", "", "", "", "","", total.ToString() };
                return total_cells;
            });
            Writer.Close();
            sw.Close();
            return fileName;
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return null;
        }
    }
    private string downLoadShift()
    {
        try
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _random.Next(1, 100).ToString() + ".xls";
            var full_path = ExportDir + fileName;
            var sw = new FileStream(full_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var Writer = new StreamWriter(sw, System.Text.Encoding.UTF8);
            string[] headers = new string[] { "收银员", "门店名称", "开班时间", "交班时间", "销售笔数","准备金", "销售金额","退款金额","总金额" };
            //var xapp = this.Xapp;
            var para = HttpContext.Current.Request.Params;
            var unit_ids = "";
            if (!string.IsNullOrEmpty(para["unit_id"]))
            {
                unit_ids = string.Join(",", para["unit_id"].Split(',').Select(o => "'" + o + "'").ToArray());
            }
            var unit_names = "";
            if (!string.IsNullOrEmpty(para["user_name"]))
            {
                unit_names = string.Join(",", para["user_name"].Split(',').Select(o => "'" + o + "'").ToArray());
            }
            var source = new cPos.Service.ReportService().SearchShiftReport(loggingSessionInfo, unit_ids, unit_names, para["order_date_begin"] ?? "", para["order_date_end"] ?? "", 36500, 0);
            var data = source.ShiftListInfo;
            ExportHtmlExcel<cPos.Model.ShiftInfo>(Writer,headers, data, obj =>
            {
                var cells = new object[] { obj.sales_user??"", obj.unit_name??"", obj.open_time??"", obj.close_time??"",obj.sales_qty,obj.deposit_amount, obj.sale_amount,obj.return_amount,obj.sales_total_amount };
                return cells;
            }, obj =>
            {
                var total_cells = new string[] { "总计", "", "", "",source.sales_total_qty.ToString(),source.total_deposit_amount.ToString(),source.total_sale_amount.ToString(),source.total_return_amount.ToString(),source.sales_total_total_amount.ToString() };
                return total_cells;
            });
            Writer.Close();
            sw.Close();
            return fileName;
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return null;
        }
    }
    private string downLoadSaleDetail()
    {
        try
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _random.Next(1, 100).ToString() + ".xls";
            var full_path = ExportDir + fileName;
            var sw = new FileStream(full_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var Writer = new StreamWriter(sw, System.Text.Encoding.UTF8);
            string[] headers = new string[] {"单据时间","销售员", "收银员", "单据单号", "会员姓名", "支付方式", "单笔销售" };
            var para = HttpContext.Current.Request.Params;
            if (HttpContext.Current.Session["_unit_detail"] == null)
            {
                return null;
            }
            var source = (IEnumerable<UnitDetailDTO>)HttpContext.Current.Session["_unit_detail"];
            var data = source;
           ExportHtmlExcel<UnitDetailDTO>(Writer,headers, source.OrderByDescending(o=>o.total_amount), obj => 
            {
                var cells = new object[] { obj.create_time ?? "", obj.sales_user ?? "",obj.create_user_name??"", obj.order_no ?? "", obj.vip_no ?? "", obj.payment_name ?? "", obj.total_amount };
                return cells; 
            }, obj =>
            {
                decimal total = 0;
                foreach (var item in obj)
                {
                    total += item.total_amount;
                }
                var total_cells = new string[] { "总计", "", "", "", "","", total.ToString() };
                return total_cells;
            });
           Writer.Close();
           sw.Close();
            return fileName;
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return null;
        }
    }
    private string downLoadSale()
    {
        try
        {
            string[] headers = new string[] { "日期", "门店名称", "销售笔数", "日销售金额" };
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _random.Next(1, 100).ToString() + ".xls";
            var full_path = ExportDir + fileName;
            var sw = new FileStream(full_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var Writer = new StreamWriter(sw, System.Text.Encoding.UTF8);
            var para = HttpContext.Current.Request.Params;
            var unit_ids = "";
            if (!string.IsNullOrEmpty(para["unit_id"]))
            {
                unit_ids = string.Join(",", para["unit_id"].Split(',').Select(o => "'" + o + "'").ToArray());
            }
            var source = new cPos.Service.ReportService().SearchSalesReport(loggingSessionInfo, unit_ids, para["order_no"].ToString(), para["order_date_begin"].ToString(), para["order_date_end"].ToString(), 36500, 0);
            var data = source.SalesReportList;
            ExportHtmlExcel<cPos.Model.Report.SalesReportInfo>(Writer, headers, source.SalesReportList, obj => { var cells = new object[] { obj.order_date ?? "", obj.unit_name ?? "", obj.sales_qty, obj.sales_amount }; return cells; }, obj => 
            {
                var total_cells = new string[] { "总计", "", source.sales_total_qty.ToString(), source.sales_total_amount.ToString() };
                return total_cells; 
            });
            Writer.Close();
            sw.Close();
            return fileName;
            
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return null;
        }
    }
    private string downLoadPos(HttpContext context, System.Collections.Specialized.NameValueCollection paras)
    {
        try
        {
            string[] headers = new string[] { "序号", "门店", "单据编号", "单据日期", "总数量", "总金额", "收银员", "收银时间","业务员" };
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _random.Next(1, 100).ToString() + ".xls";
            var full_path = ExportDir + fileName;
            var sw = new FileStream(full_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var Writer = new StreamWriter(sw, System.Text.Encoding.UTF8);
            var source = new cPos.Service.PosInoutService().SearchPosInfo(loggingSessionInfo, paras["order_no"].ToString(), (paras["unit_id"] ?? "").ToString(), paras["item_name"].ToString(), (paras["order_date_begin"] ?? "").ToString(), paras["order_date_end"].ToString(), 36500, 0);
            var data = source.InoutInfoList;
            var count_no = 1;
            ExportHtmlExcel<cPos.Model.InoutInfo>(Writer, headers, data, obj => { object[] cells = new object[] { count_no.ToString(), obj.create_unit_name??"", obj.order_no??"", obj.order_date??"",obj.total_qty, obj.total_amount, obj.create_unit_name??"", obj.create_time??"", obj.sales_user??"" }; count_no++; return cells; }, null);
            Writer.Close();
            sw.Close();
            return fileName;
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return null;
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

    private void ClearTempFilesBeforeToday()
    {
        try
        {
            if (!Directory.Exists(ExportDir))
            {
                Directory.CreateDirectory(ExportDir);
            }
            var _dic = new DirectoryInfo(ExportDir);
            var _files = _dic.GetFiles();
            foreach (var file in _files)
            {
                if (file.CreationTime <= DateTime.Now.Date)
                {
                    if (!file.IsReadOnly)
                        file.Delete();

                }
            }
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            throw ex;
        }
    }
    #region LoggingSessionInfo 登录信息类集合
    public cPos.Model.LoggingSessionInfo loggingSessionInfo
    {
        get { return sessionManage.loggingSessionInfo; }
        set { sessionManage.loggingSessionInfo = value; }
    }
    #endregion


    string ExportHtmlExcel<T>(IEnumerable<string> tabHeader,IEnumerable<T> dataRows,Func<T,IEnumerable<string>> func_row,Func<IEnumerable<T>,IEnumerable<string>> func_total_row)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //data = ds.DataSetName + "\n"; 
        //int count = 0;

        sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
        sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

        //写出列名 
        sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
        
        foreach (string head in tabHeader)
        {  
            sb.AppendLine("<td>" + System.Web.HttpUtility.HtmlEncode(head) + "</td>"); 
        }
        
        sb.AppendLine("</tr>");

        //写出数据 
        foreach (var item in dataRows)
        {
            sb.Append("<tr>");
            foreach (var cel in func_row(item))
            {
                if (cel != null)
                    sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
            }
            sb.AppendLine("</tr>"); 
        }
        if (func_total_row != null)
        {
            sb.Append("<tr>");
            foreach (var cel in func_total_row(dataRows))
            {
                sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
            }
            sb.Append("</tr>");
        }
        sb.AppendLine("</table>");

        return sb.ToString();
    }
    void ExportHtmlExcel<T>(StreamWriter sb, IEnumerable<string> tabHeader, IEnumerable<T> dataRows, Func<T, IEnumerable<object>> func_row, Func<IEnumerable<T>, IEnumerable<string>> func_total_row)
    {
        sb.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
        sb.WriteLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

        //写出列名 
        sb.WriteLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");

        foreach (string head in tabHeader)
        {
            sb.WriteLine("<td>" + System.Web.HttpUtility.HtmlEncode(head) + "</td>");
        }

        sb.WriteLine("</tr>");

        //写出数据 
        foreach (var item in dataRows)
        {
            sb.Write("<tr>");
            foreach (var cel in func_row(item))
            {
                if (cel != null)
                {
                    if (cel is decimal|| cel is Int32)
                    {
                        sb.Write("<td>" + System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
                    }
                    else
                    {
                        sb.Write("<td style=\"vnd.ms-excel.numberformat:@\" >" + System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
                    }
                }
            }
            sb.WriteLine("</tr>");
        }
        if (func_total_row != null)
        {
            sb.Write("<tr>");
            foreach (var cel in func_total_row(dataRows))
            {
                if (cel != null)
                {
                    sb.Write("<td style=\"vnd.ms-excel.numberformat:@\"></td>");
                }
            }
            sb.Write("</tr>");
            sb.Write("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            foreach (var cel in func_total_row(dataRows))
            {
                if (cel != null)
                    sb.Write("<td>" + System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
            }
            sb.Write("</tr>");
        }
        sb.WriteLine("</table>");
    }
}