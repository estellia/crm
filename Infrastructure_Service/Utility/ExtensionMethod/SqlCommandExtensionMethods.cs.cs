/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/9 17:28:50
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// SqlCommand的扩展方法
    /// </summary>
    public static class SqlCommandExtensionMethods
    {
        /// <summary>
        /// 扩展方法:模拟生成与SqlCommand执行效果相同的T-SQL语句
        /// <remarks>
        /// <para>请注意:</para>
        /// <para>1本质上SqlCommand并不会生成一个SQL或T-SQL,并丢给数据库去执行.因此,本方法仅是分析SqlCommand的属性并根据这些属性模拟出一个可执行的T-SQL语句以便于查错和调试.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pSqlCommand"></param>
        /// <returns></returns>
        public static string GenerateTSQLText(this SqlCommand pSqlCommand)
        {
            if (pSqlCommand == null)
                return null;
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat("{0}use [{1}];{0}", Environment.NewLine, pSqlCommand.Connection.Database);
                switch (pSqlCommand.CommandType)
                {
                    case CommandType.StoredProcedure:
                        //1.生成参数定义与赋值语句
                        if (pSqlCommand.Parameters != null && pSqlCommand.Parameters.Count > 0)
                        {
                            foreach (SqlParameter para in pSqlCommand.Parameters)
                            {
                                if ((para.Direction == ParameterDirection.InputOutput) || (para.Direction == ParameterDirection.Output))
                                {
                                    sql.AppendFormat("declare {1} {2}={3};{0}"
                                        , Environment.NewLine
                                        , para.ParameterName
                                        , para.SqlDbType
                                        , para.Direction == ParameterDirection.Output ? "null" : para.ToSQLExpressionFormat()
                                    );
                                }
                            }
                        }
                        //2.生成执行存储过程的语句
                        sql.AppendFormat("exec [{0}] ", pSqlCommand.CommandText);
                        if (pSqlCommand.Parameters != null && pSqlCommand.Parameters.Count > 0)
                        {
                            bool isFirst = true;
                            foreach (SqlParameter para in pSqlCommand.Parameters)
                            {
                                if (para.Direction != ParameterDirection.ReturnValue)
                                {
                                    if (isFirst)
                                    {
                                        sql.AppendFormat("	");
                                        isFirst = false;
                                    }
                                    else
                                    {
                                        sql.AppendFormat(",");
                                    }
                                    if (para.Direction == ParameterDirection.Input)
                                        sql.AppendFormat("{0}={1}", para.ParameterName, para.ToSQLExpressionFormat());
                                    else

                                        sql.AppendFormat("{0}={1} output", para.ParameterName, para.ParameterName);
                                }
                            }
                        }
                        sql.AppendFormat(";{0}", Environment.NewLine);
                        //3.生成获取输出参数的语句
                        foreach (SqlParameter para in pSqlCommand.Parameters)
                        {
                            if ((para.Direction == ParameterDirection.InputOutput) || (para.Direction == ParameterDirection.Output))
                            {
                                sql.AppendFormat("print convert(nvarchar,{1});{0}", Environment.NewLine, para.ParameterName);
                            }
                        }

                        break;
                    case CommandType.Text:
                        sql.Append(pSqlCommand.CommandText);
                        if (pSqlCommand.Parameters != null && pSqlCommand.Parameters.Count > 0)
                        {
                            foreach (SqlParameter para in pSqlCommand.Parameters)
                            {
                                sql.Replace(para.ParameterName, para.ToSQLExpressionFormat());
                            }
                        }
                        break;
                    case CommandType.TableDirect:
                        break;
                }
            }
            catch (Exception ex)
            {
                return string.Format("生成SQL语句出错:{0}{1}{2}", ex.Message,Environment.NewLine,ex.StackTrace);
            }
            return sql.ToString();
        }
        /// <summary>
        /// 将SqlParameter转换成T-SQL/SQL语句中的格式
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static String ToSQLExpressionFormat(this SqlParameter pSqlParameter)
        {
            String retval = "";

            switch (pSqlParameter.SqlDbType)
            {
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.Time:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                    if (pSqlParameter.Value != DBNull.Value && pSqlParameter.Value != null)
                        retval = string.Format("'{0}'", pSqlParameter.Value.ToString().Replace("'", "''"));
                    else
                        retval = "null";
                    break;
                case SqlDbType.Bit:
                    if (pSqlParameter.Value != DBNull.Value && pSqlParameter.Value != null)
                        retval = Convert.ToBoolean(pSqlParameter.Value) ? "1" : "0";
                    else
                        retval = "null";
                    break;

                default:
                    if (pSqlParameter.Value != DBNull.Value && pSqlParameter.Value != null)
                        retval = pSqlParameter.Value.ToString().Replace("'", "''");
                    else
                        retval = "null";
                    break;
            }

            return retval;
        }
    }
}
