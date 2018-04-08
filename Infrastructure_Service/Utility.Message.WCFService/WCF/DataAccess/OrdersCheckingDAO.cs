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
    /// ���ݷ��ʣ�  
    /// ��OrdersChecking�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class OrdersCheckingDAO : CommonDAO_QDY, ICRUDable<OrdersCheckingEntity>, IQueryable<OrdersCheckingEntity>
    {
        public OrdersCheckingEntity[] GetValidOrdersChecking()
        {
            List<OrdersCheckingEntity> list = new List<OrdersCheckingEntity> { };
            string sql = "SELECT * FROM OrdersChecking WHERE IsDelete=0 AND SendStatus=0";
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    OrdersCheckingEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
    }
}
