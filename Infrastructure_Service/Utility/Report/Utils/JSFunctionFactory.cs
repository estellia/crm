/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/15 11:40:35
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

using JIT.Utility.Report.Analysis;
using JIT.Utility.Web;

namespace JIT.Utility.Report.Utils
{
    /// <summary>
    /// Javascript函数的工厂
    /// </summary>
    public static class JSFunctionFactory
    {
        /// <summary>
        /// 创建收集筛选的JS函数
        /// </summary>
        /// <param name="pQueryConditionControlIDs">查询条件控件ID</param>
        /// <param name="pFunctionName">JS函数名</param>
        /// <returns>JS函数</returns>
        public static JSFunction CreateCollectQueryConditions(string[] pQueryConditionControlIDs, string pFunctionName = "__fnGetQueryConditions")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //组织返回值
            script.AddSentence("var result={};");
            if (pQueryConditionControlIDs != null)
            {
                foreach (var ctlID in pQueryConditionControlIDs)
                {
                    script.AddSentence("result.{0}=Ext.getCmp('{0}').jitGetValue();", ctlID);
                }
            }
            script.AddSentence("return result;");
            //
            return script;
        }

        /// <summary>
        /// 创建分析报表查询的JS函数
        /// </summary>
        /// <param name="pGetQueryConditionsJSFunction">获取查询条件的JS函数</param>
        /// <param name="pAjaxHandlerUrl">Ajax请求的url</param>
        /// <param name="pFunctionName">函数的名称</param>
        /// <param name="pLoadingMessage">执行Ajax请求时的加载中的信息</param>
        /// <returns>JS函数</returns>
        public static JSFunction CreateQuery(JSFunction pGetQueryConditionsJSFunction, string pAjaxHandlerUrl, string pFunctionName = "__fnAnalysisReportQuery", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //组织回传参数
            script.AddSentence("var param={0};",pGetQueryConditionsJSFunction.Call().ToScriptBlock(0));
            //阴影遮罩
            script.AddSentence("var mask=new Ext.LoadMask(Ext.getBody(),{");
            script.AddSentence("{0}msg:'{1}'",Keyboard.TAB,pLoadingMessage);
            script.AddSentence("});");
            //开启遮罩
            script.AddSentence("mask.show();");
            //执行Ajax请求
            script.AddSentence("Ext.Ajax.request({");
            script.AddSentence("{0}url:'{1}'",Keyboard.TAB,pAjaxHandlerUrl);
            script.AddSentence("{0},params:Ext.JSON.encode(param)",Keyboard.TAB);
            script.AddSentence("{0},method:'POST'", Keyboard.TAB);
            script.AddSentence("{0},callback:function(options,success,response){{", Keyboard.TAB);
            script.AddSentence("{0}{0}if(success){{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}eval(response.responseText);", Keyboard.TAB);
            script.AddSentence("{0}{0}}}else{{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}alert('请求失败.');", Keyboard.TAB);
            script.AddSentence("{0}{0}}}", Keyboard.TAB);
            script.AddSentence("{0}{0}mask.hide();", Keyboard.TAB);
            script.AddSentence("{0}}}", Keyboard.TAB);
            script.AddSentence("});");
            //返回
            return script;
        }

        /// <summary>
        /// 创建分析报表钻取的JS函数
        /// </summary>
        /// <param name="pAjaxHandlerUrl">Ajax请求的url</param>
        /// <param name="pFunctionName">函数的名称</param>
        /// <param name="pLoadingMessage">执行Ajax请求时的加载中的信息</param>
        /// <returns></returns>
        public static JSFunction CreateDrilling(string pAjaxHandlerUrl, string pFunctionName = "__fnAnalysisReportDrilling", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //定义函数的参数列表
            script.Params = new List<string>();
            script.Params.Add("pDimColumnID");
            script.Params.Add("pDimValue");
            script.Params.Add("pDimText");
            //组织回传参数
            script.AddSentence("var param={};");
            script.AddSentence("param.DimColumnID=pDimColumnID;");
            script.AddSentence("param.DimValue=pDimValue;");
            script.AddSentence("param.DimText=pDimText;");
            //阴影遮罩
            script.AddSentence("var mask=new Ext.LoadMask(Ext.getBody(),{");
            script.AddSentence("{0}msg:'{1}'", Keyboard.TAB, pLoadingMessage);
            script.AddSentence("});");
            //开启遮罩
            script.AddSentence("mask.show();");
            //执行Ajax请求
            script.AddSentence("Ext.Ajax.request({");
            script.AddSentence("{0}url:'{1}'", Keyboard.TAB, pAjaxHandlerUrl);
            script.AddSentence("{0},params:Ext.JSON.encode(param)", Keyboard.TAB);
            script.AddSentence("{0},method:'POST'", Keyboard.TAB);
            script.AddSentence("{0},callback:function(options,success,response){{", Keyboard.TAB);
            script.AddSentence("{0}{0}if(success){{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}eval(response.responseText);", Keyboard.TAB);
            script.AddSentence("{0}{0}}}else{{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}alert('请求失败.');", Keyboard.TAB);
            script.AddSentence("{0}{0}}}", Keyboard.TAB);
            script.AddSentence("{0}{0}mask.hide();", Keyboard.TAB);
            script.AddSentence("{0}}}", Keyboard.TAB);
            script.AddSentence("});");
            //返回
            return script;
        }

