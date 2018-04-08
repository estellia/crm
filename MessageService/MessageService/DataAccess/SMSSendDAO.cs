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
using JIT.MessageService.Entity;
using System.Data.SqlClient;
using System.Collections;

namespace JIT.MessageService.DataAccess
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
        public SMSSendEntity[] GetNoSend(int topCount=100)
        {
            //��Ӹ��ݿͻ�id��ȡδ���Ͷ���   wen.wu  20141017
            string sql = "select top {0} * from SMS_Send where Status=0 and isnull(Send_Count,0)<3".Fmt(topCount);
            List<SMSSendEntity> list = new List<SMSSendEntity> { };
            using (SqlDataReader dr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (dr.Read())
                {
                    //����ClientID��ȡ�˺ź�����
                    SMSSendEntity m;
                    this.Load(dr,out m);
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
