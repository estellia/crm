/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:35:43
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
    /// Like条件
    /// </summary>
    public class LikeCondition : IWhereCondition
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LikeCondition()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 左边是否有%号
        /// </summary>
        public bool HasLeftFuzzMatching { get; set; }

        /// <summary>
        /// 右边是否有%号
        /// </summary>
        public bool HasRightFuzzMathing { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        #endregion

        #region IWhereCondition Members
        /// <summary>
        /// 获取where条件表达式字符串
        /// </summary>
        /// <returns></returns>
        public string GetExpression()
        {
            //参数检查
            if (string.IsNullOrEmpty(this.FieldName) || string.IsNullOrEmpty(this.Value))
                throw new ArgumentException("属性FieldName和Value都不能为null或空.");
            //
            StringBuilder expression = new StringBuilder();
            //处理字段名
            expression.Append(StringUtils.WrapperSQLServerObject(this.FieldName));
            //
            expression.AppendFormat(" like '");
            //处理值
            if (this.HasLeftFuzzMatching)
            {
                if (!this.Value.StartsWith("%"))
                {
                    expression.Append("%");
                }
            }
            expression.Append(StringUtils.SqlReplace(this.Value));
            if (this.HasRightFuzzMathing)
            {
                if (!this.Value.EndsWith("%"))
                {
                    expression.Append("%");
                }
            }
            expression.Append("'");
            //
            return expression.ToString();
        }
        #endregion
    }
}