        /// <summary>
        /// 创建分析报表的跳转到指定剖面的JS函数
        /// </summary>
        /// <param name="pAjaxHandlerUrl">Ajax请求的url</param>
        /// <param name="pFunctionName">函数的名称</param>
        /// <param name="pLoadingMessage">执行Ajax请求时的加载中的信息</param>
        /// <returns></returns>
        public static JSFunction CreateGoto(string pAjaxHandlerUrl, string pFunctionName = "__fnAnalysisReportGoto", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //定义函数的参数列表
            script.Params = new List<string>();
            script.Params.Add("pSectionID");
            //组织回传参数
            script.AddSentence("var param={};");
            script.AddSentence("param.SectionID=pSectionID;");
            //阴影遮罩
            script.AddSentence("var mask=new Ext.LoadMask(Ext.getBody(),{");
            script.AddSentence("{0}msg:'{1}'", Keyboard.TAB, pLoadingMessage);
            script.AddSentence("});");
            //开启遮罩
            script.AddSentence("mask.show();");
            //执行Ajax请求
            script.AddSentence("Ext.Ajax.request({");
            script.AddSentence("{0}url:'{1}'", Keyboard.TAB, pAjaxHandlerUrl);
            script.AddSentence("{0},params:Ext.JSON.encode(param)", Keyboard.TAB);
            script.AddSentence("{0},method:'POST'", Keyboard.TAB);
            script.AddSentence("{0},callback:function(options,success,response){{", Keyboard.TAB);
            script.AddSentence("{0}{0}if(success){{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}eval(response.responseText);", Keyboard.TAB);
            script.AddSentence("{0}{0}}}else{{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}alert('请求失败.');", Keyboard.TAB);
            script.AddSentence("{0}{0}}}", Keyboard.TAB);
            script.AddSentence("{0}{0}mask.hide();", Keyboard.TAB);
            script.AddSentence("{0}}}", Keyboard.TAB);
            script.AddSentence("});");
            //
            return script;
        }

        /// <summary>
        /// 创建分析报表中改变数据透视的JS函数
        /// </summary>
        /// <param name="pAjaxHandlerUrl">Ajax请求的url</param>
        /// <param name="pContextMenuID">上下文菜单的ID</param>
        /// <param name="pFunctionName">函数的名称</param>
        /// <param name="pLoadingMessage">执行Ajax请求时的加载中的信息</param>
        /// <returns></returns>
        public static JSFunction CreateChangePivot(string pAjaxHandlerUrl, string pContextMenuID, string pFunctionName = "__fnAnalysisReportChangePivot", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //定义函数的参数列表
            script.Params = new List<string>();
            script.Params.Add("pPivotChangedColumnID");  //是哪个维度列需要改变透视
            script.Params.Add("pIsPivoted");    //改变后的值是什么
            //组织回传参数
            script.AddSentence("var param={};");
            script.AddSentence("param.PivotChangedColumnID=pPivotChangedColumnID;");
            script.AddSentence("param.IsPivoted=pIsPivoted;");
            //阴影遮罩
            script.AddSentence("var mask=new Ext.LoadMask(Ext.getBody(),{");
            script.AddSentence("{0}msg:'{1}'", Keyboard.TAB, pLoadingMessage);
            script.AddSentence("});");
            //开启遮罩
            script.AddSentence("mask.show();");
            //关闭上下文菜单
            script.AddSentence("Ext.getCmp({0}{1}{0}).hide();",JSONConst.STRING_WRAPPER,pContextMenuID);
            //执行Ajax请求
            script.AddSentence("Ext.Ajax.request({");
            script.AddSentence("{0}url:'{1}'", Keyboard.TAB, pAjaxHandlerUrl);
            script.AddSentence("{0},params:Ext.JSON.encode(param)", Keyboard.TAB);
            script.AddSentence("{0},method:'POST'", Keyboard.TAB);
            script.AddSentence("{0},callback:function(options,success,response){{", Keyboard.TAB);
            script.AddSentence("{0}{0}if(success){{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}eval(response.responseText);", Keyboard.TAB);
            script.AddSentence("{0}{0}}}else{{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}alert('请求失败.');", Keyboard.TAB);
            script.AddSentence("{0}{0}}}", Keyboard.TAB);
            script.AddSentence("{0}{0}mask.hide();", Keyboard.TAB);
            script.AddSentence("{0}}}", Keyboard.TAB);
            script.AddSentence("});");
            //
            return script;
        }
        
