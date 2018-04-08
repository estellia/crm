using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using cPos.Model;
using cPos.Admin.Model.Initial;
using cPos.Admin.Model.Customer;
using System.Collections;
using cPos.Admin.Component.SqlMappers;


namespace cPos.Admin.Service.Implements
{
    /// <summary>
    /// 获取终端的POS初始化信息
    /// </summary>
    public class InitialService :BaseService
    {
        /// <summary>
        /// 根据客户号与门店号码获取门店初始化详细信息
        /// </summary>
        /// <param name="customer_code">客户号</param>
        /// <param name="unit_code">门店号</param>
        /// <param name="strError">错误信息输出</param>
        /// <returns></returns>
        public InitialInfo GetPCInitialInfo(string customer_code, string unit_code,out string strError)
        {
            CustomerService customerService = new CustomerService();
            InitialInfo initialInfo = new InitialInfo();
            //获取客户标识
            string costomer_id = new CustomerService().GetCustomerIdByCode(customer_code);
            if (costomer_id.Equals(""))
            {
                strError = "客户不存在";
            }
            else
            {
                CustomerInfo customerInfo = new CustomerInfo();
                //获取客户信息
                customerInfo = customerService.GetCustomerByID(costomer_id, false, false);
                CustomerShopInfo customerShopInfo = new CustomerShopInfo();
                //获取门店标识
                string unit_id = customerService.GetCustomerShopIdByCode(customer_code, unit_code);
                if (unit_id.Equals("")) { strError = "门店不存在"; }
                else
                {
                    //获取门店信息
                    customerShopInfo = customerService.GetCustomerShopByID(unit_id);

                }
                strError = "ok";

                initialInfo.customerInfo = customerInfo;
                initialInfo.customerShopInfo = customerShopInfo;
            }
            return initialInfo;
        }
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="customer_code"></param>
        /// <returns></returns>
        public CustomerInfo GetCustomerInfoByCode(string customer_code)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            //获取客户标识
            string costomer_id = new CustomerService().GetCustomerIdByCode(customer_code);
            //获取客户信息
            CustomerService customerService = new CustomerService();
            customerInfo = customerService.GetCustomerByID(costomer_id, false, false);

            return customerInfo;
        }
    }
}
