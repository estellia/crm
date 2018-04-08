/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/7 16:02:29
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
using System.Data;
using System.Linq;
using System.Text;

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Report.Analysis;
using JIT.Utility.Report.DataFilter;
using JIT.Utility.Report.Export.DataFormatter;
using JIT.Utility.Report.Export.DataFormatter.Excel;
using JIT.Utility.Report.Export.SummaryFormatter;
using JIT.Utility.Report.Export.SummaryFormatter.Excel;
using JIT.Utility.Web;
using JIT.Utility.Web.ComponentModel.ExtJS;
using JIT.Utility.Web.ComponentModel.ExtJS.Data;
using JIT.Utility.Web.ComponentModel.ExtJS.Menu;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;
using System.Drawing;
using Aspose.Cells;

namespace JIT.Utility.Report
{
    /// <summary>
    /// 分析报表的输出
    /// </summary>
    public class AnalysisReportOutput
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AnalysisReportOutput()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 报表数据的模型
        /// </summary>
        public Model DataModel { get; set; }

        /// <summary>
        /// 报表的列
        /// </summary>
        public List<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column> Columns { get; set; }

        /// <summary>
        /// 表格列与分析列的映射关系
        /// </summary>
        public Dictionary<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column, AnalysisColumn> GridColumnToAnalysisColumnMappings { get; set; }

        /// <summary>
        /// 数据表列与分析列的映射关系
        /// </summary>
        public Dictionary<string, AnalysisColumn> DataTableColumnToAnalysisColumnMappings { get; set; }

        /// <summary>
        /// 报表的数据集
        /// </summary>
        public Store Store { get; set; }

        /// <summary>
        /// 报表的表格面板
        /// </summary>
        public Panel Panel { get; set; }

        /// <summary>
        /// 导航
        /// </summary>
        public ExtJSComponent Navigation { get; set; }

        /// <summary>
        /// 导航的文本信息（用于导出Excel中）
        /// </summary>
        public string NavigationText { get; set; }

        /// <summary>
        /// 上一个上下文菜单
        /// </summary>
        public Menu PrevContextMenu { get; set; }

        /// <summary>
        /// 当前的上下文菜单
        /// </summary>
        public Menu CurrentContextMenu { get; set; }

        /// <summary>
        /// 报表的摘要的呈现器
        /// </summary>
        public Dictionary<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column, IJavascriptObject> SummaryRenderers { get; set; }

        /// <summary>
        /// 列的摘要的值
        /// </summary>
        public Dictionary<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column, Dictionary<ISummary, string>> ColumnSummaryValues { get; set; }

        /// <summary>
        /// 报表的最终数据
        /// <remarks>
        /// <para>该数据经过过滤器进行筛选</para>
        /// </remarks>
        /// </summary>
        public DataTable ReportDataes { get; set; }

        /// <summary>
        /// 原始的报表数据
        /// </summary>
        public DataTable OriginalReportDataes { get; set; }

        /// <summary>
        /// 分析报表结果数据的过滤器
        /// </summary>
        public IDataFilter[] ResultDataFilters { get; set; }

        /// <summary>
        /// 是否已经执行了数据筛选
        /// </summary>
        private bool IsFiltered { get; set; }

        /// <summary>
        /// 报表所用到的一些JS函数
        /// <remarks>
        /// <para>当前有的JS函数有:</para>
        /// <para>1.ShowContextMenu             显示上下文菜单</para>
        /// <para>2.CollectQueryCondition       收集当前的查询条件</para>
        /// <para>3.Query                       报表查询</para>
        /// <para>4.Goto                        跳转到指定钻取剖面</para>
        /// <para>5.DrillIn                     钻入</para>
        /// <para>6.ChangePivot                 改变维度列的数据透视</para>
        /// <para>7.ChangeCRConversion          改变维度列的行列转换</para>
        /// <para>8.ExportToExcel               导出数据到Excel</para>
        /// <para>9.CRExchange                  行列互换</para>
        /// <para>10.ViewDetail                 查看明细</para>
        /// <para>11.MultiDimDrilling           多维度钻取</para>
        /// </remarks>
        /// </summary>
        public Dictionary<string, JSFunction> JSFunctions { get; set; }
        #endregion

