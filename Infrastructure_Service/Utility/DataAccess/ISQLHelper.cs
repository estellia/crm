/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/14 13:27:27
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

using JIT.Utility;

namespace JIT.Utility.DataAccess
{
    /// <summary>
    /// SQLHelper的接口 
    /// </summary>
    public interface ISQLHelper
    {
        #region Events
        /// <summary>
        /// 命令执行完毕后触发该事件
        /// </summary>
        event EventHandler<SqlCommandExecutionEventArgs> OnExecuted;

        /// <summary>
        /// 清除指定事件的处理程序
        /// </summary>
        /// <param name="pEventNames">事件名称,大小写敏感</param>
        void ClearEvents(string[] pEventNames);

        /// <summary>
        /// 清除所有事件的处理程序
        /// </summary>
        void ClearEvents();
        #endregion

        #region Current User Info
        /// <summary>
        /// 用户信息
        /// </summary>
        BasicUserInfo CurrentUserInfo { get; set; }
        #endregion

        #region  Create Transaction
        /// <summary>
        /// 创建一个事务
        /// </summary>
        /// <returns></returns>
        SqlTransaction CreateTransaction();
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 在指定的事务下执行一个SQL命令,该命令不返回结果集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>受SQL命令影响的数据行数</returns>
        int ExecuteNonQuery(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 在指定的事务下执行一个SQL命令(不带SQL命令参数),该命令不返回结果集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>受SQL命令影响的数据行数</returns>
        int ExecuteNonQuery(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令,该命令不返回结果集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>受SQL命令影响的数据行数</returns>
        int ExecuteNonQuery(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数),该命令不返回结果集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>受SQL命令影响的数据行数</returns>
        int ExecuteNonQuery(CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数,命令类别为SQL语句),该命令不返回结果集
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>受SQL命令影响的数据行数</returns>
        int ExecuteNonQuery(string pCommandText);
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令并返回一个向前的只读数据读取器
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>向前的只读数据读取器</returns>
        SqlDataReader ExecuteReader(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令(不带SQL命令参数)并返回一个向前的只读数据读取器
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>向前的只读数据读取器</returns>
        SqlDataReader ExecuteReader(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令并返回一个向前的只读数据读取器
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>向前的只读数据读取器</returns>
        SqlDataReader ExecuteReader(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数)并返回一个向前的只读数据读取器
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>向前的只读数据读取器</returns>
        SqlDataReader ExecuteReader(CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数，命令类别为SQL语句)并返回一个向前的只读数据读取器
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>向前的只读数据读取器</returns>
        SqlDataReader ExecuteReader(string pCommandText);
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令，该命令返回一个 1x1 的结果集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>1x1 的结果集中所包含的对象</returns>
        object ExecuteScalar(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令(不带SQL命令参数)，该命令返回一个 1x1 的结果集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>1x1 的结果集中所包含的对象</returns>
        object ExecuteScalar(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令，该命令返回一个 1x1 的结果集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>1x1 的结果集中所包含的对象</returns>
        object ExecuteScalar(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数)，该命令返回一个 1x1 的结果集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>1x1 的结果集中所包含的对象</returns>
        object ExecuteScalar(CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数，命令类别为SQL语句)，该命令返回一个 1x1 的结果集
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>1x1 的结果集中所包含的对象</returns>
        object ExecuteScalar(string pCommandText);
        #endregion

        #region ExecuteDataset
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令并返回一个数据集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataset(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令(不带SQL命令参数)并返回一个数据集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataset(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令并返回一个数据集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataset(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数)并返回一个数据集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataset(CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数，命令类别为SQL语句)并返回一个数据集
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataset(string pCommandText);
        #endregion

        #region ExecuteDataTable
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令并返回一个数据集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>数据集</returns>
        DataTable ExecuteDataTable(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令(不带SQL命令参数)并返回一个数据集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>数据集</returns>
        DataTable ExecuteDataTable(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令并返回一个数据集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>数据集</returns>
        DataTable ExecuteDataTable(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数)并返回一个数据集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>数据集</returns>
        DataTable ExecuteDataTable(CommandType pCommandType, string pCommandText);
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数，命令类别为SQL语句)并返回一个数据集
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>数据集</returns>
        DataTable ExecuteDataTable(string pCommandText);
        #endregion
    }
}
