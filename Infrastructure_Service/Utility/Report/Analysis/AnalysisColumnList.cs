/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/1 18:11:28
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 分析列列表 
    /// </summary>
    public class AnalysisColumnList:IEnumerable<AnalysisColumn>
    {
        #region 构造函数
        /// <summary>
        /// 分析列列表 
        /// </summary>
        public AnalysisColumnList(AnalysisReportTypes pReportType)
        {
            this.InnerList = new List<AnalysisColumn>();
            this.ReportType = pReportType;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 内部的分析列集合对象
        /// </summary>
        private List<AnalysisColumn> InnerList = null;

        /// <summary>
        /// 分析列所属的报表类别
        /// </summary>
        public AnalysisReportTypes ReportType { get; private set; }
        #endregion

        #region 集合操作
        /// <summary>
        /// 集合中的元素个数
        /// </summary>
        public int Count
        {
            get { return this.InnerList.Count; }
        }

        /// <summary>
        /// 添加一个分析列
        /// </summary>
        /// <param name="pColumn">分析列</param>
        public void Add(AnalysisColumn pColumn)
        {
            if(pColumn!=null)
            {
                //检查分析列与报表类型是否匹配
                switch(this.ReportType)
                {
                    case AnalysisReportTypes.MemoryBased:
                        {
                            if (pColumn.ColumnType == AnalysisColumnTypes.SQLMeasure)
                            {
                                throw new NotSupportedException("基于内存的分析报表不支持SQL度量.");
                            }
                        }
                        break;
                    case AnalysisReportTypes.SQLBased:
                        {
                            if (pColumn.ColumnType == AnalysisColumnTypes.MemoryMeasure)
                            {
                                throw new NotSupportedException("基于SQL的分析报表不支持内存度量.");
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
                //检查分析列的ID是否为空或null
                if (string.IsNullOrWhiteSpace(pColumn.ColumnID))
                {
                    throw new ArgumentNullException("pColumn.ColumnID", "分析列的ID不能为null或空.");
                }
                //检查分析列的ID是否重复
                bool isFindRepeat = false;
                var colID =pColumn.ColumnID.ToLower();
                foreach (var col in this.InnerList)
                {
                    if (col.ColumnID.ToLower() == colID)
                    {
                        isFindRepeat = true;
                        break;
                    }
                }
                if (isFindRepeat)
                {
                    throw new ArgumentNullException("pColumn.ColumnID", string.Format("分析列数组中存在相同ID[ID={0}]的分析列.注意分析列ID不区分大小写.",pColumn.ColumnID));
                }
                //检查如果是维度列,是否定义了关联维度
                if (pColumn.ColumnType == AnalysisColumnTypes.Dim)
                {
                    var col = pColumn as DimColumn;
                    if (col.DataColumn ==null)
                    {
                        throw new ArgumentNullException("pColumn.ColumnID", "没有为维度列指定相应的数据列.");
                    }
                    //if (col.DataColumn.RelatedDim == null)
                    //{
                    //    throw new ArgumentNullException("pColumn.ColumnID", "维度列所对应的数据列的关联维度(RelatedDim)属性未设置.");
                    //}
                }
                //检查如果是自定义数据源列,是否指定了自定义数据源
                if (pColumn.ColumnType == AnalysisColumnTypes.CustomizeDataSource)
                {
                    var col = pColumn as CustomizeDataSourceColumn;
                    if (col.DataSource == null)
                    {
                        throw new ArgumentException(string.Format("自定义数据源列[ColumnID={0},ColumnTitle={1}]未指定数据源.", pColumn.ColumnID, pColumn.ColumnTitle));
                    }
                }
                //
                this.InnerList.Add(pColumn);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            this.InnerList.Clear();
        }
        #endregion

        #region 获取所有的活跃的列
        /// <summary>
        /// 获取所有的活跃的列
        /// </summary>
        public AnalysisColumn[] ActiveColumns
        {
            get
            {
                var q = from m in this.InnerList
                        where (m.ColumnType == AnalysisColumnTypes.Dim && ((DimColumn)m).IsPivoted == true)
                            || m.ColumnType == AnalysisColumnTypes.CustomizeCalcaulate
                            || m.ColumnType == AnalysisColumnTypes.MemoryMeasure
                            || m.ColumnType == AnalysisColumnTypes.SQLMeasure
                            || m.ColumnType == AnalysisColumnTypes.CustomizeDataSource
                        select m as AnalysisColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 获取所有可见的列
        /// <summary>
        /// 获取所有可见的列
        /// </summary>
        public AnalysisColumn[] VisiableColumns
        {
            get
            {
                var q = from m in this.InnerList
                        where ((m.ColumnType == AnalysisColumnTypes.Dim && ((DimColumn)m).IsPivoted == true && ((DimColumn)m).IsCRConverted == false)
                            || m.ColumnType == AnalysisColumnTypes.CustomizeCalcaulate
                            || m.ColumnType == AnalysisColumnTypes.MemoryMeasure
                            || m.ColumnType == AnalysisColumnTypes.SQLMeasure
                            || m.ColumnType == AnalysisColumnTypes.CustomizeDataSource)
                            && m.IsHidden ==false
                        select m as AnalysisColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 获取所有的维度列
        /// <summary>
        /// 获取所有的维度列
        /// </summary>
        public DimColumn[] Dims
        {
            get
            {
                var q = from m in this.InnerList
                        where m.ColumnType == AnalysisColumnTypes.Dim
                        select m as DimColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 获取其中所有的活跃的维度列
        /// <summary>
        /// 获取其中所有的活跃的维度列
        /// <remarks>
        /// <para>排除掉了不进行数据透视的维度列</para>
        /// </remarks>
        /// </summary>
        public DimColumn[] ActiveDims
        {
            get
            {
                var q = from m in this.InnerList
                        where m.ColumnType == AnalysisColumnTypes.Dim && ((DimColumn)m).IsPivoted ==true
                        select m as DimColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 获取其中所有需要进行行列转换的维度列
        /// <summary>
        /// 获取其中所有需要进行行列转换的维度列
        /// </summary>
        public DimColumn[] CRConvertedDims
        {
            get
            {
                var q = from m in this.InnerList
                        where m.ColumnType == AnalysisColumnTypes.Dim && ((DimColumn)m).IsPivoted == true && ((DimColumn)m).IsCRConverted == true
                        select m as DimColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 获取其中所有不进行行列转换的维度列
        /// <summary>
        /// 获取其中所有不进行行列转换的维度列
        /// </summary>
        public DimColumn[] NotCRConvertedDims
        {
            get
            {
                var q = from m in this.InnerList
                        where m.ColumnType == AnalysisColumnTypes.Dim && ((DimColumn)m).IsPivoted ==true && ((DimColumn)m).IsCRConverted == false
                        select m as DimColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 获取其中所有的内存度量
        /// <summary>
        /// 获取其中所有的内存度量
        /// </summary>
        public MemoryMeasureColumn[] MemoryMeasures
        {
            get
            {
                var q = from m in this.InnerList
                        where m.ColumnType == AnalysisColumnTypes.MemoryMeasure
                        select m as MemoryMeasureColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 获取其中所有的SQL度量
        /// <summary>
        /// 获取其中所有的SQL度量
        /// </summary>
        public SQLMeasureColumn[] SQLMeasures
        {
            get
            {
                var q = from m in this.InnerList
                        where m.ColumnType == AnalysisColumnTypes.SQLMeasure
                        select m as SQLMeasureColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 获取其中所有的自定义计算列
        /// <summary>
        /// 获取其中所有的自定义计算列
        /// </summary>
        public CustomizeCalculateColumn[] CustomizeCalcaulates
        {
            get
            {
                var q = from m in this.InnerList
                        where m.ColumnType == AnalysisColumnTypes.CustomizeCalcaulate
                        select m as CustomizeCalculateColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 获取其中所有的自定义数据源列
        /// <summary>
        /// 获取其中所有的自定义数据源列
        /// </summary>
        public CustomizeDataSourceColumn[] CustomizeDataSources
        {
            get
            {
                var q = from m in this.InnerList
                        where m.ColumnType == AnalysisColumnTypes.CustomizeDataSource
                        select m as CustomizeDataSourceColumn;
                return q.ToArray();
            }
        }
        #endregion

        #region 根据ID找到分析列
        /// <summary>
        /// 根据ID找到分析列
        /// </summary>
        /// <param name="pColumnID"></param>
        /// <returns></returns>
        public AnalysisColumn FindBy(string pColumnID)
        {
            if (this.InnerList != null)
            {
                return this.InnerList.Where(item => item.ColumnID == pColumnID).FirstOrDefault();
            }
            return null;
        }
        #endregion

        #region IEnumerable<AnalysisColumn> 成员

        public IEnumerator<AnalysisColumn> GetEnumerator()
        {
            return this.InnerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.InnerList.GetEnumerator();
        }

        #endregion
    }
}
