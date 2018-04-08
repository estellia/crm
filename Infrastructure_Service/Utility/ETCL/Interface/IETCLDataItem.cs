/*
 * Author		:Gongxi.Yuan
 * EMail		:Gongxi.Yuan@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/10 13:24:30
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
	/// ETL的数据接口,这是一个空接口,用于表明数据是一个ETL的数据 
	/// </summary>
	public interface IETCLDataItem
	{ 
        /// <summary>
        /// 原始行数据的数据字典
        /// </summary>
		Dictionary<string,string> OriginalRow{get;set;}
	}
}
