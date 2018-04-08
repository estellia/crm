using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace WebAPIServices
{
    public class SqlHelper
    {

        //public static readonly string connectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();

        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["SAPConnection"].ToString();



        #region  执行简单SQL语句


        public static bool ConnectionTest()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {

                sqlConnection.Open();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
            
        }

       
        /// <summary>
        /// 获取当前用户 所在公司连接字符串
        /// </summary>
        /// <param name="userid">当前登录的用户</param>
        /// <param name="Company">帐套信息</param>
        /// <returns>0未配置帐套信息</returns>
        public static string ConnStr(string userid, string Company)
        {

            string usercode = string.Empty;
            string servername = string.Empty;
            string serverdatabase = string.Empty;
            string serveruser = string.Empty;
            string serverpwd = string.Empty;
            string connect = string.Empty;
            DataTable dt = new DataTable();
            usercode = userid;

            try
            {
                string sql = "select * from usercompany as a left join [Server] as b on a.company=b.serverDatabase where usercode='" + usercode + "' and company='" + Company + "'";
                dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    connect = "0";
                }
                else
                {
                    servername = dt.Rows[0]["ServerName"].ToString();
                    serverdatabase = dt.Rows[0]["ServerDataBase"].ToString();
                    serveruser = dt.Rows[0]["ServerUser"].ToString();
                    serverpwd = dt.Rows[0]["ServerPwd"].ToString();
                    connect = "Data Source=" + servername + ";Initial Catalog=" + serverdatabase + ";uid=" + serveruser + ";pwd=" + serverpwd + "";
                }
                return connect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取当前用户 所在公司连接字符串
        /// </summary>
        /// <param name="Company">帐套信息</param>
        /// <returns>0未配置帐套信息</returns>
        public static string ConnStr(string Company)
        {
            string servername = string.Empty;
            string serverdatabase = string.Empty;
            string serveruser = string.Empty;
            string serverpwd = string.Empty;
            string connect = string.Empty;
            DataTable dt = new DataTable();


            try
            {
                string sql = "select * from usercompany as a left join [Server] as b on a.company=b.serverDatabase where  company='" + Company + "'";
                dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    connect = "0";
                }
                else
                {
                    servername = dt.Rows[0]["ServerName"].ToString();
                    serverdatabase = dt.Rows[0]["ServerDataBase"].ToString();
                    serveruser = dt.Rows[0]["ServerUser"].ToString();
                    serverpwd = dt.Rows[0]["ServerPwd"].ToString();
                    connect = "Data Source=" + servername + ";Initial Catalog=" + serverdatabase + ";uid=" + serveruser + ";pwd=" + serverpwd + "";
                }
                return connect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询记录是否存在
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 查询记录是否存在
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="strSql">参数</param>
        /// <returns></returns>
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        //cmd.ExecuteScalar();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static bool ExecuteSqlBool(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        //cmd.ExecuteScalar();
                        return true;
                    }
                    catch (SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        ///  返回操作是否成功
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool ExecuteSqlBool(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        con.Open();
                        PrepareCommand(cmd, con, null, SQLString, cmdParms);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        con.Close();
                        con.Dispose();
                        return true;
                    }
                    catch (SqlException e)
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                            con.Dispose();
                        }
                        throw new Exception(e.ToString() + SQLString.ToString());
                    }
                }
            }
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="connectionString">连接字符串</param>
        /// date 2008-11-21 for 更新程序路径设置
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlAlertTable(string SQLString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        //cmd.ExecuteScalar();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>首行首列值</returns>
        public static string ExecuteScalar(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        string rows = cmd.ExecuteScalar().ToString();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>首行首列值</returns>
        public static object ExecScalar(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        return cmd.ExecuteScalar();
                    }
                    catch (SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 获取表某个字段的最大值
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="TableName">表名</param>
        /// <returns>string</returns>
        public static string GetMaxStringID(string FieldName, string TableName,string printpoint,string usercode)
        {
            string strsql = "select max(" + FieldName + ") from " + TableName + " where printpoint = '" + printpoint + "' and WorkerNo = '" + usercode + "'";
            object obj = GetSingle(strsql);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return "1";
            }
            else
            {
                return obj.ToString();
            }

        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>  
        public static bool ExecuteSqlTran(ArrayList SQLStringList)
        {
            bool bl = true;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (SqlException E)
                {
                    tx.Rollback();
                    bl = false;
                    throw new Exception(E.Message);
                }
            }
            return bl;
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);

                SqlParameter myParameter = new SqlParameter("@content", SqlDbType.Variant);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        public static int ExecuteScalar(string SQLString, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                SqlParameter myParameter = new SqlParameter("@content", SqlDbType.Variant);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = int.Parse(cmd.ExecuteScalar().ToString());
                    return rows;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                SqlParameter myParameter = new SqlParameter("@fs", SqlDbType.Binary);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet，作用和Query相同，名字更直观些
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }

        /// <summary>
        /// 返回一个DataTable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(sql, connection);
                    DataTable Dt = new DataTable();
                    Da.Fill(Dt);
                    return Dt;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 返回一个DataTable根据当前登录人
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, string company)
        {
            string connStr = string.Empty;
            connStr = ConnStr(company);
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(sql, connection);
                    DataTable Dt = new DataTable();
                    Da.Fill(Dt);
                    return Dt;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 返回一个DataTable根据当前登录人
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, string userid, string company)
        {
            string connStr = string.Empty;
            connStr = ConnStr(userid, company);
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(sql, connection);
                    DataTable Dt = new DataTable();
                    Da.Fill(Dt);
                    return Dt;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion

        #region 执行新增的sql语句并返回执行语句的主键id
        /// <summary>
        /// 执行新增的sql语句并返回执行语句的主键id
        /// </summary>
        /// <param name="SQLString">sql添加执行语句</param>
        /// <param name="FieldName">表名</param>
        /// <param name="TableName">主键名称</param>
        /// <returns></returns>
        //public static int GetMainKey(string SQLString, string FieldName, string TableName)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(SQLString, connection))
        //        {
        //            try
        //            {
        //                connection.Open();
        //                cmd.ExecuteNonQuery();
        //                int KeyID = int.Parse(GetMaxStringID(FieldName, TableName));
        //                //cmd.ExecuteScalar();
        //                return KeyID;
        //            }
        //            catch (SqlException E)
        //            {
        //                connection.Close();
        //                throw new Exception(E.Message);
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //}
        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        if (cmdParms != null)
                        {
                            foreach (SqlParameter p in cmdParms)
                            {
                                if (p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                else if (p.Value.ToString() == "0001/01/01 0:00:00" || p.Value.ToString() == "0001-01-01 0:00:00" || p.Value.ToString() == "0001-1-1 0:00:00" || p.Value.ToString() == "0001/1/1 0:00:00")
                                {
                                    p.Value = DateTime.Parse("1900-01-01");
                                }

                            }

                        }
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                    catch (Exception ee)
                    {
                        throw new Exception(ee.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回ID
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlKey(string SQLString, string FieldName, string TableName, string printpoint, string usercode, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        if (cmdParms != null)
                        {
                            foreach (SqlParameter p in cmdParms)
                            {
                                if (p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                else if (p.Value.ToString() == "0001/01/01 0:00:00" || p.Value.ToString() == "0001-01-01 0:00:00" || p.Value.ToString() == "0001-1-1 0:00:00" || p.Value.ToString() == "0001/1/1 0:00:00")
                                {
                                    p.Value = DateTime.Parse("1900-01-01");
                                }

                            }

                        }
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        int KeyID = 0;
                        if (rows > 0)
                        {
                            cmd.Parameters.Clear();
                            KeyID = int.Parse(GetMaxStringID(FieldName, TableName,printpoint,usercode));
                        }
                        return KeyID;
                    }
                    catch (SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                    catch (Exception ee)
                    {
                        throw new Exception(ee.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static int ExecuteScalar(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        if (cmdParms != null)
                        {
                            foreach (SqlParameter p in cmdParms)
                            {

                                if (p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                else if (p.Value.ToString() == "0001/01/01 0:00:00" || p.Value.ToString() == "0001-01-01 0:00:00" || p.Value.ToString() == "0001-1-1 0:00:00" || p.Value.ToString() == "0001/1/1 0:00:00")
                                {
                                    p.Value = DateTime.Parse("1900-01-01");
                                }

                            }
                        }
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = int.Parse(cmd.ExecuteScalar().ToString());
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                    catch (Exception ee)
                    {
                        throw new Exception(ee.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch (SqlException E)
                    {
                        trans.Rollback();
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);

            }
        }

        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>


        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        //public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        DataSet dataSet = new DataSet();
        //        connection.Open();
        //        SqlDataAdapter sqlDA = new SqlDataAdapter();
        //        sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
        //        sqlDA.Fill(dataSet, tableName);
        //        connection.Close();
        //        return dataSet;
        //    }
        //}


        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数  
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值) 
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        #endregion


        /// <summary>
        /// 创建链接服务器
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <param name="Company">公司</param>
        /// <returns>服务器名.数据库名.数据库所有者名称dbo</returns>
        public static string GetLinkName(string userid, string Company)
        {
            string servername = string.Empty;
            string serverdatabase = string.Empty;
            string serveruser = string.Empty;
            string serverpwd = string.Empty;
            try
            {
                string sql = "select * from usercompany as a left join [Server] as b on a.company=b.serverDatabase where usercode='" + userid + "' and company='" + Company + "'";
                DataTable dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count == 1)
                {
                    SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connectionString);
                    servername = dt.Rows[0]["ServerName"].ToString();
                  
                    serverdatabase = dt.Rows[0]["ServerDataBase"].ToString();
                    serveruser = dt.Rows[0]["ServerUser"].ToString();
                    serverpwd = dt.Rows[0]["ServerPwd"].ToString();
                    //如果是在同一服务器则不必创建
                    if (servername == sb.DataSource)
                    {
                        return serverdatabase + ".dbo.";
                    }
                    sql = "select count(*) from sys.servers where name='" + servername + "'";
                    //如果已经存在则不必创建
                    if (Exists(sql))
                    {
                        return "["+servername+"]."+serverdatabase + ".dbo."; 
                    }
                    else
                    {
                        sql = "EXEC sp_addlinkedserver '" + servername + "','','SQLOLEDB','" + servername + "';"
                        + "EXEC sp_addlinkedsrvlogin '" + servername + "','false',null,'" + serveruser + "','" + serverpwd + "';"
                        + "select count(*) from sys.servers where name='" + servername + "'";
                        if (Exists(sql))
                        {
                            return "[" + servername + "]." + serverdatabase + ".dbo."; 
                        }
                    }
                }
                return "";
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 创建链接服务器
        /// </summary>
        /// <param name="Company">公司</param>
        /// <returns>服务器名.数据库名.数据库所有者名称dbo</returns>
        public static string GetLinkName(string Company)
        {
            string servername = string.Empty;
            string serverdatabase = string.Empty;
            string serveruser = string.Empty;
            string serverpwd = string.Empty;
            try
            {
                string sql = "select * from  [Server] where ServerDataBase='" + Company + "'";
                DataTable dt = SqlHelper.GetDataTable(sql);
                if (dt.Rows.Count == 1)
                {
                    SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connectionString);
                    servername = dt.Rows[0]["ServerName"].ToString();

                    serverdatabase = dt.Rows[0]["ServerDataBase"].ToString();
                    serveruser = dt.Rows[0]["ServerUser"].ToString();
                    serverpwd = dt.Rows[0]["ServerPwd"].ToString();
                    //如果是在同一服务器则不必创建
                    if (servername == sb.DataSource)
                    {
                        return serverdatabase + ".dbo.";
                    }
                    sql = "select count(*) from sys.servers where name='" + servername + "'";
                    //如果已经存在则不必创建
                    if (Exists(sql))
                    {
                        return "[" + servername + "]." + serverdatabase + ".dbo.";
                    }
                    else
                    {
                        sql = "EXEC sp_addlinkedserver '" + servername + "','','SQLOLEDB','" + servername + "';"
                        + "EXEC sp_addlinkedsrvlogin '" + servername + "','false',null,'" + serveruser + "','" + serverpwd + "';"
                        + "select count(*) from sys.servers where name='" + servername + "'";
                        if (Exists(sql))
                        {
                            return "[" + servername + "]." + serverdatabase + ".dbo.";
                        }
                    }
                }
                return "";
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除链接服务器
        /// </summary>
        /// <param name="servername">服务器名称</param>
        public static void DropLink(string servername)
        {
            try
            {
                string sql = "select count(*) from sys.servers where name='" + servername + "'";
                if (Exists(sql))
                {
                    sql = "EXEC sp_droplinkedsrvlogin '" + servername + "',NULL; EXEC sp_dropserver '" + servername + "','droplogins' ";
                    SqlHelper.ExecuteSql(sql);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        /// <returns>是否执行成功</returns>
        public static bool ExecuteSqlTran_Hashtable(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            if (cmdParms != null)
                            {
                                foreach (SqlParameter p in cmdParms)
                                {
                                    if (p.Value == null)
                                    {
                                        p.Value = DBNull.Value;
                                    }
                                    else if (p.Value.ToString() == "0001/01/01 0:00:00" || p.Value.ToString() == "0001-01-01 0:00:00" || p.Value.ToString() == "0001-1-1 0:00:00" || p.Value.ToString() == "0001/1/1 0:00:00" || p.Value.ToString() == "0001/01/01 00:00:00" || p.Value.ToString() == "0001-01-01 00:00:00" || p.Value.ToString() == "0001-1-1 00:00:00" || p.Value.ToString() == "0001/1/1 00:00:00")
                                    {
                                        p.Value = DateTime.Parse("1900-01-01");
                                    }

                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (SqlException E)
                    {
                        trans.Rollback();
                        //throw new Exception(E.Message);
                        return false;
                    }
                }
            }
            return true;
        }

    }
}

