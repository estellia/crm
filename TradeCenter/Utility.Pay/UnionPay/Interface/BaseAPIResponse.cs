/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/24 16:37:22
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

namespace JIT.Utility.Pay.UnionPay.Interface
{
    /// <summary>
    /// 银联的支付接口的响应的基类  
    /// </summary>
    public abstract class BaseAPIResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseAPIResponse()
        {
        }
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pResponseContent">XML格式的请求响应</param>
        public BaseAPIResponse(string pResponseContent)
        {
            this.Load(pResponseContent);
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 响应内容
        /// </summary>
        protected XmlDocument Reponse { get; set; }
        #endregion

        #region 公共参数

        /// <summary>
        /// 响应码
        /// </summary>
        public virtual string Code
        {
            get
            {
                return this.GetNodeTextByXPath("//respCode");
            }
        }

        /// <summary>
        /// 响应描述
        /// </summary>
        public virtual string Description
        {
            get
            {
                return this.GetNodeTextByXPath("//respDesc");
            }
        }
        #endregion

        #region 是否成功
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return this.Code == "0000";
            }
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 加载响应内容
        /// </summary>
        /// <param name="pResponseContent"></param>
        public void Load(string pResponseContent)
        {
            this.Reponse = new XmlDocument();
            this.Reponse.LoadXml(pResponseContent);
        }

        /// <summary>
        /// 根据XPath来获取响应节点的文本
        /// </summary>
        /// <param name="pXPath"></param>
        /// <returns></returns>
        protected string GetNodeTextByXPath(string pXPath)
        {
            var node = this.Reponse.SelectSingleNode(pXPath);
            if (node != null)
                return node.InnerText;
            else
                return null;
        }
        #endregion
    }
}
