/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/13 13:32:19
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
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.MSTRIntegration.Entity;
using JIT.Utility.MSTRIntegration.Base;

namespace JIT.Utility.MSTRIntegration.DataAccess
{
    /// <summary>
    /// 数据访问： 租户平台的用户登录网站后,插入一条这样的记录.MSTR的认证模块根据记录中的信息来创建/重新加载 MSTR的Session. 
    /// 表MSTRIntegrationUserSession的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MSTRIntegrationUserSessionDAO : BaseReportDAO, ICRUDable<MSTRIntegrationUserSessionEntity>, IQueryable<MSTRIntegrationUserSessionEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MSTRIntegrationUserSessionDAO(ReportUserInfo pUserInfo,ISQLHelper pSQLHelper)
            : base(pUserInfo, true,pSQLHelper)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(MSTRIntegrationUserSessionEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(MSTRIntegrationUserSessionEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [MSTRIntegrationUserSession](");
            strSql.Append("[ClientID],[UserID],[WebSessionID],[IP],[IsCheckIP],[LCID],[IsChange],[MSTRIServerName],[MSTRIServerPort],[MSTRUserName],[MSTRUserPassword],[MSTRProjectName],[MSTRSessionID],[MSTRSessionState],[IsDelete],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime])");
            strSql.Append(" values (");
            strSql.Append("@ClientID,@UserID,@WebSessionID,@IP,@IsCheckIP,@LCID,@IsChange,@MSTRIServerName,@MSTRIServerPort,@MSTRUserName,@MSTRUserPassword,@MSTRProjectName,@MSTRSessionID,@MSTRSessionState,@IsDelete,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime)");            
			strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@ClientID",SqlDbType.NVarChar,100),
					new SqlParameter("@UserID",SqlDbType.NVarChar,100),
					new SqlParameter("@WebSessionID",SqlDbType.NVarChar,400),
					new SqlParameter("@IP",SqlDbType.NVarChar,100),
					new SqlParameter("@IsCheckIP",SqlDbType.Int),
					new SqlParameter("@LCID",SqlDbType.Int),
					new SqlParameter("@IsChange",SqlDbType.Int),
					new SqlParameter("@MSTRIServerName",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRIServerPort",SqlDbType.Int),
					new SqlParameter("@MSTRUserName",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRUserPassword",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRProjectName",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRSessionID",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRSessionState",SqlDbType.NVarChar,1000),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime)
            };
			parameters[0].Value = pEntity.ClientID;
			parameters[1].Value = pEntity.UserID;
			parameters[2].Value = pEntity.WebSessionID;
			parameters[3].Value = pEntity.IP;
			parameters[4].Value = pEntity.IsCheckIP;
			parameters[5].Value = pEntity.LCID;
			parameters[6].Value = pEntity.IsChange;
			parameters[7].Value = pEntity.MSTRIServerName;
			parameters[8].Value = pEntity.MSTRIServerPort;
			parameters[9].Value = pEntity.MSTRUserName;
			parameters[10].Value = pEntity.MSTRUserPassword;
			parameters[11].Value = pEntity.MSTRProjectName;
			parameters[12].Value = pEntity.MSTRSessionID;
			parameters[13].Value = pEntity.MSTRSessionState;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pEntity.CreateBy;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.LastUpdateBy;
			parameters[18].Value = pEntity.LastUpdateTime;

            //执行并将结果回写
            object result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.SessionID = Convert.ToInt32(result);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public MSTRIntegrationUserSessionEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MSTRIntegrationUserSession] where SessionID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            MSTRIntegrationUserSessionEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public MSTRIntegrationUserSessionEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MSTRIntegrationUserSession] where isdelete=0");
            //读取数据
            List<MSTRIntegrationUserSessionEntity> list = new List<MSTRIntegrationUserSessionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MSTRIntegrationUserSessionEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list.ToArray();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(MSTRIntegrationUserSessionEntity pEntity , IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SessionID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MSTRIntegrationUserSession] set ");
            strSql.Append("[ClientID]=@ClientID,[UserID]=@UserID,[WebSessionID]=@WebSessionID,[IP]=@IP,[IsCheckIP]=@IsCheckIP,[LCID]=@LCID,[IsChange]=@IsChange,[MSTRIServerName]=@MSTRIServerName,[MSTRIServerPort]=@MSTRIServerPort,[MSTRUserName]=@MSTRUserName,[MSTRUserPassword]=@MSTRUserPassword,[MSTRProjectName]=@MSTRProjectName,[MSTRSessionID]=@MSTRSessionID,[MSTRSessionState]=@MSTRSessionState,[LastUpdateBy]=@LastUpdateBy,[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where SessionID=@SessionID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ClientID",SqlDbType.NVarChar,100),
					new SqlParameter("@UserID",SqlDbType.NVarChar,100),
					new SqlParameter("@WebSessionID",SqlDbType.NVarChar,400),
					new SqlParameter("@IP",SqlDbType.NVarChar,100),
					new SqlParameter("@IsCheckIP",SqlDbType.Int),
					new SqlParameter("@LCID",SqlDbType.Int),
					new SqlParameter("@IsChange",SqlDbType.Int),
					new SqlParameter("@MSTRIServerName",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRIServerPort",SqlDbType.Int),
					new SqlParameter("@MSTRUserName",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRUserPassword",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRProjectName",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRSessionID",SqlDbType.NVarChar,100),
					new SqlParameter("@MSTRSessionState",SqlDbType.NVarChar,1000),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@SessionID",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.ClientID;
			parameters[1].Value = pEntity.UserID;
			parameters[2].Value = pEntity.WebSessionID;
			parameters[3].Value = pEntity.IP;
			parameters[4].Value = pEntity.IsCheckIP;
			parameters[5].Value = pEntity.LCID;
			parameters[6].Value = pEntity.IsChange;
			parameters[7].Value = pEntity.MSTRIServerName;
			parameters[8].Value = pEntity.MSTRIServerPort;
			parameters[9].Value = pEntity.MSTRUserName;
			parameters[10].Value = pEntity.MSTRUserPassword;
			parameters[11].Value = pEntity.MSTRProjectName;
			parameters[12].Value = pEntity.MSTRSessionID;
			parameters[13].Value = pEntity.MSTRSessionState;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.SessionID;

            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(MSTRIntegrationUserSessionEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MSTRIntegrationUserSessionEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(MSTRIntegrationUserSessionEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SessionID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.SessionID.Value, pTran);           
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [MSTRIntegrationUserSession] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where SessionID=@SessionID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=CurrentUserInfo.UserID},
                new SqlParameter{ParameterName="@SessionID",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(MSTRIntegrationUserSessionEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (!item.SessionID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.SessionID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(MSTRIntegrationUserSessionEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("{0},",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [MSTRIntegrationUserSession] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy="+CurrentUserInfo.UserID+",IsDelete=1 where SessionID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public MSTRIntegrationUserSessionEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MSTRIntegrationUserSession] where isdelete=0 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //执行SQL
            List<MSTRIntegrationUserSessionEntity> list = new List<MSTRIntegrationUserSessionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MSTRIntegrationUserSessionEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }
        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<MSTRIntegrationUserSessionEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [SessionID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [MSTRIntegrationUserSession] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [MSTRIntegrationUserSession] where isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<MSTRIntegrationUserSessionEntity> result = new PagedQueryResult<MSTRIntegrationUserSessionEntity>();
            List<MSTRIntegrationUserSessionEntity> list = new List<MSTRIntegrationUserSessionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MSTRIntegrationUserSessionEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public MSTRIntegrationUserSessionEntity[] QueryByEntity(MSTRIntegrationUserSessionEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<MSTRIntegrationUserSessionEntity> PagedQueryByEntity(MSTRIntegrationUserSessionEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(MSTRIntegrationUserSessionEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SessionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SessionID", Value = pQueryEntity.SessionID });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.UserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserID", Value = pQueryEntity.UserID });
            if (pQueryEntity.WebSessionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WebSessionID", Value = pQueryEntity.WebSessionID });
            if (pQueryEntity.IP!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IP", Value = pQueryEntity.IP });
            if (pQueryEntity.IsCheckIP!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsCheckIP", Value = pQueryEntity.IsCheckIP });
            if (pQueryEntity.LCID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LCID", Value = pQueryEntity.LCID });
            if (pQueryEntity.IsChange!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsChange", Value = pQueryEntity.IsChange });
            if (pQueryEntity.MSTRIServerName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MSTRIServerName", Value = pQueryEntity.MSTRIServerName });
            if (pQueryEntity.MSTRIServerPort!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MSTRIServerPort", Value = pQueryEntity.MSTRIServerPort });
            if (pQueryEntity.MSTRUserName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MSTRUserName", Value = pQueryEntity.MSTRUserName });
            if (pQueryEntity.MSTRUserPassword!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MSTRUserPassword", Value = pQueryEntity.MSTRUserPassword });
            if (pQueryEntity.MSTRProjectName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MSTRProjectName", Value = pQueryEntity.MSTRProjectName });
            if (pQueryEntity.MSTRSessionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MSTRSessionID", Value = pQueryEntity.MSTRSessionID });
            if (pQueryEntity.MSTRSessionState!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MSTRSessionState", Value = pQueryEntity.MSTRSessionState });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out MSTRIntegrationUserSessionEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new MSTRIntegrationUserSessionEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["SessionID"] != DBNull.Value)
			{
				pInstance.SessionID =   Convert.ToInt32(pReader["SessionID"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =  Convert.ToString(pReader["ClientID"]);
			}
			if (pReader["UserID"] != DBNull.Value)
			{
				pInstance.UserID =  Convert.ToString(pReader["UserID"]);
			}
			if (pReader["WebSessionID"] != DBNull.Value)
			{
				pInstance.WebSessionID =  Convert.ToString(pReader["WebSessionID"]);
			}
			if (pReader["IP"] != DBNull.Value)
			{
				pInstance.IP =  Convert.ToString(pReader["IP"]);
			}
			if (pReader["IsCheckIP"] != DBNull.Value)
			{
				pInstance.IsCheckIP =   Convert.ToInt32(pReader["IsCheckIP"]);
			}
			if (pReader["LCID"] != DBNull.Value)
			{
				pInstance.LCID =   Convert.ToInt32(pReader["LCID"]);
			}
			if (pReader["IsChange"] != DBNull.Value)
			{
				pInstance.IsChange =   Convert.ToInt32(pReader["IsChange"]);
			}
			if (pReader["MSTRIServerName"] != DBNull.Value)
			{
				pInstance.MSTRIServerName =  Convert.ToString(pReader["MSTRIServerName"]);
			}
			if (pReader["MSTRIServerPort"] != DBNull.Value)
			{
				pInstance.MSTRIServerPort =   Convert.ToInt32(pReader["MSTRIServerPort"]);
			}
			if (pReader["MSTRUserName"] != DBNull.Value)
			{
				pInstance.MSTRUserName =  Convert.ToString(pReader["MSTRUserName"]);
			}
			if (pReader["MSTRUserPassword"] != DBNull.Value)
			{
				pInstance.MSTRUserPassword =  Convert.ToString(pReader["MSTRUserPassword"]);
			}
			if (pReader["MSTRProjectName"] != DBNull.Value)
			{
				pInstance.MSTRProjectName =  Convert.ToString(pReader["MSTRProjectName"]);
			}
			if (pReader["MSTRSessionID"] != DBNull.Value)
			{
				pInstance.MSTRSessionID =  Convert.ToString(pReader["MSTRSessionID"]);
			}
			if (pReader["MSTRSessionState"] != DBNull.Value)
			{
				pInstance.MSTRSessionState =  Convert.ToString(pReader["MSTRSessionState"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}

        }
        #endregion
    }
}
