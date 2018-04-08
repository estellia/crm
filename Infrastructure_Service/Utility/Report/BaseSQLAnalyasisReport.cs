/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/17 13:04:30
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
using System.Linq;
using System.Data;
using System.Text;

using JIT.Utility;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Report.Analysis;

namespace JIT.Utility.Report
{
    /// <summary>
    /// 基于SQL的分析报表基类
    /// <remarks>
    /// <para>度量的计算是放在SQL中,通过SQL语句计算得来.</para>
    /// </remarks>
    /// </summary>
    public abstract class BaseSQLAnalyasisReport:BaseAnalysisReport
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pSQLHelper">执行数据库SQL语句的助手</param>
        public BaseSQLAnalyasisReport(ISQLHelper pSQLHelper)
        {
            this.CurrentSQLHelper = pSQLHelper;
        }
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseSQLAnalyasisReport()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 当前的数据访问助手
        /// </summary>
        protected ISQLHelper CurrentSQLHelper { get; set; }
        #endregion

        #region BaseAnalysisReport抽象成员的实现

        /// <summary>
        /// 执行分组计算
        /// <remarks>
        /// <para>分组计算的主要工作内容有：</para>
        /// <para>1.读取事实数据.</para>
        /// <para>2.对事实数据进行分组,并依次进行度量的计算</para>
        /// </remarks>
        /// </summary>
        /// <param name="pWheres">报表数据的过滤条件</param>
        /// <param name="pOrderBys">报表数据的排序条件</param>
        /// <returns>分组聚合后的数据集,数据集中的所有列的列名为对应的事实数据列的ColumnID</returns>
        protected override DataTable ProcessGroupingCalculation(IWhereCondition[] pWheres, OrderBy[] pOrderBys)
        {
            //
            this.Validate();
            //
            StringBuilder sql = new StringBuilder();
            List<string> tempTables = new List<string>();
            //将明细事实数据插入到临时表中
            sql.AppendFormat("--将明细事实数据插入到临时表中{0}", Environment.NewLine);
            sql.AppendFormat("select * into #temp_main{0}", Environment.NewLine);
            if (!string.IsNullOrWhiteSpace(this.FactDataResultAliasName))
            {
                sql.AppendFormat("from ({0}) {1}{2}", this.FactDataSQL, this.FactDataResultAliasName, Environment.NewLine);
                sql.AppendFormat("where 1=1 {0}", Environment.NewLine);
                sql.AppendFormat("{0}{1}", WhereConditions.GenerateWhereSentence(pWheres), Environment.NewLine);
            }
            else
            {
                sql.AppendFormat("from ({0}", Environment.NewLine);
                sql.AppendFormat("{0} {1} {2}", this.FactDataSQL, WhereConditions.GenerateWhereSentence(pWheres),Environment.NewLine);
                sql.AppendFormat(") a {0}",Environment.NewLine);
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat("{0};{1}", OrderBy.GenerateOrderBySentence(pOrderBys), Environment.NewLine);
            }
            tempTables.Add("#temp_main");
            //组织分组语句
            StringBuilder groupby = new StringBuilder();
            StringBuilder groupbyFields = new StringBuilder();
            StringBuilder dimSelection = new StringBuilder();
            var dims = this.CurrentAnalysisMode.DrillRouting.CurrentSection.Columns.ActiveDims;
            if (dims != null && dims.Length > 0)
            {
                groupby.AppendFormat("group by ");
                bool isFirst = true;
                foreach (var dim in dims)
                {
                    if (!isFirst)
                    {
                        groupby.Append(",");
                        groupbyFields.Append(",");
                        dimSelection.Append(",");
                    }
                    else
                    {
                        isFirst = false;
                    }
                    var columnName = this.GetColumnNameBy(dim.DataColumn.PropertyName);
                    var aliasName = this.GetColumnNameBy(dim.ColumnID);
                    groupby.Append(columnName);
                    groupbyFields.AppendFormat("{0} as {1}", columnName, aliasName);
                    dimSelection.AppendFormat("$dim_table_name$.{0}", aliasName);
                }
            }
            //执行SQL度量的计算,并将计算结果插入到临时表中
            var measures = this.CurrentAnalysisMode.DrillRouting.CurrentSection.Columns.SQLMeasures;
            var hasGroupBy = dims.Length > 0;
            if (measures != null && measures.Length > 0)
            {
                foreach (var m in measures)
                {
                    var expression = m.ExpressionTemplate.Replace("$column$", m.DataColumn.PropertyName);
                    var aliasName = StringUtils.WrapperSQLServerObject(m.ColumnID);
                    var measureTablename = string.Format("#temp_measure_{0}",m.ColumnID);
                    expression = string.Format("{0} as {1}", expression, aliasName);
                    var wheres = WhereConditions.GenerateWhereSentence(m.Wheres);
                    if (!string.IsNullOrWhiteSpace(wheres))
                    {
                        wheres = wheres.Replace("$column$", m.DataColumn.PropertyName);
                    }
                    sql.AppendFormat("--对度量[{0}]进行计算,并将计算结果插入到临时表中{1}",m.ColumnTitle,Environment.NewLine);
                    sql.AppendFormat("select {0}{1}{2} into {3}{4}"
                        ,groupbyFields.ToString()
                        ,hasGroupBy?",":string.Empty
                        , expression
                        , measureTablename
                        , Environment.NewLine);
                    sql.AppendFormat("from #temp_main{0}",Environment.NewLine);
                    sql.AppendFormat("where 1=1{0}", Environment.NewLine);
                    if (!string.IsNullOrWhiteSpace(wheres))
                    {
                        sql.AppendFormat("{0}{1}",wheres,Environment.NewLine);
                    }
                    sql.AppendFormat("{0};{1}",groupby.ToString(),Environment.NewLine);
                    //
                    tempTables.Add(measureTablename);
                }
            }
            //将所有的度量计算结果合并到结果集中
            sql.AppendFormat("--将所有的度量计算结果合并到结果集中{0}", Environment.NewLine);
            if (hasGroupBy) //有维度列,因此需要分组计算
            {
                StringBuilder resultSelection = new StringBuilder();
                resultSelection.Append(dimSelection.ToString().Replace("$dim_table_name$", "#temp_goupings"));
                foreach (var m in measures)
                {
                    resultSelection.AppendFormat(",{0}",StringUtils.WrapperSQLServerObject(m.ColumnID));
                }
                sql.AppendFormat("--先取出所有的分组值{0}", Environment.NewLine);
                sql.AppendFormat("select {0} into #temp_goupings from #temp_main {1}{2}", groupbyFields.ToString(), groupby.ToString(), Environment.NewLine);
                //
                tempTables.Add("#temp_goupings");
                //
                StringBuilder join = new StringBuilder();
                var isFirst =true;
                foreach (var dim in dims)
                {
                    var columnName = this.GetColumnNameBy(dim.ColumnID);
                    if (!isFirst)
                    {
                        join.Append(" and");
                    }
                    else
                    {
                        isFirst = false;
                    }
                    join.AppendFormat(" #temp_goupings.{0}=$measure_table_name$.{0}", columnName);
                }
                //
                sql.AppendFormat("--将分组值与所有的度量结果合并{0}", Environment.NewLine);
                sql.AppendFormat("select {0} from #temp_goupings {1}",resultSelection.ToString(),Environment.NewLine);
                foreach (var m in measures)
                {
                    string measureTableName =string.Format("#temp_measure_{0}",m.ColumnID);
                    var joinSentence =join.ToString().Replace("$measure_table_name$",measureTableName);
                    sql.AppendFormat(" left join {0} on {1} {2}", measureTableName, joinSentence,Environment.NewLine);
                }
            }
            else//没有维度列,所有的度量直接合并成一条记录
            {
                sql.AppendFormat("--没有维度列,所有的度量直接合并成一条记录{0}", Environment.NewLine);
                StringBuilder selection = new StringBuilder();
                var isFirst = true;
                foreach (var m in measures)
                {
                    var varName = string.Format("@v_{0}", m.ColumnID);
                    var aliasName = StringUtils.WrapperSQLServerObject(m.ColumnID);
                    string measureTableName = string.Format("#temp_measure_{0}", m.ColumnID);
                    sql.AppendFormat("declare {0} sql_variant;{1}", varName,Environment.NewLine);
                    sql.AppendFormat("set {0}=cast((select top 1 * from {1}) as sql_variant);{2}", varName, measureTableName, Environment.NewLine);
                    if (!isFirst)
                    {
                        selection.AppendFormat(",");
                    }
                    else
                    {
                        isFirst = false;
                    }
                    selection.AppendFormat("{0} as {1}", varName, aliasName);
                }
                sql.AppendFormat("select {0};{1}",selection.ToString(),Environment.NewLine);
            }
            //清理资源
            sql.AppendFormat("--清理资源{0}", Environment.NewLine);
            foreach (var t in tempTables)
            {
                sql.AppendFormat("drop table {0};{1}",t,Environment.NewLine);
            }
            //执行
            return this.CurrentSQLHelper.ExecuteDataset(sql.ToString()).Tables[0];
        }