        /// <summary>
        /// 创建分析报表中改变行列转换的JS函数
        /// </summary>
        /// <param name="pAjaxHandlerUrl">Ajax请求的url</param>
        /// <param name="pContextMenuID">上下文菜单的ID</param>
        /// <param name="pFunctionName">函数的名称</param>
        /// <param name="pLoadingMessage">执行Ajax请求时的加载中的信息</param>
        /// <returns></returns>
        public static JSFunction CreateChangeCRConversion(string pAjaxHandlerUrl, string pContextMenuID, string pFunctionName = "__fnAnalysisReportChangeCRConversion", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //定义函数的参数列表
            script.Params = new List<string>();
            script.Params.Add("pCRConvertionChangedColumnID");  //是哪个维度列需要改变透视
            script.Params.Add("pIsCRConverted");    //改变后的值是什么
            //组织回传参数
            script.AddSentence("var param={};");
            script.AddSentence("param.CRConvertionChangedColumnID=pCRConvertionChangedColumnID;");
            script.AddSentence("param.IsCRConverted=pIsCRConverted;");
            //阴影遮罩
            script.AddSentence("var mask=new Ext.LoadMask(Ext.getBody(),{");
            script.AddSentence("{0}msg:'{1}'", Keyboard.TAB, pLoadingMessage);
            script.AddSentence("});");
            //开启遮罩
            script.AddSentence("mask.show();");
            //关闭上下文菜单
            script.AddSentence("Ext.getCmp({0}{1}{0}).hide();", JSONConst.STRING_WRAPPER, pContextMenuID);
            //执行Ajax请求
            script.AddSentence("Ext.Ajax.request({");
            script.AddSentence("{0}url:'{1}'", Keyboard.TAB, pAjaxHandlerUrl);
            script.AddSentence("{0},params:Ext.JSON.encode(param)", Keyboard.TAB);
            script.AddSentence("{0},method:'POST'", Keyboard.TAB);
            script.AddSentence("{0},callback:function(options,success,response){{", Keyboard.TAB);
            script.AddSentence("{0}{0}if(success){{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}eval(response.responseText);", Keyboard.TAB);
            script.AddSentence("{0}{0}}}else{{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}alert('请求失败.');", Keyboard.TAB);
            script.AddSentence("{0}{0}}}", Keyboard.TAB);
            script.AddSentence("{0}{0}mask.hide();", Keyboard.TAB);
            script.AddSentence("{0}}}", Keyboard.TAB);
            script.AddSentence("});");
            //
            return script;
        }

        /// <summary>
        /// 创建分析报表中行列互换的JS函数
        /// </summary>
        /// <param name="pAjaxHandlerUrl">Ajax请求的url</param>
        /// <param name="pContextMenuID">上下文菜单的ID</param>
        /// <param name="pFunctionName">函数的名称</param>
        /// <param name="pLoadingMessage">执行Ajax请求时的加载中的信息</param>
        /// <returns></returns>
        public static JSFunction CreateCRExchange(string pAjaxHandlerUrl, string pContextMenuID, string pFunctionName = "__fnAnalysisCRExchange", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //阴影遮罩
            script.AddSentence("var mask=new Ext.LoadMask(Ext.getBody(),{");
            script.AddSentence("{0}msg:'{1}'", Keyboard.TAB, pLoadingMessage);
            script.AddSentence("});");
            //开启遮罩
            script.AddSentence("mask.show();");
            //关闭上下文菜单
            script.AddSentence("Ext.getCmp({0}{1}{0}).hide();", JSONConst.STRING_WRAPPER, pContextMenuID);
            //执行Ajax请求
            script.AddSentence("Ext.Ajax.request({");
            script.AddSentence("{0}url:'{1}'", Keyboard.TAB, pAjaxHandlerUrl);
            script.AddSentence("{0},method:'POST'", Keyboard.TAB);
            script.AddSentence("{0},callback:function(options,success,response){{", Keyboard.TAB);
            script.AddSentence("{0}{0}if(success){{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}eval(response.responseText);", Keyboard.TAB);
            script.AddSentence("{0}{0}}}else{{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}alert('请求失败.');", Keyboard.TAB);
            script.AddSentence("{0}{0}}}", Keyboard.TAB);
            script.AddSentence("{0}{0}mask.hide();", Keyboard.TAB);
            script.AddSentence("{0}}}", Keyboard.TAB);
            script.AddSentence("});");
            //
            return script;
        }

