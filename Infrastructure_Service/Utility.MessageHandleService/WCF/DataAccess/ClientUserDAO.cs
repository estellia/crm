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
    /// ���ݷ��ʣ� �ͻ���Ա�� 
    /// ��ClientUser�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ClientUserDAO : CommonDAO_QDY, ICRUDable<ClientUserEntity>, IQueryable<ClientUserEntity>
    {
        public ClientUserEntity[] GetValidUserByStatus0(OrdersCheckingEntity entity, out Dictionary<ClientUserEntity, string> Messages)
        {
            Messages = new Dictionary<ClientUserEntity, string> { };
            List<ClientUserEntity> list = new List<ClientUserEntity> { };
            string sql = string.Format(@"select distinct d.*,'����,�ж�����Ҫ���' Message
                                         from OrdersChecking a 
                                             inner join Orders b on a.OrdersID=b.OrdersID
                                             inner join Store e on b.storeid=e.storeid
                                             cross apply fnGetSuperiorIDByUserID(b.CreateBy) c
                                             inner join ClientUser d on c.ClientUserID=d.ClientUserID and d.IsDelete=0 and a.ClientPositionID=d.ClientPositionID
                                         where a.CheckingID={0}", entity.CheckingID);

            //string sql = string.Format("select * from clientuser where clientpositionid={0}", entity.ClientPositionID);

            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    ClientUserEntity m;
                    this.Load(rd, out m);
                    Messages[m] = rd["Message"].ToString();
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        public ClientUserEntity[] GetValidUserByStatus1(OrdersCheckingEntity entity, out Dictionary<ClientUserEntity, string> Messages)
        {
            Messages = new Dictionary<ClientUserEntity, string> { };
            List<ClientUserEntity> list = new List<ClientUserEntity> { };
            string sql = string.Format(@"select distinct c.*,'����,�������'+(case when a.status=6 then 'ͨ��' else '��ͨ��' end) Message
                                            from Orders a,OrdersChecking b,ClientUser c,Store d
                                            where a.IsDelete=0 and b.SendStatus=1 and b.IsDelete=0 
                                            and a.OrdersID=b.OrdersID and a.[Status] in (-2,6)
                                            and a.ClientUserID=c.ClientUserID and a.StoreID=d.StoreID
                                            and b.CheckingID={0}", entity.CheckingID);
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    ClientUserEntity m;
                    this.Load(rd, out m);
                    Messages[m] = rd["Message"].ToString();
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
    }
}
