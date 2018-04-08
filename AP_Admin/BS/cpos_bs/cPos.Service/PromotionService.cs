using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;

using cPos.Model;
using cPos.Model.Promotion;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// 会员服务
    /// </summary>
    public class PromotionService : BaseService
    {
        #region 会员类型
        /// <summary>
        /// 获取所有的会员类型列表
        /// </summary>
        /// <returns></returns>
        public IList<VipTypeInfo> SelectVipTypeList()
        {
            return cSqlMapper.Instance().QueryForList<VipTypeInfo>("Promotion.Vip.SelectVipTypeList", null);
        }
        #endregion

        #region 会员
        /// <summary>
        /// 根据会员ID获取会员信息
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="vipID">会员ID</param>
        /// <returns></returns>
        public VipInfo GetVipByID(LoggingSessionInfo loggingSession, string vipID)
        {
            if (string.IsNullOrEmpty(vipID))
                return null;
            else
                return cSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<VipInfo>("Promotion.Vip.SelectByID", vipID);
        }

        /// <summary>
        /// 根据会员号获取会员信息
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="vipNo">会员号</param>
        /// <returns></returns>
        public VipInfo GetVipByNo(LoggingSessionInfo loggingSession, string vipNo)
        {
            if (string.IsNullOrEmpty(vipNo))
                return null;
            else
                return cSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<VipInfo>("Promotion.Vip.SelectByNo", vipNo);
        }

        /// <summary>
        /// 插入一个会员信息
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="vipExchange">会员信息</param>
        public void InsertVip(LoggingSessionInfo loggingSession, VipExchangeInfo vipExchange)
        {
            if (string.IsNullOrEmpty(vipExchange.ID))
            {
                vipExchange.ID = this.NewGuid();
            }

            //保存
            ISqlMapper sqlMapper = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
            try
            {
                sqlMapper.BeginTransaction();
                //添加仓库
                sqlMapper.Insert("Promotion.Vip.Insert", vipExchange);

                sqlMapper.CommitTransaction();
            }
            catch (Exception ex)
            {
                sqlMapper.RollBackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 修改一个会员信息
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="vipExchange">会员信息</param>
        public bool ModifyVip(LoggingSessionInfo loggingSession, VipExchangeInfo vipExchange)
        {
            int ret = 0;
            ISqlMapper sqlMapper = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
            try
            {
                sqlMapper.BeginTransaction();
                //修改会员
                ret = sqlMapper.Update("Promotion.Vip.Update", vipExchange);

                sqlMapper.CommitTransaction();
            }
            catch (Exception ex)
            {
                sqlMapper.RollBackTransaction();
                throw ex;
            }
            return ret == 1;
        }

        /// <summary>
        /// 获取满足查询条件的会员的记录总数
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="condition">No:会员卡号；Type：会员类型；Name：会员姓名；Cell：会员手机；UnitName：办卡门店；Status：会员状态；IdentityNo:身份证；</param>
        /// <returns></returns>
        public int SelectVipListCount(Hashtable condition)
        {
            return cSqlMapper.Instance().QueryForObject<int>("Promotion.Vip.SelectVipListCount", condition);
        }

        /// <summary>
        /// 获取满足查询条件的会员的某页上的所有会员
        /// </summary>
        /// <param name="condition">No:会员卡号；Type：会员类型；Name：会员姓名；Cell：会员手机；UnitName：办卡门店；Status：会员状态；IdentityNo:身份证；</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的会员的列表</returns>
        public IList<VipInfo> SelectVipList(Hashtable condition, int maxRowCount, int startRowIndex)
        {
            condition.Add("StartRow", startRowIndex);
            condition.Add("EndRow", startRowIndex + maxRowCount);
            condition.Add("MaxRowCount", maxRowCount);

            return cSqlMapper.Instance().QueryForList<VipInfo>("Promotion.Vip.SelectVipList", condition);
        }

        /// <summary>
        /// 获取满足查询条件的会员的某页上的所有会员
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="condition">vip_no:会员卡号；vip_type：会员类型；vip_name：会员姓名；vip_cell：会员手机；vip_identity_no:身份证；</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns>满足条件的会员的列表</returns>
        public IList<VipInfo> SelectVipListFromTerminal(LoggingSessionInfo loggingSession, Hashtable condition)
        {
            if (!condition.ContainsKey("record_count"))
                condition.Add("record_count", 50);

            return cSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForList<VipInfo>("Promotion.Vip.SelectVipListTerminal", condition);
        }

        /// <summary>
        /// 查询满足条件的会员的数量
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="condition">vip_no:会员卡号;vip_id:会员ID</param>
        /// <returns></returns>
        public int CountVip(LoggingSessionInfo loggingSession, Hashtable condition)
        {
            int ret = cSqlMapper.Instance(loggingSession.CurrentLoggingManager).QueryForObject<int>("Promotion.Vip.CountVip", condition);
            return ret;
        }

        /// <summary>
        /// 保存从终端上来的会员信息
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="vipExchanges">会员信息列表</param>
        /// <returns></returns>
        public string SaveVips(LoggingSessionInfo loggingSession, IList<VipExchangeInfo> vipExchanges)
        {
            ISqlMapper sqlmap = cSqlMapper.Instance(loggingSession.CurrentLoggingManager);
            try
            {
                sqlmap.BeginTransaction();
                Hashtable ht = new Hashtable();
                foreach (VipExchangeInfo vip in vipExchanges)
                {
                    //校验会员是否是新建还是修改
                    VipInfo old_vip = sqlmap.QueryForObject<VipInfo>("Promotion.Vip.SelectByID", vip.ID);
                    bool is_new = (old_vip == null);
                    //校验会员号
                    ht["vip_no"] = vip.No;
                    ht["vip_id"] = vip.ID;
                    int count = sqlmap.QueryForObject<int>("Promotion.Vip.CountVip", ht);
                    if (count > 0)
                    {
                        sqlmap.RollBackTransaction();
                        return string.Format("会员号已经存在[会员号:{0},ID:{1}]", vip.No, vip.ID); 
                    }
                     
                    if (is_new)
                    {
                        //新建会员
                        sqlmap.Insert("Promotion.Vip.Insert", vip);
                    }
                    else
                    {
                        //修改会员
                        //判断会员状态
                        if (old_vip.Status == -1)
                        {   
                            sqlmap.RollBackTransaction();
                            return string.Format("会员被停用,不能修改[会员号:{0},ID:{1}]", vip.No, vip.ID);
                        }
                        count = sqlmap.Update("Promotion.Vip.Update", vip);
                        if (count == 0)
                        {
                            sqlmap.RollBackTransaction();
                            return string.Format("会员版本不正确,不能修改[会员号:{0},ID:{1}]", vip.No, vip.ID);
                        }
                    }
                }
                sqlmap.CommitTransaction();
            }
            catch (Exception ex)
            {
                sqlmap.RollBackTransaction();
                throw ex;
            }
            return "";
        }
        #endregion
    }
}
