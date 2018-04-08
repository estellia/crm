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

namespace cPos.Dex.ServicesAP
{
    public class BasicService
    {
        #region CheckApplyCustomerAndUnits
        /// <summary>
        /// 检查CustomerUnitApply
        /// </summary>
        public Hashtable CheckApplyCustomerAndUnit(CustomerUnitApply order, string type)
        {
            Hashtable htError = new Hashtable();
            if (order == null)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "信息不能为空", true);
                return htError;
            }
            if (order.customers != null)
            {
                foreach (var customerObj in order.customers)
                {
                    if (customerObj == null) continue;
                    if (customerObj.customer_id == null || customerObj.customer_id.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, customerObj.customer_name + ":客户标识不能为空", true);
                        return htError;
                    }
                    if (customerObj.customer_code == null || customerObj.customer_code.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, customerObj.customer_id + ":客户代码不能为空", true);
                        return htError;
                    }
                    if (customerObj.customer_name == null || customerObj.customer_name.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, customerObj.customer_id + ":客户名称不能为空", true);
                        return htError;
                    }
                }
            }
            if (order.units != null)
            {
                foreach (var unitObj in order.units)
                {
                    if (unitObj == null) continue;
                    if (unitObj.unit_id == null || unitObj.unit_id.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, unitObj.unit_name + ":门店标识不能为空", true);
                        return htError;
                    }
                    if (unitObj.customer_id == null || unitObj.customer_id.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, unitObj.unit_id + ":门店的客户标识不能为空", true);
                        return htError;
                    }
                    if (unitObj.unit_code == null || unitObj.unit_code.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, unitObj.unit_id + ":门店代码不能为空", true);
                        return htError;
                    }
                    if (unitObj.unit_name == null || unitObj.unit_name.Trim().Length == 0)
                    {
                        htError = ErrorService.OutputError(ErrorCode.A016, unitObj.unit_id + ":门店名称不能为空", true);
                        return htError;
                    }
                }
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        /// <summary>
        /// 检查CustomerUnitApply集合
        /// </summary>
        public Hashtable CheckApplyCustomerAndUnit(IList<CustomerUnitApply> orders, string type)
        {
            Hashtable htError = new Hashtable();
            if (orders == null || orders.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "集合不能为空", true);
                return htError;
            }
            foreach (var order in orders)
            {
                htError = CheckApplyCustomerAndUnit(order, type);
                if (!Convert.ToBoolean(htError["status"])) return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }
        #endregion

        #region ApplyCustomerAndUnit
        /// <summary>
        /// 申请客户及门店
        /// </summary>
        public string ApplyCustomerAndUnit(CustomerUnitApply order,
            string customerId, string unitId, string userId)
        {
            string error = string.Empty;
            if (order == null) return null;

            var customerInfo = ToCustomerInfoModel(order);
            var unitInfo = ToCustomerShopInfoModel(order);

            var apService = new Dex.WsService.AP.APBasicService();

            // 处理客户/门店
            string content1 = string.Empty;
            string content2 = string.Empty;
            if (customerInfo != null && customerInfo.CustomerList != null)
            {
                Jayrock.Json.JsonTextWriter writer1 = new Jayrock.Json.JsonTextWriter();
                Jayrock.Json.Conversion.JsonConvert.Export(customerInfo, writer1);
                content1 = writer1.ToString();
            }
            if (unitInfo != null && unitInfo.CustomerShopList != null)
            {
                Jayrock.Json.JsonTextWriter writer2 = new Jayrock.Json.JsonTextWriter();
                Jayrock.Json.Conversion.JsonConvert.Export(unitInfo, writer2);
                content2 = writer2.ToString();
            }
            error = apService.ApplyCustomerAndUnit(userId, content1, content2);
            return error;
        }
        #endregion

        #region ToCustomerInfoModel
        public cPos.Admin.Model.Customer.CustomerInfo ToCustomerInfoModel(CustomerUnitApply model)
        {
            var obj = new cPos.Admin.Model.Customer.CustomerInfo();
            obj.CustomerList = new List<cPos.Admin.Model.Customer.CustomerInfo>();

            if (model.customers != null && model.customers.Count > 0)
            {
                foreach (var customerObj in model.customers)
                {
                    var apCusttomerObj = new Admin.Model.Customer.CustomerInfo();
                    apCusttomerObj.ID = customerObj.customer_id;
                    apCusttomerObj.Code = customerObj.customer_code;
                    apCusttomerObj.Name = customerObj.customer_name;
                    apCusttomerObj.EnglishName = customerObj.customer_name_en;
                    apCusttomerObj.Address = customerObj.address;
                    apCusttomerObj.PostCode = customerObj.post_code;
                    apCusttomerObj.Contacter = customerObj.contacter;
                    apCusttomerObj.Tel = customerObj.tel;
                    apCusttomerObj.Fax = customerObj.fax;
                    apCusttomerObj.Email = customerObj.email;
                    apCusttomerObj.Cell = customerObj.cell;
                    apCusttomerObj.Memo = customerObj.memo;
                    obj.CustomerList.Add(apCusttomerObj);
                }
            }
            return obj;
        }
        #endregion

        #region ToCustomerShopInfoModel
        public cPos.Admin.Model.Customer.CustomerShopInfo ToCustomerShopInfoModel(CustomerUnitApply model)
        {
            var obj = new cPos.Admin.Model.Customer.CustomerShopInfo();
            obj.CustomerShopList = new List<cPos.Admin.Model.Customer.CustomerShopInfo>();

            if (model.units != null && model.units.Count > 0)
            {
                foreach (var unitObj in model.units)
                {
                    var apUnitObj = new Admin.Model.Customer.CustomerShopInfo();
                    apUnitObj.ID = unitObj.unit_id;
                    apUnitObj.customer_id = unitObj.customer_id;
                    apUnitObj.Code = unitObj.unit_code;
                    apUnitObj.Name = unitObj.unit_name;
                    apUnitObj.EnglishName = unitObj.unit_name_en;
                    apUnitObj.ShortName = unitObj.unit_name_short;
                    apUnitObj.City = unitObj.unit_city_id;
                    apUnitObj.Address = unitObj.unit_address;
                    apUnitObj.Contact = unitObj.unit_contact;
                    apUnitObj.Tel = unitObj.unit_tel;
                    apUnitObj.Fax = unitObj.unit_fax;
                    apUnitObj.Email = unitObj.unit_email;
                    apUnitObj.PostCode = unitObj.unit_postcode;
                    apUnitObj.Remark = unitObj.unit_remark;
                    apUnitObj.longitude = unitObj.longitude;
                    apUnitObj.dimension = unitObj.dimension;
                    apUnitObj.shop_url1 = unitObj.shop_url1;
                    apUnitObj.shop_url2 = unitObj.shop_url2;
                    obj.CustomerShopList.Add(apUnitObj);
                }
            }
            return obj;
        }
        #endregion
    }
}
