using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Drawing;
using System.Xml;
using System.Data;
using JIT.Utility.Log;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.Checker;
using JIT.Utility.ETCL.DataStructure;
using JIT.Utility.ETCL.Base;
using JIT.Utility.ETCL.Transformer;
using JIT.Utility.ETCL.Loader;
using JIT.Utility.DataAccess;

namespace JIT.Utility.ETCL
{
    /// <summary>
    /// 通用的Excel数据ETCL处理类
    /// </summary>
    public class CommonExcelETCL : BaseExcelETCL
    {
        /// <summary>
        /// 当前正在处理的ETCL模板及业务数据
        /// </summary>
        protected ETCLTemplateInfo _currentBusinessInfo;

        ///// <summary>
        ///// 所有转换器
        ///// </summary>
        //protected Dictionary<ITransformer, List<string>> AllTransformer;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        public CommonExcelETCL(BasicUserInfo pUserInfo)
        {
            //从XML加载列及模板信息
            this._currentBusinessInfo = new ETCLTemplateInfo();
            this._currentBusinessInfo.Lang = (int)Reflection.DynamicReflectionHandler.GetProperty(pUserInfo, "Lang");
            this.CurrentUserInfo = pUserInfo;
            XmlDocument xmlDoc = new XmlDocument();
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "\\ETCLConfig.xml";
            xmlDoc.Load(xmlPath);
            List<XmlNode> lstTemplates = XMLHandler.GetNodesByProperty(xmlDoc, "JitETCLTemplates/JitETCLTemplate", "ClientID", pUserInfo.ClientID);
            if (this._currentBusinessInfo.Lang == 2)
                this._currentBusinessInfo.TemplateName = lstTemplates[0].Attributes["TemplateNameEn"].Value;
            else
                this._currentBusinessInfo.TemplateName = lstTemplates[0].Attributes["TemplateName"].Value;
            this._currentBusinessInfo.TemplateDescription = lstTemplates[0].Attributes["TemplateDescription"].Value;
            this._currentBusinessInfo.WorkSheets = new List<ETCLWorkSheetInfo>();
            //加载Worksheets
            foreach (XmlNode worksheetItem in lstTemplates[0].ChildNodes)
            {
                ETCLWorkSheetInfo worksheetInfo = new ETCLWorkSheetInfo();
                worksheetInfo.Transformers = new List<ITransformer>();
                worksheetInfo.Transformers.Add(new DefaultTransformer(worksheetInfo));
                worksheetInfo.Tables = new List<ETCLTable>();
                if (this._currentBusinessInfo.Lang == 2)
                    worksheetInfo.WorkSheetName = worksheetItem.Attributes["WorkSheetNameEn"].Value;
                else
                    worksheetInfo.WorkSheetName = worksheetItem.Attributes["WorkSheetName"].Value;
                worksheetInfo.WorkSheetDescription = worksheetItem.Attributes["WorkSheetDescription"].Value;
                worksheetInfo.Index = this._currentBusinessInfo.WorkSheets.Count;
                this._currentBusinessInfo.WorkSheets.Add(worksheetInfo);
                
                //加载 当前Worksheet所涉及到的数据表
                foreach (XmlNode tableItem in worksheetItem.ChildNodes)
                {
                    ETCLTable etclTable = new ETCLTable();
                    etclTable.Columns = new List<ETCLColumn>();
                    etclTable.TableName = tableItem.Attributes["TableName"].Value;
                    if (this._currentBusinessInfo.Lang == 2)
                        etclTable.TableDesc = tableItem.Attributes["TableDescEn"].Value;
                    else
                        etclTable.TableDesc = tableItem.Attributes["TableDesc"].Value;
                    if (tableItem.Attributes["ReferenceOnly"] != null && !string.IsNullOrWhiteSpace(tableItem.Attributes["ReferenceOnly"].Value))
                        etclTable.ReferenceOnly = Convert.ToInt32(tableItem.Attributes["ReferenceOnly"].Value);
                    if (tableItem.Attributes["DAOAssemblyName"] != null)
                        etclTable.DAOAssemblyName = tableItem.Attributes["DAOAssemblyName"].Value;
                    if (tableItem.Attributes["EntityAssemblyName"] != null)
                        etclTable.EntityAssemblyName = tableItem.Attributes["EntityAssemblyName"].Value;
                    if (tableItem.Attributes["DAOName"] != null)
                        etclTable.DAOName = tableItem.Attributes["DAOName"].Value;
                    if (tableItem.Attributes["EntityName"] != null)
                        etclTable.EntityName = tableItem.Attributes["EntityName"].Value;
                    etclTable.ReferenceDepth = 0;
                    etclTable.Worksheet = worksheetInfo;
                    etclTable.Index = worksheetInfo.Tables.Count;
                    etclTable.Loaders = new List<ILoader>();
                    etclTable.Loaders.Add(new DefaultLoader());
                    worksheetInfo.Tables.Add(etclTable);

                    //加载表中的列
                    foreach (XmlNode columnItem in tableItem.ChildNodes)
                    {
                        ETCLColumn etclColumnInfo = new ETCLColumn();
                        if (columnItem.Attributes["ColumnName"] != null)
                            etclColumnInfo.ColumnName = columnItem.Attributes["ColumnName"].Value;


                        if (this._currentBusinessInfo.Lang == 2)
                        {
                            if (columnItem.Attributes["ColumnTextEn"] != null)
                                etclColumnInfo.ColumnText = columnItem.Attributes["ColumnTextEn"].Value;
                        }
                        else
                        {
                            if (columnItem.Attributes["ColumnText"] != null)
                                etclColumnInfo.ColumnText = columnItem.Attributes["ColumnText"].Value;
                        }
                        if (columnItem.Attributes["ColumnType"] != null)
                            etclColumnInfo.ColumnType = columnItem.Attributes["ColumnType"].Value;
                        if (columnItem.Attributes["Visible"] != null)
                            if (!string.IsNullOrEmpty(columnItem.Attributes["Visible"].Value))
                                etclColumnInfo.Visible = columnItem.Attributes["Visible"].Value != "0";
                        if (columnItem.Attributes["Nullable"] != null)
                            if (!string.IsNullOrEmpty(columnItem.Attributes["Nullable"].Value))
                                etclColumnInfo.Nullable = columnItem.Attributes["Nullable"].Value != "0";
                        if (columnItem.Attributes["Duplicatable"] != null)
                            if (!string.IsNullOrEmpty(columnItem.Attributes["Duplicatable"].Value))
                                etclColumnInfo.Duplicatable = columnItem.Attributes["Duplicatable"].Value != "0";
                        if (columnItem.Attributes["CanTermReplace"] != null)
                            if (!string.IsNullOrEmpty(columnItem.Attributes["CanTermReplace"].Value))
                                etclColumnInfo.CanTermReplace = columnItem.Attributes["CanTermReplace"].Value != "0";
                        if (columnItem.Attributes["IsLogicalField"] != null)
                            if (!string.IsNullOrEmpty(columnItem.Attributes["IsLogicalField"].Value))
                                etclColumnInfo.IsLogicalField = columnItem.Attributes["IsLogicalField"].Value != "0";
                        if (columnItem.Attributes["MaxValue"] != null)
                            etclColumnInfo.MaxValue = columnItem.Attributes["MaxValue"].Value;
                        if (columnItem.Attributes["MinValue"] != null)
                            etclColumnInfo.MinValue = columnItem.Attributes["MinValue"].Value;
                        if (columnItem.Attributes["ReferenceTableName"] != null)
                            etclColumnInfo.ReferenceTableName = columnItem.Attributes["ReferenceTableName"].Value;
                        if (columnItem.Attributes["ReferenceAdditionalCondition"] != null)
                            etclColumnInfo.ReferenceAdditionalCondition = columnItem.Attributes["ReferenceAdditionalCondition"].Value;
                        if (columnItem.Attributes["ReferenceColumnName"] != null)
                            etclColumnInfo.ReferenceColumnName = columnItem.Attributes["ReferenceColumnName"].Value;

                        if (this._currentBusinessInfo.Lang == 2)
                        {
                            if (columnItem.Attributes["ReferenceTextColumnNameEn"] != null)
                                etclColumnInfo.ReferenceTextColumnName = columnItem.Attributes["ReferenceTextColumnNameEn"].Value;
                        }
                        else
                        {
                            if (columnItem.Attributes["ReferenceTextColumnName"] != null)
                                etclColumnInfo.ReferenceTextColumnName = columnItem.Attributes["ReferenceTextColumnName"].Value;
                        }
                        etclColumnInfo.ValueCountCache = new Dictionary<object, int>();
                        etclColumnInfo.ValueCache = new List<object>();
                        //添加默认Checker
                        etclColumnInfo.Checkers = new List<IChecker>();
                        if (etclColumnInfo.Nullable.HasValue && !etclColumnInfo.Nullable.Value)
                        {
                            etclColumnInfo.Checkers.Add(new DefaultNullValueChecker());
                        }
                        if (etclColumnInfo.Duplicatable.HasValue && !etclColumnInfo.Duplicatable.Value)
                        {
                            etclColumnInfo.Checkers.Add(new DefaultDuplicateChecker(this.CurrentUserInfo, etclTable, etclColumnInfo));
                        }
                        if (!string.IsNullOrWhiteSpace(etclColumnInfo.MaxValue) || !string.IsNullOrWhiteSpace(etclColumnInfo.MinValue))
                        {
                            etclColumnInfo.Checkers.Add(new ValueRangeChecker(etclColumnInfo));
                        }
                        etclTable.Columns.Add(etclColumnInfo);
                    }
                }
            }
        }

