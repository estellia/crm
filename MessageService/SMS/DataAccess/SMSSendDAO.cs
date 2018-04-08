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
using JIT.Utility.DataAccess.Query;
using JIT.Utility.SMS.Entity;
using System.Data.SqlClient;
using JIT.Utility.Cache;
using JIT.ManagementPlatform.Web.Module.BLL;
using JIT.ManagementPlatform.Web.Module.Entity;
using System.Web;
using System.Web.Caching;

namespace JIT.Utility.SMS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表SMS_send的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SMSSendDAO : BaseDAO<BasicUserInfo>, ICRUDable<SMSSendEntity>, IQueryable<SMSSendEntity>
    {
        /// <summary>
        /// 获取TOP数据短信实体
        /// </summary>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public SMSSendEntity[] GetNoSend(int topCount = 100)
        {
            string sql = "select top {0} * from SMS_Send where Status=0 and isnull(Send_Count,0)<3 order by SMS_send_id desc".Fmt(topCount);
            List<SMSSendEntity> list = new List<SMSSendEntity> { };
            JITMemoryCache jitCache = new JITMemoryCache();
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            using (SqlDataReader dr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                objCache.Insert("SMSCustomerID",(Object)string.Empty);
                while (dr.Read())
                {
                    SMSSendEntity m;
                    this.Load(dr, out m);
                    // update by:wuwen   20141020 增加适用多账号 
                    //1：账号id放入缓存 缓存不存在，则添加缓存
                    //2:根据短信账号id，获取账号用户名和密码 cf_jieyt  kLhNMF
                    //客户id缓存不为空，并且客户id不为空，当前的客户id缓存不等于当前的客户id，则获取客户名称和客户密码
                    if ((!objCache["SMSCustomerID"].Equals(string.Empty)) && (!string.IsNullOrWhiteSpace(m.SMSCustomerID)) && (objCache["SMSCustomerID"].ToString().Equals(m.SMSCustomerID)))
                    {
                        //获取短信账号和密码
                        SMSCustomerBLL SMSCustomerBLL = new SMSCustomerBLL(new BasicUserInfo());
                        SMSCustomerEntity SMSCustomerEntity = SMSCustomerBLL.GetByID(m.SMSCustomerID,null);
                        m.Account = SMSCustomerEntity.Account;
                        m.Password = SMSCustomerEntity.Password;
                        objCache.Insert("Account", (Object)m.Account);
                        objCache.Insert("Password", (Object)m.Password);
                    }
                     //用于用户领取会员卡，会员云程序中没有设置对应客户id，用sign签名关联客户表和发送消息表
                    else if (string.IsNullOrWhiteSpace(m.SMSCustomerID))
                    {
                        //获取短信账号和密码
                        SMSCustomerBLL SMSCustomerBLL = new SMSCustomerBLL(new BasicUserInfo());
                        SMSCustomerEntity SMSCustomerEntity = SMSCustomerBLL.GetByID(null,m.Sign);
                        m.Account = SMSCustomerEntity.Account;
                        m.Password = SMSCustomerEntity.Password;
                        m.SMSCustomerID = SMSCustomerEntity.SMSCustomerID.ToString();
                        objCache.Insert("Account", (Object)m.Account);
                        objCache.Insert("Password", (Object)m.Password);
                    }
                    //客户id缓存不为空，并且客户id不为空，则获取客户名称和客户密码，第一次进入方法时操作
                    //else if ((objCache["SMSCustomerID"].Equals(string.Empty)) && (!string.IsNullOrWhiteSpace(m.SMSCustomerID)))
                    else if ((!string.IsNullOrWhiteSpace(m.SMSCustomerID)))
                    {
                        //获取短信账号和密码
                        SMSCustomerBLL SMSCustomerBLL = new SMSCustomerBLL(new BasicUserInfo());
                        SMSCustomerEntity SMSCustomerEntity = SMSCustomerBLL.GetByID(m.SMSCustomerID,null);
                        m.Account = SMSCustomerEntity.Account;
                        m.Password = SMSCustomerEntity.Password;
                        objCache.Insert("Account", (Object)m.Account);
                        objCache.Insert("Password", (Object)m.Password);
                    }
                    else//获取客户账号和客户密码
                    {
                        m.Account = objCache["Account"].ToString();
                        m.Password = objCache["Password"].ToString();
                    }
                    objCache.Insert("SMSCustomerID", (Object)m.SMSCustomerID);
                    list.Add(m);
                }
            }
            Loggers.Debug(new DebugLogInfo() { Message = sql });
            return list.ToArray();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Update(string sql)
        {
            Loggers.Debug(new DebugLogInfo() { Message = sql });
            return this.SQLHelper.ExecuteNonQuery(sql);
        }

    }
}
