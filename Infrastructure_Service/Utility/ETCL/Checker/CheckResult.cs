/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:5/31/2011 2:46:24 PM
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


namespace JIT.Utility.ETCL.DataChecking
{
	/// <summary>
	/// 对ETL数据进行检查的检查结果 
	/// </summary>
	public class CheckResult
	{
		#region 构造函数
		/// <summary>
		/// 构造函数 
		/// </summary>
		public CheckResult()
		{
		}
		#endregion
		
		#region 属性集
		/// <summary>
		/// 检查类型
		/// </summary>
		public CheckType CheckType{get;set;}
		/// <summary>
		/// 结果代码(0-99为成功，100以上表示失败,500以上表示异常)
		/// </summary>
		public int ResultCode{ get; set; }
        /// <summary>
        /// 数据源表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 行序号
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 列序号
        /// </summary>
        public int ColumnOrder { get; set; }
		/// <summary>
		/// 信息
		/// </summary>
		public string Message{get;set;}
		#endregion
	}
}