        /// <summary>
        /// 执行数据源到ETCL数据的转换
        /// </summary>
        /// <param name="pDataSource">源数据（XLS文件路径）</param>
        /// <param name="oResult">转换结果详情</param>
        /// <returns>ETCL数据</returns>
        protected override IETCLDataItem[] Transform(DataTable pDataSource, out IETCLResultItem[] oResult)
        {
            IETCLDataItem[] dataDetail = null;
            List<IETCLDataItem> lstItems = new List<IETCLDataItem>();
            foreach (var item in this._currentBusinessInfo.GetWorksheetByName(this.CurrentWorkSheet.Name).Transformers)
            {
                if (!item.Process(pDataSource, this.CurrentUserInfo, out dataDetail, out oResult))
                    return null;
                lstItems.AddRange(dataDetail);
            }
            oResult = null;
            return lstItems.ToArray();
        }

        ///// <summary>
        ///// 检查当前Worksheet中的所有数据
        ///// </summary>
        ///// <param name="pDataSource">ETCL工作簿信息</param>
        ///// <param name="oResult">检查结果详情</param>
        ///// <returns>TRUE：检查通过，FALSE：检查不通过。</returns>
        //private bool CommonCheck(object pDataSource, out IETCLResultItem[] oResult)
        //{
        //    bool isPass = true;
        //    List<ETCLCommonResultItem> lstCheckResult = new List<ETCLCommonResultItem>();
        //    ETCLWorkSheetInfo worksheetItem = (ETCLWorkSheetInfo)pDataSource;
        //    foreach (var tableItem in worksheetItem.Tables)
        //    {//每个库表进行处理
        //        if (tableItem.Entities != null)
        //        {
        //            if (!Check(tableItem.Entities.ToArray(), out oResult))
        //                return false;
        //        }
        //    }
        //    oResult = lstCheckResult.ToArray();
        //    return isPass;
        //}

