using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PageHelper
/// </summary>
public class PageHelper
{
    /// <summary>
    /// 获取一个页面参数的值
    /// </summary>
    /// <param name="page">页面</param>
    /// <param name="name">参数名称</param>
    /// <param name="defaultValue">如果没有传入该参数，返回的缺省值</param>
    /// <returns></returns>
    public static string GetRequestParam(System.Web.UI.Page page, string name, string defaultValue)
    {
        if (page.Request.Params[name] == null)
            return defaultValue;
        else
            return page.Request.Params[name].ToString();
    }

    #region 操作类型
    /// <summary>
    /// 操作类型是否是查看
    /// </summary>
    /// <param name="page">页面</param>
    /// <returns></returns>
    public static bool IsViewOperate(System.Web.UI.Page page)
    {
        return GetRequestParam(page, "oper_type", "") == "1";
    }

    /// <summary>
    /// 操作类型是否是新建
    /// </summary>
    /// <param name="page">页面</param>
    /// <returns></returns>
    public static bool IsCreateOperate(System.Web.UI.Page page)
    {
        return GetRequestParam(page, "oper_type", "") == "2";
    }

    /// <summary>
    /// 操作类型是否是修改
    /// </summary>
    /// <param name="page">页面</param>
    /// <returns></returns>
    public static bool IsModifyOperate(System.Web.UI.Page page)
    {
        return GetRequestParam(page, "oper_type", "") == "3";
    }

    /// <summary>
    /// 获取操作类型是查看的界面参数
    /// </summary>
    /// <returns></returns>
    public static string GetViewOperatePageRequestParam()
    {
        return string.Format("oper_type={0}", 1);
    }

    /// <summary>
    /// 获取操作类型是新建的界面参数
    /// </summary>
    /// <returns></returns>
    public static string GetCreateOperatePageRequestParam()
    {
        return string.Format("oper_type={0}", 2);
    }

    /// <summary>
    /// 获取操作类型是修改的界面参数
    /// </summary>
    /// <returns></returns>
    public static string GetModifyOperatePageRequestParam()
    {
        return string.Format("oper_type={0}", 3);
    }

    #endregion

    /// <summary>
    /// 格式化日期(yyyy-MM-dd HH:mm:ss fff)
    /// </summary>
    /// <param name="dt">日期</param>
    /// <returns></returns>
    public static string FormatDateTime(DateTime dt)
    {
        return dt.ToString("yyyy-MM-dd HH:mm:ss fff");
    }
}