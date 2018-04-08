using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.Utility.ETCL.Base;
using JIT.Utility.ETCL.Interface;

namespace JIT.Utility.ETCL.DataStructure
{
    /// <summary>
    /// 源数据的表信息
    /// </summary>
    public class ETCLTable
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
       
        /// <summary>
        /// 表描述
        /// </summary>
        public string TableDesc { get; set; }
        
        ///// <summary>
        ///// 英文描述
        ///// </summary>
        //public string TableDescEn { get; set; }

        /// <summary>
        /// 数据库表的排序
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// 引用深度，用于Load的时候，按此顺序将表信息插入到数据库
        /// </summary>
        public int? ReferenceDepth { get; set; }

        /// <summary>
        /// 当前表所需的加载器
        /// </summary>
        public List<ILoader> Loaders;

        /// <summary>
        /// 列集合
        /// </summary>
        public List<ETCLColumn> Columns { get; set; }

        /// <summary>
        /// 抽取并转换后的Entity
        /// </summary>
        public List<CommonExcelDataItem> Entities { get; set; }

        /// <summary>
        /// 仅作为参考表(不创建此表的数据。)
        /// </summary>
        public int? ReferenceOnly { get; set; }

        /// <summary>
        /// 主表数据
        /// </summary>
        public bool Master { get; set; }

        /// <summary>
        /// 表中的数据
        /// </summary>
        public DataTable Data { get; set; }

        /// <summary>
        /// 此表的DAO对应的DLL文件名
        /// </summary>
        public string DAOAssemblyName { get; set; }

        /// <summary>
        /// 此表的Entity对应的DLL文件名
        /// </summary>
        public string EntityAssemblyName { get; set; }

        /// <summary>
        /// 此表的DAO名称（命名空间.类名）
        /// </summary>
        public string DAOName { get; set; }

        /// <summary>
        /// 此表的Entity名称（命名空间.类名）
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 所属工作簿
        /// </summary>
        public ETCLWorkSheetInfo Worksheet { get; set; }
    }
}
