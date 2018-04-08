/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:35:06
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

namespace JIT.Utility.DataAccess.Query
{
    /// <summary>
    /// null 判断
    /// </summary>
    public class IsNullCondition : IWhereCondition
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public IsNullCondition()
        {
        }
        #endregion

        #region 属性集

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 是否为null
        /// </summary>
        public bool IsNull { get; set; }

        #endregion

        #region IWhereCondition Members
        /// <summary>
        /// 获取where条件表达式字符串
        /// </summary>
        /// <returns></returns>
        public string GetExpression()
        {
            //参数处理
            if (string.IsNullOrEmpty(this.FieldName))
                throw new ArgumentException("属性FieldName不能为null或空.");
            //
            StringBuilder expression = new StringBuilder();
            //处理字段名
            expression.Append(StringUtils.WrapperSQLServerObject(this.FieldName));
            //
            if (this.IsNull)
            {
                expression.Append(" is null");
            }
            else
            {
                expression.Append(" is not null");
            }
            //
            return expression.ToString();
        }
        #endregion
    }
}
