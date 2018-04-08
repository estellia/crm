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
    /// ���ݷ��ʣ�  
    /// ��AppVersion�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class AppVersionDAO : CommonDAO, ICRUDable<AppVersionEntity>, IQueryable<AppVersionEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public AppVersionDAO(BasicUserInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(AppVersionEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(AppVersionEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�
            pEntity.IsDelete = 0;
            pEntity.CreateTime = DateTime.Now;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [AppVersion](");
            strSql.Append("[AppID],[AppCode],[Name],[Version],[Description],[AndroidPackageUrl],[IOSPackageUrl],[ClientID],[UserID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[AppVersionID])");
            strSql.Append(" values (");
            strSql.Append("@AppID,@AppCode,@Name,@Version,@Description,@AndroidPackageUrl,@IOSPackageUrl,@ClientID,@UserID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@AppVersionID)");

            Guid? pkGuid;
            if (pEntity.AppVersionID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.AppVersionID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@AppID",SqlDbType.Int),
					new SqlParameter("@AppCode",SqlDbType.NVarChar),
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@AndroidPackageUrl",SqlDbType.NVarChar),
					new SqlParameter("@IOSPackageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@AppVersionID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.AppID;
            parameters[1].Value = pEntity.AppCode;
            parameters[2].Value = pEntity.Name;
            parameters[3].Value = pEntity.Version;
            parameters[4].Value = pEntity.Description;
            parameters[5].Value = pEntity.AndroidPackageUrl;
            parameters[6].Value = pEntity.IOSPackageUrl;
            parameters[7].Value = pEntity.ClientID;
            parameters[8].Value = pEntity.UserID;
            parameters[9].Value = pEntity.CreateBy;
            parameters[10].Value = pEntity.CreateTime;
            parameters[11].Value = pEntity.LastUpdateBy;
            parameters[12].Value = pEntity.LastUpdateTime;
            parameters[13].Value = pEntity.IsDelete;
            parameters[14].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.AppVersionID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public AppVersionEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AppVersion] where AppVersionID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            AppVersionEntity m = null;
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
        public AppVersionEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AppVersion] where 1=1  and isdelete=0");
            //��ȡ����
            List<AppVersionEntity> list = new List<AppVersionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    AppVersionEntity m;
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
        public void Update(AppVersionEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(AppVersionEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.AppVersionID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [AppVersion] set ");
            if (pIsUpdateNullField || pEntity.AppID != null)
                strSql.Append("[AppID]=@AppID,");
            if (pIsUpdateNullField || pEntity.AppCode != null)
                strSql.Append("[AppCode]=@AppCode,");
            if (pIsUpdateNullField || pEntity.Name != null)
                strSql.Append("[Name]=@Name,");
            if (pIsUpdateNullField || pEntity.Version != null)
                strSql.Append("[Version]=@Version,");
            if (pIsUpdateNullField || pEntity.Description != null)
                strSql.Append("[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.AndroidPackageUrl != null)
                strSql.Append("[AndroidPackageUrl]=@AndroidPackageUrl,");
            if (pIsUpdateNullField || pEntity.IOSPackageUrl != null)
                strSql.Append("[IOSPackageUrl]=@IOSPackageUrl,");
            if (pIsUpdateNullField || pEntity.ClientID != null)
                strSql.Append("[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.UserID != null)
                strSql.Append("[UserID]=@UserID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where AppVersionID=@AppVersionID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@AppID",SqlDbType.Int),
					new SqlParameter("@AppCode",SqlDbType.NVarChar),
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@AndroidPackageUrl",SqlDbType.NVarChar),
					new SqlParameter("@IOSPackageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@AppVersionID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.AppID;
            parameters[1].Value = pEntity.AppCode;
            parameters[2].Value = pEntity.Name;
            parameters[3].Value = pEntity.Version;
            parameters[4].Value = pEntity.Description;
            parameters[5].Value = pEntity.AndroidPackageUrl;
            parameters[6].Value = pEntity.IOSPackageUrl;
            parameters[7].Value = pEntity.ClientID;
            parameters[8].Value = pEntity.UserID;
            parameters[9].Value = pEntity.LastUpdateBy;
            parameters[10].Value = pEntity.LastUpdateTime;
            parameters[11].Value = pEntity.AppVersionID;

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
        public void Update(AppVersionEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(AppVersionEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(AppVersionEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.AppVersionID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.AppVersionID.Value, pTran);
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
            sql.AppendLine("update [AppVersion] set  isdelete=1 where AppVersionID=@AppVersionID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@AppVersionID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(AppVersionEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.AppVersionID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.AppVersionID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(AppVersionEntity[] pEntities)
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
            sql.AppendLine("update [AppVersion] set  isdelete=1 where AppVersionID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public AppVersionEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AppVersion] where 1=1  and isdelete=0 ");
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
            List<AppVersionEntity> list = new List<AppVersionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    AppVersionEntity m;
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
        public PagedQueryResult<AppVersionEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [AppVersionID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [AppVersion] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [AppVersion] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<AppVersionEntity> result = new PagedQueryResult<AppVersionEntity>();
            List<AppVersionEntity> list = new List<AppVersionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    AppVersionEntity m;
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
        public AppVersionEntity[] QueryByEntity(AppVersionEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<AppVersionEntity> PagedQueryByEntity(AppVersionEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(AppVersionEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.AppVersionID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppVersionID", Value = pQueryEntity.AppVersionID });
            if (pQueryEntity.AppID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppID", Value = pQueryEntity.AppID });
            if (pQueryEntity.AppCode != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppCode", Value = pQueryEntity.AppCode });
            if (pQueryEntity.Name != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Name", Value = pQueryEntity.Name });
            if (pQueryEntity.Version != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Version", Value = pQueryEntity.Version });
            if (pQueryEntity.Description != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.AndroidPackageUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AndroidPackageUrl", Value = pQueryEntity.AndroidPackageUrl });
            if (pQueryEntity.IOSPackageUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IOSPackageUrl", Value = pQueryEntity.IOSPackageUrl });
            if (pQueryEntity.ClientID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.UserID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserID", Value = pQueryEntity.UserID });
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
        protected void Load(IDataReader pReader, out AppVersionEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new AppVersionEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["AppVersionID"] != DBNull.Value)
            {
                pInstance.AppVersionID = (Guid)pReader["AppVersionID"];
            }
            if (pReader["AppID"] != DBNull.Value)
            {
                pInstance.AppID = Convert.ToInt32(pReader["AppID"]);
            }
            if (pReader["AppCode"] != DBNull.Value)
            {
                pInstance.AppCode = Convert.ToString(pReader["AppCode"]);
            }
            if (pReader["Name"] != DBNull.Value)
            {
                pInstance.Name = Convert.ToString(pReader["Name"]);
            }
            if (pReader["Version"] != DBNull.Value)
            {
                pInstance.Version = Convert.ToString(pReader["Version"]);
            }
            if (pReader["Description"] != DBNull.Value)
            {
                pInstance.Description = Convert.ToString(pReader["Description"]);
            }
            if (pReader["AndroidPackageUrl"] != DBNull.Value)
            {
                pInstance.AndroidPackageUrl = Convert.ToString(pReader["AndroidPackageUrl"]);
            }
            if (pReader["IOSPackageUrl"] != DBNull.Value)
            {
                pInstance.IOSPackageUrl = Convert.ToString(pReader["IOSPackageUrl"]);
            }
            if (pReader["ClientID"] != DBNull.Value)
            {
                pInstance.ClientID = Convert.ToString(pReader["ClientID"]);
            }
            if (pReader["UserID"] != DBNull.Value)
            {
                pInstance.UserID = Convert.ToString(pReader["UserID"]);
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
