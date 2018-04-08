/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/10/9 15:12:07
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
    /// 直接的表达式 
    /// </summary>
    public class DirectCondition:IWhereCondition
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DirectCondition()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pExpression">表达式</param>
        public DirectCondition(string pExpression)
        {
            this.Expression = pExpression;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 表达式
        /// </summary>
        public string Expression { get; set; }
        #endregion

        #region IWhereCondition 成员
        /// <summary>
        /// 获取where条件表达式字符串
        /// </summary>
        /// <returns></returns>
        public string GetExpression()
        {
            return this.Expression;
        }
        #endregion
    }
}
