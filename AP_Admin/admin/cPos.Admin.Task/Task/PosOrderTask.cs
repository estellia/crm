using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Admin.Model;
using cPos.Model;
using cPos.Admin.Model.Customer;

namespace cPos.Admin.Task
{
    public class PosOrderTask
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

                var apInoutService = new Service.InoutService();
                var bsService = BsWebService.CreatePosInoutService(customer);
                count = bsService.GetPosInoutNotPackagedCount(customerId, unitId);

                IList<InoutInfo> orderList = new List<InoutInfo>();
                string dataBatId = string.Empty;
                while (count > 0 && startRow < count)
                {
                    dataBatId = Utils.NewGuid();
                    orderList.Clear();
                    var tmpList = bsService.GetPosInoutListPackaged(customerId, unitId, 0, rowsCount, dataBatId);
                    if (tmpList != null && tmpList.Length > 0)
                    {
                        foreach (var tmpObj in tmpList)
                        {
                            var orderObj = new InoutInfo();
                            orderObj.order_id = tmpObj.order_id;
                            orderObj.order_no = tmpObj.order_no;
                            orderObj.order_type_id = tmpObj.order_type_id;
                            orderObj.order_reason_id = tmpObj.order_reason_id;
                            orderObj.red_flag = tmpObj.red_flag;
                            orderObj.ref_order_id = tmpObj.ref_order_id;
                            orderObj.ref_order_no = tmpObj.ref_order_no;
                            orderObj.warehouse_id = tmpObj.warehouse_id;
                            orderObj.order_date = tmpObj.order_date;
                            orderObj.request_date = tmpObj.request_date;
                            orderObj.complete_date = tmpObj.complete_date;
                            orderObj.create_unit_id = tmpObj.create_unit_id;
                            orderObj.unit_id = tmpObj.unit_id;
                            orderObj.related_unit_id = tmpObj.related_unit_id;
                            //orderObj.ref_unit_id = tmpObj.ref_unit_id;
                            orderObj.pos_id = tmpObj.pos_id;
                            orderObj.shift_id = tmpObj.shift_id;
                            orderObj.sales_user = tmpObj.sales_user;
                            orderObj.total_amount = tmpObj.total_amount;
                            orderObj.discount_rate = tmpObj.discount_rate;
                            orderObj.actual_amount = tmpObj.actual_amount;
                            orderObj.receive_points = tmpObj.receive_points;
                            orderObj.pay_points = tmpObj.pay_points;
                            orderObj.pay_id = tmpObj.pay_id;
                            orderObj.print_times = tmpObj.print_times;
                            orderObj.carrier_id = tmpObj.carrier_id;
                            orderObj.remark = tmpObj.remark;
                            orderObj.status = tmpObj.status;
                            orderObj.create_time = tmpObj.create_time;
                            orderObj.create_user_id = tmpObj.create_user_id;
                            orderObj.approve_time = tmpObj.approve_time;
                            orderObj.approve_user_id = tmpObj.approve_user_id;
                            orderObj.send_user_id = tmpObj.send_user_id;
                            orderObj.send_time = tmpObj.send_time;
                            orderObj.accpect_user_id = tmpObj.accpect_user_id;
                            orderObj.accpect_time = tmpObj.accpect_time;
                            orderObj.modify_user_id = tmpObj.modify_user_id;
                            orderObj.modify_time = tmpObj.modify_time;
                            orderObj.total_qty = tmpObj.total_qty;
                            orderObj.total_retail = tmpObj.total_retail;
                            orderObj.keep_the_change = tmpObj.keep_the_change;
                            orderObj.wiping_zero = tmpObj.wiping_zero;
                            orderObj.vip_no = tmpObj.vip_no;
                            orderObj.data_from_id = tmpObj.data_from_id;
                            orderObj.sales_unit_id = tmpObj.sales_unit_id;
                            orderObj.purchase_unit_id = tmpObj.purchase_unit_id;
                            orderObj.if_flag = tmpObj.if_flag;
                            orderList.Add(orderObj);
                        }
                        var tmpDetailList = bsService.GetPosInoutDetailListPackaged(customerId, unitId, tmpList);
                        IList<InoutDetailInfo> detailList = new List<InoutDetailInfo>();
                        if (tmpDetailList != null && tmpDetailList.Length > 0)
                        {
                            foreach (var tmpDetail in tmpDetailList)
                            {
                                var detailObj = new InoutDetailInfo();
                                detailObj.order_detail_id = tmpDetail.order_detail_id;
                                detailObj.order_id = tmpDetail.order_id;
                                detailObj.ref_order_detail_id = tmpDetail.ref_order_detail_id;
                                detailObj.sku_id = tmpDetail.sku_id;
                                detailObj.unit_id = tmpDetail.unit_id;
                                detailObj.enter_qty = tmpDetail.enter_qty;
                                detailObj.order_qty = tmpDetail.order_qty;
                                detailObj.enter_price = tmpDetail.enter_price;
                                detailObj.std_price = tmpDetail.std_price;
                                detailObj.discount_rate = tmpDetail.discount_rate;
                                detailObj.retail_price = tmpDetail.retail_price;
                                detailObj.retail_amount = tmpDetail.retail_amount;
                                detailObj.enter_amount = tmpDetail.enter_amount;
                                detailObj.receive_points = tmpDetail.receive_points;
                                detailObj.pay_points = tmpDetail.pay_points;
                                detailObj.remark = tmpDetail.remark;
                                detailObj.order_detail_status = tmpDetail.order_detail_status;
                                detailObj.display_index = tmpDetail.display_index;
                                detailObj.create_time = tmpDetail.create_time;
                                detailObj.create_user_id = tmpDetail.create_user_id;
                                detailObj.modify_time = tmpDetail.modify_time;
                                detailObj.modify_user_id = tmpDetail.modify_user_id;
                                detailObj.ref_order_id = tmpDetail.ref_order_id;
                                detailObj.if_flag = tmpDetail.if_flag;
                                detailObj.pos_order_code = tmpDetail.pos_order_code;
                                detailObj.plan_price = tmpDetail.plan_price;
                                detailList.Add(detailObj);
                            }
                        }

                        // 保存Order
                        data = apInoutService.SaveInoutList(true, orderList, detailList);

                        // 更新标记
                        bsService.SetPosInoutIfFlagInfo(customerId, dataBatId);
                    }
                    startRow += orderList.Count;
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
