using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.DbAccess.t_unit;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_Inout
{
    public class T_InoutFacade
    {
        private readonly T_InoutCMD _cmd = new T_InoutCMD();
        private readonly T_InoutQuery _query = new T_InoutQuery();
        private readonly t_unit.t_unitQuery _unitQuery = new t_unitQuery();
        private readonly TInoutStatusCMD _tinoutStatusCMD = new TInoutStatusCMD();

        public void Create(T_InoutEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_InoutEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_InoutEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public T_InoutEntity GetOrderByOrderId(string orderid)
        {
            return _query.GetOrderByOrderId(orderid);
        }

        public void UpdateDeliveryTime(string deliveryTime, string orderid)
        {
            string field = "reserveDay";
            var result = _query.UpdateInoutByField(field, orderid, deliveryTime);
        }

        public void UpdateSalesUnitId(string unitid, string orderid,string customerid)
        {
            string field = "sales_unit_id";

            var result = _query.UpdateInoutByField(field, orderid, unitid);

            var entity = GetOrderByOrderId(orderid);
            var unitEntity = _unitQuery.GetUnitById(unitid);

            TInoutStatusEntity info = new TInoutStatusEntity();
            info.InoutStatusID = Guid.Parse(Guid.NewGuid().ToString());
            info.OrderID = unitid;
            info.CustomerID = customerid;
            info.OrderStatus = Convert.ToInt32(entity.Field7);
            info.StatusRemark = "订单门店变更[被" + unitEntity.unit_name + "退回]";
            info.CheckResult = 1;
            info.CreateTime = DateTime.Now;
            info.CreateBy = "";
            info.LastUpdateBy = "";
            info.LastUpdateTime = info.CreateTime;
            info.IsDelete = 0;

            _tinoutStatusCMD.Create(info);

        }

        public void UpdateStatus(string status, string orderid, string customerid)
        {
            var entity = GetOrderByOrderId(orderid);

            if (entity.Field8 == "1")
            {
                var result = _query.UpdateInoutStauts(orderid, "500", "待发货");
            }
            else if (entity.Field8 == "2")
            {
                var result = _query.UpdateInoutStauts(orderid, "410", "待备货");
            }
            else if (entity.Field8 == "4")
            {
                var result = _query.UpdateInoutStauts(orderid, "520", "待服务");
            }


            TInoutStatusEntity info = new TInoutStatusEntity();
            info.InoutStatusID = Guid.Parse(Guid.NewGuid().ToString());
            info.OrderID = orderid;
            info.CustomerID = customerid;
            info.OrderStatus = Convert.ToInt32(entity.Field7);
            info.StatusRemark = "订单状态变更[操作人：]";
            info.CheckResult = 1;
            info.CreateTime = DateTime.Now;
            info.CreateBy = "";
            info.LastUpdateBy = "";
            info.LastUpdateTime = info.CreateTime;
            info.IsDelete = 0;

            _tinoutStatusCMD.Create(info);
        }

        public void UpdateNoApprovalStatus(string orderid, string customerid)
        {
            var entity = GetOrderByOrderId(orderid);

            var result = _query.UpdateInoutStauts(orderid, "310", "退款中");

            TInoutStatusEntity info = new TInoutStatusEntity();
            info.InoutStatusID = Guid.Parse(Guid.NewGuid().ToString());
            info.OrderID = orderid;
            info.CustomerID = customerid;
            info.OrderStatus = Convert.ToInt32(entity.Field7);
            info.StatusRemark = "订单状态变更[操作人：]";
            info.CheckResult = 1;
            info.CreateTime = DateTime.Now;
            info.CreateBy = "";
            info.LastUpdateBy = "";
            info.LastUpdateTime = info.CreateTime;
            info.IsDelete = 0;

            _tinoutStatusCMD.Create(info);
        }

        public void UpdateStockDoneStatus(string orderid, string customerid)
        {
            var entity = GetOrderByOrderId(orderid);

            if (entity.Field8 == "2")
            {
                var result = _query.UpdateInoutStauts(orderid, "510", "待提货");

                TInoutStatusEntity info = new TInoutStatusEntity();
                info.InoutStatusID = Guid.Parse(Guid.NewGuid().ToString());
                info.OrderID = orderid;
                info.CustomerID = customerid;
                info.OrderStatus = Convert.ToInt32(entity.Field7);
                info.StatusRemark = "订单状态变更[操作人：]";
                info.CheckResult = 1;
                info.CreateTime = DateTime.Now;
                info.CreateBy = "";
                info.LastUpdateBy = "";
                info.LastUpdateTime = info.CreateTime;
                info.IsDelete = 0;

                _tinoutStatusCMD.Create(info);
            }
            
        }
    }
    
}
