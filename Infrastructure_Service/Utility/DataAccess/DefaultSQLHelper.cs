/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/14 14:38:34
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
using System.Diagnostics;
using System.Text;

using JIT.Utility;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.DataAccess
{
    /// <summary>
    /// 默认的数据访问助手 
    /// </summary>
    public class DefaultSQLHelper : ISQLHelper
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pConnectionString">数据库连接字符串</param>
        public DefaultSQLHelper(string pConnectionString)
        {
            this.ConnectionString = pConnectionString;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected string ConnectionString { get; set; }
        #endregion

        #region ISQLHelper 成员

        #region Events

        /// <summary>
        /// 命令执行前触发该事件
        /// </summary>
        public event EventHandler<SqlCommandExecutionEventArgs> BeforeExecute;

        /// <summary>
        /// 命令执行完毕后触发该事件
        /// </summary>
        public event EventHandler<SqlCommandExecutionEventArgs> OnExecuted;

        /// <summary>
        /// 清除指定事件的处理程序
        /// </summary>
        /// <param name="pEventNames">事件名称,大小写敏感</param>
        public void ClearEvents(string[] pEventNames)
        {
            if (pEventNames != null)
            {
                foreach (var name in pEventNames)
                {
                    switch (name)
                    {
                        case "OnExecuted":
                            this.OnExecuted = null;
                            break;
                        case "BeforeExecute":
                            this.BeforeExecute = null;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 清除所有事件的处理程序
        /// </summary>
        public void ClearEvents()
        {
            this.OnExecuted = null;
            this.BeforeExecute = null;
        }
        #endregion

        #region Current User Info
        /// <summary>
        /// 用户信息
        /// </summary>
        public BasicUserInfo CurrentUserInfo { get; set; }
        #endregion

        #region  Create Transaction
        /// <summary>
        /// 创建一个事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction CreateTransaction()
        {
            SqlConnection conn = new SqlConnection(this.ConnectionString);
            conn.Open();
            return conn.BeginTransaction();
        }
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
        public int ExecuteNonQuery(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            //参数校验
            if (pTransaction == null)
                throw new ArgumentNullException("pTransaction");
            if (pTransaction != null && pTransaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "pTransaction");
            //创建SQL命令
            SqlCommand cmd = new SqlCommand();
            this.PrepareCommand(cmd, pTransaction.Connection, pTransaction, pCommandType, pCommandText, pCommandParameters);

            //触发事件
            if (this.BeforeExecute != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.UserInfo = this.CurrentUserInfo;
                this.BeforeExecute(this, arg);
            }

            //开启SQL语句执行时间监控
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //执行SQL命令
            int result = cmd.ExecuteNonQuery();
            //停止监控
            sw.Stop();
            //触发事件
            if (this.OnExecuted != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.ExecutionTime = sw.Elapsed;
                arg.UserInfo = this.CurrentUserInfo;
                this.OnExecuted(this, arg);
            }
            //清理资源
            cmd.Parameters.Clear();
            //返回
            return result;
        }
        /// <summary>
        /// 在指定的事务下执行一个SQL命令(不带SQL命令参数),该命令不返回结果集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>受SQL命令影响的数据行数</returns>
        public int ExecuteNonQuery(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteNonQuery(pTransaction, pCommandType, pCommandText, (SqlParameter)null);
        }
        /// <summary>
        /// 执行一个SQL命令,该命令不返回结果集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>受SQL命令影响的数据行数</returns>
        public int ExecuteNonQuery(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            //创建SQL命令
            using (var connection = this.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                this.PrepareCommand(cmd, connection, (SqlTransaction)null, pCommandType, pCommandText, pCommandParameters);
                //触发事件
                if (this.BeforeExecute != null)
                {
                    SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                    arg.Command = cmd;
                    arg.UserInfo = this.CurrentUserInfo;
                    this.BeforeExecute(this, arg);
                }
                //开启SQL语句执行时间监控
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //执行SQL命令
                int result = cmd.ExecuteNonQuery();
                //停止监控
                sw.Stop();
                //触发事件
                if (this.OnExecuted != null)
                {
                    SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                    arg.Command = cmd;
                    arg.ExecutionTime = sw.Elapsed;
                    arg.UserInfo = this.CurrentUserInfo;
                    this.OnExecuted(this, arg);
                }
                //清理资源
                cmd.Parameters.Clear();
                //返回
                return result;
            }
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数),该命令不返回结果集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>受SQL命令影响的数据行数</returns>
        public int ExecuteNonQuery(CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteNonQuery(pCommandType, pCommandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数,命令类别为SQL语句),该命令不返回结果集
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>受SQL命令影响的数据行数</returns>
        public int ExecuteNonQuery(string pCommandText)
        {
            return this.ExecuteNonQuery(CommandType.Text, pCommandText, (SqlParameter[])null);
        }
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
        public SqlDataReader ExecuteReader(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            //参数校验
            if (pTransaction == null)
                throw new ArgumentNullException("pTransaction");
            if (pTransaction != null && pTransaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "pTransaction");
            //创建SQL命令
            SqlCommand cmd = new SqlCommand();
            this.PrepareCommand(cmd, pTransaction.Connection, pTransaction, pCommandType, pCommandText, pCommandParameters);

            //触发事件
            if (this.BeforeExecute != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.UserInfo = this.CurrentUserInfo;
                this.BeforeExecute(this, arg);
            }
            //开启SQL语句执行时间监控
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //执行SQL命令
            SqlDataReader reader = null;
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //停止监控
            sw.Stop();
            //触发事件
            if (this.OnExecuted != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.ExecutionTime = sw.Elapsed;
                arg.UserInfo = this.CurrentUserInfo;
                this.OnExecuted(this, arg);
            }
            //清理资源
            cmd.Parameters.Clear();
            //返回
            return reader;
        }
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令(不带SQL命令参数)并返回一个向前的只读数据读取器
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>向前的只读数据读取器</returns>
        public SqlDataReader ExecuteReader(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteReader(pTransaction, pCommandType, pCommandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行一个SQL命令并返回一个向前的只读数据读取器
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>向前的只读数据读取器</returns>
        public SqlDataReader ExecuteReader(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            //创建SQL命令
            var connection = this.GetConnection();
            SqlCommand cmd = new SqlCommand();
            this.PrepareCommand(cmd, connection, (SqlTransaction)null, pCommandType, pCommandText, pCommandParameters);

            //触发事件
            if (this.BeforeExecute != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.UserInfo = this.CurrentUserInfo;
                this.BeforeExecute(this, arg);
            }
            //开启SQL语句执行时间监控
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //执行SQL命令
            SqlDataReader reader = null;
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //停止监控
            sw.Stop();
            //触发事件
            if (this.OnExecuted != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.ExecutionTime = sw.Elapsed;
                arg.UserInfo = this.CurrentUserInfo;
                this.OnExecuted(this, arg);
            }
            //清理资源
            cmd.Parameters.Clear();
            //返回
            return reader;
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数)并返回一个向前的只读数据读取器
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>向前的只读数据读取器</returns>
        public SqlDataReader ExecuteReader(CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteReader(pCommandType, pCommandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数，命令类别为SQL语句)并返回一个向前的只读数据读取器
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>向前的只读数据读取器</returns>
        public SqlDataReader ExecuteReader(string pCommandText)
        {
            return this.ExecuteReader(CommandType.Text, pCommandText, (SqlParameter[])null);
        }
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
        public object ExecuteScalar(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            //参数校验
            if (pTransaction == null)
                throw new ArgumentNullException("pTransaction");
            if (pTransaction != null && pTransaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "pTransaction");
            //创建SQL命令
            SqlCommand cmd = new SqlCommand();
            this.PrepareCommand(cmd, pTransaction.Connection, pTransaction, pCommandType, pCommandText, pCommandParameters);

            //触发事件
            if (this.BeforeExecute != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.UserInfo = this.CurrentUserInfo;
                this.BeforeExecute(this, arg);
            }
            //开启SQL语句执行时间监控
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //执行SQL命令
            var result = cmd.ExecuteScalar();
            //停止监控
            sw.Stop();
            //触发事件
            if (this.OnExecuted != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.ExecutionTime = sw.Elapsed;
                arg.UserInfo = this.CurrentUserInfo;
                this.OnExecuted(this, arg);
            }
            //清理资源
            cmd.Parameters.Clear();
            //返回
            return result;
        }
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令(不带SQL命令参数)，该命令返回一个 1x1 的结果集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>1x1 的结果集中所包含的对象</returns>
        public object ExecuteScalar(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteScalar(pTransaction, pCommandType, pCommandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行一个SQL命令，该命令返回一个 1x1 的结果集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>1x1 的结果集中所包含的对象</returns>
        public object ExecuteScalar(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            using (var connection = this.GetConnection())
            {
                //创建SQL命令
                SqlCommand cmd = new SqlCommand();
                this.PrepareCommand(cmd, connection, (SqlTransaction)null, pCommandType, pCommandText, pCommandParameters);

                //触发事件
                if (this.BeforeExecute != null)
                {
                    SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                    arg.Command = cmd;
                    arg.UserInfo = this.CurrentUserInfo;
                    this.BeforeExecute(this, arg);
                }
                //开启SQL语句执行时间监控
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //执行SQL命令
                var result = cmd.ExecuteScalar();
                //停止监控
                sw.Stop();
                //触发事件
                if (this.OnExecuted != null)
                {
                    SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                    arg.Command = cmd;
                    arg.ExecutionTime = sw.Elapsed;
                    arg.UserInfo = this.CurrentUserInfo;
                    this.OnExecuted(this, arg);
                }
                //清理资源
                cmd.Parameters.Clear();
                //返回
                return result;
            }
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数)，该命令返回一个 1x1 的结果集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>1x1 的结果集中所包含的对象</returns>
        public object ExecuteScalar(CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteScalar(pCommandType, pCommandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数，命令类别为SQL语句)，该命令返回一个 1x1 的结果集
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>1x1 的结果集中所包含的对象</returns>
        public object ExecuteScalar(string pCommandText)
        {
            return this.ExecuteScalar(CommandType.Text, pCommandText, (SqlParameter[])null);
        }
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
        public DataSet ExecuteDataset(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            //参数校验
            if (pTransaction == null)
                throw new ArgumentNullException("pTransaction");
            if (pTransaction != null && pTransaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "pTransaction");
            //创建SQL命令
            SqlCommand cmd = new SqlCommand();
            this.PrepareCommand(cmd, pTransaction.Connection, pTransaction, pCommandType, pCommandText, pCommandParameters);

            //触发事件
            if (this.BeforeExecute != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.UserInfo = this.CurrentUserInfo;
                this.BeforeExecute(this, arg);
            }
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                //开启SQL语句执行时间监控
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //执行SQL命令
                da.Fill(ds);
                //停止监控
                sw.Stop();
                //触发事件
                if (this.OnExecuted != null)
                {
                    SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                    arg.Command = cmd;
                    arg.ExecutionTime = sw.Elapsed;
                    arg.UserInfo = this.CurrentUserInfo;
                    this.OnExecuted(this, arg);
                }
                //清理资源
                cmd.Parameters.Clear();
                //返回
                return ds;
            }
        }
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令(不带SQL命令参数)并返回一个数据集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataset(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteDataset(pTransaction, pCommandType, pCommandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行一个SQL命令并返回一个数据集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataset(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            //创建SQL命令
            using (var connection = this.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                this.PrepareCommand(cmd, connection, (SqlTransaction)null, pCommandType, pCommandText, pCommandParameters);

                //触发事件
                if (this.BeforeExecute != null)
                {
                    SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                    arg.Command = cmd;
                    arg.UserInfo = this.CurrentUserInfo;
                    this.BeforeExecute(this, arg);
                }
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    //开启SQL语句执行时间监控
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    //执行SQL命令
                    da.Fill(ds);
                    //停止监控
                    sw.Stop();
                    //触发事件
                    if (this.OnExecuted != null)
                    {
                        SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                        arg.Command = cmd;
                        arg.ExecutionTime = sw.Elapsed;
                        arg.UserInfo = this.CurrentUserInfo;
                        this.OnExecuted(this, arg);
                    }
                    //清理资源
                    cmd.Parameters.Clear();
                    //返回
                    return ds;
                }
            }
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数)并返回一个数据集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataset(CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteDataset(pCommandType, pCommandText, (SqlParameter[])null);
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数，命令类别为SQL语句)并返回一个数据集
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataset(string pCommandText)
        {
            return this.ExecuteDataset(CommandType.Text, pCommandText, (SqlParameter[])null);
        }
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
        public DataTable ExecuteDataTable(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            //参数校验
            if (pTransaction == null)
                throw new ArgumentNullException("pTransaction");
            if (pTransaction != null && pTransaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "pTransaction");
            //创建SQL命令
            SqlCommand cmd = new SqlCommand();
            this.PrepareCommand(cmd, pTransaction.Connection, pTransaction, pCommandType, pCommandText, pCommandParameters);

            //触发事件
            if (this.BeforeExecute != null)
            {
                SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                arg.Command = cmd;
                arg.UserInfo = this.CurrentUserInfo;
                this.BeforeExecute(this, arg);
            }
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                //开启SQL语句执行时间监控
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //执行SQL命令
                da.Fill(dt);
                //停止监控
                sw.Stop();
                //触发事件
                if (this.OnExecuted != null)
                {
                    SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                    arg.Command = cmd;
                    arg.ExecutionTime = sw.Elapsed;
                    arg.UserInfo = this.CurrentUserInfo;
                    this.OnExecuted(this, arg);
                }
                //清理资源
                cmd.Parameters.Clear();
                //返回
                return dt;
            }
        }
        /// <summary>
        /// 在指定的事务下,执行一个SQL命令(不带SQL命令参数)并返回一个数据集
        /// </summary>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>数据集</returns>
        public DataTable ExecuteDataTable(SqlTransaction pTransaction, CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteDataTable(pTransaction, pCommandType, pCommandText, null);
        }
        /// <summary>
        /// 执行一个SQL命令并返回一个数据集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <returns>数据集</returns>
        public DataTable ExecuteDataTable(CommandType pCommandType, string pCommandText, params SqlParameter[] pCommandParameters)
        {
            //创建SQL命令
            using (var connection = this.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                this.PrepareCommand(cmd, connection, (SqlTransaction)null, pCommandType, pCommandText, pCommandParameters);

                //触发事件
                if (this.BeforeExecute != null)
                {
                    SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                    arg.Command = cmd;
                    arg.UserInfo = this.CurrentUserInfo;
                    this.BeforeExecute(this, arg);
                }
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    //开启SQL语句执行时间监控
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    //执行SQL命令
                    da.Fill(dt);
                    //停止监控
                    sw.Stop();
                    //触发事件
                    if (this.OnExecuted != null)
                    {
                        SqlCommandExecutionEventArgs arg = new SqlCommandExecutionEventArgs();
                        arg.Command = cmd;
                        arg.ExecutionTime = sw.Elapsed;
                        arg.UserInfo = this.CurrentUserInfo;
                        this.OnExecuted(this, arg);
                    }
                    //清理资源
                    cmd.Parameters.Clear();
                    //返回
                    return dt;
                }
            }
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数)并返回一个数据集
        /// </summary>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <returns>数据集</returns>
        public DataTable ExecuteDataTable(CommandType pCommandType, string pCommandText)
        {
            return this.ExecuteDataTable(pCommandType, pCommandText, null);
        }
        /// <summary>
        /// 执行一个SQL命令(不带SQL命令参数，命令类别为SQL语句)并返回一个数据集
        /// </summary>
        /// <param name="pCommandText">SQL语句</param>
        /// <returns>数据集</returns>
        public DataTable ExecuteDataTable(string pCommandText)
        {
            return this.ExecuteDataTable(CommandType.Text, pCommandText, null);
        }
        #endregion

        #endregion

        #region 工具方法
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        protected SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(this.ConnectionString);
            return conn;
        }
        /// <summary>
        /// 设置SQL命令
        /// </summary>
        /// <param name="pCommand">SQL命令</param>
        /// <param name="pConnection">数据库连接</param>
        /// <param name="pTransaction">事务</param>
        /// <param name="pCommandType">SQL命令的类别(存储过程,SQL语句等)</param>
        /// <param name="pCommandText">存储过程名称或者是SQL语句</param>
        /// <param name="pCommandParameters">SQL命令的参数</param>
        /// <param name="pMustCloseConnection">如果数据库连接在方法内开启,则返回true,否则返回false</param>
        protected void PrepareCommand(SqlCommand pCommand, SqlConnection pConnection, SqlTransaction pTransaction, CommandType pCommandType, string pCommandText, SqlParameter[] pCommandParameters)
        {
            if (pCommand == null)
                throw new ArgumentNullException("pCommand");
            //判断连接状态，开启连接
            if (pConnection.State != ConnectionState.Open)
            {
                pConnection.Open();
            }
            //设置SQL命令
            pCommand.Connection = pConnection;
            pCommand.CommandText = pCommandText;
            pCommand.CommandTimeout = 0;
            //
            if (pTransaction != null)
            {
                if (pTransaction.Connection == null)
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "pTransaction");
                pCommand.Transaction = pTransaction;
            }
            pCommand.CommandType = pCommandType;
            if (pCommandParameters != null && pCommandParameters.Length > 0)
                this.AttachParameter(pCommand, pCommandParameters);
        }
        /// <summary>
        /// 将命令参数添加到命令中去
        /// </summary>
        /// <param name="pCommand"></param>
        /// <param name="pCommandParameters"></param>
        protected void AttachParameter(SqlCommand pCommand, SqlParameter[] pCommandParameters)
        {
            if (pCommand == null)
                throw new ArgumentNullException("pCommand");
            if (pCommandParameters != null)
            {
                foreach (var p in pCommandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        pCommand.Parameters.Add(p);
                    }
                }
            }
        }
        #endregion
    }
}
