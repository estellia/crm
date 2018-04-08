/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/5 11:30:35
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
using JIT.Utility.Message.WCF.Base;
using JIT.Utility.Message.WCF.Entity;

namespace JIT.Utility.Message.WCF.DataAccess
{
    /// <summary>
    /// 数据访问： 客户人员表 
    /// 表ClientUser的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ClientUserDAO : CommonDAO_QDY, ICRUDable<ClientUserEntity>, IQueryable<ClientUserEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ClientUserDAO(BasicUserInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(ClientUserEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(ClientUserEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = Convert.ToInt32(CurrentUserInfo.UserID);
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = Convert.ToInt32(CurrentUserInfo.UserID);
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ClientUser](");
            strSql.Append("[UserNo],[Username],[UserPWD],[Name],[ClientPositionID],[ClientStructureID],[Sex],[Tel],[ProvinceID],[CityID],[DistrictID],[Addr],[Postcode],[BankName],[BankAcct],[IDCard],[ParentID],[SysPosition],[PushChannel],[Remark],[Photo],[Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col14],[Col15],[Status],[IsEnable],[LatestVersion],[DefaultPage],[ClientID],[ClientDistributorID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete])");
            strSql.Append(" values (");
            strSql.Append("@UserNo,@Username,@UserPWD,@Name,@ClientPositionID,@ClientStructureID,@Sex,@Tel,@ProvinceID,@CityID,@DistrictID,@Addr,@Postcode,@BankName,@BankAcct,@IDCard,@ParentID,@SysPosition,@PushChannel,@Remark,@Photo,@Col1,@Col2,@Col3,@Col4,@Col5,@Col6,@Col7,@Col8,@Col9,@Col10,@Col11,@Col12,@Col13,@Col14,@Col15,@Status,@IsEnable,@LatestVersion,@DefaultPage,@ClientID,@ClientDistributorID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete)");            
			strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@UserNo",SqlDbType.NVarChar),
					new SqlParameter("@Username",SqlDbType.NVarChar),
					new SqlParameter("@UserPWD",SqlDbType.NVarChar),
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@ClientPositionID",SqlDbType.Int),
					new SqlParameter("@ClientStructureID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@Sex",SqlDbType.Int),
					new SqlParameter("@Tel",SqlDbType.NVarChar),
					new SqlParameter("@ProvinceID",SqlDbType.Int),
					new SqlParameter("@CityID",SqlDbType.Int),
					new SqlParameter("@DistrictID",SqlDbType.Int),
					new SqlParameter("@Addr",SqlDbType.NVarChar),
					new SqlParameter("@Postcode",SqlDbType.NVarChar),
					new SqlParameter("@BankName",SqlDbType.NVarChar),
					new SqlParameter("@BankAcct",SqlDbType.NVarChar),
					new SqlParameter("@IDCard",SqlDbType.NVarChar),
					new SqlParameter("@ParentID",SqlDbType.Int),
					new SqlParameter("@SysPosition",SqlDbType.Int),
					new SqlParameter("@PushChannel",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@Photo",SqlDbType.NVarChar),
					new SqlParameter("@Col1",SqlDbType.NVarChar),
					new SqlParameter("@Col2",SqlDbType.NVarChar),
					new SqlParameter("@Col3",SqlDbType.NVarChar),
					new SqlParameter("@Col4",SqlDbType.NVarChar),
					new SqlParameter("@Col5",SqlDbType.NVarChar),
					new SqlParameter("@Col6",SqlDbType.NVarChar),
					new SqlParameter("@Col7",SqlDbType.NVarChar),
					new SqlParameter("@Col8",SqlDbType.NVarChar),
					new SqlParameter("@Col9",SqlDbType.NVarChar),
					new SqlParameter("@Col10",SqlDbType.NVarChar),
					new SqlParameter("@Col11",SqlDbType.NVarChar),
					new SqlParameter("@Col12",SqlDbType.NVarChar),
					new SqlParameter("@Col13",SqlDbType.NVarChar),
					new SqlParameter("@Col14",SqlDbType.NVarChar),
					new SqlParameter("@Col15",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@IsEnable",SqlDbType.Int),
					new SqlParameter("@LatestVersion",SqlDbType.NVarChar),
					new SqlParameter("@DefaultPage",SqlDbType.NVarChar),
					new SqlParameter("@ClientID",SqlDbType.Int),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.UserNo;
			parameters[1].Value = pEntity.Username;
			parameters[2].Value = pEntity.UserPWD;
			parameters[3].Value = pEntity.Name;
			parameters[4].Value = pEntity.ClientPositionID;
			parameters[5].Value = pEntity.ClientStructureID;
			parameters[6].Value = pEntity.Sex;
			parameters[7].Value = pEntity.Tel;
			parameters[8].Value = pEntity.ProvinceID;
			parameters[9].Value = pEntity.CityID;
			parameters[10].Value = pEntity.DistrictID;
			parameters[11].Value = pEntity.Addr;
			parameters[12].Value = pEntity.Postcode;
			parameters[13].Value = pEntity.BankName;
			parameters[14].Value = pEntity.BankAcct;
			parameters[15].Value = pEntity.IDCard;
			parameters[16].Value = pEntity.ParentID;
			parameters[17].Value = pEntity.SysPosition;
			parameters[18].Value = pEntity.PushChannel;
			parameters[19].Value = pEntity.Remark;
			parameters[20].Value = pEntity.Photo;
			parameters[21].Value = pEntity.Col1;
			parameters[22].Value = pEntity.Col2;
			parameters[23].Value = pEntity.Col3;
			parameters[24].Value = pEntity.Col4;
			parameters[25].Value = pEntity.Col5;
			parameters[26].Value = pEntity.Col6;
			parameters[27].Value = pEntity.Col7;
			parameters[28].Value = pEntity.Col8;
			parameters[29].Value = pEntity.Col9;
			parameters[30].Value = pEntity.Col10;
			parameters[31].Value = pEntity.Col11;
			parameters[32].Value = pEntity.Col12;
			parameters[33].Value = pEntity.Col13;
			parameters[34].Value = pEntity.Col14;
			parameters[35].Value = pEntity.Col15;
			parameters[36].Value = pEntity.Status;
			parameters[37].Value = pEntity.IsEnable;
			parameters[38].Value = pEntity.LatestVersion;
			parameters[39].Value = pEntity.DefaultPage;
			parameters[40].Value = pEntity.ClientID;
			parameters[41].Value = pEntity.ClientDistributorID;
			parameters[42].Value = pEntity.CreateBy;
			parameters[43].Value = pEntity.CreateTime;
			parameters[44].Value = pEntity.LastUpdateBy;
			parameters[45].Value = pEntity.LastUpdateTime;
			parameters[46].Value = pEntity.IsDelete;

            //执行并将结果回写
            object result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ClientUserID = Convert.ToInt32(result);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public ClientUserEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ClientUser] where ClientUserID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            ClientUserEntity m = null;
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
        public ClientUserEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ClientUser] where isdelete=0");
            //读取数据
            List<ClientUserEntity> list = new List<ClientUserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ClientUserEntity m;
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
        public void Update(ClientUserEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(ClientUserEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ClientUserID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = Convert.ToInt32(CurrentUserInfo.UserID);

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ClientUser] set ");
                        if (pIsUpdateNullField || pEntity.UserNo!=null)
                strSql.Append( "[UserNo]=@UserNo,");
            if (pIsUpdateNullField || pEntity.Username!=null)
                strSql.Append( "[Username]=@Username,");
            if (pIsUpdateNullField || pEntity.UserPWD!=null)
                strSql.Append( "[UserPWD]=@UserPWD,");
            if (pIsUpdateNullField || pEntity.Name!=null)
                strSql.Append( "[Name]=@Name,");
            if (pIsUpdateNullField || pEntity.ClientPositionID!=null)
                strSql.Append( "[ClientPositionID]=@ClientPositionID,");
            if (pIsUpdateNullField || pEntity.ClientStructureID!=null)
                strSql.Append( "[ClientStructureID]=@ClientStructureID,");
            if (pIsUpdateNullField || pEntity.Sex!=null)
                strSql.Append( "[Sex]=@Sex,");
            if (pIsUpdateNullField || pEntity.Tel!=null)
                strSql.Append( "[Tel]=@Tel,");
            if (pIsUpdateNullField || pEntity.ProvinceID!=null)
                strSql.Append( "[ProvinceID]=@ProvinceID,");
            if (pIsUpdateNullField || pEntity.CityID!=null)
                strSql.Append( "[CityID]=@CityID,");
            if (pIsUpdateNullField || pEntity.DistrictID!=null)
                strSql.Append( "[DistrictID]=@DistrictID,");
            if (pIsUpdateNullField || pEntity.Addr!=null)
                strSql.Append( "[Addr]=@Addr,");
            if (pIsUpdateNullField || pEntity.Postcode!=null)
                strSql.Append( "[Postcode]=@Postcode,");
            if (pIsUpdateNullField || pEntity.BankName!=null)
                strSql.Append( "[BankName]=@BankName,");
            if (pIsUpdateNullField || pEntity.BankAcct!=null)
                strSql.Append( "[BankAcct]=@BankAcct,");
            if (pIsUpdateNullField || pEntity.IDCard!=null)
                strSql.Append( "[IDCard]=@IDCard,");
            if (pIsUpdateNullField || pEntity.ParentID!=null)
                strSql.Append( "[ParentID]=@ParentID,");
            if (pIsUpdateNullField || pEntity.SysPosition!=null)
                strSql.Append( "[SysPosition]=@SysPosition,");
            if (pIsUpdateNullField || pEntity.PushChannel!=null)
                strSql.Append( "[PushChannel]=@PushChannel,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.Photo!=null)
                strSql.Append( "[Photo]=@Photo,");
            if (pIsUpdateNullField || pEntity.Col1!=null)
                strSql.Append( "[Col1]=@Col1,");
            if (pIsUpdateNullField || pEntity.Col2!=null)
                strSql.Append( "[Col2]=@Col2,");
            if (pIsUpdateNullField || pEntity.Col3!=null)
                strSql.Append( "[Col3]=@Col3,");
            if (pIsUpdateNullField || pEntity.Col4!=null)
                strSql.Append( "[Col4]=@Col4,");
            if (pIsUpdateNullField || pEntity.Col5!=null)
                strSql.Append( "[Col5]=@Col5,");
            if (pIsUpdateNullField || pEntity.Col6!=null)
                strSql.Append( "[Col6]=@Col6,");
            if (pIsUpdateNullField || pEntity.Col7!=null)
                strSql.Append( "[Col7]=@Col7,");
            if (pIsUpdateNullField || pEntity.Col8!=null)
                strSql.Append( "[Col8]=@Col8,");
            if (pIsUpdateNullField || pEntity.Col9!=null)
                strSql.Append( "[Col9]=@Col9,");
            if (pIsUpdateNullField || pEntity.Col10!=null)
                strSql.Append( "[Col10]=@Col10,");
            if (pIsUpdateNullField || pEntity.Col11!=null)
                strSql.Append( "[Col11]=@Col11,");
            if (pIsUpdateNullField || pEntity.Col12!=null)
                strSql.Append( "[Col12]=@Col12,");
            if (pIsUpdateNullField || pEntity.Col13!=null)
                strSql.Append( "[Col13]=@Col13,");
            if (pIsUpdateNullField || pEntity.Col14!=null)
                strSql.Append( "[Col14]=@Col14,");
            if (pIsUpdateNullField || pEntity.Col15!=null)
                strSql.Append( "[Col15]=@Col15,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.IsEnable!=null)
                strSql.Append( "[IsEnable]=@IsEnable,");
            if (pIsUpdateNullField || pEntity.LatestVersion!=null)
                strSql.Append( "[LatestVersion]=@LatestVersion,");
            if (pIsUpdateNullField || pEntity.DefaultPage!=null)
                strSql.Append( "[DefaultPage]=@DefaultPage,");
            if (pIsUpdateNullField || pEntity.ClientID!=null)
                strSql.Append( "[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.ClientDistributorID!=null)
                strSql.Append( "[ClientDistributorID]=@ClientDistributorID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where ClientUserID=@ClientUserID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@UserNo",SqlDbType.NVarChar),
					new SqlParameter("@Username",SqlDbType.NVarChar),
					new SqlParameter("@UserPWD",SqlDbType.NVarChar),
					new SqlParameter("@Name",SqlDbType.NVarChar),
					new SqlParameter("@ClientPositionID",SqlDbType.Int),
					new SqlParameter("@ClientStructureID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@Sex",SqlDbType.Int),
					new SqlParameter("@Tel",SqlDbType.NVarChar),
					new SqlParameter("@ProvinceID",SqlDbType.Int),
					new SqlParameter("@CityID",SqlDbType.Int),
					new SqlParameter("@DistrictID",SqlDbType.Int),
					new SqlParameter("@Addr",SqlDbType.NVarChar),
					new SqlParameter("@Postcode",SqlDbType.NVarChar),
					new SqlParameter("@BankName",SqlDbType.NVarChar),
					new SqlParameter("@BankAcct",SqlDbType.NVarChar),
					new SqlParameter("@IDCard",SqlDbType.NVarChar),
					new SqlParameter("@ParentID",SqlDbType.Int),
					new SqlParameter("@SysPosition",SqlDbType.Int),
					new SqlParameter("@PushChannel",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@Photo",SqlDbType.NVarChar),
					new SqlParameter("@Col1",SqlDbType.NVarChar),
					new SqlParameter("@Col2",SqlDbType.NVarChar),
					new SqlParameter("@Col3",SqlDbType.NVarChar),
					new SqlParameter("@Col4",SqlDbType.NVarChar),
					new SqlParameter("@Col5",SqlDbType.NVarChar),
					new SqlParameter("@Col6",SqlDbType.NVarChar),
					new SqlParameter("@Col7",SqlDbType.NVarChar),
					new SqlParameter("@Col8",SqlDbType.NVarChar),
					new SqlParameter("@Col9",SqlDbType.NVarChar),
					new SqlParameter("@Col10",SqlDbType.NVarChar),
					new SqlParameter("@Col11",SqlDbType.NVarChar),
					new SqlParameter("@Col12",SqlDbType.NVarChar),
					new SqlParameter("@Col13",SqlDbType.NVarChar),
					new SqlParameter("@Col14",SqlDbType.NVarChar),
					new SqlParameter("@Col15",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@IsEnable",SqlDbType.Int),
					new SqlParameter("@LatestVersion",SqlDbType.NVarChar),
					new SqlParameter("@DefaultPage",SqlDbType.NVarChar),
					new SqlParameter("@ClientID",SqlDbType.Int),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ClientUserID",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.UserNo;
			parameters[1].Value = pEntity.Username;
			parameters[2].Value = pEntity.UserPWD;
			parameters[3].Value = pEntity.Name;
			parameters[4].Value = pEntity.ClientPositionID;
			parameters[5].Value = pEntity.ClientStructureID;
			parameters[6].Value = pEntity.Sex;
			parameters[7].Value = pEntity.Tel;
			parameters[8].Value = pEntity.ProvinceID;
			parameters[9].Value = pEntity.CityID;
			parameters[10].Value = pEntity.DistrictID;
			parameters[11].Value = pEntity.Addr;
			parameters[12].Value = pEntity.Postcode;
			parameters[13].Value = pEntity.BankName;
			parameters[14].Value = pEntity.BankAcct;
			parameters[15].Value = pEntity.IDCard;
			parameters[16].Value = pEntity.ParentID;
			parameters[17].Value = pEntity.SysPosition;
			parameters[18].Value = pEntity.PushChannel;
			parameters[19].Value = pEntity.Remark;
			parameters[20].Value = pEntity.Photo;
			parameters[21].Value = pEntity.Col1;
			parameters[22].Value = pEntity.Col2;
			parameters[23].Value = pEntity.Col3;
			parameters[24].Value = pEntity.Col4;
			parameters[25].Value = pEntity.Col5;
			parameters[26].Value = pEntity.Col6;
			parameters[27].Value = pEntity.Col7;
			parameters[28].Value = pEntity.Col8;
			parameters[29].Value = pEntity.Col9;
			parameters[30].Value = pEntity.Col10;
			parameters[31].Value = pEntity.Col11;
			parameters[32].Value = pEntity.Col12;
			parameters[33].Value = pEntity.Col13;
			parameters[34].Value = pEntity.Col14;
			parameters[35].Value = pEntity.Col15;
			parameters[36].Value = pEntity.Status;
			parameters[37].Value = pEntity.IsEnable;
			parameters[38].Value = pEntity.LatestVersion;
			parameters[39].Value = pEntity.DefaultPage;
			parameters[40].Value = pEntity.ClientID;
			parameters[41].Value = pEntity.ClientDistributorID;
			parameters[42].Value = pEntity.LastUpdateBy;
			parameters[43].Value = pEntity.LastUpdateTime;
			parameters[44].Value = pEntity.ClientUserID;

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
        public void Update(ClientUserEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ClientUserEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(ClientUserEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ClientUserID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ClientUserID.Value, pTran);           
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
            sql.AppendLine("update [ClientUser] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ClientUserID=@ClientUserID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.Int,Value=Convert.ToInt32(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ClientUserID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(ClientUserEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ClientUserID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.ClientUserID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(ClientUserEntity[] pEntities)
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
            sql.AppendLine("update [ClientUser] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy="+CurrentUserInfo.UserID+",IsDelete=1 where ClientUserID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ClientUserEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ClientUser] where isdelete=0 ");
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
            List<ClientUserEntity> list = new List<ClientUserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ClientUserEntity m;
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
        public PagedQueryResult<ClientUserEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ClientUserID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [ClientUser] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [ClientUser] where isdelete=0 ");
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
            PagedQueryResult<ClientUserEntity> result = new PagedQueryResult<ClientUserEntity>();
            List<ClientUserEntity> list = new List<ClientUserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ClientUserEntity m;
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
        public ClientUserEntity[] QueryByEntity(ClientUserEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ClientUserEntity> PagedQueryByEntity(ClientUserEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ClientUserEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ClientUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientUserID", Value = pQueryEntity.ClientUserID });
            if (pQueryEntity.UserNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserNo", Value = pQueryEntity.UserNo });
            if (pQueryEntity.Username!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Username", Value = pQueryEntity.Username });
            if (pQueryEntity.UserPWD!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserPWD", Value = pQueryEntity.UserPWD });
            if (pQueryEntity.Name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Name", Value = pQueryEntity.Name });
            if (pQueryEntity.ClientPositionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientPositionID", Value = pQueryEntity.ClientPositionID });
            if (pQueryEntity.ClientStructureID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientStructureID", Value = pQueryEntity.ClientStructureID });
            if (pQueryEntity.Sex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sex", Value = pQueryEntity.Sex });
            if (pQueryEntity.Tel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Tel", Value = pQueryEntity.Tel });
            if (pQueryEntity.ProvinceID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProvinceID", Value = pQueryEntity.ProvinceID });
            if (pQueryEntity.CityID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CityID", Value = pQueryEntity.CityID });
            if (pQueryEntity.DistrictID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DistrictID", Value = pQueryEntity.DistrictID });
            if (pQueryEntity.Addr!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Addr", Value = pQueryEntity.Addr });
            if (pQueryEntity.Postcode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Postcode", Value = pQueryEntity.Postcode });
            if (pQueryEntity.BankName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BankName", Value = pQueryEntity.BankName });
            if (pQueryEntity.BankAcct!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BankAcct", Value = pQueryEntity.BankAcct });
            if (pQueryEntity.IDCard!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IDCard", Value = pQueryEntity.IDCard });
            if (pQueryEntity.ParentID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentID", Value = pQueryEntity.ParentID });
            if (pQueryEntity.SysPosition!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SysPosition", Value = pQueryEntity.SysPosition });
            if (pQueryEntity.PushChannel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PushChannel", Value = pQueryEntity.PushChannel });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.Photo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Photo", Value = pQueryEntity.Photo });
            if (pQueryEntity.Col1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col1", Value = pQueryEntity.Col1 });
            if (pQueryEntity.Col2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col2", Value = pQueryEntity.Col2 });
            if (pQueryEntity.Col3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col3", Value = pQueryEntity.Col3 });
            if (pQueryEntity.Col4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col4", Value = pQueryEntity.Col4 });
            if (pQueryEntity.Col5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col5", Value = pQueryEntity.Col5 });
            if (pQueryEntity.Col6!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col6", Value = pQueryEntity.Col6 });
            if (pQueryEntity.Col7!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col7", Value = pQueryEntity.Col7 });
            if (pQueryEntity.Col8!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col8", Value = pQueryEntity.Col8 });
            if (pQueryEntity.Col9!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col9", Value = pQueryEntity.Col9 });
            if (pQueryEntity.Col10!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col10", Value = pQueryEntity.Col10 });
            if (pQueryEntity.Col11!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col11", Value = pQueryEntity.Col11 });
            if (pQueryEntity.Col12!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col12", Value = pQueryEntity.Col12 });
            if (pQueryEntity.Col13!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col13", Value = pQueryEntity.Col13 });
            if (pQueryEntity.Col14!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col14", Value = pQueryEntity.Col14 });
            if (pQueryEntity.Col15!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col15", Value = pQueryEntity.Col15 });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.IsEnable!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsEnable", Value = pQueryEntity.IsEnable });
            if (pQueryEntity.LatestVersion!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LatestVersion", Value = pQueryEntity.LatestVersion });
            if (pQueryEntity.DefaultPage!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DefaultPage", Value = pQueryEntity.DefaultPage });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.ClientDistributorID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientDistributorID", Value = pQueryEntity.ClientDistributorID });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out ClientUserEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new ClientUserEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ClientUserID"] != DBNull.Value)
			{
				pInstance.ClientUserID =   Convert.ToInt32(pReader["ClientUserID"]);
			}
			if (pReader["UserNo"] != DBNull.Value)
			{
				pInstance.UserNo =  Convert.ToString(pReader["UserNo"]);
			}
			if (pReader["Username"] != DBNull.Value)
			{
				pInstance.Username =  Convert.ToString(pReader["Username"]);
			}
			if (pReader["UserPWD"] != DBNull.Value)
			{
				pInstance.UserPWD =  Convert.ToString(pReader["UserPWD"]);
			}
			if (pReader["Name"] != DBNull.Value)
			{
				pInstance.Name =  Convert.ToString(pReader["Name"]);
			}
			if (pReader["ClientPositionID"] != DBNull.Value)
			{
				pInstance.ClientPositionID =   Convert.ToInt32(pReader["ClientPositionID"]);
			}
			if (pReader["ClientStructureID"] != DBNull.Value)
			{
				pInstance.ClientStructureID =  (Guid)pReader["ClientStructureID"];
			}
			if (pReader["Sex"] != DBNull.Value)
			{
				pInstance.Sex =   Convert.ToInt32(pReader["Sex"]);
			}
			if (pReader["Tel"] != DBNull.Value)
			{
				pInstance.Tel =  Convert.ToString(pReader["Tel"]);
			}
			if (pReader["ProvinceID"] != DBNull.Value)
			{
				pInstance.ProvinceID =   Convert.ToInt32(pReader["ProvinceID"]);
			}
			if (pReader["CityID"] != DBNull.Value)
			{
				pInstance.CityID =   Convert.ToInt32(pReader["CityID"]);
			}
			if (pReader["DistrictID"] != DBNull.Value)
			{
				pInstance.DistrictID =   Convert.ToInt32(pReader["DistrictID"]);
			}
			if (pReader["Addr"] != DBNull.Value)
			{
				pInstance.Addr =  Convert.ToString(pReader["Addr"]);
			}
			if (pReader["Postcode"] != DBNull.Value)
			{
				pInstance.Postcode =  Convert.ToString(pReader["Postcode"]);
			}
			if (pReader["BankName"] != DBNull.Value)
			{
				pInstance.BankName =  Convert.ToString(pReader["BankName"]);
			}
			if (pReader["BankAcct"] != DBNull.Value)
			{
				pInstance.BankAcct =  Convert.ToString(pReader["BankAcct"]);
			}
			if (pReader["IDCard"] != DBNull.Value)
			{
				pInstance.IDCard =  Convert.ToString(pReader["IDCard"]);
			}
			if (pReader["ParentID"] != DBNull.Value)
			{
				pInstance.ParentID =   Convert.ToInt32(pReader["ParentID"]);
			}
			if (pReader["SysPosition"] != DBNull.Value)
			{
				pInstance.SysPosition =   Convert.ToInt32(pReader["SysPosition"]);
			}
			if (pReader["PushChannel"] != DBNull.Value)
			{
				pInstance.PushChannel =  Convert.ToString(pReader["PushChannel"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["Photo"] != DBNull.Value)
			{
				pInstance.Photo =  Convert.ToString(pReader["Photo"]);
			}
			if (pReader["Col1"] != DBNull.Value)
			{
				pInstance.Col1 =  Convert.ToString(pReader["Col1"]);
			}
			if (pReader["Col2"] != DBNull.Value)
			{
				pInstance.Col2 =  Convert.ToString(pReader["Col2"]);
			}
			if (pReader["Col3"] != DBNull.Value)
			{
				pInstance.Col3 =  Convert.ToString(pReader["Col3"]);
			}
			if (pReader["Col4"] != DBNull.Value)
			{
				pInstance.Col4 =  Convert.ToString(pReader["Col4"]);
			}
			if (pReader["Col5"] != DBNull.Value)
			{
				pInstance.Col5 =  Convert.ToString(pReader["Col5"]);
			}
			if (pReader["Col6"] != DBNull.Value)
			{
				pInstance.Col6 =  Convert.ToString(pReader["Col6"]);
			}
			if (pReader["Col7"] != DBNull.Value)
			{
				pInstance.Col7 =  Convert.ToString(pReader["Col7"]);
			}
			if (pReader["Col8"] != DBNull.Value)
			{
				pInstance.Col8 =  Convert.ToString(pReader["Col8"]);
			}
			if (pReader["Col9"] != DBNull.Value)
			{
				pInstance.Col9 =  Convert.ToString(pReader["Col9"]);
			}
			if (pReader["Col10"] != DBNull.Value)
			{
				pInstance.Col10 =  Convert.ToString(pReader["Col10"]);
			}
			if (pReader["Col11"] != DBNull.Value)
			{
				pInstance.Col11 =  Convert.ToString(pReader["Col11"]);
			}
			if (pReader["Col12"] != DBNull.Value)
			{
				pInstance.Col12 =  Convert.ToString(pReader["Col12"]);
			}
			if (pReader["Col13"] != DBNull.Value)
			{
				pInstance.Col13 =  Convert.ToString(pReader["Col13"]);
			}
			if (pReader["Col14"] != DBNull.Value)
			{
				pInstance.Col14 =  Convert.ToString(pReader["Col14"]);
			}
			if (pReader["Col15"] != DBNull.Value)
			{
				pInstance.Col15 =  Convert.ToString(pReader["Col15"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["IsEnable"] != DBNull.Value)
			{
				pInstance.IsEnable =   Convert.ToInt32(pReader["IsEnable"]);
			}
			if (pReader["LatestVersion"] != DBNull.Value)
			{
				pInstance.LatestVersion =  Convert.ToString(pReader["LatestVersion"]);
			}
			if (pReader["DefaultPage"] != DBNull.Value)
			{
				pInstance.DefaultPage =  Convert.ToString(pReader["DefaultPage"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =   Convert.ToInt32(pReader["ClientID"]);
			}
			if (pReader["ClientDistributorID"] != DBNull.Value)
			{
				pInstance.ClientDistributorID =   Convert.ToInt32(pReader["ClientDistributorID"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =   Convert.ToInt32(pReader["CreateBy"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =   Convert.ToInt32(pReader["LastUpdateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
