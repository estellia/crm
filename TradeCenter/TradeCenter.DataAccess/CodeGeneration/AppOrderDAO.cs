/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/8 16:14:10
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
using JIT.TradeCenter.Entity;
using JIT.TradeCenter.DataAccess.Base;

namespace JIT.TradeCenter.DataAccess
{
    /// <summary>
    /// ���ݷ��ʣ� Ӧ�ö����� 
    /// ��AppOrder�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class AppOrderDAO : BaseTradeCenerDAO, ICRUDable<AppOrderEntity>, IQueryable<AppOrderEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public AppOrderDAO(BasicUserInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(AppOrderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(AppOrderEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [AppOrder](");
            strSql.Append("[AppID],[AppClientID],[AppUserID],[PayChannelID],[AppOrderID],[AppOrderDesc],[AppOrderTime],[AppOrderAmount],[MobileNO],[Currency],[ErrorMessage],[PayUrl],[IsNotified],[NotifyCount],[NextNotifyTime],[Status],[IsDelete],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime])");
            strSql.Append(" values (");
            strSql.Append("@AppID,@AppClientID,@AppUserID,@PayChannelID,@AppOrderID,@AppOrderDesc,@AppOrderTime,@AppOrderAmount,@MobileNO,@Currency,@ErrorMessage,@PayUrl,@IsNotified,@NotifyCount,@NextNotifyTime,@Status,@IsDelete,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime)");
            strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@AppID",SqlDbType.Int),
					new SqlParameter("@AppClientID",SqlDbType.NVarChar),
					new SqlParameter("@AppUserID",SqlDbType.NVarChar),
					new SqlParameter("@PayChannelID",SqlDbType.Int),
					new SqlParameter("@AppOrderID",SqlDbType.NVarChar),
					new SqlParameter("@AppOrderDesc",SqlDbType.NVarChar),
					new SqlParameter("@AppOrderTime",SqlDbType.DateTime),
					new SqlParameter("@AppOrderAmount",SqlDbType.Int),
					new SqlParameter("@MobileNO",SqlDbType.NVarChar),
					new SqlParameter("@Currency",SqlDbType.Int),
					new SqlParameter("@ErrorMessage",SqlDbType.NVarChar),
					new SqlParameter("@PayUrl",SqlDbType.NVarChar),
					new SqlParameter("@IsNotified",SqlDbType.Bit),
					new SqlParameter("@NotifyCount",SqlDbType.Int),
					new SqlParameter("@NextNotifyTime",SqlDbType.DateTime),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime)
            };
            parameters[0].Value = pEntity.AppID;
            parameters[1].Value = pEntity.AppClientID;
            parameters[2].Value = pEntity.AppUserID;
            parameters[3].Value = pEntity.PayChannelID;
            parameters[4].Value = pEntity.AppOrderID;
            parameters[5].Value = pEntity.AppOrderDesc;
            parameters[6].Value = pEntity.AppOrderTime;
            parameters[7].Value = pEntity.AppOrderAmount;
            parameters[8].Value = pEntity.MobileNO;
            parameters[9].Value = pEntity.Currency;
            parameters[10].Value = pEntity.ErrorMessage;
            parameters[11].Value = pEntity.PayUrl;
            parameters[12].Value = pEntity.IsNotified;
            parameters[13].Value = pEntity.NotifyCount;
            parameters[14].Value = pEntity.NextNotifyTime;
            parameters[15].Value = pEntity.Status;
            parameters[16].Value = pEntity.IsDelete;
            parameters[17].Value = pEntity.CreateBy;
            parameters[18].Value = pEntity.CreateTime;
            parameters[19].Value = pEntity.LastUpdateBy;
            parameters[20].Value = pEntity.LastUpdateTime;

            //ִ�в��������д
            object result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            pEntity.OrderID = Convert.ToInt32(result);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public AppOrderEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AppOrder] where OrderID={0} and IsDelete=0 ", id.ToString());
            //��ȡ����
            AppOrderEntity m = null;
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
        public AppOrderEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AppOrder] where isdelete=0");
            //��ȡ����
            List<AppOrderEntity> list = new List<AppOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    AppOrderEntity m;
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
        public void Update(AppOrderEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(AppOrderEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [AppOrder] set ");
            if (pIsUpdateNullField || pEntity.AppID != null)
                strSql.Append("[AppID]=@AppID,");
            if (pIsUpdateNullField || pEntity.AppClientID != null)
                strSql.Append("[AppClientID]=@AppClientID,");
            if (pIsUpdateNullField || pEntity.AppUserID != null)
                strSql.Append("[AppUserID]=@AppUserID,");
            if (pIsUpdateNullField || pEntity.PayChannelID != null)
                strSql.Append("[PayChannelID]=@PayChannelID,");
            if (pIsUpdateNullField || pEntity.AppOrderID != null)
                strSql.Append("[AppOrderID]=@AppOrderID,");
            if (pIsUpdateNullField || pEntity.AppOrderDesc != null)
                strSql.Append("[AppOrderDesc]=@AppOrderDesc,");
            if (pIsUpdateNullField || pEntity.AppOrderTime != null)
                strSql.Append("[AppOrderTime]=@AppOrderTime,");
            if (pIsUpdateNullField || pEntity.AppOrderAmount != null)
                strSql.Append("[AppOrderAmount]=@AppOrderAmount,");
            if (pIsUpdateNullField || pEntity.MobileNO != null)
                strSql.Append("[MobileNO]=@MobileNO,");
            if (pIsUpdateNullField || pEntity.Currency != null)
                strSql.Append("[Currency]=@Currency,");
            if (pIsUpdateNullField || pEntity.ErrorMessage != null)
                strSql.Append("[ErrorMessage]=@ErrorMessage,");
            if (pIsUpdateNullField || pEntity.PayUrl != null)
                strSql.Append("[PayUrl]=@PayUrl,");
            if (pIsUpdateNullField || pEntity.IsNotified != null)
                strSql.Append("[IsNotified]=@IsNotified,");
            if (pIsUpdateNullField || pEntity.NotifyCount != null)
                strSql.Append("[NotifyCount]=@NotifyCount,");
            if (pIsUpdateNullField || pEntity.NextNotifyTime != null)
                strSql.Append("[NextNotifyTime]=@NextNotifyTime,");
            if (pIsUpdateNullField || pEntity.Status != null)
                strSql.Append("[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where OrderID=@OrderID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@AppID",SqlDbType.Int),
					new SqlParameter("@AppClientID",SqlDbType.NVarChar),
					new SqlParameter("@AppUserID",SqlDbType.NVarChar),
					new SqlParameter("@PayChannelID",SqlDbType.Int),
					new SqlParameter("@AppOrderID",SqlDbType.NVarChar),
					new SqlParameter("@AppOrderDesc",SqlDbType.NVarChar),
					new SqlParameter("@AppOrderTime",SqlDbType.DateTime),
					new SqlParameter("@AppOrderAmount",SqlDbType.Int),
					new SqlParameter("@MobileNO",SqlDbType.NVarChar),
					new SqlParameter("@Currency",SqlDbType.Int),
					new SqlParameter("@ErrorMessage",SqlDbType.NVarChar),
					new SqlParameter("@PayUrl",SqlDbType.NVarChar),
					new SqlParameter("@IsNotified",SqlDbType.Bit),
					new SqlParameter("@NotifyCount",SqlDbType.Int),
					new SqlParameter("@NextNotifyTime",SqlDbType.DateTime),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@OrderID",SqlDbType.BigInt)
            };
            parameters[0].Value = pEntity.AppID;
            parameters[1].Value = pEntity.AppClientID;
            parameters[2].Value = pEntity.AppUserID;
            parameters[3].Value = pEntity.PayChannelID;
            parameters[4].Value = pEntity.AppOrderID;
            parameters[5].Value = pEntity.AppOrderDesc;
            parameters[6].Value = pEntity.AppOrderTime;
            parameters[7].Value = pEntity.AppOrderAmount;
            parameters[8].Value = pEntity.MobileNO;
            parameters[9].Value = pEntity.Currency;
            parameters[10].Value = pEntity.ErrorMessage;
            parameters[11].Value = pEntity.PayUrl;
            parameters[12].Value = pEntity.IsNotified;
            parameters[13].Value = pEntity.NotifyCount;
            parameters[14].Value = pEntity.NextNotifyTime;
            parameters[15].Value = pEntity.Status;
            parameters[16].Value = pEntity.LastUpdateBy;
            parameters[17].Value = pEntity.LastUpdateTime;
            parameters[18].Value = pEntity.OrderID;

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
        public void Update(AppOrderEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(AppOrderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(AppOrderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.OrderID, pTran);
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
            sql.AppendLine("update [AppOrder] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where OrderID=@OrderID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.Int,Value=Convert.ToInt32(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@OrderID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(AppOrderEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.OrderID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.OrderID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(AppOrderEntity[] pEntities)
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
            sql.AppendLine("update [AppOrder] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy=" + CurrentUserInfo.UserID + ",IsDelete=1 where OrderID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public AppOrderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [AppOrder] where isdelete=0 ");
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
            List<AppOrderEntity> list = new List<AppOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    AppOrderEntity m;
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
        public PagedQueryResult<AppOrderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [AppOrder] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [AppOrder] where isdelete=0 ");
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
            PagedQueryResult<AppOrderEntity> result = new PagedQueryResult<AppOrderEntity>();
            List<AppOrderEntity> list = new List<AppOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    AppOrderEntity m;
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
        public AppOrderEntity[] QueryByEntity(AppOrderEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<AppOrderEntity> PagedQueryByEntity(AppOrderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(AppOrderEntity pQueryEntity)
        {
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderID", Value = pQueryEntity.OrderID });
            if (pQueryEntity.AppID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppID", Value = pQueryEntity.AppID });
            if (pQueryEntity.AppClientID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppClientID", Value = pQueryEntity.AppClientID });
            if (pQueryEntity.AppUserID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppUserID", Value = pQueryEntity.AppUserID });
            if (pQueryEntity.PayChannelID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayChannelID", Value = pQueryEntity.PayChannelID });
            if (pQueryEntity.AppOrderID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppOrderID", Value = pQueryEntity.AppOrderID });
            if (pQueryEntity.AppOrderDesc != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppOrderDesc", Value = pQueryEntity.AppOrderDesc });
            if (pQueryEntity.AppOrderTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppOrderTime", Value = pQueryEntity.AppOrderTime });
            if (pQueryEntity.AppOrderAmount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppOrderAmount", Value = pQueryEntity.AppOrderAmount });
            if (pQueryEntity.MobileNO != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MobileNO", Value = pQueryEntity.MobileNO });
            if (pQueryEntity.Currency != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Currency", Value = pQueryEntity.Currency });
            if (pQueryEntity.ErrorMessage != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ErrorMessage", Value = pQueryEntity.ErrorMessage });
            if (pQueryEntity.PayUrl != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayUrl", Value = pQueryEntity.PayUrl });
            if (pQueryEntity.IsNotified != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsNotified", Value = pQueryEntity.IsNotified });
            if (pQueryEntity.NotifyCount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NotifyCount", Value = pQueryEntity.NotifyCount });
            if (pQueryEntity.NextNotifyTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NextNotifyTime", Value = pQueryEntity.NextNotifyTime });
            if (pQueryEntity.Status != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out AppOrderEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new AppOrderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["OrderID"] != DBNull.Value)
            {
                pInstance.OrderID = Convert.ToInt64(pReader["OrderID"]);
            }
            if (pReader["AppID"] != DBNull.Value)
            {
                pInstance.AppID = Convert.ToInt32(pReader["AppID"]);
            }
            if (pReader["AppClientID"] != DBNull.Value)
            {
                pInstance.AppClientID = Convert.ToString(pReader["AppClientID"]);
            }
            if (pReader["AppUserID"] != DBNull.Value)
            {
                pInstance.AppUserID = Convert.ToString(pReader["AppUserID"]);
            }
            if (pReader["PayChannelID"] != DBNull.Value)
            {
                pInstance.PayChannelID = Convert.ToInt32(pReader["PayChannelID"]);
            }
            if (pReader["AppOrderID"] != DBNull.Value)
            {
                pInstance.AppOrderID = Convert.ToString(pReader["AppOrderID"]);
            }
            if (pReader["AppOrderDesc"] != DBNull.Value)
            {
                pInstance.AppOrderDesc = Convert.ToString(pReader["AppOrderDesc"]);
            }
            if (pReader["AppOrderTime"] != DBNull.Value)
            {
                pInstance.AppOrderTime = Convert.ToDateTime(pReader["AppOrderTime"]);
            }
            if (pReader["AppOrderAmount"] != DBNull.Value)
            {
                pInstance.AppOrderAmount = Convert.ToInt32(pReader["AppOrderAmount"]);
            }
            if (pReader["MobileNO"] != DBNull.Value)
            {
                pInstance.MobileNO = Convert.ToString(pReader["MobileNO"]);
            }
            if (pReader["Currency"] != DBNull.Value)
            {
                pInstance.Currency = Convert.ToInt32(pReader["Currency"]);
            }
            if (pReader["ErrorMessage"] != DBNull.Value)
            {
                pInstance.ErrorMessage = Convert.ToString(pReader["ErrorMessage"]);
            }
            if (pReader["PayUrl"] != DBNull.Value)
            {
                pInstance.PayUrl = Convert.ToString(pReader["PayUrl"]);
            }
            if (pReader["IsNotified"] != DBNull.Value)
            {
                pInstance.IsNotified = Convert.ToBoolean(pReader["IsNotified"]);
            }
            if (pReader["NotifyCount"] != DBNull.Value)
            {
                pInstance.NotifyCount = Convert.ToInt32(pReader["NotifyCount"]);
            }
            if (pReader["NextNotifyTime"] != DBNull.Value)
            {
                pInstance.NextNotifyTime = Convert.ToDateTime(pReader["NextNotifyTime"]);
            }
            if (pReader["Status"] != DBNull.Value)
            {
                pInstance.Status = Convert.ToInt32(pReader["Status"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
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
        }
        #endregion
    }
}
