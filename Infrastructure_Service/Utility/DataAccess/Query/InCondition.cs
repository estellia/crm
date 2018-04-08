/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:13:45
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

using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.DataAccess.Query
{
    /// <summary>
    /// In条件
    /// <typeparam name="T">T为条件值的数据类型</typeparam>
    /// </summary>
    public class InCondition<T> : IWhereCondition
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public InCondition()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T[] Values { get; set; }
        #endregion

        #region IWhereCondition Members
        /// <summary>
        /// 获取where条件表达式字符串
        /// </summary>
        /// <returns></returns>
        public string GetExpression()
        {
            //参数处理
            if (string.IsNullOrEmpty(this.FieldName) || this.Values == null)
                throw new ArgumentException("属性FieldName和Value都不能为null或空.");
            if (this.Values.Length <= 0)
                return string.Empty;
            //
            StringBuilder expression = new StringBuilder();
            //处理字段名
            expression.Append(StringUtils.WrapperSQLServerObject(this.FieldName));
            //
            expression.AppendFormat(" in (");
            //处理值
            string valueType = typeof(T).ToString();
            switch (valueType)
            {
                case "System.String":
                case "System.Guid":
                    foreach (var value in this.Values)
                    {
                        expression.AppendFormat("'{0}',", StringUtils.SqlReplace(value.ToString()));
                    }
                    break;
                case "System.DateTime":
                case "System.Char":
                    foreach (var value in this.Values)
                    {
                        expression.AppendFormat("'{0}',", value.ToString());
                    }
                    break;
                case "System.Int32":
                case "System.UInt32":
                case "System.Int16":
                case "System.UInt16":
                case "System.Int64":
                case "System.UInt64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                case "System.Byte":
                case "System.SByte":
                    foreach (var value in this.Values)
                    {
                        expression.AppendFormat("{0},", value);
                    }
                    break;
                case "System.Boolean":
                    foreach (var value in this.Values)
                    {
                        expression.AppendFormat("{0}", Convert.ToBoolean(value).ToInt32());
                    }
                    break;
                default:
                    throw new ArgumentException("无法处理的值类型.");
            }
            expression.Remove(expression.Length - 1, 1);
            expression.Append(")");
            //
            return expression.ToString();
        }
        #endregion
    }
}
