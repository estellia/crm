﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.DataStructure;
using JIT.Utility.ETCL.Interface;

namespace JIT.Utility.ETCL.DataStructure
{
    /// <summary>
    /// 通用Excel数据项
    /// </summary>
    public class CommonExcelDataItem:IExcelDataItem
    {
        /// <summary>
        /// 工作簿名称
        /// </summary>
        public string SheetName
        {
            get;
            set;
        }

        /// <summary>
        /// 行索引
        /// </summary>
        public int? Index
        {
            get;
            set;
        }

        /// <summary>
        /// 原始行数据的数据字典
        /// </summary>
        public Dictionary<string, string> OriginalRow
        {
            get;
            set;
        }

        /// <summary>
        /// 对象实体
        /// </summary>
        public object Entity { get; set; }

        /// <summary>
        /// 所属ETCL表
        /// </summary>
        public ETCLTable Table { get; set; }
    }
}
