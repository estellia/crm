using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;

using cPos.Model;
using cPos.Model.Exchange;
using cPos.Components.SqlMappers;


namespace cPos.Service
{
    public class ExchangeService : BaseService
    {
        #region 业务通告
        /// <summary>
        /// 添加一个业务通告
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="announce">要添加的业务通告</param>
        /// <returns></returns>
        public bool InsertAnnounce(LoggingSessionInfo loggingSession, AnnounceInfo announce)
        {
            if (announce == null)
                throw new ArgumentNullException("announce");
            if (announce.AnnounceUnits == null || announce.AnnounceUnits.Count == 0)
                throw new ArgumentNullException("announce.AnnounceUnits");

            if (string.IsNullOrEmpty(announce.ID))
            {
                announce.ID = this.NewGuid();
            }
            announce.CreateUserID = loggingSession.CurrentUser.User_Id;
            announce.CreateUserName = loggingSession.CurrentUser.User_Name;
            //保存
            ISqlMapper sqlMap = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
            try
            {
                sqlMap.BeginTransaction();
                //插入通告信息
                sqlMap.Insert("Exchange.Announce.Insert", announce);
                //插入通告与单位的关系
                foreach (AnnounceUnitInfo au in announce.AnnounceUnits)
                {
                    au.Announce = announce;
                    sqlMap.Insert("Exchange.AnnounceUnit.InsertAnnounceUnit", au);
                }
                sqlMap.CommitTransaction();
            }
            catch (Exception ex)
            {
                sqlMap.RollBackTransaction();
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 修改一个业务通告
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="announce">要修改的业务通告</param>
        /// <returns></returns>
        public bool ModifyAnnounce(LoggingSessionInfo loggingSession, AnnounceInfo announce)
        {
            if (announce.AnnounceUnits == null || announce.AnnounceUnits.Count == 0)
                throw new ArgumentNullException("announce.AnnounceUnits");

            announce.ModifyUserID = loggingSession.CurrentUser.User_Id;
            announce.ModifyUserName = loggingSession.CurrentUser.User_Name;
            //保存
            ISqlMapper sqlMap = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
            try
            {
                sqlMap.BeginTransaction();
                //修改通告信息
                sqlMap.Update("Exchange.Announce.Update", announce);
                //删除通告与单位的关系
                sqlMap.Delete("Exchange.AnnounceUnit.DeleteByAnnounceID", announce.ID);
                //插入通告与单位的关系
                foreach (AnnounceUnitInfo au in announce.AnnounceUnits)
                {
                    au.Announce = announce;
                    sqlMap.Insert("Exchange.AnnounceUnit.InsertAnnounceUnit", au);
                }
                sqlMap.CommitTransaction();
            }
            catch (Exception ex)
            {
                sqlMap.RollBackTransaction();
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 获取单个业务通告信息
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="announceID">业务通告ID</param>
        /// <returns></returns>
        public AnnounceQueryInfo GetAnnounceByID(LoggingSessionInfo loggingSession, string announceID)
        {
            AnnounceQueryInfo announce = null;

            ISqlMapper sqlMap = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
            try
            {
                sqlMap.BeginTransaction();
                //获取通告信息
                announce = sqlMap.QueryForObject<AnnounceQueryInfo>("Exchange.Announce.SelectByID", announceID);
                if (announce!=null)
                {
                //获取通告与单位的关系
                announce.AnnounceUnits = sqlMap.QueryForList<AnnounceUnitInfo>("Exchange.AnnounceUnit.SelectByAnnounceID", announceID);
                }
                sqlMap.CommitTransaction();
            }
            catch (Exception ex)
            {
                sqlMap.RollBackTransaction();
                throw ex;
            }
            return announce;
        }

        /// <summary>
        /// 发布业务通告（即允许下发业务通告）
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="announceID">业务通告ID</param>
        /// <returns></returns>
        public bool PublishAnnounce(LoggingSessionInfo loggingSession, string announceID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("AnnounceID", announceID);
            ht.Add("UserID", loggingSession.CurrentUser.User_Id);
            ht.Add("UserName", loggingSession.CurrentUser.User_Name);

            int ret = cSqlMapper.Instance(loggingSession.CurrentLoggingManager).Update("Exchange.Announce.Publish", ht);

            return ret == 1;
        }

        /// <summary>
        /// 停止发布业务通告（即不允许下发业务通告）
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="announceID">业务通告ID</param>
        /// <returns></returns>
        public bool StopPublishAnnounce(LoggingSessionInfo loggingSession, string announceID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("AnnounceID", announceID);
            ht.Add("UserID", loggingSession.CurrentUser.User_Id);
            ht.Add("UserName", loggingSession.CurrentUser.User_Name);

            cSqlMapper.Instance(loggingSession.CurrentLoggingManager).Update("Exchange.Announce.StopPublish", ht);

            return true;
        }

        /// <summary>
        /// 获取满足查询条件的通告的某页上的所有通告
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="condition">Title:主题；BeginDate：通告起始日期；EndDate：通告结束日期；UnitID：通告单位；AllowDownload：允许下发；StartNo:起始编号</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的终端的列表</returns>
        public SelectObjectResultsInfo<AnnounceQueryInfo> SelectAnnounceList(LoggingSessionInfo loggingSession, Hashtable condition, int maxRowCount, int startRowIndex)
        {
            condition.Add("UserID", loggingSession.CurrentUser.User_Id);
            condition.Add("RoleID", loggingSession.CurrentUserRole.RoleId);

            condition.Add("StartRow", startRowIndex);
            condition.Add("EndRow", startRowIndex + maxRowCount);
            condition.Add("MaxRowCount", maxRowCount);

            SelectObjectResultsInfo<AnnounceQueryInfo> ret = new SelectObjectResultsInfo<AnnounceQueryInfo>();
            IList<AnnounceUnitInfo> announce_unit_lst = null;

            ISqlMapper sqlMap = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
            try
            {
                sqlMap.BeginTransaction();
                //插入满足条件的单位进临时表
                cPos.Model.Unit.UnitQueryCondition unitQueryCondition = new Model.Unit.UnitQueryCondition();
                if (condition.Contains("UnitID"))
                {
                    unitQueryCondition.SuperUnitIDs.Add(condition["UnitID"].ToString());
                }
                condition.Add("UnitSQL", this.GenInsertUnitTemporaryTableSQL(loggingSession, unitQueryCondition));

                //查某页上的通告
                ret.Data = sqlMap.QueryForList<AnnounceQueryInfo>("Exchange.Announce.SelectAnnounceList2", condition);
                //有StartNo查询条件,表示是从终端发起的查询,不需要单位
                if (!condition.ContainsKey("StartNo"))
                {
                    if (ret.Data != null && ret.Data.Count > 0)
                    {
                        //查通告的通告单位
                        StringBuilder sb = new StringBuilder(string.Format("b.announce_id='{0}' ", ret.Data[0].ID));
                        for (int i = 1; i < ret.Data.Count; i++)
                        {
                            sb.Append(string.Format("or b.announce_id='{0}' ", ret.Data[i].ID));
                        }
                        announce_unit_lst = sqlMap.QueryForList<AnnounceUnitInfo>("Exchange.AnnounceUnit.SelectByCondition", sb.ToString());
                    }
                }

                sqlMap.CommitTransaction();
            }
            catch (Exception ex)
            {
                sqlMap.RollBackTransaction();
                throw ex;
            }

            if (announce_unit_lst != null && announce_unit_lst.Count > 0)
            {
                foreach (AnnounceInfo a in ret.Data)
                {
                    foreach (AnnounceUnitInfo au in announce_unit_lst)
                    {
                        if (au.Announce.ID == a.ID)
                        {
                            a.AnnounceUnits.Add(au);
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 删除一个业务通告（即允许下发业务通告）
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="announceID">业务通告ID</param>
        /// <returns></returns>
        public bool DeleteAnnounce(LoggingSessionInfo loggingSession, string announceID)
        {
            ISqlMapper sqlMap = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
            try
            {
                sqlMap.BeginTransaction();
                int ret = sqlMap.Delete("Exchange.Announce.DeleteByID", announceID);
                if (ret != 1)
                {
                    sqlMap.RollBackTransaction();
                    return false;
                }
                sqlMap.Delete("Exchange.AnnounceUnit.DeleteByAnnounceID", announceID);
                sqlMap.CommitTransaction();
            }
            catch (Exception ex)
            {
                sqlMap.RollBackTransaction();
                throw ex;
            }
            return true;
        }

        /// <summary>
        /// 获取满足查询条件的通告的某页上的所有通告
        /// </summary>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="condition">UnitID：通告单位；StartNo:起始编号</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的终端的列表</returns>
        public SelectObjectResultsInfo<AnnounceQueryInfo> SelectDownloadAnnounceList(LoggingSessionInfo loggingSession, Hashtable condition, int maxRowCount, int startRowIndex)
        {
            condition.Add("StartRow", startRowIndex);
            condition.Add("EndRow", startRowIndex + maxRowCount);
            condition.Add("MaxRowCount", maxRowCount);

            SelectObjectResultsInfo<AnnounceQueryInfo> ret = new SelectObjectResultsInfo<AnnounceQueryInfo>();

            ISqlMapper sqlMap = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);

            //查某页上的通告
            ret.Data = sqlMap.QueryForList<AnnounceQueryInfo>("Exchange.Announce.SelectAnnounceList3", condition);

            return ret;
        }
        #endregion
    }
}