        /// <summary>
        /// 检查指定的ETCL数据项
        /// </summary>
        /// <param name="pDataItems">ETCL数据项</param>
        /// <param name="oResult">检查结果详情</param>
        /// <returns>TRUE：检查通过，FALSE：检查不通过。</returns>
        protected override bool Check(IETCLDataItem[] pDataItems, out IETCLResultItem[] oResults)
        {
            if (pDataItems == null || pDataItems.Length == 0)
            {
                oResults = null;
                return true;
            }
            //3.实体的每个属性
            //CommonExcelDataItem[] dataItems = pDataItems.Cast<CommonExcelDataItem>().ToArray();
            bool isPass = true;
            List<ETCLCommonResultItem> lstCheckResult = new List<ETCLCommonResultItem>();
            List<string> tableNames = new List<string>();
            foreach (var item in pDataItems)
            {
                CommonExcelDataItem dataItem = (CommonExcelDataItem)item;
                if (tableNames.Contains(dataItem.Table.TableName))
                    continue;
                tableNames.Add(dataItem.Table.TableName);
                foreach (var properyItem in dataItem.Table.Columns)
                {// 列 （并不是所有的列都要校验）
                    if (properyItem.Checkers == null || properyItem.Checkers.Count == 0 || properyItem.IsLogicalField.Value)
                        continue;
                    foreach (var checker in properyItem.Checkers)
                    {
                        foreach (var rowItem in pDataItems)
                        {//4.每一行记录
                            CommonExcelDataItem entityItem = (CommonExcelDataItem)rowItem;
                            object oCellValue = properyItem.ValueCache[entityItem.Index.Value];// Reflection.DynamicReflectionHandler.GetProperty(entityItem.Entity, properyItem.ColumnName);
                            ETCLCommonResultItem tempResult = new ETCLCommonResultItem();
                            tempResult.WorksheetIndex = entityItem.Table.Worksheet.Index.Value;
                            tempResult.TableIndex = entityItem.Table.Index.Value;
                            tempResult.RowIndex = entityItem.Index.Value;
                            tempResult.ColumnOrder = entityItem.Table.Columns.IndexOf(properyItem);
                            IETCLResultItem tempResult1 = new ETCLCommonResultItem();
                            if (!checker.Process(oCellValue, properyItem.ColumnText, entityItem, ref  tempResult1))
                            {//无效
                                isPass = false;
                                tempResult.Message = tempResult1.Message;
                                tempResult.OPType = tempResult1.OPType;
                                tempResult.ResultCode = tempResult1.ResultCode;
                                lstCheckResult.Add(tempResult);
                            }
                        }
                    }
                }
            }
            oResults = lstCheckResult.ToArray();
            return isPass;
        }