        #region 执行数据过滤
        /// <summary>
        /// 执行数据过滤
        /// </summary>
        protected void ExecFilters()
        {
            if (this.ResultDataFilters != null && this.IsFiltered ==false)
            {
                var detailColumns = this.GetDetailColumns(this.Columns);
                foreach (var filter in this.ResultDataFilters)
                {
                    switch (filter.FilterType)
                    {
                        case DataFilterTypes.FilterColumn:
                            this.FilterColumns(filter, detailColumns);
                            break;
                        case DataFilterTypes.FilterRow:
                            this.FilterRow(filter, this.ReportDataes);
                            break;
                        case DataFilterTypes.Both:
                            this.FilterColumns(filter, detailColumns);
                            this.FilterRow(filter, this.ReportDataes);
                            break;
                    }
                }
                //
                this.IsFiltered = true;
            }
        }
        /// <summary>
        /// 过滤列
        /// </summary>
        /// <param name="pFilter"></param>
        /// <param name="pAllDetailColumns">所有的底层表格列</param>
        private void FilterColumns(IDataFilter pFilter,List<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column> pAllDetailColumns)
        {
            //获取所有的底层列
            if (pAllDetailColumns != null && pAllDetailColumns.Count > 0 && pFilter != null)
            {
                bool isChangeHidden = false;
                foreach (var gridCol in pAllDetailColumns)
                {
                    FilteringColumn fc = new FilteringColumn();
                    fc.GridColumn = gridCol;
                    if (this.GridColumnToAnalysisColumnMappings.ContainsKey(gridCol))
                    {
                        fc.AnalysisColumn = this.GridColumnToAnalysisColumnMappings[gridCol];
                    }
                    ColumnDataRetriever cdr = new ColumnDataRetriever(this.ReportDataes,this.ColumnSummaryValues);
                    //
                    if (!pFilter.IsShowColumn(fc, cdr))
                    {
                        gridCol.IsHidden = true;//不显示该列
                        isChangeHidden = true;
                    }
                }
                if (isChangeHidden)
                {
                    if (this.Columns != null)
                    {
                        foreach (var col in this.Columns)
                        {
                            this.HideColumnBecauseAllChildrenIsHidden(col);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 过滤行
        /// </summary>
        /// <param name="pFilter"></param>
        /// <param name="pDataes"></param>
        private void FilterRow(IDataFilter pFilter, DataTable pDataes)
        {
            if (pFilter != null && pDataes != null && pDataes.Rows.Count > 0)
            {
                List<int> needRemovedRowIndex = new List<int>();
                for (var i = 0; i < pDataes.Rows.Count; i++)
                {
                    var dr = pDataes.Rows[i];
                    RowDataRetriever rdr = new RowDataRetriever(dr, this.DataTableColumnToAnalysisColumnMappings);
                    if (!pFilter.IsShowRow(i, rdr))
                    {
                        needRemovedRowIndex.Add(i);
                    }
                }
                //移除列
                if (needRemovedRowIndex != null && needRemovedRowIndex.Count > 0)
                {
                    int removedCount = 0;
                    foreach (var item in needRemovedRowIndex)
                    {
                        var index = item - removedCount;
                        pDataes.Rows.RemoveAt(index);
                        removedCount++;
                    }
                    //
                    pDataes.AcceptChanges();
                }
            }
        }
        #endregion

        #region 生成初始化脚本
        /// <summary>
        /// 生成初始化脚本
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <param name="pRenderTo">表格面板呈现到哪个页面元素下</param>
        /// <returns></returns>
        public string GenerateInitScript(int pPrevTabs, IJavascriptObject pRenderTo)
        {
            //执行数据筛选
            this.ExecFilters();
            //
            var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
            StringBuilder script = new StringBuilder();
            //生成合计行样式
            script.AppendFormat("{0}var ___styleContainer =document.createElement('style');{1}", tabs, Environment.NewLine);
            script.AppendFormat("{0}___styleContainer.setAttribute('type','text/css');{1}", tabs, Environment.NewLine);
            script.AppendFormat("{0}document.getElementsByTagName('head')[0].appendChild(___styleContainer);{1}", tabs, Environment.NewLine);
            script.AppendFormat("{0}___cssContent ='.x-grid-row-summary{{background-color:#e0e2e7;}}';{1}", tabs, Environment.NewLine);
            script.AppendFormat("{0}if(___styleContainer.styleSheet){{{1}", tabs, Environment.NewLine);
            script.AppendFormat("{0}{1}___styleContainer.styleSheet.cssText=___cssContent;{2}", tabs,Keyboard.TAB, Environment.NewLine);
            script.AppendFormat("{0}}}else{{{1}", tabs, Environment.NewLine);
            script.AppendFormat("{0}{1}___styleContainer.innerHTML =___cssContent;{2}", tabs, Keyboard.TAB, Environment.NewLine);
            script.AppendFormat("{0}}}{1}", tabs, Environment.NewLine);
            //script.AppendFormat("{0}if(Ext.isIE7==false){{{1}", tabs, Environment.NewLine);
            //script.AppendFormat("{0}{2}var __summaryStyle = document.createElement('style');{1}", tabs, Environment.NewLine, Keyboard.TAB);
            //script.AppendFormat("{0}{2}__summaryStyle.setAttribute('type','text/css');{1}", tabs, Environment.NewLine, Keyboard.TAB);
            //script.AppendFormat("{0}{2}__summaryStyle.appendChild('.x-grid-row-summary{{background-color:#e0e2e7;}}');{1}", tabs, Environment.NewLine, Keyboard.TAB);
            //script.AppendFormat("{0}{2}document.body.appendChild(__summaryStyle);{1}", tabs, Environment.NewLine, Keyboard.TAB);
            //script.AppendFormat("{0}}}{1}", tabs, Environment.NewLine);
           
            //强制A标签样式
            //script.AppendFormat("{0}var __anchorStyle = document.createElement('style');{1}", tabs, Environment.NewLine);
            //script.AppendFormat("{0}__anchorStyle.setAttribute('type','text/css');{1}", tabs, Environment.NewLine);
            //script.AppendFormat("{0}__anchorStyle.innerHTML ='a{{color:#666;text-decoration:underline;}} a:hover{{color:#f47900;text-decoration:underline;}} a.hoverNone:hover{{text-decoration:underline;}}';{1}", tabs, Environment.NewLine);
            //script.AppendFormat("{0}document.body.appendChild(__anchorStyle);{1}", tabs, Environment.NewLine);
            
            //生成函数
            if (this.JSFunctions != null)
            {
                foreach (var item in this.JSFunctions)
                {
                    var fn = item.Value;
                    fn.Type = JSFunctionTypes.Variable;
                    var fnContent = fn.ToScriptBlock(pPrevTabs);
                    fnContent = fnContent.TrimStart(Keyboard.SPACE);
                    script.AppendFormat("{0}window.{1}={2}{3}", tabs, fn.FunctionName, fnContent, Environment.NewLine);
                    script.AppendFormat("{0}var {1}=window.{1};{2}", tabs, fn.FunctionName, Environment.NewLine);
                }
            }
            //将生成的代码放在一个代码块中，避免变量名的重名
            script.AppendFormat("{0}{{{1}", tabs, Environment.NewLine);
            //生成定义Model的脚本
            var modelCreation = this.DataModel.ToDefineScript(pPrevTabs + 1);
            modelCreation = modelCreation.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, modelCreation, Environment.NewLine);
            //生成创建Store的脚本
            var storeCreation = this.Store.ToCreationScript(pPrevTabs + 1);
            storeCreation = storeCreation.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, storeCreation, Environment.NewLine);
            //生成创建Grid的脚本
            this.Panel.RenderTo = pRenderTo;
            var panelCreation = this.Panel.ToCreationScript(pPrevTabs + 1);
            panelCreation = panelCreation.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, panelCreation, Environment.NewLine);
            //设置表格行的上下文事件处理
            var showContextMenuFn = this.JSFunctions.Where(item => item.Key == "ShowContextMenu").Select(item => item.Value).FirstOrDefault();
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, this.Panel.AddListener("itemcontextmenu", showContextMenuFn).ToScriptBlock(0), Environment.NewLine);
            //生成创建上下文菜单的脚本
            var contextMenuCreation = this.CurrentContextMenu.ToCreationScript(pPrevTabs + 1);
            contextMenuCreation = contextMenuCreation.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, contextMenuCreation, Environment.NewLine);
            //代码块结束
            script.AppendFormat("{0}}}{1}", tabs, Environment.NewLine);
            //
            return script.ToString();
        }
        #endregion

