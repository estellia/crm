using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Interface;

namespace JIT.Utility.ETCL.DataStructure
{
    /// <summary>
    /// 源数据的列信息
    /// </summary>
    public class ETCLColumn
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName;

        /// <summary>
        /// 显示文本
        /// </summary>
        public string ColumnText;

        ///// <summary>
        ///// 显示英文文本
        ///// </summary>
        //public string ColumnTextEn;

        /// <summary>
        /// 列类型
        /// </summary>
        public string ColumnType;

        /// <summary>
        /// 列顺序(一个WorkSheet中包含多个数据库表中的列，应对这些列进行排序)
        /// </summary>
        public int? ColumnOrder
        {
            get;
            set;
        }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool? Visible;

        /// <summary>
        /// 是否允许空值
        /// </summary>
        public bool? Nullable;

        /// <summary>
        /// 是否允许重复
        /// </summary>
        public bool? Duplicatable;

        /// <summary>
        /// 是否可以进行术语替换
        /// </summary>
        public bool? CanTermReplace { get; set; }

        /// <summary>
        /// 该项是否是逻辑字段
        /// </summary>
        public bool? IsLogicalField;

        /// <summary>
        /// 数值/日期的最大值，文本的最大长度
        /// </summary>
        public string MaxValue;

        /// <summary>
        /// 数值/日期的最小值，文本的最小长度
        /// </summary>
        public string MinValue;

        /// <summary>
        /// 依赖的代码表表名
        /// </summary>
        public string ReferenceTableName;

        /// <summary>
        /// 附加条件（如Options表中的OptionName：OptionName=OrderType 多个条件以逗号分隔）
        /// </summary>
        public string ReferenceAdditionalCondition;
        /// <summary>
        /// 依赖的代码表列名
        /// </summary>
        public string ReferenceColumnName;

        /// <summary>
        /// 依赖的代码表的用于显示的列名
        /// </summary>
        public string ReferenceTextColumnName;

        ///// <summary>
        ///// 依赖的代码表的用于显示的英文列名
        ///// </summary>
        //public string ReferenceTextColumnNameEn;

        /// <summary>
        /// 当前列所需要的检查器
        /// </summary>
        public List<IChecker> Checkers;

        /// <summary>
        /// 当前列的所有可用值及数量
        /// </summary>
        public Dictionary<object,int> ValueCountCache;

        /// <summary>
        /// 当前列的所有可用值
        /// </summary>
        public List<object> ValueCache;
    }
}