        /// <summary>
        /// 执行钻取后的分组计算,钻取只是多一个过滤条件
        /// </summary>
        /// <param name="pWheres">报表数据的过滤条件</param>
        /// <param name="pOrderBys">报表数据的排序条件</param>
        /// <param name="pDrilledColumn">用户选择钻入的维度列</param>
        /// <param name="pDimValue">用户选择钻入的维度项的值</param>
        /// <param name="pBringFromDrilling">从用户的钻取带入的其他查询条件</param>
        /// <returns>分组聚合后的数据</returns>
        protected override DataTable ProcessGroupingCalculationByDrilled(IWhereCondition[] pWheres, OrderBy[] pOrderBys, DimColumn pDrilledColumn, string pDimValue, IWhereCondition[] pBringFromDrilling)
        {
            //钻取带来的筛选条件的处理
            var where = this.GenerateWhereCondition4DrillIn(pDrilledColumn, pDimValue);
            List<IWhereCondition> wheresByDrilling = new List<IWhereCondition>();   //由于钻取带入的条件
            wheresByDrilling.Add(where);
            if (pBringFromDrilling != null)
                wheresByDrilling.AddRange(pBringFromDrilling);
            //获取当前的筛选条件
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (wheresByDrilling != null)
                wheres.AddRange(wheresByDrilling.ToArray());
            if (pWheres != null)
                wheres.AddRange(pWheres);
            if (this.ConditionsByDrilling != null)
                wheres.AddRange(this.ConditionsByDrilling.SelectMany(item => item.Value).ToArray());
            //将钻取带来的筛选条件保存
            if (this.ConditionsByDrilling == null)
                this.ConditionsByDrilling = new Dictionary<string, IWhereCondition[]>();
            this.ConditionsByDrilling.Add(this.CurrentAnalysisMode.DrillRouting.CurrentSection.SectionID, wheresByDrilling.ToArray());
            //执行分组计算
            return this.ProcessGroupingCalculation(wheres.ToArray(), pOrderBys);
        }

