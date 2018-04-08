/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:6/15/2011 4:25:07 PM
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
	/// 常用的正则表达式 
	/// </summary>
	public static class RegularExrepssions
	{
		/// <summary>
		/// 用于验证邮件格式合法性的正则表达式
		/// </summary>
		public const string EMail = @"^[_A-Za-z0-9]+(\.[A-Za-z0-9]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9]+)*(\.[A-Za-z]{2,})$";
		
		/// <summary>
		/// 验证手机号格式的正则表达式
		/// </summary>
		public const string MobilePhone = @"^(13|14|15|18)\d{9}$";
		
		/// <summary>
		/// 宽泛的验证身份证号的正则表达式
		/// </summary>
		public const string IDCard = @"^[A-Za-z0-9]{15}|[A-Za-z0-9]{18}$";
		
		/// <summary>
		/// 正整数的正则表达式
		/// </summary>
		public const string UINT	=@"^[0-9]+$";
	}
}