        /// <summary>
        /// 创建将数据导出到Excel的JS函数
        /// </summary>
        /// <param name="pAjaxHandlerUrl">Ajax请求的url</param>
        /// <param name="pFunctionName">函数的名称</param>
        /// <param name="pLoadingMessage">执行Ajax请求时的加载中的信息</param>
        /// <returns></returns>
        public static JSFunction CreateExportToExcel(string pAjaxHandlerUrl, string pFunctionName = "__fnAnalysisReportExportToExcel", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //创建Form表单提交
            script.AddSentence("var submitForm =document.createElement({0}FORM{0});", JSONConst.STRING_WRAPPER);
            script.AddSentence("document.body.appendChild(submitForm);");
            script.AddSentence("submitForm.method={0}POST{0};",JSONConst.STRING_WRAPPER);
            script.AddSentence("submitForm.action ={0}{1}{0};",JSONConst.STRING_WRAPPER,pAjaxHandlerUrl);
            script.AddSentence("submitForm.submit();");
            //
            return script;
        }

        /// <summary>
        /// 创建查看明细的JS函数
        /// </summary>
        /// <param name="pAjaxHandlerUrl">Ajax请求的url</param>
        /// <param name="pDrilledDim">需要钻取的维度列</param>
        /// <param name="pOtherDim">其他的维度列</param>
        /// <param name="pMultiDrillJSFunctionName">多维度钻取的函数名</param>
        /// <param name="pFunctionName">函数的名称</param>
        /// <param name="pLoadingMessage">执行Ajax请求时的加载中的信息</param>
        /// <returns></returns>
        public static JSFunction CreateViewDetail(string pAjaxHandlerUrl, DimColumn pDrilledDim,DimColumn[] pOtherDim, string pMultiDrillJSFunctionName = "__fnAnalysisReportMultiDrill", string pFunctionName = "__fnAnalysisReportViewDetail", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //定义函数的参数列表
            script.Params = new List<string>();
            script.Params.Add("item");
            script.Params.Add("e");
            //
            script.AddSentence("var menu =item.parentMenu;");
            script.AddSentence("var clickRecord =menu.___current_record;");
            script.AddSentence("if(clickRecord){");
            script.AddSentence("    var params={};");
            script.AddSentence("    params.DimColumnID ={0}{1}{0};", JSONConst.STRING_WRAPPER, pDrilledDim.ColumnID);
            script.AddSentence("    params.DimValue=clickRecord.get({0}{1}{0});", JSONConst.STRING_WRAPPER, pDrilledDim.ColumnID);
            script.AddSentence("    params.DimText=clickRecord.get({0}{1}{0});", JSONConst.STRING_WRAPPER, pDrilledDim.GetTextColumnID());
            if (pOtherDim != null)
            {
                script.AddSentence("    params.DrillingItems=new Array();");
                int i = 0;
                foreach (var dim in pOtherDim)
                {
                    script.AddSentence("    var item{0}={{}};",i.ToString());
                    script.AddSentence("    item{2}.DimColumnID={0}{1}{0};", JSONConst.STRING_WRAPPER, dim.ColumnID, i.ToString());
                    script.AddSentence("    item{2}.DimValue=clickRecord.get({0}{1}{0});", JSONConst.STRING_WRAPPER, dim.ColumnID, i.ToString());
                    script.AddSentence("    item{2}.DimText=clickRecord.get({0}{1}{0});", JSONConst.STRING_WRAPPER, dim.GetTextColumnID(), i.ToString());
                    script.AddSentence("    params.DrillingItems.push(item{0});", i.ToString());
                    i++;
                }
            }
            script.AddSentence("    {0}(params);",pMultiDrillJSFunctionName);
            script.AddSentence("}");
            //
            return script;
        }

