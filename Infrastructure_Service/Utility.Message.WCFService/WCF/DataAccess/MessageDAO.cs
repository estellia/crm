/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/3 17:21:09
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
using System.Data.SqlClient;
using JIT.Utility.Message.WCF.Base;
using JIT.Utility.Message.WCF.Entity;

namespace JIT.Utility.Message.WCF.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ� ������Ϣ�� 
    /// ��Message�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MessageDAO : CommonDAO, ICRUDable<MessageEntity>, IQueryable<MessageEntity>
    {
        public MessageEntity[] GetValidMessage()
        {
            List<MessageEntity> list = new List<MessageEntity> { };
            string sql = "select * from Message where sendcount<3 and status<2 and isdelete=0";
            using (SqlDataReader rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    MessageEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
    }
}
