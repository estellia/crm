using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.Extractor;
using Aspose.Cells;
using JIT.Utility.ETCL.DataStructure;
using System.Drawing;

namespace JIT.Utility.ETCL.Base
{
    public abstract  class BaseExcelETCL : BaseETCL
    {
        /// <summary>
        /// 当前工作表
        /// </summary>
        protected Worksheet CurrentWorkSheet { get; set; }
        /// <summary>
        /// 执行处理
        /// </summary>
        /// <param name="pSource">数据源(数据源可能会多种多样.它可能是一个文件名、可能是一个工作表对象、可能是一个工作簿)</param>
        /// <param name="pResult">导入结果</param>
        /// <returns>是否成功</returns>
        public override bool Process(object pSource, out ImportingResult pResult)
        {//IETCLResultItem[] pResult
            if (pSource == null || (!(pSource is Worksheet)))
                throw new ETCLException(100,"数据源为null或类型不为Aspose.Worksheet.");
            Worksheet sheet = pSource as Worksheet;
            List<IETCLResultItem> opResult = new List<IETCLResultItem>();
            IETCLResultItem[] tempResult;
            //实例化out参数
            pResult = new ImportingResult();
            //保存对当前的工作表的引用
            this.CurrentWorkSheet = sheet;
            bool isPass = true;
            //解析出数据
            DataTable dt = this.Extract(sheet, out tempResult);
            if (tempResult != null)
                opResult.AddRange(tempResult);
            if (dt != null && dt.Rows.Count > 0)
            {
                //数据转换
                IETCLDataItem[] items = this.Transform(dt, out tempResult);
                if (tempResult != null)
                    opResult.AddRange(tempResult);

                if (items == null || items.Length <= 0)
                {
                    isPass = false;
                    //没有数据
                    pResult.IsSuccess = false ;
                    pResult.IsExistsData = true;
                }
                else
                {
                    pResult.IsExistsData = true;
                }
                if (tempResult != null)
                    opResult.AddRange(tempResult);

                //检查数据 
                if (isPass)
                {
                    isPass = this.Check(items, out tempResult);
                    if (tempResult != null)
                    {
                        opResult.AddRange(tempResult);
                        List<IETCLResultItem> results = new List<IETCLResultItem>();
                        if(pResult.CheckResults!=null && pResult.CheckResults.Length>0)
                            results.AddRange(pResult.CheckResults);
                        if(tempResult!=null&& tempResult.Length>0)
                            results.AddRange(tempResult);
                        pResult.CheckResults =results.ToArray();
                    }
                }
                //写入数据库
                if (isPass)
                {
                    pResult.ImportedRowCount = this.Load(items, out tempResult,null);
                    if (tempResult != null)
                        opResult.AddRange(tempResult);

                }
                //错误信息回写到XLS文件
                insertCheckResultIntoWorkSheet(sheet, opResult.ToArray());
                if (isPass)
                {
                    sheet.Cells.InsertRow(0);
                } 
                //设置为导入成功
                pResult.IsSuccess = isPass;
            }
            else
            {
                //没有数据
                pResult.IsSuccess = true;
                pResult.IsExistsData = false;
                return pResult.IsSuccess;
            }
            //
            return pResult.IsSuccess; 
        }

