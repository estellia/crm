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
    /// ���ݷ��ʣ�  
    /// ��SMS_send�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SMSSendDAO : BaseDAO<BasicUserInfo>, ICRUDable<SMSSendEntity>, IQueryable<SMSSendEntity>
    {
        /// <summary>
        /// ��ȡTOP���ݶ���ʵ��
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
                    // update by:wuwen   20141020 �������ö��˺� 
                    //1���˺�id���뻺�� ���治���ڣ�����ӻ���
                    //2:���ݶ����˺�id����ȡ�˺��û��������� cf_jieyt  kLhNMF
                    //�ͻ�id���治Ϊ�գ����ҿͻ�id��Ϊ�գ���ǰ�Ŀͻ�id���治���ڵ�ǰ�Ŀͻ�id�����ȡ�ͻ����ƺͿͻ�����
                    if ((!objCache["SMSCustomerID"].Equals(string.Empty)) && (!string.IsNullOrWhiteSpace(m.SMSCustomerID)) && (objCache["SMSCustomerID"].ToString().Equals(m.SMSCustomerID)))
                    {
                        //��ȡ�����˺ź�����
                        SMSCustomerBLL SMSCustomerBLL = new SMSCustomerBLL(new BasicUserInfo());
                        SMSCustomerEntity SMSCustomerEntity = SMSCustomerBLL.GetByID(m.SMSCustomerID,null);
                        m.Account = SMSCustomerEntity.Account;
                        m.Password = SMSCustomerEntity.Password;
                        objCache.Insert("Account", (Object)m.Account);
                        objCache.Insert("Password", (Object)m.Password);
                    }
                     //�����û���ȡ��Ա������Ա�Ƴ�����û�����ö�Ӧ�ͻ�id����signǩ�������ͻ���ͷ�����Ϣ��
                    else if (string.IsNullOrWhiteSpace(m.SMSCustomerID))
                    {
                        //��ȡ�����˺ź�����
                        SMSCustomerBLL SMSCustomerBLL = new SMSCustomerBLL(new BasicUserInfo());
                        SMSCustomerEntity SMSCustomerEntity = SMSCustomerBLL.GetByID(null,m.Sign);
                        m.Account = SMSCustomerEntity.Account;
                        m.Password = SMSCustomerEntity.Password;
                        m.SMSCustomerID = SMSCustomerEntity.SMSCustomerID.ToString();
                        objCache.Insert("Account", (Object)m.Account);
                        objCache.Insert("Password", (Object)m.Password);
                    }
                    //�ͻ�id���治Ϊ�գ����ҿͻ�id��Ϊ�գ����ȡ�ͻ����ƺͿͻ����룬��һ�ν��뷽��ʱ����
                    //else if ((objCache["SMSCustomerID"].Equals(string.Empty)) && (!string.IsNullOrWhiteSpace(m.SMSCustomerID)))
                    else if ((!string.IsNullOrWhiteSpace(m.SMSCustomerID)))
                    {
                        //��ȡ�����˺ź�����
                        SMSCustomerBLL SMSCustomerBLL = new SMSCustomerBLL(new BasicUserInfo());
                        SMSCustomerEntity SMSCustomerEntity = SMSCustomerBLL.GetByID(m.SMSCustomerID,null);
                        m.Account = SMSCustomerEntity.Account;
                        m.Password = SMSCustomerEntity.Password;
                        objCache.Insert("Account", (Object)m.Account);
                        objCache.Insert("Password", (Object)m.Password);
                    }
                    else//��ȡ�ͻ��˺źͿͻ�����
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
        /// ��������
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
