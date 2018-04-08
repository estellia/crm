/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:5/30/2011 2:14:01 PM
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

namespace JIT.Utility.ETCL.Base
{
	/// <summary>
	/// ETL中的异常
	/// <remarks>
	/// <para>注意:</para>
	/// <para>1.ETL中所有自定义异常都通过本类及其子类抛出.</para>
	/// </remarks>
	/// </summary>
	public class ETLException:Exception
	{
		public ETLException(){}
		public ETLException(string pErrorMessage):base(pErrorMessage){}
		public ETLException(string pErrorMessageTemplate,params object[] pTemplateArgs):base(string.Format(pErrorMessageTemplate,pTemplateArgs)){}
		public ETLException(string pErrorMessage,Exception pInnerException):base(pErrorMessage,pInnerException){}
	}
}