        /// <summary>
        /// 报表的类型
        /// </summary>
        public override AnalysisReportTypes ReportType
        {
            get { return AnalysisReportTypes.SQLBased; }
        }
        #endregion

        #region 抽象成员
        /// <summary>
        /// 获取事实明细数据的SQL语句
        /// <remarks>
        /// <para>1.不能是表名，必须是一个select语句</para>
        /// </remarks>
        /// </summary>
        protected abstract string FactDataSQL { get;  }

        /// <summary>
        /// 事实明细数据结果的表别名，用于过滤数据时与过滤条件匹配
        /// </summary>
        protected abstract string FactDataResultAliasName { get;}
        #endregion

        #region 虚方法
        /// <summary>
        /// 为钻入生成where条件
        /// </summary>
        /// <param name="pDrilledColumn">用户选择钻入的维度列</param>
        /// <param name="pDimValue">钻入的维度项的值</param>
        /// <returns></returns>
        protected virtual IWhereCondition GenerateWhereCondition4DrillIn(DimColumn pDrilledColumn, string pDimValue)
        {
            return new EqualsCondition() { FieldName=pDrilledColumn.DataColumn.PropertyName, Value=pDimValue };
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 验证
        /// </summary>
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.FactDataSQL))
            {
                throw new Exception("FactDataSQL属性不能为空或null.");
            }
            if (string.IsNullOrWhiteSpace(this.FactDataResultAliasName))
            {
                if (this.FactDataSQL.IndexOf("where 1=1") < 0)
                {
                    throw new Exception("事实表别名(属性FactDataResultAliasName)为空或null时.事实数据的SQL语句(属性FactDataSQL)中必须包含where 1=1.");
                }
            }
        }

        /// <summary>
        /// 根据属性名获取字段名,字段名移除掉表别名部分
        /// </summary>
        /// <param name="pPropertyName"></param>
        /// <returns></returns>
        private string GetColumnNameBy(string pPropertyName)
        {
            if (string.IsNullOrWhiteSpace(pPropertyName))
            {
                return pPropertyName;
            }
            else
            {
                var columnName = StringUtils.WrapperSQLServerObject(pPropertyName);
                if (columnName.IndexOf(".") >= 0)
                {
                    string[] temp = columnName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    columnName = temp[temp.Length - 1];
                }
                return columnName;
            }
        }
        #endregion
    }
}
