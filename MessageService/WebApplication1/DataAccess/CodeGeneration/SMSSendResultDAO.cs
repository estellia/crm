/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/4 16:34:30
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
using JIT.SMSReportService.Entity;
using JIT.SMSReportService.Base;

namespace JIT.SMSReportService.DataAccess
{
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��SMS_SendResult�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SMSSendResultDAO : BaseDAO<BasicUserInfo>, ICRUDable<SMSSendResultEntity>, IQueryable<SMSSendResultEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SMSSendResultDAO(BasicUserInfo pUserInfo)
            : base(pUserInfo, new ConnectionStringManager())
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(SMSSendResultEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(SMSSendResultEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [SMS_SendResult](");
            strSql.Append("[Command],[Spid],[Sppassword],[Spsc],[Mtmsgid],[Mtstat],[Mterrocde],[Rttime],[CreateTime],[CreateBy],[LastUpdateTime],[IsDelete],[LastUpdateBy])");
            strSql.Append(" values (");
            strSql.Append("@Command,@Spid,@Sppassword,@Spsc,@Mtmsgid,@Mtstat,@Mterrocde,@Rttime,@CreateTime,@CreateBy,@LastUpdateTime,@IsDelete,@LastUpdateBy)");
            strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@Command",SqlDbType.NVarChar),
					new SqlParameter("@Spid",SqlDbType.NVarChar),
					new SqlParameter("@Sppassword",SqlDbType.NVarChar),
					new SqlParameter("@Spsc",SqlDbType.NVarChar),
					new SqlParameter("@Mtmsgid",SqlDbType.NVarChar),
					new SqlParameter("@Mtstat",SqlDbType.NVarChar),
					new SqlParameter("@Mterrocde",SqlDbType.NVarChar),
					new SqlParameter("@Rttime",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.Command;
            parameters[1].Value = pEntity.Spid;
            parameters[2].Value = pEntity.Sppassword;
            parameters[3].Value = pEntity.Spsc;
            parameters[4].Value = pEntity.Mtmsgid;
            parameters[5].Value = pEntity.Mtstat;
            parameters[6].Value = pEntity.Mterrocde;
            parameters[7].Value = pEntity.Rttime;
            parameters[8].Value = pEntity.CreateTime;
            parameters[9].Value = pEntity.CreateBy;
            parameters[10].Value = pEntity.LastUpdateTime;
            parameters[11].Value = pEntity.IsDelete;
            parameters[12].Value = pEntity.LastUpdateBy;

            //ִ�в��������д
            object result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            pEntity.SendResultID = Convert.ToInt32(result);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public SMSSendResultEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SMS_SendResult] where SendResultID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            SMSSendResultEntity m = null;
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
        public SMSSendResultEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SMS_SendResult] where isdelete=0");
            //��ȡ����
            List<SMSSendResultEntity> list = new List<SMSSendResultEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SMSSendResultEntity m;
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
        public void Update(SMSSendResultEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(SMSSendResultEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SendResultID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SMS_SendResult] set ");
            if (pIsUpdateNullField || pEntity.Command != null)
                strSql.Append("[Command]=@Command,");
            if (pIsUpdateNullField || pEntity.Spid != null)
                strSql.Append("[Spid]=@Spid,");
            if (pIsUpdateNullField || pEntity.Sppassword != null)
                strSql.Append("[Sppassword]=@Sppassword,");
            if (pIsUpdateNullField || pEntity.Spsc != null)
                strSql.Append("[Spsc]=@Spsc,");
            if (pIsUpdateNullField || pEntity.Mtmsgid != null)
                strSql.Append("[Mtmsgid]=@Mtmsgid,");
            if (pIsUpdateNullField || pEntity.Mtstat != null)
                strSql.Append("[Mtstat]=@Mtstat,");
            if (pIsUpdateNullField || pEntity.Mterrocde != null)
                strSql.Append("[Mterrocde]=@Mterrocde,");
            if (pIsUpdateNullField || pEntity.Rttime != null)
                strSql.Append("[Rttime]=@Rttime,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where SendResultID=@SendResultID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Command",SqlDbType.NVarChar),
					new SqlParameter("@Spid",SqlDbType.NVarChar),
					new SqlParameter("@Sppassword",SqlDbType.NVarChar),
					new SqlParameter("@Spsc",SqlDbType.NVarChar),
					new SqlParameter("@Mtmsgid",SqlDbType.NVarChar),
					new SqlParameter("@Mtstat",SqlDbType.NVarChar),
					new SqlParameter("@Mterrocde",SqlDbType.NVarChar),
					new SqlParameter("@Rttime",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@SendResultID",SqlDbType.Int)
            };
            parameters[0].Value = pEntity.Command;
            parameters[1].Value = pEntity.Spid;
            parameters[2].Value = pEntity.Sppassword;
            parameters[3].Value = pEntity.Spsc;
            parameters[4].Value = pEntity.Mtmsgid;
            parameters[5].Value = pEntity.Mtstat;
            parameters[6].Value = pEntity.Mterrocde;
            parameters[7].Value = pEntity.Rttime;
            parameters[8].Value = pEntity.LastUpdateTime;
            parameters[9].Value = pEntity.LastUpdateBy;
            parameters[10].Value = pEntity.SendResultID;

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
        public void Update(SMSSendResultEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(SMSSendResultEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(SMSSendResultEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.SendResultID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.SendResultID.Value, pTran);
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
            sql.AppendLine("update [SMS_SendResult] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where SendResultID=@SendResultID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.Int,Value=Convert.ToInt32(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@SendResultID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(SMSSendResultEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (!item.SendResultID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.SendResultID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(SMSSendResultEntity[] pEntities)
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
            sql.AppendLine("update [SMS_SendResult] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy=" + CurrentUserInfo.UserID + ",IsDelete=1 where SendResultID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public SMSSendResultEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SMS_SendResult] where isdelete=0 ");
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
            List<SMSSendResultEntity> list = new List<SMSSendResultEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SMSSendResultEntity m;
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
        public PagedQueryResult<SMSSendResultEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [SendResultID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [SMSSendResult] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [SMSSendResult] where isdelete=0 ");
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
            PagedQueryResult<SMSSendResultEntity> result = new PagedQueryResult<SMSSendResultEntity>();
            List<SMSSendResultEntity> list = new List<SMSSendResultEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    SMSSendResultEntity m;
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
        public SMSSendResultEntity[] QueryByEntity(SMSSendResultEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<SMSSendResultEntity> PagedQueryByEntity(SMSSendResultEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(SMSSendResultEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.SendResultID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendResultID", Value = pQueryEntity.SendResultID });
            if (pQueryEntity.Command != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Command", Value = pQueryEntity.Command });
            if (pQueryEntity.Spid != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Spid", Value = pQueryEntity.Spid });
            if (pQueryEntity.Sppassword != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sppassword", Value = pQueryEntity.Sppassword });
            if (pQueryEntity.Spsc != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Spsc", Value = pQueryEntity.Spsc });
            if (pQueryEntity.Mtmsgid != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Mtmsgid", Value = pQueryEntity.Mtmsgid });
            if (pQueryEntity.Mtstat != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Mtstat", Value = pQueryEntity.Mtstat });
            if (pQueryEntity.Mterrocde != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Mterrocde", Value = pQueryEntity.Mterrocde });
            if (pQueryEntity.Rttime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Rttime", Value = pQueryEntity.Rttime });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out SMSSendResultEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new SMSSendResultEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["SendResultID"] != DBNull.Value)
            {
                pInstance.SendResultID = Convert.ToInt32(pReader["SendResultID"]);
            }
            if (pReader["Command"] != DBNull.Value)
            {
                pInstance.Command = Convert.ToString(pReader["Command"]);
            }
            if (pReader["Spid"] != DBNull.Value)
            {
                pInstance.Spid = Convert.ToString(pReader["Spid"]);
            }
            if (pReader["Sppassword"] != DBNull.Value)
            {
                pInstance.Sppassword = Convert.ToString(pReader["Sppassword"]);
            }
            if (pReader["Spsc"] != DBNull.Value)
            {
                pInstance.Spsc = Convert.ToString(pReader["Spsc"]);
            }
            if (pReader["Mtmsgid"] != DBNull.Value)
            {
                pInstance.Mtmsgid = Convert.ToString(pReader["Mtmsgid"]);
            }
            if (pReader["Mtstat"] != DBNull.Value)
            {
                pInstance.Mtstat = Convert.ToString(pReader["Mtstat"]);
            }
            if (pReader["Mterrocde"] != DBNull.Value)
            {
                pInstance.Mterrocde = Convert.ToString(pReader["Mterrocde"]);
            }
            if (pReader["Rttime"] != DBNull.Value)
            {
                pInstance.Rttime = Convert.ToString(pReader["Rttime"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }

        }
        #endregion
    }
}