        /// <summary>
        /// 创建多维度钻取的函数
        /// </summary>
        /// <param name="pAjaxHandlerUrl"></param>
        /// <param name="pFunctionName"></param>
        /// <param name="pLoadingMessage"></param>
        /// <returns></returns>
        public static JSFunction CreateMultiDimDrillingFunction(string pAjaxHandlerUrl, string pFunctionName = "__fnAnalysisReportMultiDrill", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Common;
            script.FunctionName = pFunctionName;
            //定义函数的参数列表
            script.Params = new List<string>();
            script.Params.Add("pDrillings");
            //参数检查
            script.AddSentence("if(pDrillings==null || pDrillings.length<=0){");
            script.AddSentence("    return;");
            script.AddSentence("}");
            //阴影遮罩
            script.AddSentence("var mask=new Ext.LoadMask(Ext.getBody(),{");
            script.AddSentence("{0}msg:'{1}'", Keyboard.TAB, pLoadingMessage);
            script.AddSentence("});");
            //开启遮罩
            script.AddSentence("mask.show();");
            //执行Ajax请求
            script.AddSentence("Ext.Ajax.request({");
            script.AddSentence("{0}url:'{1}'", Keyboard.TAB, pAjaxHandlerUrl);
            script.AddSentence("{0},params:Ext.JSON.encode(pDrillings)", Keyboard.TAB);
            script.AddSentence("{0},method:'POST'", Keyboard.TAB);
            script.AddSentence("{0},callback:function(options,success,response){{", Keyboard.TAB);
            script.AddSentence("{0}{0}if(success){{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}eval(response.responseText);", Keyboard.TAB);
            script.AddSentence("{0}{0}}}else{{", Keyboard.TAB);
            script.AddSentence("{0}{0}{0}alert('请求失败.');", Keyboard.TAB);
            script.AddSentence("{0}{0}}}", Keyboard.TAB);
            script.AddSentence("{0}{0}mask.hide();", Keyboard.TAB);
            script.AddSentence("{0}}}", Keyboard.TAB);
            script.AddSentence("});");
            //返回
            return script;
        }

        /// <summary>
        /// 创建在表格行右键方式显示上下文菜单的JS函数
        /// </summary>
        /// <param name="pContextMenuID">上下文菜单组件的ID</param>
        /// <param name="pFunctionName">函数名</param>
        /// <returns></returns>
        public static JSFunction CreateShowContextMenu(string pContextMenuID, string pFunctionName = "__fnAnalysisReportShowMenu")
        {
            JSFunction showMenu = new JSFunction();
            showMenu.FunctionName = pFunctionName;
            showMenu.Type = JSFunctionTypes.Common;
            //表格行的上下文事件
            showMenu.Params = new List<string>();
            showMenu.Params.Add("view");
            showMenu.Params.Add("record");
            showMenu.Params.Add("itemHtml");
            showMenu.Params.Add("index");
            showMenu.Params.Add("e");
            showMenu.Params.Add("eOptions");
            showMenu.AddSentence("var menu =Ext.getCmp({0}{1}{0});", JSONConst.STRING_WRAPPER, pContextMenuID);
            showMenu.AddSentence("menu.___current_record =record;");
            showMenu.AddSentence("if(menu!=null && !menu.destroying){");
            showMenu.AddSentence("{0}menu.showAt(e.getXY());", Keyboard.TAB);
            showMenu.AddSentence("}");
            showMenu.AddSentence("e.stopEvent();");
            //
            return showMenu;
        }

