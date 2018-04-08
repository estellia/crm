using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Util;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.Alipay.Interface.Wap;
using JIT.Utility.Pay.Alipay.Interface.Base;

namespace JIT.Utility.Pay.Alipay.Interface.Base
{
    public abstract class BaseRequest : IAPIRequest
    {
        #region 构造函数
        public BaseRequest()
        {
            Paras = new Dictionary<string, string>();
            DataParas = new Dictionary<string, string>();
            InputCharset = AliPayConfig.InputCharset;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 请求的基参数值
        /// </summary>
        protected Dictionary<string, string> Paras { get; set; }

        /// <summary>
        /// 业务参数
        /// </summary>
        protected Dictionary<string, string> DataParas { get; set; }

        /// <summary>
        /// 参数编码字符集,无需赋值
        /// <para>商户网站使用的编码格式，如 utf-8、gbk、gb2312 等</para>
        /// </summary>
        public string InputCharset
        {
            get { return this.GetPara("_input_charset"); }
            private set { this.Paras["_input_charset"] = value; }
        }

        /// <summary>
        /// 商户签约的支付宝账号对应的支付宝唯一用户号。以 2088 开头的 16 位纯数字组成
        /// </summary>
        public string Partner
        {
            get { return this.GetPara("partner"); }
            set { this.Paras["partner"] = value; }
        }

        /// <summary>
        /// 签名
        /// </summary>
        protected string Sign
        {
            get { return this.GetPara("sign"); }
            set { this.Paras["sign"] = value; }
        }

        /// <summary>
        /// 执行授权网关
        /// </summary>
        protected string Service
        {
            get { return this.GetPara("service"); }
            set { this.Paras["service"] = value; }
        }


        #endregion

        #region 接口方法
        public string GetContent()
        {
            SetService();
            SetReqData();
            SetSign();
            if (!IsValid())
                throw new Exception("缺少参数或者参数未赋值。");
            return AliPayFunction.CreateLinkString(Paras, Encoding.GetEncoding(this.InputCharset));
        }
        #endregion

        #region 基类方法
        protected bool BaseValid()
        {
            return !string.IsNullOrEmpty(Partner) && !string.IsNullOrEmpty(Sign) && !string.IsNullOrEmpty(Service) && !string.IsNullOrEmpty(Partner);
        }

        /// <summary>
        /// 获取基参数
        /// </summary>
        /// <param name="pParaName"></param>
        /// <returns></returns>
        protected string GetPara(string pParaName)
        {
            if (this.Paras.ContainsKey(pParaName))
                return this.Paras[pParaName];
            else
                return default(string);
        }
        /// <summary>
        /// 设置基参数
        /// </summary>
        /// <param name="pParaName"></param>
        /// <param name="pValue"></param>
        protected void SetPara(string pParaName, string pValue)
        {
            this.Paras[pParaName] = pValue;
        }
        /// <summary>
        /// 获取业务参数
        /// </summary>
        /// <param name="pParaName"></param>
        /// <returns></returns>
        protected string GetDataPara(string pParaName)
        {
            if (this.DataParas.ContainsKey(pParaName))
                return this.DataParas[pParaName];
            else
                return default(string);
        }
        /// <summary>
        /// 设置业务参数
        /// </summary>
        /// <param name="pParaName"></param>
        /// <param name="pValue"></param>
        protected void SetDataPara(string pParaName, string pValue)
        {
            this.DataParas[pParaName] = pValue;
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 由子类实现具体的业务参数
        /// </summary>
        /// <returns></returns>
        protected abstract void SetReqData();
        /// <summary>
        /// 由子类实现判别请求是否有效
        /// </summary>
        /// <returns></returns>
        protected abstract bool IsValid();
        /// <summary>
        /// 签名由子类实现
        /// </summary>
        protected abstract void SetSign();
        /// <summary>
        /// 接口名称由子类实现
        /// </summary>
        protected abstract void SetService();
        #endregion

    }
}
