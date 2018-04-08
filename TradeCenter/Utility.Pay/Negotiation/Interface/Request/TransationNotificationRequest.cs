using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JIT.Utility.Pay.Negotiation.Interface.Request
{
    public class TransationNotificationRequest
    {
        #region 属性集
        /// <summary>
        /// 请求内容
        /// </summary>
        protected XmlDocument Request { get; set; }
        #endregion

        #region 参数集
        //private IVRTransTypes? _transType = null;
        /// <summary>
        /// 交易类型
        /// </summary>
        //public IVRTransTypes? TransType
        //{
        //    get
        //    {
        //        if (this._transType == null)
        //        {
        //            string strVal = this.GetNodeTextByXPath("//transType");
        //            if (!string.IsNullOrWhiteSpace(strVal))
        //            {
        //                this._transType = strVal.ParseTransTypes();
        //            }
        //        }
        //        return this._transType;
        //    }
        //}

        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime
        {
            get { return this.GetNodeTextByXPath("//sendTime"); }
        }

        /// <summary>
        /// 交易代码
        /// </summary>
        public string TransCode
        {
            get { return this.GetNodeTextByXPath("//transCode"); }
        }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName
        {
            get { return this.GetNodeTextByXPath("//merchantName"); }
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
        public string MerchantOrderId
        {
            get { return this.GetNodeTextByXPath("//merchantOrderId"); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string Transtatus
        {
            get { return this.GetNodeTextByTranstatus(); }
        }
        /// <summary>
        /// 明细流水号
        /// </summary>
        public string PreDetailId
        {
            get { return this.GetNodeTextByXPath("//preDetailId"); }
        }
        /// <summary>
        /// Cups响应码
        /// </summary>
        public string RespCode
        {
            get { return this.GetNodeTextByXPath("//respCode"); }
        }

        /// <summary>
        /// Cups应答码描述
        /// </summary>
        public string CupsRespDesc
        {
            get { return this.GetNodeTextByXPath("//respDesc"); }
        }

        #endregion

        /// <summary>
        /// 用户支付是否成功
        /// </summary>
        public bool IsPayOK
        {
            get
            {
                return this.RespCode == "0000" && this.Transtatus == "1";
            }
        }
        /// <summary>
        /// 用户支付失败
        /// </summary>
        public bool IsPayFailuer
        {
            get
            {
                return this.RespCode == "0000" && this.Transtatus == "2";
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
                switch (this.RespCode)
                {
                    case "0000":
                        return "支付成功";
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
        protected string GetNodeTextByTranstatus()
        {
            var node = this.Request.SelectSingleNode("//lists")["list"].SelectSingleNode("//transtatus");
            if (node != null)
                return node.InnerText;
            else
                return null;
        }

        #endregion
    }

}
