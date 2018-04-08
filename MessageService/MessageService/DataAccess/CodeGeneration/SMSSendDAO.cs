/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/17 19:34:40
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
using JIT.MessageService.Entity;
using JIT.MessageService.Base;
using JIT.Utility.DataAccess.Query;

namespace JIT.MessageService.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表SMS_send的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SMSSendDAO : BaseDAO<BasicUserInfo>, ICRUDable<SMSSendEntity>, IQueryable<SMSSendEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SMSSendDAO(BasicUserInfo pUserInfo)
            : base(pUserInfo,new ConnectionStringManager())
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(SMSSendEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(SMSSendEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //初始化固定字段
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [SMS_send](");
            strSql.Append("[Mobile_NO],[SMS_content],[Send_time],[Regularly_send_time],[SMS_send_NO],[User_id],[Status],[SMS_status],[mtmsgid],[Receive_user_id],[Send_count],[sign],[SMS_source])");
            strSql.Append(" values (");
            strSql.Append("@MobileNO,@SMSContent,@SendTime,@RegularlySendTime,@SMSSendNO,@UserID,@Status,@SMSStatus,@Mtmsgid,@ReceiveUserID,@SendCount,@Sign,@SMSSource)");
            strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@MobileNO",SqlDbType.NChar),
					new SqlParameter("@SMSContent",SqlDbType.NVarChar),
					new SqlParameter("@SendTime",SqlDbType.DateTime),
					new SqlParameter("@RegularlySendTime",SqlDbType.DateTime),
					new SqlParameter("@SMSSendNO",SqlDbType.Int),
					new SqlParameter("@UserID",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@SMSStatus",SqlDbType.Int),
					new SqlParameter("@Mtmsgid",SqlDbType.NChar),
					new SqlParameter("@ReceiveUserID",SqlDbType.Int),
					new SqlParameter("@SendCount",SqlDbType.Int),
					new SqlParameter("@Sign",SqlDbType.NVarChar),
					new SqlParameter("@SMSSource",SqlDbType.Int)
            };
            parameters[0].Value = pEntity.MobileNO;
            parameters[1].Value = pEntity.SMSContent;
            parameters[2].Value = pEntity.SendTime;
            parameters[3].Value = pEntity.RegularlySendTime;
            parameters[4].Value = pEntity.SMSSendNO;
            parameters[5].Value = pEntity.UserID;
            parameters[6].Value = pEntity.Status;
            parameters[7].Value = pEntity.SMSStatus;
            parameters[8].Value = pEntity.Mtmsgid;
            parameters[9].Value = pEntity.ReceiveUserID;
            parameters[10].Value = pEntity.SendCount;
            parameters[11].Value = pEntity.Sign;
            parameters[12].Value = pEntity.SMSSource;

            //执行并将结果回写
            object result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            pEntity.SMSSendID = Convert.ToInt32(result);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public SMSSendEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SMS_send] where SMS_send_id='{0}'", id.ToString());
            //读取数据
            SMSSendEntity m = null;
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
        public SMSSendEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SMS_send] where isdelete=0");
            //读取数据
            List<SMSSendEntity> list = new List<SMSSendEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SMSSendEntity m;
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
        public void Update(SMSSendEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(SMSSendEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SMSSendID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SMS_send] set ");
            if (pIsUpdateNullField || pEntity.MobileNO != null)
                strSql.Append("[Mobile_NO]=@MobileNO,");
            if (pIsUpdateNullField || pEntity.SMSContent != null)
                strSql.Append("[SMS_content]=@SMSContent,");
            if (pIsUpdateNullField || pEntity.SendTime != null)
                strSql.Append("[Send_time]=@SendTime,");
            if (pIsUpdateNullField || pEntity.RegularlySendTime != null)
                strSql.Append("[Regularly_send_time]=@RegularlySendTime,");
            if (pIsUpdateNullField || pEntity.SMSSendNO != null)
                strSql.Append("[SMS_send_NO]=@SMSSendNO,");
            if (pIsUpdateNullField || pEntity.UserID != null)
                strSql.Append("[User_id]=@UserID,");
            if (pIsUpdateNullField || pEntity.Status != null)
                strSql.Append("[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.SMSStatus != null)
                strSql.Append("[SMS_status]=@SMSStatus,");
            if (pIsUpdateNullField || pEntity.Mtmsgid != null)
                strSql.Append("[mtmsgid]=@Mtmsgid,");
            if (pIsUpdateNullField || pEntity.ReceiveUserID != null)
                strSql.Append("[Receive_user_id]=@ReceiveUserID,");
            if (pIsUpdateNullField || pEntity.SendCount != null)
                strSql.Append("[Send_count]=@SendCount,");
            if (pIsUpdateNullField || pEntity.Sign != null)
                strSql.Append("[sign]=@Sign,");
            if (pIsUpdateNullField || pEntity.SMSSource != null)
                strSql.Append("[SMS_source]=@SMSSource");
            strSql.Append(" where SMS_send_id=@SMSSendID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@MobileNO",SqlDbType.NChar),
					new SqlParameter("@SMSContent",SqlDbType.NVarChar),
					new SqlParameter("@SendTime",SqlDbType.DateTime),
					new SqlParameter("@RegularlySendTime",SqlDbType.DateTime),
					new SqlParameter("@SMSSendNO",SqlDbType.Int),
					new SqlParameter("@UserID",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@SMSStatus",SqlDbType.Int),
					new SqlParameter("@Mtmsgid",SqlDbType.NChar),
					new SqlParameter("@ReceiveUserID",SqlDbType.Int),
					new SqlParameter("@SendCount",SqlDbType.Int),
					new SqlParameter("@Sign",SqlDbType.NVarChar),
					new SqlParameter("@SMSSource",SqlDbType.Int),
					new SqlParameter("@SMSSendID",SqlDbType.Int)
            };
            parameters[0].Value = pEntity.MobileNO;
            parameters[1].Value = pEntity.SMSContent;
            parameters[2].Value = pEntity.SendTime;
            parameters[3].Value = pEntity.RegularlySendTime;
            parameters[4].Value = pEntity.SMSSendNO;
            parameters[5].Value = pEntity.UserID;
            parameters[6].Value = pEntity.Status;
            parameters[7].Value = pEntity.SMSStatus;
            parameters[8].Value = pEntity.Mtmsgid;
            parameters[9].Value = pEntity.ReceiveUserID;
            parameters[10].Value = pEntity.SendCount;
            parameters[11].Value = pEntity.Sign;
            parameters[12].Value = pEntity.SMSSource;
            parameters[13].Value = pEntity.SMSSendID;

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
        public void Update(SMSSendEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(SMSSendEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(SMSSendEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SMSSendID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.SMSSendID.Value, pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("delete from [SMS_send] where SMS_send_id=@SMSSendID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@SMSSendID",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(SMSSendEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (!item.SMSSendID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.SMSSendID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(SMSSendEntity[] pEntities)
        {
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            if (pIDs == null || pIDs.Length == 0)
                return;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("{0},", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("delete from [SMS_send] where SMS_send_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public SMSSendEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SMS_send] where 1=1 ");
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
            List<SMSSendEntity> list = new List<SMSSendEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SMSSendEntity m;
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
        public PagedQueryResult<SMSSendEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [SMSSendID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [SMSSend] where 1=1 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [SMSSend] where 1=1 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<SMSSendEntity> result = new PagedQueryResult<SMSSendEntity>();
            List<SMSSendEntity> list = new List<SMSSendEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    SMSSendEntity m;
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
        public SMSSendEntity[] QueryByEntity(SMSSendEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<SMSSendEntity> PagedQueryByEntity(SMSSendEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(SMSSendEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SMSSendID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SMSSendID", Value = pQueryEntity.SMSSendID });
            if (pQueryEntity.MobileNO != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MobileNO", Value = pQueryEntity.MobileNO });
            if (pQueryEntity.SMSContent != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SMSContent", Value = pQueryEntity.SMSContent });
            if (pQueryEntity.SendTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendTime", Value = pQueryEntity.SendTime });
            if (pQueryEntity.RegularlySendTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RegularlySendTime", Value = pQueryEntity.RegularlySendTime });
            if (pQueryEntity.SMSSendNO != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SMSSendNO", Value = pQueryEntity.SMSSendNO });
            if (pQueryEntity.UserID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserID", Value = pQueryEntity.UserID });
            if (pQueryEntity.Status != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.SMSStatus != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SMSStatus", Value = pQueryEntity.SMSStatus });
            if (pQueryEntity.Mtmsgid != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Mtmsgid", Value = pQueryEntity.Mtmsgid });
            if (pQueryEntity.ReceiveUserID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceiveUserID", Value = pQueryEntity.ReceiveUserID });
            if (pQueryEntity.SendCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendCount", Value = pQueryEntity.SendCount });
            if (pQueryEntity.Sign != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sign", Value = pQueryEntity.Sign });
            if (pQueryEntity.SMSSource != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SMSSource", Value = pQueryEntity.SMSSource });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out SMSSendEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new SMSSendEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["SMS_send_id"] != DBNull.Value)
            {
                pInstance.SMSSendID = Convert.ToInt32(pReader["SMS_send_id"]);
            }
            if (pReader["Mobile_NO"] != DBNull.Value)
            {
                pInstance.MobileNO = Convert.ToString(pReader["Mobile_NO"]);
            }
            if (pReader["SMS_content"] != DBNull.Value)
            {
                pInstance.SMSContent = Convert.ToString(pReader["SMS_content"]);
            }
            if (pReader["Send_time"] != DBNull.Value)
            {
                pInstance.SendTime = Convert.ToDateTime(pReader["Send_time"]);
            }
            if (pReader["Regularly_send_time"] != DBNull.Value)
            {
                pInstance.RegularlySendTime = Convert.ToDateTime(pReader["Regularly_send_time"]);
            }
            if (pReader["SMS_send_NO"] != DBNull.Value)
            {
                pInstance.SMSSendNO = Convert.ToInt32(pReader["SMS_send_NO"]);
            }
            if (pReader["User_id"] != DBNull.Value)
            {
                pInstance.UserID = Convert.ToInt32(pReader["User_id"]);
            }
            if (pReader["Status"] != DBNull.Value)
            {
                pInstance.Status = Convert.ToInt32(pReader["Status"]);
            }
            if (pReader["SMS_status"] != DBNull.Value)
            {
                pInstance.SMSStatus = Convert.ToInt32(pReader["SMS_status"]);
            }
            if (pReader["mtmsgid"] != DBNull.Value)
            {
                pInstance.Mtmsgid = Convert.ToString(pReader["mtmsgid"]);
            }
            if (pReader["Receive_user_id"] != DBNull.Value)
            {
                pInstance.ReceiveUserID = Convert.ToInt32(pReader["Receive_user_id"]);
            }
            if (pReader["Send_count"] != DBNull.Value)
            {
                pInstance.SendCount = Convert.ToInt32(pReader["Send_count"]);
            }
            if (pReader["sign"] != DBNull.Value)
            {
                pInstance.Sign = Convert.ToString(pReader["sign"]);
            }
            if (pReader["SMS_source"] != DBNull.Value)
            {
                pInstance.SMSSource = Convert.ToInt32(pReader["SMS_source"]);
            }
        }
        #endregion
    }
}
