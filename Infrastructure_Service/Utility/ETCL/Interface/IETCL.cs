/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:5/30/2011 1:53:15 PM
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
using System.Data;
using System.Text;
using JIT.Utility.ETCL.Checker;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Checker
{
	/// <summary>
	/// ETL过程接口 
	/// </summary>
	public interface IETCL
    {
        /// <summary>
        /// 执行处理过程
        /// </summary> 
        /// <param name="pSource">数据源(数据源可能会多种多样.它可能是一个文件名、可能是一个工作表对象、可能是一个工作簿)</param>
        /// <param name="pResult">处理结果详情</param>
        /// <returns>是否成功</returns>
        bool Process(object pSource, out ImportingResult pResult);
	}
}
