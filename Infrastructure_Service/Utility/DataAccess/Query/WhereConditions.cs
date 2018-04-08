/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/10/30 10:19:19
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
    /// 过滤条件的工具类 
    /// </summary>
    public static class WhereConditions
    {
        /// <summary>
        /// 生成Where子句
        /// </summary>
        /// <param name="pWhereConditions">Where条件</param>
        /// <returns></returns>
        public static string GenerateWhereSentence(IWhereCondition[] pWhereConditions)
        {
            if (pWhereConditions == null || pWhereConditions.Length <= 0)
                return string.Empty;
            StringBuilder sentence = new StringBuilder();
            foreach (var item in pWhereConditions)
            {
                var expression = item.GetExpression();
                if (!string.IsNullOrEmpty(expression))
                {
                    sentence.AppendFormat(" and {0}", expression);
                }
            }
            return sentence.ToString();
        }
    }
}
