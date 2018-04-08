using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Service;

namespace cPos.ExchangeBsService
{   
    /// <summary>
    /// 初始化
    /// </summary>
    public class InitialService
    {
        /// <summary>
        /// 根据客户号与门店号获取终端初始信息
        /// </summary>
        /// <param name="customer_code"></param>
        /// <param name="unit_code"></param>
        /// <returns></returns>
        public InitialInfo GetPCInitialInfo(string customer_code, string unit_code)
        {
            cPos.Service.InitialService initialService = new Service.InitialService();
            InitialInfo initialInfo = new InitialInfo();
            initialInfo = initialService.GetPCInitialInfo(customer_code, unit_code);
            return initialInfo;
        }

        /// <summary>
        /// 根据客户号与门店号获取终端初始信息(设置pos终端序列号)
        /// </summary>
        /// <param name="customer_code">客户号码</param>
        /// <param name="unit_code">门店号码</param>
        /// <param name="posSN">终端序列号</param>
        /// <returns></returns>
        public InitialInfo GetPCInitialInfo(string customer_code, string unit_code,string posSN)
        {
            cPos.Service.InitialService initialService = new Service.InitialService();
            InitialInfo initialInfo = new InitialInfo();
            initialInfo = initialService.GetPCInitialInfo(customer_code, unit_code,posSN);
            return initialInfo;
        }
    }
}