        ///// <summary>
        ///// 将当前工作表中的所有数据创建到数据库中
        ///// </summary>
        ///// <param name="pDataSource">ETCL工作簿信息</param>
        ///// <param name="oResult">处理结果详情</param>
        ///// <returns>TRUE：处理成功，FALSE：处理失败。</returns>
        //private bool CommonLoad(ETCLWorkSheetInfo pDataSource, out IETCLResultItem[] oResult)
        //{
        //    ETCLWorkSheetInfo worksheetItem = (ETCLWorkSheetInfo)pDataSource;
        //    foreach (var tableItem in worksheetItem.Tables)
        //    {
        //        if (tableItem.ReferenceOnly == 1 || tableItem.Entities == null)
        //            continue;
        //        if (Load(tableItem.Entities.ToArray(), out oResult) <= 0)
        //            return false;
        //        if (worksheetItem.Loaders != null)
        //        {
        //            foreach (var loaderItem in worksheetItem.Loaders)
        //            {
        //                if (!loaderItem.Process(tableItem.Entities.ToArray(), this.CurrentUserInfo, out oResult))
        //                    return false;
        //            }
        //        }
        //    }
        //    oResult = null;
        //    return true;
        //}

        /// <summary>
        /// 将指定的ETCL数据项创建到数据库中
        /// </summary>
        /// <param name="pItems">ETCL数据项</param>
        /// <param name="oResult">处理结果详情</param>
        /// <param name="pTran">数据库事务</param>
        /// <returns>成功的行数</returns>
        protected override int Load(IETCLDataItem[] pItems, out IETCLResultItem[] oResult, IDbTransaction pTran)
        {
            oResult = null;
            CommonExcelDataItem[] dataItems = pItems.Cast<CommonExcelDataItem>().ToArray();
            if (dataItems == null || dataItems.Length == 0)
            {
                return 0;
            }
            int rowCount = 0;
            List<string> tableName = new List<string>();
            //将pItems按表处理
            foreach (var item in dataItems)
            {
                if (tableName.Contains(item.Table.TableName))
                    continue;
                tableName.Add(item.Table.TableName);
                if (item.Table.ReferenceOnly.HasValue && item.Table.ReferenceOnly.Value == 1)
                    continue; 
                foreach (var loaderItem in item.Table.Loaders)
                {
                    if (!loaderItem.Process(item.Table.Entities.ToArray(), this.CurrentUserInfo, out oResult,pTran))
                        return -1;
                    rowCount += item.Table.Entities.Count;
                }
            }
            return rowCount;
        }

