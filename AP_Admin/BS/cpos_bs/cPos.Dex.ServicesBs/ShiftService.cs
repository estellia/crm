using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;
using cPos.Dex.ContractModel;
using cPos.Dex.Services;

namespace cPos.Dex.ServicesBs
{
    public class ShiftService
    {
        #region CheckShifts
        /// <summary>
        /// 检查Shift
        /// </summary>
        public Hashtable CheckShift(string orderType, ShiftContract order)
        {
            Hashtable htError = new Hashtable();
            if (order == null)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "班次信息不能为空", true);
                return htError;
            }
            if (order.shift_id == null || order.shift_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "班次ID不能为空", true);
                return htError;
            }
            if (order.sales_user == null || order.sales_user.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "收营员不能为空", true);
                return htError;
            }
            //if (order.pos_id == null || order.pos_id.Trim().Length == 0)
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A016, "pos标识不能为空", true);
            //    return htError;
            //}
            //if (order.parent_shift_id == null || order.parent_shift_id.Trim().Length == 0)
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A016, "上一个班次标识不能为空", true);
            //    return htError;
            //}
            if (order.unit_id == null || order.unit_id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "门店标识不能为空", true);
                return htError;
            }
            if (order.deposit_amount == null || order.deposit_amount.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "准备金不能为空", true);
                return htError;
            }
            if (order.sale_amount == null || order.sale_amount.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "销售金额不能为空", true);
                return htError;
            }
            if (order.return_amount == null || order.return_amount.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "退款金额不能为空", true);
                return htError;
            }
            if (order.pos_date == null || order.pos_date.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "营业日期不能为空", true);
                return htError;
            }
            if (order.sales_qty == null || order.sales_qty.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "销售笔数不能为空", true);
                return htError;
            }
            if (order.sales_total_amount == null || order.sales_total_amount.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "销售总金额不能为空", true);
                return htError;
            }
            if (order.open_time == null || order.open_time.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "开班时间不能为空", true);
                return htError;
            }
            if (order.close_time == null || order.close_time.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "交班时间不能为空", true);
                return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        /// <summary>
        /// 检查Shift集合
        /// </summary>
        public Hashtable CheckShifts(string orderType, IList<ShiftContract> orders)
        {
            Hashtable htError = new Hashtable();
            if (orders == null || orders.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "班次集合不能为空", true);
                return htError;
            }
            foreach (var order in orders)
            {
                htError = CheckShift(orderType, order);
                if (!Convert.ToBoolean(htError["status"])) return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }
        #endregion

        #region SaveShifts
        /// <summary>
        /// 保存Shift集合
        /// </summary>
        public void SaveShifts(IList<ShiftContract> orders,
            string customerId, string unitId, string userId, string orderType)
        {
            if (customerId == null || customerId.Trim().Length == 0)
                throw new Exception("客户ID不能为空");
            if (userId == null || userId.Trim().Length == 0)
                throw new Exception("用户ID不能为空");
            //if (orderType == null || orderType.Trim().Length == 0)
            //    throw new Exception("班次处理类型不能为空");

            var orderInfoList = ToShiftModels(orders);

            // Save
            var shiftService = new ExchangeBsService.ShiftBsServer();
            shiftService.SetShiftInfoList(customerId, unitId, userId, orderInfoList);
        }
        #endregion

        #region ToShiftModels
        public IList<cPos.Model.ShiftInfo> ToShiftModels(IList<ShiftContract> models)
        {
            if (models == null) return null;
            var objs = new List<cPos.Model.ShiftInfo>();
            foreach (var model in models)
            {
                objs.Add(ToShiftModel(model));
            }
            return objs;
        }

        public IList<ShiftContract> ToShiftContracts(IList<cPos.Model.ShiftInfo> models)
        {
            if (models == null) return null;
            var objs = new List<ShiftContract>();
            foreach (var model in models)
            {
                objs.Add(ToShiftContract(model));
            }
            return objs;
        }

        #region ToShiftModel
        public cPos.Model.ShiftInfo ToShiftModel(ShiftContract model)
        {
            var obj = new cPos.Model.ShiftInfo();
            obj.shift_id = model.shift_id;
            obj.sales_user = model.sales_user;
            obj.pos_id = model.pos_id;
            obj.parent_shift_id = model.parent_shift_id;
            obj.unit_id = model.unit_id;
            obj.deposit_amount = Utils.GetDecimalVal(model.deposit_amount);
            obj.sale_amount = Utils.GetDecimalVal(model.sale_amount);
            obj.return_amount = Utils.GetDecimalVal(model.return_amount);
            obj.pos_date = model.pos_date;
            obj.sales_qty = Utils.GetIntVal(model.sales_qty);
            obj.sales_total_amount = Utils.GetDecimalVal(model.sales_total_amount);
            obj.open_time = model.open_time;
            obj.close_time = model.close_time;
            obj.create_time = model.create_time;
            obj.create_user_id = model.create_user_id;
            obj.modify_time = model.modify_time;
            obj.modify_user_id = model.modify_user_id;
            obj.sales_total_qty = Utils.GetIntVal(model.sales_total_qty);
            obj.sales_total_total_amount = Utils.GetDecimalVal(model.sales_total_total_amount);
            return obj;
        }
        #endregion

        #region ToShiftContract
        public ShiftContract ToShiftContract(cPos.Model.ShiftInfo model)
        {
            var obj = new ShiftContract();
            obj.shift_id = model.shift_id;
            obj.sales_user = model.sales_user;
            obj.pos_id = model.pos_id;
            obj.parent_shift_id = model.parent_shift_id;
            obj.unit_id = model.unit_id;
            obj.deposit_amount = Utils.GetStrVal(model.deposit_amount);
            obj.sale_amount = Utils.GetStrVal(model.sale_amount);
            obj.return_amount = Utils.GetStrVal(model.return_amount);
            obj.pos_date = model.pos_date;
            obj.sales_qty = Utils.GetStrVal(model.sales_qty);
            obj.sales_total_amount = Utils.GetStrVal(model.sales_total_amount);
            obj.open_time = model.open_time;
            obj.close_time = model.close_time;
            obj.create_time = model.create_time;
            obj.create_user_id = model.create_user_id;
            obj.modify_time = model.modify_time;
            obj.modify_user_id = model.modify_user_id;
            obj.sales_total_qty = Utils.GetStrVal(model.sales_total_qty);
            obj.sales_total_total_amount = Utils.GetStrVal(model.sales_total_total_amount);
            return obj;
        }
        #endregion

        #endregion
    }
}
