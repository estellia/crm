/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/9 13:57:27
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
using JIT.Utility.MobileDeviceManagement.Base;
using JIT.Utility.MobileDeviceManagement.Entity;

namespace JIT.Utility.MobileDeviceManagement.DataAccess
{
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��MobileCommandRecord�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MobileCommandRecordDAO : BaseMobileDeviceManagementDAO, ICRUDable<MobileCommandRecordEntity>, IQueryable<MobileCommandRecordEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MobileCommandRecordDAO(MobileDeviceManagementUserInfo pUserInfo, ISQLHelper pSQLHelper)
            : base(pUserInfo, true, pSQLHelper)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(MobileCommandRecordEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(MobileCommandRecordEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [MobileCommandRecord](");
            strSql.Append("[ClientID],[UserID],[AppCode],[AppVersion],[CommandText],[Status],[RequestCount],[ResponseJson],[ResponseCode],[CommandResponseCount],[Period],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[RecordID])");
            strSql.Append(" values (");
            strSql.Append("@ClientID,@UserID,@AppCode,@AppVersion,@CommandText,@Status,@RequestCount,@ResponseJson,@ResponseCode,@CommandResponseCount,@Period,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@RecordID)");

            Guid? pkGuid;
            if (pEntity.RecordID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.RecordID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@AppCode",SqlDbType.NVarChar),
					new SqlParameter("@AppVersion",SqlDbType.Decimal),
					new SqlParameter("@CommandText",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@RequestCount",SqlDbType.Int),
					new SqlParameter("@ResponseJson",SqlDbType.NVarChar),
					new SqlParameter("@ResponseCode",SqlDbType.Int),
					new SqlParameter("@CommandResponseCount",SqlDbType.Int),
					new SqlParameter("@Period",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@RecordID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.ClientID;
            parameters[1].Value = pEntity.UserID;
            parameters[2].Value = pEntity.AppCode;
            parameters[3].Value = pEntity.AppVersion;
            parameters[4].Value = pEntity.CommandText;
            parameters[5].Value = pEntity.Status;
            parameters[6].Value = pEntity.RequestCount;
            parameters[7].Value = pEntity.ResponseJson;
            parameters[8].Value = pEntity.ResponseCode;
            parameters[9].Value = pEntity.CommandResponseCount;
            parameters[10].Value = pEntity.Period;
            parameters[11].Value = pEntity.CreateBy;
            parameters[12].Value = pEntity.CreateTime;
            parameters[13].Value = pEntity.LastUpdateBy;
            parameters[14].Value = pEntity.LastUpdateTime;
            parameters[15].Value = pEntity.IsDelete;
            parameters[16].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.RecordID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public MobileCommandRecordEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MobileCommandRecord] where RecordID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            MobileCommandRecordEntity m = null;
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
        public MobileCommandRecordEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MobileCommandRecord] where isdelete=0");
            //��ȡ����
            List<MobileCommandRecordEntity> list = new List<MobileCommandRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileCommandRecordEntity m;
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
        public void Update(MobileCommandRecordEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(MobileCommandRecordEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.RecordID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MobileCommandRecord] set ");
            if (pIsUpdateNullField || pEntity.ClientID != null)
                strSql.Append("[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.UserID != null)
                strSql.Append("[UserID]=@UserID,");
            if (pIsUpdateNullField || pEntity.AppCode != null)
                strSql.Append("[AppCode]=@AppCode,");
            if (pIsUpdateNullField || pEntity.AppVersion != null)
                strSql.Append("[AppVersion]=@AppVersion,");
            if (pIsUpdateNullField || pEntity.CommandText != null)
                strSql.Append("[CommandText]=@CommandText,");
            if (pIsUpdateNullField || pEntity.Status != null)
                strSql.Append("[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.RequestCount != null)
                strSql.Append("[RequestCount]=@RequestCount,");
            if (pIsUpdateNullField || pEntity.ResponseJson != null)
                strSql.Append("[ResponseJson]=@ResponseJson,");
            if (pIsUpdateNullField || pEntity.ResponseCode != null)
                strSql.Append("[ResponseCode]=@ResponseCode,");
            if (pIsUpdateNullField || pEntity.CommandResponseCount != null)
                strSql.Append("[CommandResponseCount]=@CommandResponseCount,");
            if (pIsUpdateNullField || pEntity.Period != null)
                strSql.Append("[Period]=@Period,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where RecordID=@RecordID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@AppCode",SqlDbType.NVarChar),
					new SqlParameter("@AppVersion",SqlDbType.Decimal),
					new SqlParameter("@CommandText",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@RequestCount",SqlDbType.Int),
					new SqlParameter("@ResponseJson",SqlDbType.NVarChar),
					new SqlParameter("@ResponseCode",SqlDbType.Int),
					new SqlParameter("@CommandResponseCount",SqlDbType.Int),
					new SqlParameter("@Period",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@RecordID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.ClientID;
            parameters[1].Value = pEntity.UserID;
            parameters[2].Value = pEntity.AppCode;
            parameters[3].Value = pEntity.AppVersion;
            parameters[4].Value = pEntity.CommandText;
            parameters[5].Value = pEntity.Status;
            parameters[6].Value = pEntity.RequestCount;
            parameters[7].Value = pEntity.ResponseJson;
            parameters[8].Value = pEntity.ResponseCode;
            parameters[9].Value = pEntity.CommandResponseCount;
            parameters[10].Value = pEntity.Period;
            parameters[11].Value = pEntity.LastUpdateBy;
            parameters[12].Value = pEntity.LastUpdateTime;
            parameters[13].Value = pEntity.RecordID;

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
        public void Update(MobileCommandRecordEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MobileCommandRecordEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(MobileCommandRecordEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.RecordID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.RecordID.Value, pTran);
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
            sql.AppendLine("update [MobileCommandRecord] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where RecordID=@RecordID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.Int,Value=Convert.ToInt32(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@RecordID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(MobileCommandRecordEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.RecordID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.RecordID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(MobileCommandRecordEntity[] pEntities)
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
                primaryKeys.AppendFormat("'{0}',", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [MobileCommandRecord] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy=" + CurrentUserInfo.UserID + ",IsDelete=1 where RecordID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public MobileCommandRecordEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MobileCommandRecord] where isdelete=0 ");
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
            List<MobileCommandRecordEntity> list = new List<MobileCommandRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileCommandRecordEntity m;
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
        public PagedQueryResult<MobileCommandRecordEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [RecordID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [MobileCommandRecord] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [MobileCommandRecord] where isdelete=0 ");
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
            PagedQueryResult<MobileCommandRecordEntity> result = new PagedQueryResult<MobileCommandRecordEntity>();
            List<MobileCommandRecordEntity> list = new List<MobileCommandRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileCommandRecordEntity m;
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
        public MobileCommandRecordEntity[] QueryByEntity(MobileCommandRecordEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<MobileCommandRecordEntity> PagedQueryByEntity(MobileCommandRecordEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(MobileCommandRecordEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.RecordID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RecordID", Value = pQueryEntity.RecordID });
            if (pQueryEntity.ClientID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.UserID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserID", Value = pQueryEntity.UserID });
            if (pQueryEntity.AppCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppCode", Value = pQueryEntity.AppCode });
            if (pQueryEntity.AppVersion != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppVersion", Value = pQueryEntity.AppVersion });
            if (pQueryEntity.CommandText != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommandText", Value = pQueryEntity.CommandText });
            if (pQueryEntity.Status != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.RequestCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RequestCount", Value = pQueryEntity.RequestCount });
            if (pQueryEntity.ResponseJson != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ResponseJson", Value = pQueryEntity.ResponseJson });
            if (pQueryEntity.ResponseCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ResponseCode", Value = pQueryEntity.ResponseCode });
            if (pQueryEntity.CommandResponseCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommandResponseCount", Value = pQueryEntity.CommandResponseCount });
            if (pQueryEntity.Period != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Period", Value = pQueryEntity.Period });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out MobileCommandRecordEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new MobileCommandRecordEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["RecordID"] != DBNull.Value)
            {
                pInstance.RecordID = (Guid)pReader["RecordID"];
            }
            if (pReader["ClientID"] != DBNull.Value)
            {
                pInstance.ClientID = Convert.ToString(pReader["ClientID"]);
            }
            if (pReader["UserID"] != DBNull.Value)
            {
                pInstance.UserID = Convert.ToString(pReader["UserID"]);
            }
            if (pReader["AppCode"] != DBNull.Value)
            {
                pInstance.AppCode = Convert.ToString(pReader["AppCode"]);
            }
            if (pReader["AppVersion"] != DBNull.Value)
            {
                pInstance.AppVersion = Convert.ToDecimal(pReader["AppVersion"]);
            }
            if (pReader["CommandText"] != DBNull.Value)
            {
                pInstance.CommandText = Convert.ToString(pReader["CommandText"]);
            }
            if (pReader["Status"] != DBNull.Value)
            {
                pInstance.Status = Convert.ToInt32(pReader["Status"]);
            }
            if (pReader["RequestCount"] != DBNull.Value)
            {
                pInstance.RequestCount = Convert.ToInt32(pReader["RequestCount"]);
            }
            if (pReader["ResponseJson"] != DBNull.Value)
            {
                pInstance.ResponseJson = Convert.ToString(pReader["ResponseJson"]);
            }
            if (pReader["ResponseCode"] != DBNull.Value)
            {
                pInstance.ResponseCode = Convert.ToInt32(pReader["ResponseCode"]);
            }
            if (pReader["CommandResponseCount"] != DBNull.Value)
            {
                pInstance.CommandResponseCount = Convert.ToInt32(pReader["CommandResponseCount"]);
            }
            if (pReader["Period"] != DBNull.Value)
            {
                pInstance.Period = Convert.ToInt32(pReader["Period"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }

        }
        #endregion
    }
}
