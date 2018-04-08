using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Admin.Model;
//using cPos.Model;
using cPos.Model.Advertise;
using cPos.Admin.Model.Customer;

namespace cPos.Admin.Task
{
    public class AdOrderTask
    {
        public Hashtable Run(string batId, cPos.Admin.Model.Customer.CustomerInfo customer)
        {
            string bizId = Utils.NewGuid();
            var data = new Hashtable();
            try
            {
                string customerId = customer.ID;
                int count = 0; // 总数量
                int rowsCount = 10; // 每页数量
                int startRow = 0;
                string unitId = null;

                var apService = new Service.AdOrderService();
                var bsService = BsWebService.CreateAdvertiseOrderService(customer);
                var apCustomerService = new cPos.Admin.Service.Implements.CustomerService();

                //// Units
                //Hashtable htUnits = new Hashtable();
                //htUnits["CustomerID"] = customerId;
                //htUnits["ShopStatus"] = "1";
                //IList<cPos.Admin.Model.Customer.CustomerShopInfo> units =
                //    apCustomerService.GetAllShopList(htUnits);

                //unitId = unitObj.ID;
                Hashtable ht = new Hashtable();
                count = apService.GetAdOrderCountPackaged(customerId, unitId, ht);

                IList<AdvertiseOrderInfo> orderList = new List<AdvertiseOrderInfo>();
                string dataBatId = string.Empty;
                while (count > 0 && startRow < count)
                {
                    dataBatId = Utils.NewGuid();
                    orderList.Clear();
                    var tmpOrderList = apService.GetAdOrderListPackaged(customerId, unitId, ht, 0, rowsCount, dataBatId);
                    if (tmpOrderList != null && tmpOrderList.Count > 0)
                    {
                        foreach (var tmpOrderObj in tmpOrderList)
                        {
                            // 获取并保存广告信息
                            var adList = apService.GetAdList(tmpOrderObj.order_id);
                            if (adList != null)
                            {
                                bsService.SetAdvertiseInfoXML(Utils.Serialiaze(adList), customerId);
                            }

                            // 获取并保存与广告关系信息
                            var orderAdList = apService.GetOrderAdList(tmpOrderObj.order_id);
                            if (orderAdList != null)
                            {
                                bsService.SetAdvertiseOrderAdvertiseInfoXML(Utils.Serialiaze(orderAdList), customerId);
                            }

                            // 获取并保存与门店关系信息
                            var orderUnitList = apService.GetOrderUnitList(tmpOrderObj.order_id, customerId);
                            if (orderUnitList != null)
                            {
                                bsService.SetAdvertiseOrderUnitInfoXML(Utils.Serialiaze(orderUnitList), customerId);
                            }
                        }

                        // 保存订单信息
                        string orderXmlStr = Utils.Serialiaze(tmpOrderList);
                        bsService.SetAdvertiseOrderInfos(orderXmlStr, customerId);

                        // 更新标记
                        apService.SetAdOrderListFlagByBatId(customerId, dataBatId);
                    }
                    startRow += tmpOrderList.Count;
                }

                data["status"] = Utils.GetStatus(true);
                return data;
            }
            catch (Exception ex)
            {
                data["status"] = Utils.GetStatus(false);
                data["error"] = ex.ToString();
                Console.WriteLine(ex.ToString());
            }
            return data;
        }
    }
}
