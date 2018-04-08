/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:6/24/2011 10:27:48 AM
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
using JIT.Utility.ETCL.Interface;

namespace JIT.Utility.ETCL.DataStructure
{
	/// <summary>
	/// 导入结果 
	/// </summary>
	public class ImportingResult
	{
		#region 构造函数
		/// <summary>
		/// 构造函数 
		/// </summary>
		public ImportingResult()
		{
		}
		#endregion
		
		/// <summary>
		/// 是否导入成功
		/// </summary>
		public bool IsSuccess{get;set;}
		
		/// <summary>
		/// 对导入数据检查的检查结果数组
		/// </summary>
        public IETCLResultItem[] CheckResults { get; set; }
		
		/// <summary>
		/// 是否有数据
		/// </summary>
		public bool IsExistsData{get;set;}
		
		/// <summary>
		/// 被导入的数据总数
		/// </summary>
		public int ImportedRowCount{get;set;}
	}
}