        #region 生成模板
        /// <summary>
        /// 根据ETCLConfig.xml配置导出模板
        /// </summary>
        /// <returns>TRUE：导出成功，FALSE：导出失败。</returns>
        public Workbook ExportTemplate()
        {
            return ExportTemplate(this._currentBusinessInfo);
        }
        /// <summary>
        /// 根据ETCLConfig.xml配置导出模板
        /// </summary>
        /// <param name="pETCLBusinessInfo">模板定义信息</param>
        /// <returns>TRUE：导出成功，FALSE：导出失败。</returns>
        public Workbook ExportTemplate(ETCLTemplateInfo pETCLBusinessInfo)
        {
            Aspose.Cells.License lic = new License();
            //lic.SetLicense("Aspose.Total.lic");
            Workbook wb = new Workbook();
            wb.Worksheets.Clear();
            //1.创建使用说明
            this.AddInstructionWorkSheet(wb);
            //2.创建导入数据模板
            foreach (var item in pETCLBusinessInfo.WorkSheets)
            {
                this.AddDataImportingWorkSheet(wb, item.WorkSheetName, item.Columns);
            }
            ////3.Response模板
            //wb.Save(string.Format("Template{0}.xls", this.CurrentUserInfo.ClientID), FileFormatType.Excel97);

            return wb;
        }

        /// <summary>
        /// 添加使用说明工作表
        /// </summary>
        /// <param name="pWorkbook"></param>
        private void AddInstructionWorkSheet(Workbook pWorkbook)
        {
            Worksheet ws = pWorkbook.Worksheets[pWorkbook.Worksheets.Add()];
            ws.Name = "使用说明";
            ws.IsGridlinesVisible = false;

            Cells cells = ws.Cells;
            cells.SetColumnWidth(1, 6.5);
            cells.SetColumnWidth(2, 6.5);
            cells.SetColumnWidth(3, 74);
            //1.标题
            Range title = cells.CreateRange("B2", "D2");
            title.Merge();
            DrawTitleStyle(pWorkbook, title);
            cells["B2"].PutValue("使用说明");
            //2.
            cells.CreateRange("C4", "D4").Merge();
            cells.CreateRange("C5", "D5").Merge();
            cells.CreateRange("C6", "D6").Merge();
            DrawBorderAll(pWorkbook, cells.CreateRange("B2", "D6"));

            cells["B3"].PutValue(1);

            cells["D3"].PutValue("标示此背景色的列为必填项");//标示此背景色的列为必填项；
            cells["B4"].PutValue(2);
            cells["C4"].PutValue("  如果第一行为空行,那么系统将不处理这个工作表.");
            cells["B5"].PutValue(3);
            cells["C5"].PutValue("  如果导入失败,那么系统将在第一列中说明错误原因.导入成功后,系统将自动插入生成的编码列.");
            cells["B6"].PutValue(4);
            cells["C6"].PutValue("  如果导入失败,您只需按错误提示修改数据,然后重新上传数据文件.");

            Range r = ws.Cells.CreateRange("C3", "C3");
            DrawMastFillStyle(pWorkbook, r);
        }

