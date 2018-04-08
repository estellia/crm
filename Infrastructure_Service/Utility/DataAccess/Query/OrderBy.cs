/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:36:23
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

using JIT.Utility;

namespace JIT.Utility.DataAccess.Query
{
    /// <summary>
    /// OrderBy 
    /// </summary>
    public class OrderBy
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        public OrderByDirections Direction { get; set; }

        /// <summary>
        /// 生成Order by子句
        /// </summary>
        /// <param name="pOrderBys">OrderBy条件</param>
        /// <returns></returns>
        public static string GenerateOrderBySentence(OrderBy[] pOrderBys)
        {
            if (pOrderBys == null || pOrderBys.Length <= 0)
                return string.Empty;
            StringBuilder sentence = new StringBuilder();
            sentence.AppendFormat(" order by ");
            foreach (var item in pOrderBys)
            {
                if (string.IsNullOrEmpty(item.FieldName))
                {
                    throw new ArgumentException("OrderBy条件中未指定字段名.");
                }
                sentence.AppendFormat("{0} {1},",StringUtils.WrapperSQLServerObject(item.FieldName),item.Direction == OrderByDirections.Asc?"asc":"desc");
            }
            sentence.Remove(sentence.Length - 1, 1);
            return sentence.ToString();
        }
    }
}