        /// <summary>
        /// 创建分析报表中维度列的呈现器函数
        /// </summary>
        /// <param name="pDimColumn">需要创建呈现器函数的维度列</param>
        /// <param name="pDrillingFunctionName">报表的Drilling的JS函数的函数名</param>
        /// <returns></returns>
        public static JSFunction CreateDimColumnRenderer(DimColumn pDimColumn,string pDrillingFunctionName)
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Variable;
            script.FunctionName = string.Format("fn{0}Renderer",pDimColumn.ColumnID);
            //定义函数的参数列表
            script.Params = new List<string>();
            script.Params.Add("pVal");
            script.Params.Add("pMetaData");
            script.Params.Add("pRecord");
            script.Params.Add("pRowIndex");
            script.Params.Add("pColIndex");
            script.Params.Add("pStore");
            script.Params.Add("pView");
            //定义函数体
            script.AddSentence("var dimID = pRecord.get('{0}');", pDimColumn.ColumnID);
            script.AddSentence("var text = '';");
            script.AddSentence("if(pVal!=null){");
            script.AddSentence("{0}text =pVal.toString();", Keyboard.TAB);
            script.AddSentence("}");
            script.AddSentence("var html = '';");
            //script.AddSentence("html += '<a href=\"javascript:{0}('+'\\'{1}\\''+',\\''+dimID+'\\''+');\">' + text + '</a>';", pDrillingFunctionName,pDimColumn.ColumnID);
            script.AddSentence("html += '<a style=\"color:#666;text-decoration:underline;\" href=\"javascript:{0}('+'\\'{1}\\''+',\\''+dimID+'\\''+',\\''+text+'\\''+');\">' + text + '</a>';", pDrillingFunctionName, pDimColumn.ColumnID);
            script.AddSentence("return html;");
            //
            return script;
        }

        /// <summary>
        /// 创建分析报表中维度列的呈现器函数
        /// </summary>
        /// <param name="pDrilledColumn">钻取的列</param>
        /// <param name="pOtherColumns">其他作为筛选条件的维度列</param>
        /// <param name="pMultiDrillJSFunctionName">多维度钻取的函数名</param>
        /// <param name="pLoadingMessage">执行Ajax请求时的加载中的信息</param>
        /// <returns></returns>
        public static JSFunction CreateDimColumnRenderer(DimColumn pDrilledColumn, DimColumn[] pOtherColumns, string pMultiDrillJSFunctionName = "__fnAnalysisReportMultiDrill", string pLoadingMessage = "数据加载中,请稍后...")
        {
            JSFunction script = new JSFunction();
            script.Type = JSFunctionTypes.Variable;
            script.FunctionName = string.Format("fn{0}Renderer", pDrilledColumn.ColumnID);
            //定义函数的参数列表
            script.Params = new List<string>();
            script.Params.Add("pVal");
            script.Params.Add("pMetaData");
            script.Params.Add("pRecord");
            script.Params.Add("pRowIndex");
            script.Params.Add("pColIndex");
            script.Params.Add("pStore");
            script.Params.Add("pView");
            //定义函数体
            script.AddSentence("//设置多维度钻取的参数");
            script.AddSentence("var params={};");
            script.AddSentence("params.DimColumnID ={0}{1}{0};", JSONConst.STRING_WRAPPER, pDrilledColumn.ColumnID);
            script.AddSentence("params.DimValue=pRecord.get({0}{1}{0});", JSONConst.STRING_WRAPPER, pDrilledColumn.ColumnID);
            script.AddSentence("params.DimText=pRecord.get({0}{1}{0});", JSONConst.STRING_WRAPPER, pDrilledColumn.GetTextColumnID());
            if (pOtherColumns != null)
            {
                script.AddSentence("params.DrillingItems=new Array();");
                int i = 0;
                foreach (var dim in pOtherColumns)
                {
                    script.AddSentence("var item{0}={{}};", i.ToString());
                    script.AddSentence("item{2}.DimColumnID={0}{1}{0};", JSONConst.STRING_WRAPPER, dim.ColumnID, i.ToString());
                    script.AddSentence("item{2}.DimValue=pRecord.get({0}{1}{0});", JSONConst.STRING_WRAPPER, dim.ColumnID, i.ToString());
                    script.AddSentence("item{2}.DimText=pRecord.get({0}{1}{0});", JSONConst.STRING_WRAPPER, dim.GetTextColumnID(), i.ToString());
                    script.AddSentence("params.DrillingItems.push(item{0});", i.ToString());
                    i++;
                }
            }
            script.AddSentence("if (window.___MultiDrillingParams == null)");
            script.AddSentence("    window.___MultiDrillingParams = {};");
            script.AddSentence("window.___MultiDrillingParams[pRowIndex.toString()] = params;");
            script.AddSentence("//生成链接");
            script.AddSentence("var html = '';");
            script.AddSentence("html += '<a style=\"color:#666;text-decoration:underline;\" href=\"javascript:{0}(window.___MultiDrillingParams['+pRowIndex.toString()+']);\">' + params.DimText + '</a>';", pMultiDrillJSFunctionName);
            script.AddSentence("return html;");
            //
            return script;
        }
    }
}
