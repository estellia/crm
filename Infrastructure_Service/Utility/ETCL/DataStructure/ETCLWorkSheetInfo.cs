using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.Utility.ETCL.Interface;

namespace JIT.Utility.ETCL.DataStructure
{
    /// <summary>
    /// 通用ETCL工作簿
    /// </summary>
    public class ETCLWorkSheetInfo
    {
        /// <summary>
        /// 业务功能名
        /// </summary>
        public string WorkSheetName { get; set; }

        /// <summary>
        /// 业务功能名(英文)
        /// </summary>
        public string WorkSheetNameEn { get; set; }

        /// <summary>
        /// 业务功能描述
        /// </summary>
        public string WorkSheetDescription { get; set; }

        /// <summary>
        /// 工作表的排序
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// 从数据源抽取到DataTable的原始数据
        /// </summary>
        public DataTable OriginalData;

        /// <summary>
        /// 此业务所设计到的所有表集合
        /// </summary>
        public List<ETCLTable> Tables { get; set; }

        /// <summary>
        /// 当前工作簿中的所有列
        /// </summary>
        public List<ETCLColumn> Columns
        {
            get
            {
                List<ETCLColumn> result = new List<ETCLColumn>();
                foreach (var item in this.Tables)
                {
                    result.AddRange(item.Columns);
                } 
                var q =
                    from e in result
                    orderby e.ColumnOrder
                    select e;
                return q.ToList();
            }
        }

        /// <summary>
        /// 转换器
        /// </summary>
        public List<ITransformer> Transformers { get; set; }
        /// <summary>
        /// 根据名称获取ETCL表
        /// </summary>
        /// <param name="pTableName">表名称</param>
        /// <returns>ETCL表</returns>
        public ETCLTable GetTableByName(string pTableName)
        {
            foreach (var tableItem in this.Tables)
            {
                if (tableItem.TableName == pTableName)
                    return tableItem;
            }
            return null;
        }
        /// <summary>
        /// 根据名称获取ETCL列
        /// </summary>
        /// <param name="pTableName">表名称</param>
        /// <returns>ETCL表</returns>
        public ETCLColumn GetColumnByText(string pColumnText)
        {
            foreach (var columnItem in this.Columns)
            {
                if (columnItem.ColumnText == pColumnText)
                    return columnItem;
            }
            return null;
        }
    }
}
