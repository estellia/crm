/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 18:07:42
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

using JIT.Utility.Pay.UnionPay.ExtensionMethod;
using JIT.Utility.Pay.UnionPay.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface.IVR.ValueObject;

namespace JIT.Utility.Pay.UnionPay.Interface.IVR.Response
{
    /// <summary>
    /// 订单查询响应 
    /// <remarks>
    /// <para>支付前置 -> 商户平台</para>
    /// </remarks>
    /// </summary>
    public class QueryOrderResponse : BaseAPIResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public QueryOrderResponse()
        {
        }
        #endregion

        #region 属性集
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
                    var strVal = this.GetNodeTextByXPath("//transType");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._transType = strVal.ParseTransTypes();
                    }
                }
                return this._transType;
            }
        }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName
        {
            get
            {
                return this.GetNodeTextByXPath("//merchantName");
            }
        }

        /// <summary>
        /// 商户代码
        /// </summary>
        public string MerchantID
        {
            get
            {
                return this.GetNodeTextByXPath("//merchantId");
            }
        }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string MerchantOrderID
        {
            get
            {
                return this.GetNodeTextByXPath("//merchantOrderId");
            }
        }

        private DateTime? _merchantOrderTime = null;
        /// <summary>
        /// 商户订单时间
        /// </summary>
        public DateTime? MerchantOrderTime
        {
            get
            {
                if (this._merchantOrderTime == null)
                {
                    var strVal = this.GetNodeTextByXPath("//merchantOrderTime");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._merchantOrderTime = strVal.ParseAPIDateTime();
                    }
                }
                return this._merchantOrderTime;
            }
        }

        private OrderQueryResults? _queryResult = null;
        /// <summary>
        /// 查询结果
        /// </summary>
        public OrderQueryResults? QueryResult
        {
            get
            {
                if (this._queryResult == null)
                {
                    string strVal = this.GetNodeTextByXPath("//queryResult");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._queryResult = (OrderQueryResults)int.Parse(strVal);
                    }
                }
                return this._queryResult;
            }
        }


        private string _settleDate = null;
        /// <summary>
        /// 清算日期
        /// <remarks>
        /// <para>4位数值,格式为：MMDD</para>
        /// </remarks>
        /// </summary>
        public string SettleDate
        {
            get
            {
                return this.GetNodeTextByXPath("//settleDate");
            }
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
        /// <remarks>
        /// <para>银联与入网机构约定的交易币种到清算币种的转换汇率.</para>
        /// <para>格式为右对齐，无小数点。小数位数由最左边一位数字表示，第二位至第八位指的是汇率的值</para>
        /// </remarks>
        /// </summary>
        public string ConverRate
        {
            get
            {
                return this.GetNodeTextByXPath("//converRate");
            }
        }

        /// <summary>
        /// Cups交易流水号
        /// <remarks>
        /// <para>对于每一笔支付交易，银联互联网系统都赋予其一个流水号。该流水号不得重复</para>
        /// </remarks>
        /// </summary>
        public string CupsQid
        {
            get
            {
                return this.GetNodeTextByXPath("//cupsQid");
            }
        }

        /// <summary>
        /// Cups系统跟踪号
        /// </summary>
        public string CupsTraceNum
        {
            get
            {
                return this.GetNodeTextByXPath("//cupsTraceNum");
            }
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
                    var strVal = this.GetNodeTextByXPath("//cupsTraceTime");
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
            get
            {
                return this.GetNodeTextByXPath("//cupsRespCode");
            }
        }

        /// <summary>
        /// Cups应答码描述
        /// </summary>
        public string CupsRespDesc
        {
            get
            {
                return this.GetNodeTextByXPath("//cupsRespDesc");
            }
        }
        #endregion
    }
}