        #region 生成更新数据的脚本
        /// <summary>
        /// 生成更新数据的脚本
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns></returns>
        public string GenerateUpdateDataScript(int pPrevTabs = 0)
        {
            //执行数据筛选
            this.ExecFilters();
            //
            var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
            StringBuilder script = new StringBuilder();
            //将生成的代码放在一个代码块中，避免变量名的重名
            script.AppendFormat("{0}{{{1}", tabs, Environment.NewLine);
            //生成定义Model的脚本
            var modelCreation = this.DataModel.ToDefineScript(pPrevTabs + 1);
            modelCreation = modelCreation.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, modelCreation, Environment.NewLine);
            //生成创建Store的脚本
            var storeCreation = this.Store.ToCreationScript(pPrevTabs + 1);
            storeCreation = storeCreation.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, storeCreation, Environment.NewLine);
            //生成创建Grid列的脚本
            var columnCreation = this.Columns.ToCreationConfigScript(pPrevTabs + 1);
            columnCreation = columnCreation.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}var __newAnalysisReportColumns ={2};{3}", tabs, Keyboard.TAB, columnCreation, Environment.NewLine);
            //生成重置Grid的脚本
            var reconfigureGridScript = string.Format("Ext.getCmp(\"{0}\").reconfigure({1},{2});", this.Panel.ID, StoreManager.Lookup(this.Store.StoreID).ToScriptBlock(0), "__newAnalysisReportColumns");
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, reconfigureGridScript, Environment.NewLine);
            //重新设置钻取的导航
            script.AppendFormat("{0}{1}var __newNaviText={2};{3}", tabs, Keyboard.TAB, this.Navigation.HTML.ToScriptBlock(0), Environment.NewLine);
            script.AppendFormat("{0}{1}Ext.getCmp(\"{2}\").setText(__newNaviText,false);{3}", tabs, Keyboard.TAB, this.Navigation.ID, Environment.NewLine);
            //重新创建上下文菜单
            if (this.PrevContextMenu != null)
            {
                //销毁上一个上下文菜单
                script.AppendFormat("{0}{1}Ext.getCmp(\"{2}\").destroy();{3}", tabs, Keyboard.TAB, this.PrevContextMenu.ID, Environment.NewLine);
            }
            var contextMenuCreation = this.CurrentContextMenu.ToCreationScript(pPrevTabs + 1);
            contextMenuCreation = contextMenuCreation.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, contextMenuCreation, Environment.NewLine);
            //重置显示上下文菜单的函数
            var showContextMenuFn = this.JSFunctions.Where(item => item.Key == "ShowContextMenu").Select(item => item.Value).FirstOrDefault();
            showContextMenuFn.Type = JSFunctionTypes.Variable;
            var showContextMenuFnContent = showContextMenuFn.ToScriptBlock(pPrevTabs);
            showContextMenuFnContent = showContextMenuFnContent.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}window.{2}={3}{4}", tabs, Keyboard.TAB, showContextMenuFn.FunctionName, showContextMenuFnContent, Environment.NewLine);
            script.AppendFormat("{0}{1}var {2}=window.{2};{3}", tabs, Keyboard.TAB, showContextMenuFn.FunctionName, Environment.NewLine);
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, this.Panel.RemoveListener("itemcontextmenu", showContextMenuFn).ToScriptBlock(0), Environment.NewLine);
            script.AppendFormat("{0}{1}{2}{3}", tabs, Keyboard.TAB, this.Panel.AddListener("itemcontextmenu", showContextMenuFn).ToScriptBlock(0), Environment.NewLine);
            //代码块结束
            script.AppendFormat("{0}}}{1}", tabs, Environment.NewLine);
            //
            return script.ToString();
        }
        #endregion

        #region 将报表数据以所见即所得的方式导出为Excel
        /// <summary>
        /// 将报表数据以所见即所得的方式导出为Excel
        /// </summary>
        /// <returns></returns>
        public Workbook WriteXLS()
        {
            //执行筛选
            this.ExecFilters();
            //创建Excel工作薄和工作表
            Aspose.Cells.License lic = new License();
            lic.SetLicense("Aspose.Total.lic");
            Workbook wb = new Workbook();
            wb.Worksheets.Clear();
            wb.Worksheets.Add();
            wb.Worksheets[0].Name = this.ReportDataes.TableName;
            var sheet = wb.Worksheets[0];
            sheet.IsGridlinesVisible = true;
            //定义一些全局属性
            int startRowIndex = 1; //从第二行开始,第一行保留给报表的导航栏
            List<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column> detailColumns;
            //写入标题
            this.WriteHead(wb, sheet,ref startRowIndex, this.Columns,out detailColumns);
            //写入数据
            int summaryDepth = 0;   //摘要区域的深度
            if (this.ReportDataes != null && detailColumns!=null && detailColumns.Count>0)
            {
                List<ISummary> writtenTitleSummaries = new List<ISummary>();    //已经写了摘要标题的摘要列表
                //逐列写数据&摘要
                for (int i = 0; i < detailColumns.Count;i++ )  //detailColumns列表是有序的
                {
                    var col = detailColumns[i];
                    //找到表格列所属的分析列,从而获取格式化器
                    var analysisCol = this.GridColumnToAnalysisColumnMappings.FirstOrDefault(item => item.Key == col).Value;
                    IDataFormatter formatter = analysisCol.DataFormatter != null ? analysisCol.DataFormatter : new CommonExcelDataFormatter();
                    //一列一列的写数据
                    for(int j=0;j<this.ReportDataes.Rows.Count;j++)
                    {
                        var dr =this.ReportDataes.Rows[j];
                        var data = dr[col.DataIndex];
                        if (data == DBNull.Value)
                            data = null;
                        //格式化并写数据
                        formatter.WriteData(sheet, data, startRowIndex + j, i, col.ColumnType,col);
                    }
                    //写摘要
                    var summaries = this.ColumnSummaryValues.FirstOrDefault(item => item.Key == col).Value;
                    if (summaries != null && summaries.Count > 0)
                    {
                        int summaryRowIndex =startRowIndex+this.ReportDataes.Rows.Count;
                        //通过摘要格式化器写摘要
                        foreach (var summary in summaries)
                        {
                            var summaryFormatter = summary.Key.Formatter != null ? summary.Key.Formatter : new CommonExcelSummaryFormatter();
                            summaryFormatter.WriteSummary(sheet, summary.Key, summary.Value, summaryRowIndex, i,i==0, col.ColumnType);
                            if (i == 0)
                            {
                                writtenTitleSummaries.Add(summary.Key);
                            }
                            else
                            {
                                var isWrittenTitle = (writtenTitleSummaries.FirstOrDefault(item => item == summary.Key) !=null);
                                if (!isWrittenTitle)
                                {
                                    summaryFormatter.WriteSummaryTitle(sheet, summary.Key, summaryRowIndex, 0);
                                }
                                writtenTitleSummaries.Add(summary.Key);
                            }
                            //
                            summaryRowIndex++;
                        }
                        if (summaries.Count > summaryDepth)
                            summaryDepth = summaries.Count;
                    }
                }
            }
            //设置数据区域和摘要区域的边框
            var dataRange = sheet.Cells.CreateRange(startRowIndex, 0, this.ReportDataes.Rows.Count, detailColumns.Count);
            this.SetDataRangeStyle(wb, dataRange);
            if (summaryDepth > 0)
            {
                var summaryRange = sheet.Cells.CreateRange(startRowIndex + this.ReportDataes.Rows.Count, 0, summaryDepth, detailColumns.Count);
                this.SetSummaryRangeStyle(wb, summaryRange);
            }
            //写面包屑导航
            var navigationRange = sheet.Cells.CreateRange(0, 0, 1, detailColumns.Count);
            navigationRange.Merge();
            navigationRange.Value = this.NavigationText;
            this.SetNavigationStyle(wb, navigationRange);
            //
            return wb;
        }

        /// <summary>
        /// Excel列
        /// </summary>
        class InnerExcelColumn
        {
            /// <summary>
            /// 所属的Ext JS表格列
            /// </summary>
            public JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column Column { get; set; }

            /// <summary>
            /// 开始的列索引
            /// </summary>
            public int StartColumnIndex { get; set; }

            /// <summary>
            /// 列合并
            /// </summary>
            public int ColSpan { get; set; }

            /// <summary>
            /// 行合并
            /// </summary>
            public int RowSpan { get; set; }
        }

        /// <summary>
        /// 写标题
        /// </summary>
        /// <param name="pWorkbook">当前的工作薄</param>
        /// <param name="pSheet">需要写标题的工作表</param>
        /// <param name="pStartRowIndex">标题的起始行索引</param>
        /// <param name="pColumns">所有的列</param>
        /// <param name="pDetailColumns">所有子列</param>
        private void WriteHead(Workbook pWorkbook, Worksheet pSheet,ref int pStartRowIndex, List<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column> pColumns,out List<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column> pDetailColumns )
        {
            pDetailColumns = new List<Web.ComponentModel.ExtJS.Grid.Column.Column>();
            //先遍历表中的所有列（包含子列）,按层级将各个列组织好
            if (pColumns != null)
            {
                int maxDepth = 0;
                int startColumnIndex = 0;
                Dictionary<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column, Dictionary<int, List<InnerExcelColumn>>> columnMappings = new Dictionary<Web.ComponentModel.ExtJS.Grid.Column.Column, Dictionary<int, List<InnerExcelColumn>>>();
                //首先将Ext的表格列转化为层级的Excel列
                foreach (var item in pColumns)
                {
                    if (this.IsShowColumn(item))
                    {
                        //获取当前表格列的所有Excel列
                        Dictionary<int, List<InnerExcelColumn>> currentExcelColumns = new Dictionary<int, List<InnerExcelColumn>>();
                        int temp;
                        int depth = 0;
                        this.RetrieveColumns(item, depth, currentExcelColumns, out temp,startColumnIndex);
                        //
                        var currentMaxDepth =currentExcelColumns.Max(dicItem => dicItem.Key);
                        if (currentMaxDepth > maxDepth)
                            maxDepth = currentMaxDepth;
                        //
                        startColumnIndex += currentExcelColumns[currentMaxDepth].Count;
                        //添加到映射对象中
                        columnMappings.Add(item, currentExcelColumns);
                    }
                }
                //设置Excel列的行合并,并从中获取所有最底层的列
                if (columnMappings != null)
                {
                    foreach (var item in columnMappings)
                    {
                        if (item.Key.Children == null || item.Key.Children.Count <= 0)
                        {
                            //如果没有子列,则Excel列应该为只有一个,从而设置该列的行合并为层级的总深度
                            item.Value[0][0].RowSpan = maxDepth + 1;
                            //
                            pDetailColumns.Add(item.Value[0][0].Column);
                        }
                        else
                        {
                            var currentColMaxDepth = item.Value.Max(ecl => ecl.Key);
                            pDetailColumns.AddRange(item.Value[currentColMaxDepth].Select(ecl => ecl.Column).ToArray());
                        }
                    }
                }
                //合并到总的Excel列列表中
                Dictionary<int, List<InnerExcelColumn>> allExcelColumns = new Dictionary<int, List<InnerExcelColumn>>();
                if (columnMappings != null)
                {
                    foreach (var item in columnMappings)
                    {
                        var currentExcelColumns = item.Value;
                        foreach (var cec in currentExcelColumns)
                        {
                            if (allExcelColumns.ContainsKey(cec.Key))
                            {
                                var list = allExcelColumns[cec.Key];
                                if (list == null)
                                    list = new List<InnerExcelColumn>();
                                list.AddRange(cec.Value);
                            }
                            else
                            {
                                allExcelColumns.Add(cec.Key, cec.Value);
                            }
                        }
                    }
                }
                //写Excel表头
                if (allExcelColumns != null && allExcelColumns.Count > 0)
                {
                    foreach (var item in allExcelColumns)
                    {
                        int rowIndex = pStartRowIndex;
                        foreach (var col in item.Value)
                        {
                            Range r = pSheet.Cells.CreateRange(rowIndex, col.StartColumnIndex, col.RowSpan, col.ColSpan);
                            if (col.RowSpan > 1 || col.ColSpan > 1)
                            {
                                r.Merge();
                            }
                            r.Value = col.Column.ColumnTitle;
                            r.ColumnWidth = col.Column.Width / 7.03;
                            this.SetTitleStyle(pWorkbook, r);   //设置标题样式
                        }
                        pStartRowIndex++;
                    }
                }
            }
        }

        /// <summary>
        /// 递归将Ext的表格列中的所有列抓取出来
        /// </summary>
        /// <param name="pColumn">表格列</param>
        /// <param name="pCurrentDepth">当前的深度</param>
        /// <param name="pColumns">按层级深度组织的表格列</param>
        /// <param name="pCurrentColSpan">当前列的列合并</param>
        /// <param name="pCurrentColumnStartIndex">当前列的开始索引</param>
        private void RetrieveColumns(JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column pColumn,int pCurrentDepth,Dictionary<int,List<InnerExcelColumn>> pColumns,out int pCurrentColSpan,int pCurrentColumnStartIndex)
        {
            pCurrentColSpan = 0;
            if (this.IsShowColumn(pColumn))
            {
                pCurrentColSpan = 1;    //列需要显示,因此占一个Excel列
                //获得当前深度的表格列列表
                List<InnerExcelColumn> depthList = null;
                if (pColumns.ContainsKey(pCurrentDepth))
                {
                    depthList = pColumns[pCurrentDepth];
                }
                else
                {
                    depthList = new List<InnerExcelColumn>();
                    pColumns.Add(pCurrentDepth, depthList);
                }
                InnerExcelColumn col = new InnerExcelColumn();
                col.Column = pColumn;
                col.ColSpan = pCurrentColSpan;
                col.RowSpan = 1;
                col.StartColumnIndex = pCurrentColumnStartIndex;
                depthList.Add(col);
                //处理子列
                if (pColumn.Children != null && pColumn.Children.Count > 0)
                {
                    bool hasChildren = false;
                    foreach (var item in pColumn.Children)
                    {
                        if (this.IsShowColumn(item))
                        {
                            int childrenColSpan;
                            if (hasChildren)
                            {
                                pCurrentColumnStartIndex++;
                            }
                            else
                            {
                                ++pCurrentDepth;    //在第一个子节点的时候,将节点的深度+1,从而获得子节点的深度
                                hasChildren = true;
                            }
                            this.RetrieveColumns(item,pCurrentDepth, pColumns, out childrenColSpan, pCurrentColumnStartIndex);
                            pCurrentColSpan += childrenColSpan;
                            col.ColSpan = pCurrentColSpan;
                        }
                    }
                    if (hasChildren)    //列合并的起始值是从1开始,根据子列对父列进行列合并时,最后要减一
                    {
                        pCurrentColSpan--;
                        col.ColSpan -= 1;
                    }
                }
            }
        }

        /// <summary>
        /// 判断指定的EXT表格列是否要显示
        /// </summary>
        /// <param name="pColumnItem"></param>
        /// <returns></returns>
        private bool IsShowColumn(Web.ComponentModel.ExtJS.Grid.Column.Column pColumnItem)
        {
            bool result = true;
            if (pColumnItem == null || (pColumnItem.IsHidden.HasValue && pColumnItem.IsHidden.Value == true))
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 设置标题区域的样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="pRange"></param>
        private void SetTitleStyle(Workbook workbook, Range pRange)
        {
            Style style = this.CreateStyle(workbook);

            StyleFlag styleFlag = new StyleFlag();
            //Specify all attributes
            styleFlag.All = true;
            styleFlag.Borders = true;
            pRange.ApplyStyle(style, styleFlag);
        }

        /// <summary>
        /// 设置面包屑导航的样式
        /// </summary>
        /// <param name="pWorkbook"></param>
        /// <param name="pRange"></param>
        private void SetNavigationStyle(Workbook pWorkbook, Range pRange)
        {
            Style style = this.CreateStyle(pWorkbook);
            style.HorizontalAlignment = TextAlignmentType.Left;

            StyleFlag styleFlag = new StyleFlag();
            //Specify all attributes
            styleFlag.All = true;
            styleFlag.Borders = true;
            pRange.ApplyStyle(style, styleFlag);
        }

        /// <summary>
        /// 创建标题样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="AlignmentType"></param>
        /// <returns></returns>
        private Style CreateStyle(Workbook workbook, TextAlignmentType AlignmentType = TextAlignmentType.Center)
        {
            Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
            Color c = Color.Gray;
            style.ForegroundColor = c;
            style.Pattern = BackgroundType.Solid;
            style.Font.Color = Color.White;
            style.Font.IsBold = true;
            style.HorizontalAlignment = AlignmentType;
            SetBorder(style);
            return style;
        }

        /// <summary>
        /// 设置数据区域的样式
        /// </summary>
        /// <param name="pWorkbook"></param>
        /// <param name="pRange"></param>
        private void SetDataRangeStyle(Workbook pWorkbook, Range pRange)
        {
            Style dataRangeStyle = pWorkbook.Styles[pWorkbook.Styles.Add()];
            SetBorder(dataRangeStyle);
            StyleFlag styleFlag = new StyleFlag();
            styleFlag.All = true;
            styleFlag.Borders = true;
            pRange.ApplyStyle(dataRangeStyle, styleFlag);
        }

        /// <summary>
        /// 设置摘要区域的样式
        /// </summary>
        /// <param name="pWorkbook"></param>
        /// <param name="pRange"></param>
        private void SetSummaryRangeStyle(Workbook pWorkbook, Range pRange)
        {
            Style dataRangeStyle = pWorkbook.Styles[pWorkbook.Styles.Add()];
            SetBorder(dataRangeStyle);
            StyleFlag styleFlag = new StyleFlag();
            styleFlag.All = true;
            styleFlag.Borders = true;
            pRange.ApplyStyle(dataRangeStyle, styleFlag);
        }

        /// <summary>
        /// 设置边框样式
        /// </summary>
        /// <param name="style"></param>
        private static void SetBorder(Style style)
        {
            //Setting the line style of the top border
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            //Setting the color of the top border
            style.Borders[BorderType.TopBorder].Color = Color.Black;
            //Setting the line style of the bottom border
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            //Setting the color of the bottom border
            style.Borders[BorderType.BottomBorder].Color = Color.Black;
            //Setting the line style of the left border
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            //Setting the color of the left border
            style.Borders[BorderType.LeftBorder].Color = Color.Black;
            //Setting the line style of the right border
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            //Setting the color of the right border
            style.Borders[BorderType.RightBorder].Color = Color.Black;
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 获取最底层的列
        /// </summary>
        /// <param name="pAllColumns">所有的列</param>
        /// <returns></returns>
        protected List<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column> GetDetailColumns(List<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column> pAllColumns)
        {
            List<JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column> result = new List<Web.ComponentModel.ExtJS.Grid.Column.Column>();
            //
            if (pAllColumns != null && pAllColumns.Count > 0)
            {
                foreach (var item in pAllColumns)
                {
                    if (item.Children == null || item.Children.Count <= 0)
                    {
                        result.Add(item);
                    }
                    else
                    {
                        result.AddRange(this.GetDetailColumns(item.Children));
                    }
                }
            }
            //
            return result;
        }

        /// <summary>
        /// 列因为其下的所有子列都是隐藏的而被隐藏
        /// </summary>
        /// <param name="pColumns"></param>
        protected void HideColumnBecauseAllChildrenIsHidden(JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column pColumn)
        {
            var needHidden = false;
            if (pColumn.Children != null && pColumn.Children.Count > 0)
            {
                //递归隐藏
                foreach (var child in pColumn.Children)
                {
                    this.HideColumnBecauseAllChildrenIsHidden(child);
                }
                //判断当前子列是否都为隐藏
                bool isAllHidden = true;
                foreach (var child in pColumn.Children)
                {
                    if (child.IsHidden.HasValue ==false || child.IsHidden.Value ==false)
                    {
                        isAllHidden = false;
                        break;
                    }
                }
                if (isAllHidden)    //如果所有的子列都为隐藏的,则自身也隐藏
                    needHidden = true;
            }
            if (needHidden)
                pColumn.IsHidden = true;
        }
        #endregion
    }
}