        /// <summary>
        /// 将XLS中的数据解析为DataTable
        /// <remarks>
        /// <para>解析规则:</para>
        /// <para>1.默认第一行为列标题,之后的所有行都为数据行</para>
        /// </remarks>
        /// </summary>
        /// <param name="pWorkSheet">工作簿对象</param>
        /// <param name="pResult">抽取结果详情</param>
        /// <returns>抽取出的DataTable</returns>
        public override System.Data.DataTable Extract(object pSource, out IETCLResultItem[] pResult)
        {
            DefaultExtractor defaultExtractor = new DefaultExtractor();
            object dtData;
            bool bResult = defaultExtractor.Process(pSource, out dtData, out pResult);
            if (bResult)
            {
                this.OriginalData = (DataTable)dtData;
                return this.OriginalData;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 对数据进行格式转换并将DataRow转换成强类型的IExcelDataItem实例
        /// </summary>
        /// <param name="pSourceDataTable">从源取出的直接数据</param>
        /// <param name="pResult">转换处理结果详情</param>
        /// <returns>转换出的ETCL数据项</returns>
        protected override abstract IETCLDataItem[] Transform(System.Data.DataTable pSourceDataTable, out IETCLResultItem[] pResult);
       
        /// <summary>
        /// 对导入的数据执行检查
        /// <remarks>
        /// <para>在数据检查过程中可能需要做的事情有:</para>
        /// <para>1.数据项的有效性检查.有效性包含:数据的类型有效、数据项的值范围合法</para>
        /// <para>2.数据的重复性检查.重复性检查包含导入的数据源中就重复,还有一种是和数据库中的重复</para>
        /// <para>3.外键依赖性检查.根据编码检查外键记录是否存在并且唯一.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pDataItems">数据</param>
        /// <param name="oResults">如果检查不通过时,返回检查结果数组</param>
        /// <returns>是否检查通过</returns>
        protected override abstract bool Check(IETCLDataItem[]  pDataItems, out IETCLResultItem[] oResults);

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="pItems">需要导入的数据条目数组</param>
        /// <param name="pResult">加载结果详情</param>
        /// <param name="pTran">数据库事务</param>
        /// <returns>被加载的记录数</returns>
        protected override abstract int Load(IETCLDataItem[] pItems, out IETCLResultItem[] pResult, IDbTransaction pTran);
        #region 将检查结果添加到Excel中
        /// <summary>
        /// 将检查结果添加到Excel中
        /// </summary>
        /// <param name="pWorkSheet"></param>
        /// <param name="pCheckResults"></param>
        private void insertCheckResultIntoWorkSheet(Worksheet pWorkSheet, IETCLResultItem[] pCheckResults)
        {
            if (pWorkSheet != null && pCheckResults != null && pCheckResults.Length > 0)
            {
                //1.如果没有添加检查结果列则增加
                if (!string.IsNullOrEmpty(pWorkSheet.Cells[0, 0].StringValue))
                {
                    pWorkSheet.Cells.InsertColumn(0);
                    pWorkSheet.Cells.SetColumnWidthPixel(0, 300);
                }
                //2.清除所有已经存在的批注
                pWorkSheet.ClearComments();
                var rowHeight = pWorkSheet.Cells.GetRowHeight(0);
                foreach (var item in pCheckResults)
                {
                    Cell resultCell = pWorkSheet.Cells[item.RowIndex + 1, 0];
                    Style style = resultCell.GetStyle();
                    string message;
                    switch (item.ServerityLevel)
                    {
                        case ServerityLevel.Warning:
                            message = "警告信息:";
                            break;
                        case ServerityLevel.Info:
                            message = "一般信息:";
                            break;
                        case ServerityLevel.Error:
                            message = "错误信息:";
                            break;
                        default:
                            message = "其它信息:";
                            break;
                    }
                    message += item.Message;
                    if (resultCell.Value != null && !string.IsNullOrWhiteSpace(resultCell.Value.ToString()))
                        message = resultCell.Value.ToString() + System.Environment.NewLine + message;
                    resultCell.PutValue(message);
                    //设置行高
                    var rowCount = System.Text.RegularExpressions.Regex.Matches(message, System.Environment.NewLine).Count + 1;
                    if (rowCount > 1)
                    {
                        pWorkSheet.Cells.SetRowHeight(item.RowIndex + 1, rowHeight * rowCount);
                        style.IsTextWrapped = true;
                    }
                    style.ForegroundColor = Color.Red;
                    style.Pattern = BackgroundType.Solid;
                    style.Font.Color = Color.White;
                    style.Font.IsBold = true;
                    style.HorizontalAlignment = TextAlignmentType.Left;
                    resultCell.SetStyle(style);
                }
            }
        }
        #endregion

    }
}
