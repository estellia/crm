/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:5/30/2011 4:36:19 PM
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

namespace JIT.Utility.ETCL.Interface
{
	/// <summary>
	/// 来自于Excel的ETCL数据
	/// </summary>
    public interface IExcelDataItem : IETCLDataItem
	{
		/// <summary>
		/// 工作簿名称
		/// </summary>
		string SheetName{get;set;}

		/// <summary>
		/// 行索引
		/// </summary>
		int? Index{get;set;}
	}
}
