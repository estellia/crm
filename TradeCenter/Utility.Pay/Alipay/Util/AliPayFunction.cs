using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using System;

namespace JIT.Utility.Pay.Alipay.Util
{
    public static class AliPayFunction
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="dicArrayPre">待签名字符串</param>
        /// <param name="privatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>经过UrlEncode转码后的签名字符串</returns>
        public static string BuildMysign(SortedDictionary<string, string> dicArrayPre, string privatekey, string input_charset)
        {
            Dictionary<string, string> dicArray = FilterPara(dicArrayPre);
            string prestr = CreateLinkString(dicArray);
            string mysign = RSAFromPkcs8.Sign(prestr, privatekey, input_charset);
            mysign = HttpUtility.UrlEncode(mysign, Encoding.GetEncoding(input_charset)); //此处需要对签名进行Encode，否则出现+号等特殊字符，通过base64转换并post提交给支付宝服务器会丢失，变成空格
            return mysign;
        }

        /// <summary>
        /// 验签（不排序 Notify验签用这个）
        /// </summary>
        /// <param name="pContent">待验签字符串</param>
        /// <param name="pSignedString">签名（支付宝返回sign）</param>
        /// <param name="pPublickey">支付宝公钥</param>
        /// <param name="pInputCharset">编码格式</param>
        /// <returns>返回验签结果，true(相同)，false(不相同)</returns>
        public static bool Verify(string pContent, string pSignedString, string pPublickey, string pInputCharset)
        {
            bool b = RSAFromPkcs8.Verify(pContent, pSignedString, pPublickey, pInputCharset);
            return b;
        }

        /// <summary>
        /// 验签（排序验签）
        /// </summary>
        /// <param name="pSArrary">待签名数组</param>
        /// <param name="pSignedString">签名（支付宝返回sign）</param>
        /// <param name="pPublickey">支付宝公钥</param>
        /// <param name="pInputCharset">编码格式</param>
        /// <returns>返回验签结果，true(相同)，false(不相同)</returns>
        public static bool Verify(SortedDictionary<string, string> pSArrary, string pSignedString, string pPublickey,
            string pInputCharset)
        {
            Dictionary<string, string> sPara = FilterPara(pSArrary);
            string content = CreateLinkString(sPara);
            bool b = RSAFromPkcs8.Verify(content, pSignedString, pPublickey, pInputCharset);
            return b;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pContent">待解密字符串</param>
        /// <param name="pPrivateKey">商户私钥</param>
        /// <param name="pInputCharset">编码格式</param>
        /// <returns>返回明文</returns>
        public static string Decrypt(string pContent, string pPrivateKey, string pInputCharset)
        {
            string strDecryptData = RSAFromPkcs8.DecryptData(pContent, pPrivateKey, pInputCharset);
            return strDecryptData;
        }

        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="pDicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> pDicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in pDicArrayPre)
            {
                if (temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type" && temp.Value != "" && temp.Value != null)//&& temp.Key.ToLower() != "sec_id"
                {
                    dicArray.Add(temp.Key.ToLower(), temp.Value);
                }
            }

            return dicArray;
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(IDictionary<string, string> pDicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in pDicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            return prestr.ToString();
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(IDictionary<string, string> pDicArray, Encoding pCode)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in pDicArray)
            {
                prestr.Append(temp.Key + "=" + HttpUtility.UrlEncode(temp.Value) + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            return prestr.ToString();
        }


        public static string GetSignContent(IDictionary<string, string> parameters)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder("");
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append("=").Append(value).Append("&");
                }
            }
            string content = query.ToString().Substring(0, query.Length - 1);

            return content;
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateScanLinkString(IDictionary<string, string> pDicArray, Encoding pCode)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in pDicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            return prestr.ToString();
        }

        /// <summary>
        /// 返回 XML字符串 节点value
        /// </summary>
        /// <param name="pXmlDoc">XML格式 数据</param>
        /// <param name="pXmlNode">节点</param>
        /// <returns>节点value</returns>
        public static string GetStrForXmlDoc(string pXmlDoc, string pXmlNode)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlDoc);
            XmlNode xn = xml.SelectSingleNode(pXmlNode);
            return xn.InnerText;
        }

        /// <summary>
        /// 解析远程模拟提交后返回的信息
        /// </summary>
        /// <param name="pStrText">要解析的字符串</param>
        /// <param name="pSignType">加密方式</param>
        /// <param name="pPrivateKey">RSA加密算法的私有密钥</param>
        /// <param name="InputCharset">编码方式</param>
        /// <returns>解析结果</returns>
        public static Dictionary<string, string> ParseResponse(string pStrText, string pSignType, string pPrivateKey, string InputCharset)
        {
            //以“&”字符切割字符串
            string[] strSplitText = pStrText.Split('&');
            //把切割后的字符串数组变成变量与数值组合的字典数组
            Dictionary<string, string> dicText = new Dictionary<string, string>();
            for (int i = 0; i < strSplitText.Length; i++)
            {
                //获得第一个=字符的位置
                int nPos = strSplitText[i].IndexOf('=');
                //获得字符串长度
                int nLen = strSplitText[i].Length;
                //获得变量名
                string strKey = strSplitText[i].Substring(0, nPos);
                //获得数值
                string strValue = strSplitText[i].Substring(nPos + 1, nLen - nPos - 1);
                //放入字典类数组中
                dicText.Add(strKey, strValue);
            }

            if (dicText["res_data"] != null)
            {
                //解析加密部分字符串（RSA与MD5区别仅此一句）
                if (pSignType == "0001")
                {
                    dicText["res_data"] = RSAFromPkcs8.DecryptData(dicText["res_data"], pPrivateKey, InputCharset);
                }

                //token从res_data中解析出来（也就是说res_data中已经包含token的内容）
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(dicText["res_data"]);
                    string strRequest_token = xmlDoc.SelectSingleNode("/direct_trade_create_res/request_token").InnerText;
                    dicText.Add("request_token", strRequest_token);
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }

            return dicText;
        }

        public static Dictionary<string, string> ParseResponse(string pStrXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pStrXml);
            Dictionary<string, string> tempDic = new Dictionary<string, string>();
            foreach (XmlNode item in doc.DocumentElement.ChildNodes)
            {
                tempDic[item.Name] = item.InnerText;
            }
            return tempDic;
        }
    }
}