        /// <summary>
        /// 添加数据导入工作表
        /// </summary>
        /// <param name="pWorkbook"></param>
        private void AddDataImportingWorkSheet(Workbook pWorkbook, string pWorkSheetName, List<ETCLColumn> pWorkSheetColumns)
        {
            Worksheet ws = pWorkbook.Worksheets[pWorkbook.Worksheets.Add()];
            //1.设置工作表名
            ws.IsGridlinesVisible = false;
            ws.Cells.StandardWidth = 12;
            ws.Name = pWorkSheetName;
            //2.创建列标题
            if (pWorkSheetColumns != null)
            {
                int shownColumnCount = 0;
                foreach (var columnItem in pWorkSheetColumns)
                {
                    //if (columnItem.IsLogicalField.HasValue && columnItem.IsLogicalField.Value == false && columnItem.Visible.Value)
                    if (columnItem.Visible.Value)
                    {
                        ws.Cells[0, shownColumnCount].PutValue(columnItem.ColumnText);
                        Range r = ws.Cells.CreateRange(0, shownColumnCount, 1, 1);
                        if (columnItem.Nullable.HasValue && !columnItem.Nullable.Value)
                            this.DrawMastFillStyle(pWorkbook, r);
                        else
                            this.DrawTitleStyle(pWorkbook, r);
                        shownColumnCount++;
                    }
                }
                //显示网格线
                ws.IsGridlinesVisible = true;
            }
        }
        /// <summary>
        /// 标题样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="r"></param>
        private void DrawTitleStyle(Workbook workbook, Range r)
        {
            Aspose.Cells.Style s = workbook.Styles[workbook.Styles.Add()];
            Color c = Color.Gray;
            s.ForegroundColor = c;
            s.Pattern = BackgroundType.Solid;
            s.Font.Color = Color.White;
            s.Font.IsBold = true;
            s.HorizontalAlignment = TextAlignmentType.Center;
            StyleFlag styleFlag = new StyleFlag();
            //Specify all attributes
            styleFlag.All = true;
            r.ApplyStyle(s, styleFlag);
        }
        /// <summary>
        /// 边框样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="r"></param>
        private void DrawBorderAll(Workbook workbook, Range r)
        {
            Aspose.Cells.Style s = workbook.Styles[workbook.Styles.Add()];
            Color c = Color.Black;
            s.Borders.SetColor(c);
            s.Borders.SetStyle(CellBorderType.Thin);
            s.Borders.DiagonalStyle = CellBorderType.None;
            s.Pattern = BackgroundType.Solid;
            StyleFlag styleFlag = new StyleFlag();
            styleFlag.Borders = true;
            r.ApplyStyle(s, styleFlag);
        }

        /// <summary>
        /// 必填字段的样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="r"></param>
        private void DrawMastFillStyle(Workbook workbook, Range r)
        {
            Aspose.Cells.Style s = workbook.Styles[workbook.Styles.Add()];
            Color c = Color.Maroon;
            s.ForegroundColor = c;
            s.Pattern = BackgroundType.Solid;
            s.Font.Color = Color.White;
            s.Font.IsBold = true;
            s.HorizontalAlignment = TextAlignmentType.Center;
            StyleFlag styleFlag = new StyleFlag();
            styleFlag.All = true;
            r.ApplyStyle(s, styleFlag);
        }
        #endregion

        /// <summary>
        /// 设置转换器
        /// </summary>
        /// <param name="pWorksheetName">工作簿名称</param>
        /// <param name="pTransformer">待添加的转换器</param>
        public void SetTransformer(string pWorksheetName, ITransformer[] pTransformer)
        {
            this._currentBusinessInfo.GetWorksheetByName(pWorksheetName).Transformers.AddRange(pTransformer);
        }

        /// <summary>
        /// 添加自定义加载器
        /// </summary>
        /// <param name="pTableName">工作簿名称</param>
        /// <param name="pLoader">加载器</param>
        public void SetLoader(string pTableName, ILoader[] pLoader)
        {
            this._currentBusinessInfo.GetTableByName(pTableName).Loaders.AddRange(pLoader);
        }
        /// <summary>
        /// 按工作簿中的列设置检查器
        /// </summary>
        /// <param name="pWorksheetName">工作簿名称</param>
        /// <param name="pColumnCaption">列名（防止中英文的情况，此处不使用文本）</param>
        /// <param name="pChecker">待添加的检查器</param>
        public void SetChecker(string pWorksheetName, string pColumnName, IChecker[] pChecker)
        {
            foreach (var item in this._currentBusinessInfo.WorkSheets)
            {
                if (item.WorkSheetName == pWorksheetName)
                {
                    foreach (var colItem in item.Columns)
                    {
                        if (colItem.ColumnName == pColumnName)
                        {
                            List<IChecker> lstCheckers;
                            if (colItem.Checkers == null)
                            {
                                lstCheckers = new List<IChecker>();
                                colItem.Checkers = lstCheckers;
                            }
                            else
                            {
                                lstCheckers = colItem.Checkers;
                            }
                            lstCheckers.AddRange(pChecker);
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}