/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/17 11:16:35
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
using JIT.Utility.AppVersion.DataAccess.Base;
using JIT.Utility.AppVersion.Entity;

namespace JIT.Utility.AppVersion.DataAccess
{
    /// <summary>
    /// ���ݷ��ʣ� ��Ȧ�� 
    /// ��BusinessZone�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class BusinessZoneDAO : CommonDAO, ICRUDable<BusinessZoneEntity>, IQueryable<BusinessZoneEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public BusinessZoneDAO(BasicUserInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(BusinessZoneEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(BusinessZoneEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [BusinessZone](");
            strSql.Append("[BusinessZoneCode],[BusinessZoneName],[Type],[IsLeaf],[ParentID],[ServiceUrl],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete])");
            strSql.Append(" values (");
            strSql.Append("@BusinessZoneCode,@BusinessZoneName,@Type,@IsLeaf,@ParentID,@ServiceUrl,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete)");
            strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@BusinessZoneCode",SqlDbType.NVarChar),
					new SqlParameter("@BusinessZoneName",SqlDbType.NVarChar),
					new SqlParameter("@Type",SqlDbType.Int),
					new SqlParameter("@IsLeaf",SqlDbType.Bit),
					new SqlParameter("@ParentID",SqlDbType.Int),
					new SqlParameter("@ServiceUrl",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int)
            };
            parameters[0].Value = pEntity.BusinessZoneCode;
            parameters[1].Value = pEntity.BusinessZoneName;
            parameters[2].Value = pEntity.Type;
            parameters[3].Value = pEntity.IsLeaf;
            parameters[4].Value = pEntity.ParentID;
            parameters[5].Value = pEntity.ServiceUrl;
            parameters[6].Value = pEntity.CreateBy;
            parameters[7].Value = pEntity.CreateTime;
            parameters[8].Value = pEntity.LastUpdateBy;
            parameters[9].Value = pEntity.LastUpdateTime;
            parameters[10].Value = pEntity.IsDelete;

            //ִ�в��������д
            object result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            pEntity.BusinessZoneID = Convert.ToInt32(result);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public BusinessZoneEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [BusinessZone] where BusinessZoneID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            BusinessZoneEntity m = null;
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
        public BusinessZoneEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [BusinessZone] where isdelete=0");
            //��ȡ����
            List<BusinessZoneEntity> list = new List<BusinessZoneEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    BusinessZoneEntity m;
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
        public void Update(BusinessZoneEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(BusinessZoneEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.BusinessZoneID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [BusinessZone] set ");
            if (pIsUpdateNullField || pEntity.BusinessZoneCode != null)
                strSql.Append("[BusinessZoneCode]=@BusinessZoneCode,");
            if (pIsUpdateNullField || pEntity.BusinessZoneName != null)
                strSql.Append("[BusinessZoneName]=@BusinessZoneName,");
            if (pIsUpdateNullField || pEntity.Type != null)
                strSql.Append("[Type]=@Type,");
            if (pIsUpdateNullField || pEntity.IsLeaf != null)
                strSql.Append("[IsLeaf]=@IsLeaf,");
            if (pIsUpdateNullField || pEntity.ParentID != null)
                strSql.Append("[ParentID]=@ParentID,");
            if (pIsUpdateNullField || pEntity.ServiceUrl != null)
                strSql.Append("[ServiceUrl]=@ServiceUrl,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where BusinessZoneID=@BusinessZoneID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@BusinessZoneCode",SqlDbType.NVarChar),
					new SqlParameter("@BusinessZoneName",SqlDbType.NVarChar),
					new SqlParameter("@Type",SqlDbType.Int),
					new SqlParameter("@IsLeaf",SqlDbType.Bit),
					new SqlParameter("@ParentID",SqlDbType.Int),
					new SqlParameter("@ServiceUrl",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@BusinessZoneID",SqlDbType.Int)
            };
            parameters[0].Value = pEntity.BusinessZoneCode;
            parameters[1].Value = pEntity.BusinessZoneName;
            parameters[2].Value = pEntity.Type;
            parameters[3].Value = pEntity.IsLeaf;
            parameters[4].Value = pEntity.ParentID;
            parameters[5].Value = pEntity.ServiceUrl;
            parameters[6].Value = pEntity.LastUpdateBy;
            parameters[7].Value = pEntity.LastUpdateTime;
            parameters[8].Value = pEntity.BusinessZoneID;

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
        public void Update(BusinessZoneEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(BusinessZoneEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(BusinessZoneEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.BusinessZoneID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.BusinessZoneID.Value, pTran);
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
            sql.AppendLine("update [BusinessZone] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where BusinessZoneID=@BusinessZoneID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.Int,Value=Convert.ToInt32(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@BusinessZoneID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(BusinessZoneEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.BusinessZoneID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.BusinessZoneID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(BusinessZoneEntity[] pEntities)
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
            sql.AppendLine("update [BusinessZone] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy=" + CurrentUserInfo.UserID + ",IsDelete=1 where BusinessZoneID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public BusinessZoneEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [BusinessZone] where isdelete=0 ");
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
            List<BusinessZoneEntity> list = new List<BusinessZoneEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    BusinessZoneEntity m;
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
        public PagedQueryResult<BusinessZoneEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [BusinessZoneID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [BusinessZone] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [BusinessZone] where isdelete=0 ");
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
            PagedQueryResult<BusinessZoneEntity> result = new PagedQueryResult<BusinessZoneEntity>();
            List<BusinessZoneEntity> list = new List<BusinessZoneEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    BusinessZoneEntity m;
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
        public BusinessZoneEntity[] QueryByEntity(BusinessZoneEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<BusinessZoneEntity> PagedQueryByEntity(BusinessZoneEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(BusinessZoneEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.BusinessZoneID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BusinessZoneID", Value = pQueryEntity.BusinessZoneID });
            if (pQueryEntity.BusinessZoneCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BusinessZoneCode", Value = pQueryEntity.BusinessZoneCode });
            if (pQueryEntity.BusinessZoneName != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BusinessZoneName", Value = pQueryEntity.BusinessZoneName });
            if (pQueryEntity.Type != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Type", Value = pQueryEntity.Type });
            if (pQueryEntity.IsLeaf != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsLeaf", Value = pQueryEntity.IsLeaf });
            if (pQueryEntity.ParentID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentID", Value = pQueryEntity.ParentID });
            if (pQueryEntity.ServiceUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceUrl", Value = pQueryEntity.ServiceUrl });
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
        protected void Load(SqlDataReader pReader, out BusinessZoneEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new BusinessZoneEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["BusinessZoneID"] != DBNull.Value)
            {
                pInstance.BusinessZoneID = Convert.ToInt32(pReader["BusinessZoneID"]);
            }
            if (pReader["BusinessZoneCode"] != DBNull.Value)
            {
                pInstance.BusinessZoneCode = Convert.ToString(pReader["BusinessZoneCode"]);
            }
            if (pReader["BusinessZoneName"] != DBNull.Value)
            {
                pInstance.BusinessZoneName = Convert.ToString(pReader["BusinessZoneName"]);
            }
            if (pReader["Type"] != DBNull.Value)
            {
                pInstance.Type = Convert.ToInt32(pReader["Type"]);
            }
            if (pReader["IsLeaf"] != DBNull.Value)
            {
                pInstance.IsLeaf = Convert.ToBoolean(pReader["IsLeaf"]);
            }
            if (pReader["ParentID"] != DBNull.Value)
            {
                pInstance.ParentID = Convert.ToInt32(pReader["ParentID"]);
            }
            if (pReader["ServiceUrl"] != DBNull.Value)
            {
                pInstance.ServiceUrl = Convert.ToString(pReader["ServiceUrl"]);
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
