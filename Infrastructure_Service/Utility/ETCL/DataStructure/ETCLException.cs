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

namespace JIT.Utility.ETCL.DataStructure
{
    /// <summary>
    /// ETL中的异常
    /// <remarks>
    /// <para>注意:</para>
    /// <para>1.ETL中所有自定义异常都通过本类及其子类抛出.</para>
    /// </remarks>
    /// </summary>
    public class ETCLException : Exception
    {
        public ETCLException(int code) { this.Code = code; }
        public ETCLException(int code, string pErrorMessage) : base(string.Format("【错误类型】：{0}\r\n【错误明细】：{1}", code, pErrorMessage)) { this.Code = code; }
        public ETCLException(int code, string pErrorMessageTemplate, params object[] pTemplateArgs) : base(string.Format("【错误类型】：{0}\r\n【错误明细】：{1}", code, string.Format(pErrorMessageTemplate, pTemplateArgs))) { this.Code = code; }
        public ETCLException(int code, string pErrorMessage, Exception pInnerException) : base(string.Format("【错误类型】：{0}\r\n【错误明细】：{1}", code, pErrorMessage), pInnerException) { this.Code = code; }

        /// <summary>
        /// 错误类型
        /// <remarks>
        /// <para>100-数据源错误</para>
        /// <para>200-配置文件错误</para>
        /// <para>300-导入文件错误</para>
        /// <para>400-数据类型错误</para>
        /// </remarks>
        /// </summary>
        public int Code { get; set; }
    }
}
