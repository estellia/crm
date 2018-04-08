/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:5/31/2011 2:56:45 PM
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

namespace JIT.Utility.ETCL.DataStructure
{
	/// <summary>
	/// 检查类别 
	/// </summary>
	public enum OperationType
	{
		/// <summary>
		/// 数据重复性检查
		/// </summary>
		DataRepeatly=1,
		/// <summary>
		/// 外键依赖性检查
		/// </summary>
		ForeignKeyDependence=2,
		/// <summary>
		/// 数据有效性检查(非空值)
		/// </summary>
		DataValidaty=3,
        /// <summary>
        /// 将Entity创建到数据库中
        /// </summary>
        CreateEntity=4,
        /// <summary>
        /// 抽取数据
        /// </summary>
        Extract=5
	}
}
