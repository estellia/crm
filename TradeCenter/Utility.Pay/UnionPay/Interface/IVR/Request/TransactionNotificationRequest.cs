/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 18:25:58
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using JIT.Utility.Pay.UnionPay.ExtensionMethod;
using JIT.Utility.Pay.UnionPay.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface.IVR.ValueObject;

namespace JIT.Utility.Pay.UnionPay.Interface.IVR.Request
{
    /// <summary>
    /// 交易通知请求
    /// <remarks>
    /// <para>支付前置 -> 商户平台</para>
    /// </remarks> 
    /// </summary>
    public class TransactionNotificationRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TransactionNotificationRequest()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 请求内容
        /// </summary>
        protected XmlDocument Request { get; set; }
        #endregion

        #region 参数集
        private IVRTransTypes? _transType = null;
        /// <summary>
        /// 交易类型
        /// </summary>
        public IVRTransTypes? TransType
        {
            get
            {
                if (this._transType == null)
                {
                    string strVal = this.GetNodeTextByXPath("//transType");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._transType = strVal.ParseTransTypes();
                    }
                }
                return this._transType;
            }
        }

        /// <summary>
        /// 商户代码
        /// </summary>
        public string MerchantID
        {
            get { return this.GetNodeTextByXPath("//merchantId"); }
        }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string MerchantOrderID
        {
            get { return this.GetNodeTextByXPath("//merchantOrderId"); }
        }

        private int? _merchantOrderAmt = null;
        /// <summary>
        /// 商户订单金额
        /// <remarks>
        /// <para>以分为单位</para>
        /// </remarks>
        /// </summary>
        public int? MerchantOrderAmt
        {
            get
            {
                if (this._merchantOrderAmt == null)
                {
                    string strVal = this.GetNodeTextByXPath("//merchantOrderAmt");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._merchantOrderAmt = int.Parse(strVal);
                    }
                }
                return this._merchantOrderAmt;
            }
        }

        /// <summary>
        /// 清算日期
        /// <remarks>
        /// <para>格式为：MMDD</para>
        /// </remarks>
        /// </summary>
        public string SettleDate
        {
            get { return this.GetNodeTextByXPath("//settleDate"); }
        }

        private int? _setlAmt = null;
        /// <summary>
        /// 清算金额
        /// <remarks>
        /// <para>以分为单位</para>
        /// </remarks>
        /// </summary>
        public int? SetlAmt
        {
            get
            {
                if (this._setlAmt == null)
                {
                    string strVal = this.GetNodeTextByXPath("//setlAmt");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._setlAmt = int.Parse(strVal);
                    }
                }
                return this._setlAmt;
            }
        }

        private Currencys? _setlCurrency = null;
        /// <summary>
        /// 清算币种
        /// </summary>
        public Currencys? SetlCurrency
        {
            get
            {
                if (this._setlCurrency == null)
                {
                    string strVal = this.GetNodeTextByXPath("//setlCurrency");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._setlCurrency = (Currencys)int.Parse(strVal);
                    }
                }
                return this._setlCurrency;
            }
        }

        /// <summary>
        /// 清算汇率
        /// </summary>
        public string ConverRate
        {
            get
            {
                string val = this.GetNodeTextByXPath("//converRate");
                if (val == "null")
                    return null;
                else
                    return val;
            }
        }

        /// <summary>
        /// Cups交易流水号
        /// </summary>
        public string CupsQid
        {
            get { return this.GetNodeTextByXPath("//cupsQid"); }
        }

        /// <summary>
        /// Cups系统跟踪号
        /// </summary>
        public string CupsTraceNum
        {
            get { return this.GetNodeTextByXPath("//cupsTraceNum"); }
        }

        private DateTime? _cupsTraceTime = null;
        /// <summary>
        /// Cups系统跟踪时间
        /// </summary>
        public DateTime? CupsTraceTime
        {
            get
            {
                if (this._cupsTraceTime == null)
                {
                    string strVal = this.GetNodeTextByXPath("//cupsTraceTime");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._cupsTraceTime = strVal.ParseAPIDateTime();
                    }
                }
                return this._cupsTraceTime;
            }
        }

        /// <summary>
        /// Cups响应码
        /// </summary>
        public string CupsRespCode
        {
            get { return this.GetNodeTextByXPath("//cupsRespCode"); }
        }

        /// <summary>
        /// Cups应答码描述
        /// </summary>
        public string CupsRespDesc
        {
            get { return this.GetNodeTextByXPath("//cupsRespDesc"); }
        }

        #endregion

        /// <summary>
        /// 用户支付是否成功
        /// </summary>
        public bool IsPayOK
        {
            get
            {
                return this.CupsRespCode == "00";
            }
        }

        /// <summary>
        /// 支付失败的原因
        /// </summary>
        /// <returns></returns>
        public string PayFailedReason
        {
            get
            {
                switch (this.CupsRespCode)
                {
                    case "00":
                        return "支付成功";
                    case "01":
                        return "交易异常，支付失败。详情请咨询95516";
                    case "02":
                        return "您输入的卡号无效，请确认后输入";
                    case "03":
                        return "发卡银行不支持，支付失败";
                    case "05":
                        return "发卡行不予承兑";
                    case "06":
                        return "您的卡已经过期，请使用其他卡支付";
                    case "11":
                        return "您卡上的余额不足";
                    case "14":
                        return "您的卡已过期或者是您输入的有效期不正确，支付失败";
                    case "15":
                        return "您输入的银行卡密码有误，支付失败";
                    case "20":
                        return "您输入的转入卡卡号有误，支付失败";
                    case "21":
                        return "您输入的手机号或CVN2有误，支付失败";
                    case "22":
                        return "操作有误";
                    case "25":
                        return "原始交易查找失败";
                    case "30":
                        return "报文格式错误";
                    case "36":
                        return "交易金额超过网上银行交易金额限制，支付失败";
                    case "39":
                        return "您已连续多次输入错误密码";
                    case "40":
                        return "请与您的银行联系";
                    case "41":
                        return "您的银行不支持认证支付，请选择快捷支付";
                    case "42":
                        return "您的银行不支持普通支付，请选择快捷支付";
                    case "51":
                        return "余额不足";
                    case "54":
                        return "卡片过期";
                    case "55":
                        return "密码错";
                    case "56":
                        return "交易受限";
                    case "57":
                        return "不允许持卡人进行的交易";
                    case "59":
                        return "有作弊嫌疑";
                    case "71":
                        return "交易无效，无法完成，支付失败";
                    case "75":
                        return "连续多次输入密码错";
                    case "80":
                        return "内部错误";
                    case "81":
                        return "可疑报文";
                    case "82":
                        return "验签失败";
                    case "83":
                        return "超时";
                    case "84":
                        return "订单不存在";
                    case "94":
                        return "重复交易";
                    default:
                        return "未知的错误.";
                }
            }
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <returns></returns>
        public string GetContent()
        {
            if (this.Request == null)
                return null;
            else
                return this.Request.InnerXml;
        }

        #region 工具方法
        /// <summary>
        /// 加载请求内容
        /// </summary>
        /// <param name="pRequestContent"></param>
        public void Load(string pRequestContent)
        {
            this.Request = new XmlDocument();
            this.Request.LoadXml(pRequestContent);
        }

        /// <summary>
        /// 根据XPath来获取响应节点的文本
        /// </summary>
        /// <param name="pXPath"></param>
        /// <returns></returns>
        protected string GetNodeTextByXPath(string pXPath)
        {
            var node = this.Request.SelectSingleNode(pXPath);
            if (node != null)
                return node.InnerText;
            else
                return null;
        }
        #endregion
    }
}
