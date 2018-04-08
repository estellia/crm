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
    /// ���ݷ��ʣ�  
    /// ��SMS_send�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SMSSendDAO : BaseDAO<BasicUserInfo>, ICRUDable<SMSSendEntity>, IQueryable<SMSSendEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SMSSendDAO(BasicUserInfo pUserInfo)
            : base(pUserInfo,new ConnectionStringManager())
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(SMSSendEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(SMSSendEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�
            
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

            //ִ�в��������д
            object result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            pEntity.SMSSendID = Convert.ToInt32(result);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public SMSSendEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SMS_send] where SMS_send_id='{0}'", id.ToString());
            //��ȡ����
            SMSSendEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public SMSSendEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SMS_send] where isdelete=0");
            //��ȡ����
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
            //����
            return list.ToArray();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(SMSSendEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(SMSSendEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SMSSendID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }

            //��֯������SQL
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

            //ִ�����
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(SMSSendEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(SMSSendEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(SMSSendEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SMSSendID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.SMSSendID.Value, pTran);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //��֯������SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("delete from [SMS_send] where SMS_send_id=@SMSSendID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@SMSSendID",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //ִ�����
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return;
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(SMSSendEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (!item.SMSSendID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.SMSSendID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(SMSSendEntity[] pEntities)
        {
            Delete(pEntities, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            if (pIDs == null || pIDs.Length == 0)
                return;
            //��֯������SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("{0},", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("delete from [SMS_send] where SMS_send_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //ִ�����
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public SMSSendEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
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
            //ִ��SQL
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
            //���ؽ��
            return list.ToArray();
        }
        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<SMSSendEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //��ҳSQL
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
                pagedSql.AppendFormat(" [SMSSendID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [SMSSend] where 1=1 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [SMSSend] where 1=1 ");
            //��������
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
            //ȡָ��ҳ������
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //ִ����䲢���ؽ��
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
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public SMSSendEntity[] QueryByEntity(SMSSendEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<SMSSendEntity> PagedQueryByEntity(SMSSendEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region ���߷���
        /// <summary>
        /// ����ʵ���Null�������ɲ�ѯ������
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(SMSSendEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
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
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out SMSSendEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
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
