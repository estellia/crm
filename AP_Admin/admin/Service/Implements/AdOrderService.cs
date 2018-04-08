using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Admin.Model;
using System.Collections;
using cPos.Admin.Component.SqlMappers;
using cPos.Admin.Component;
using cPos.Model.Advertise;

namespace cPos.Admin.Service
{
    /// <summary>
    /// AdOrderService
    /// </summary>
    public class AdOrderService : BaseService
    {
        #region AdOrder保存
        /// <summary>
        /// AdOrder保存
        /// </summary>
        /// <param name="models">models</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <returns>Hashtable: 
        ///  status(成功：true, 失败：false)
        ///  error(错误描述)
        /// </returns>
        public Hashtable SaveAdOrderList(bool IsTrans, IList<AdvertiseOrderInfo> models)
        {
            Hashtable ht = new Hashtable();
            ht["status"] = false;
            try
            {
                if (IsTrans) MSSqlMapper.Instance().BeginTransaction();
                foreach (var model in models)
                {
                    if (!CheckExistAdOrder(model))
                    {
                        MSSqlMapper.Instance().Insert("AdOrder.InsertAdOrder", model);
                    }
                }

                if (IsTrans) MSSqlMapper.Instance().CommitTransaction();
                ht["status"] = true;
            }
            catch (Exception ex)
            {
                if (IsTrans) MSSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
            return ht;
        }

        /// <summary>
        /// 检查AdOrder是否已存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckExistAdOrder(AdvertiseOrderInfo model)
        {
            int count = MSSqlMapper.Instance().QueryForObject<int>("AdOrder.CheckExistAdOrder", model);
            return count > 0 ? true : false;
        }
        #endregion

        #region 获取AdOrder
        /// <summary>
        /// 获取AdOrder
        /// </summary>
        /// <param name="customer_id">客户ID</param>
        /// <param name="unit_id">门店ID</param>
        /// <param name="ht">查询条件</param>
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public IList<AdvertiseOrderInfo> GetAdOrderListPackaged(string customer_id, string unit_id,
            Hashtable ht, int startRowIndex, int maxRowCount, string batId)
        {
            ht["customer_id"] = customer_id;
            ht["unit_id"] = unit_id;
            //ht["if_flag"] = "0";
            ht["StartRow"] = startRowIndex;
            ht["EndRow"] = startRowIndex + maxRowCount;

            IList<AdvertiseOrderInfo> list = new List<AdvertiseOrderInfo>();
            list = MSSqlMapper.Instance().QueryForList<AdvertiseOrderInfo>("AdOrder.GetAdOrderListPackaged", ht);

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    SetAdOrderBatId(item.order_id, batId, customer_id);
                }
            }

            return list;
        }

        /// <summary>
        /// AdOrder查询总数量
        /// </summary>
        public int GetAdOrderCountPackaged(string customer_id, string unit_id, Hashtable ht)
        {
            ht["customer_id"] = customer_id;
            ht["unit_id"] = unit_id;
            //ht["if_flag"] = "0";
            return MSSqlMapper.Instance().QueryForObject<int>("AdOrder.GetAdOrderCountPackaged", ht);
        }
        #endregion

        /// <summary>
        /// 更新导出标记
        /// </summary>
        public void SetAdOrderBatId(string orderId, string batId, string customer_id)
        {
            Hashtable ht = new Hashtable();
            ht["order_id"] = orderId;
            ht["bat_id"] = batId;
            ht["customer_id"] = customer_id;
            MSSqlMapper.Instance().Update("AdOrder.SetAdOrderBatId", ht);
        }

        /// <summary>
        /// 更新BatId
        /// </summary>
        public void SetAdOrderBatId(IList<AdvertiseOrderInfo> list, string batId, string customer_id)
        {
            MSSqlMapper.Instance().BeginTransaction();
            try
            {
                foreach (var item in list)
                {
                    SetAdOrderBatId(item.order_id, batId, customer_id);
                }
                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 更新导出标记
        /// </summary>
        public void SetAdOrderListFlagByBatId(string customerId, string batId)
        {
            Hashtable ht = new Hashtable();
            ht["customer_id"] = customerId;
            ht["bat_id"] = batId;
            MSSqlMapper.Instance().Update("AdOrder.SetAdOrderListFlagByBatId", ht);
        }

        /// <summary>
        /// 获取Advertise
        /// </summary>
        public IList<AdvertiseInfo> GetAdList(string order_id)
        {
            Hashtable ht = new Hashtable();
            ht["order_id"] = order_id;
            return MSSqlMapper.Instance().QueryForList<AdvertiseInfo>("AdOrder.GetAdList", ht);
        }

        /// <summary>
        /// 获取AdvertiseOrderAdvertise
        /// </summary>
        public IList<AdvertiseOrderAdvertiseInfo> GetOrderAdList(string order_id)
        {
            Hashtable ht = new Hashtable();
            ht["order_id"] = order_id;
            return MSSqlMapper.Instance().QueryForList<AdvertiseOrderAdvertiseInfo>("AdOrder.GetOrderAdList", ht);
        }

        /// <summary>
        /// 获取AdvertiseOrderUnit
        /// </summary>
        public IList<AdvertiseOrderUnitInfo> GetOrderUnitList(string order_id, string customer_id)
        {
            Hashtable ht = new Hashtable();
            ht["order_id"] = order_id;
            ht["customer_id"] = customer_id;
            return MSSqlMapper.Instance().QueryForList<AdvertiseOrderUnitInfo>("AdOrder.GetOrderUnitList", ht);
        }
    }
}
