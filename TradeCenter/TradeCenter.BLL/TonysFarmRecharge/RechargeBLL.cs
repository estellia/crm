using JIT.TradeCenter.DataAccess.Base;
using JIT.TradeCenter.Entity;
using JIT.TradeCenter.Entity.HuiFuPayEntity.Reponse;
using JIT.TradeCenter.Entity.HuiFuPayEntity.Request;
using JIT.Utility.Log;
using JIT.Utility.Pay.WeiXinPay.Util;
using LitJson;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace JIT.TradeCenter.BLL.TonysFarmRecharge
{
    /// <summary>
    /// 充值方法
    /// </summary>
    public class RechargeBLL
    {
        #region 多利储值卡充值方法
        /// <summary>
        /// 多利储值卡充值方法
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public string RechargeTonysCardAct2(AppOrderEntity appOrder)
        {
            try
            {
                string msg = string.Empty;
                var rest = GetTonyCardInfo(appOrder.AppOrderID, appOrder.AppClientID, out msg);
                if (!rest.Item1)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("多利【{0}】充值异常：{1}", appOrder.AppOrderID, msg),
                    });
                }

                if (rest.Item4 == 0)
                {
                    // 调用储值卡充值接口
                    var rspData = RechargeCard(rest.Item2, rest.Item3, appOrder);
                    CreateTonyCardChargeOrder(appOrder, rspData);
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("多利【{0}】充值异常：" + ex.Message),
                });
            }
            return "SUCCESS";
        }

        private string CreateTonyCardChargeOrder(AppOrderEntity appOrder, string rspData)
        {
            string returnStr = "FAIL";
            try
            {
                var notifyUrl = ConfigurationManager.AppSettings["ApiHost"] + "OnlineShopping/data/Data.aspx";
                string param = "{\"common\": { \"isALD\": \"0\", \"customerId\": \"" + appOrder.AppClientID + "\", \"locale\": \"zh\"}, \"special\": { \"orderId\": \"" + appOrder.AppOrderID + "\",\"status\":\"" + (rspData == "SUCCESS" ? "2" : "1") + "\"} }";
                string fulParam = string.Format("action=createTonyCardChargeOrder&ReqContent={0}", param);
                Loggers.Debug(new DebugLogInfo() { Message = "GetTonyCardInfo 开始调用通知接口:" + notifyUrl + "?" + fulParam });
                returnStr = HttpService.Get(notifyUrl + "?" + fulParam);
                string message = returnStr == "SUCCESS" ? " CreateTonyCardChargeOrder调用通知接口成功" : " CreateTonyCardChargeOrder调用通知接口失败";
                message += ("：" + returnStr);
                Loggers.Debug(new DebugLogInfo() { Message = message });
                return returnStr;
            }
            catch (Exception ex)
            {
            }
            return returnStr;
        }
        #endregion

        #region 调用汇付充值接口
        /// <summary>
        /// 调用汇付充值接口
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="facePrice"></param>
        /// <param name="appOrder"></param>
        /// <returns></returns>
        private string RechargeCard(string cardNo, decimal facePrice, AppOrderEntity appOrder)
        {
            string url = PrePaidCardUtil.GetTonyRechargeUrl();
            string desKey = PrePaidCardUtil.GetEncodingKey();
            string merchantCode = PrePaidCardUtil.GetMerchantCode();
            BaseRequest<RechargeReqBody> req = new BaseRequest<RechargeReqBody>();
            var recordBll = new PayRequestRecordBLL(new Utility.BasicUserInfo());
            var recordEntity = new PayRequestRecordEntity()
            {
                ChannelID = appOrder.PayChannelID,
                ClientID = appOrder.AppClientID,
                UserID = appOrder.AppUserID,
                Platform = 8
            };
            req.bizCd = "recharge";
            req.reqSeq = DateTime.Now.ToString("yyMMddHHmmss");
            req.reqBody = new RechargeReqBody();
            req.reqBody.orderId = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            req.reqBody.transDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            req.reqBody.transAmt = (facePrice * 100).ToString();
            // req.reqBody.cardNo = "9300100203000020002";
            // req.reqBody.transAmt = "10";    // 交易金额以分为单位            
            req.reqBody.cardNo = cardNo;

            string reqJson = JsonMapper.ToJson(req).Replace("\\\"", "'");
            string strReq = CommonUtil.EncryptDES(reqJson, desKey);
            url = string.Format("{0}{1}&merchantCode={2}", url, HttpContext.Current.Server.UrlEncode(strReq), merchantCode);
            string result = HttpService.Get(url);

            string strRsp = CommonUtil.DecryptDES(result);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("多利【{0}】充值结果：", appOrder.AppOrderID) + strRsp,
            });
            recordEntity.RequestJson = reqJson;
            recordEntity.ResponseJson = strRsp;
            recordBll.Create(recordEntity);

            var rspEntity = JsonMapper.ToObject<BaseRsponse>(strRsp);
            return rspEntity.rspCd == "0000" ? "SUCCESS" : "FAIL";
        }
        #endregion

        #region 获取多利储值卡号，面值金额
        /// <summary>
        /// 获取多利储值卡号，面值金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Tuple<bool, string, decimal, int> GetTonyCardInfo(string orderId, string customerId, out string msg)
        {
            Tuple<bool, string, decimal, int> rest = null;
            msg = string.Empty;
            try
            {
                var notifyUrl = ConfigurationManager.AppSettings["ApiHost"] + "OnlineShopping/data/Data.aspx";
                string param = "{\"common\": { \"isALD\": \"0\", \"customerId\": \"" + customerId + "\", \"locale\": \"zh\"}, \"special\": { \"orderId\": \"" + orderId + "\"} }";
                string fulParam = string.Format("action=getTonyRechargeCardInfo&ReqContent={0}", param);
                Loggers.Debug(new DebugLogInfo() { Message = "GetTonyCardInfo 开始调用通知接口:" + notifyUrl + "?" + fulParam });
                var returnStr = HttpService.Get(notifyUrl + "?" + fulParam);
                string message = string.Empty;
                if (returnStr.Contains("Code"))
                {
                    JsonData data = JsonMapper.ToObject(returnStr);
                    var isOK = data["Code"].ToString() == "200";
                    var cardNo = data["CardNo"].ToString();
                    decimal totalAmount = 0m;
                    decimal.TryParse(data["TotalAmount"].ToString(), out totalAmount);
                    msg = data["Description"].ToString();
                    int cardType = Convert.ToInt32(data["CardType"].ToString());   // 卡类型，0-储值卡，1-套餐卡
                    rest = new Tuple<bool, string, decimal, int>(isOK, cardNo, totalAmount, cardType);
                    message = " GetTonyCardInfo 调用通知接口成功:" + returnStr;
                }
                else
                {
                    rest = new Tuple<bool, string, decimal, int>(false, "", 0, 0);
                    message = " GetTonyCardInfo 调用通知接口失败:" + returnStr;
                    msg = "GetTonyCardInfo 调用通知接口失败:" + returnStr;
                }

                Loggers.Debug(new DebugLogInfo() { Message = message });
                return rest;
            }
            catch (Exception ex)
            {
                msg = "GetTonyCardInfo 调用通知接口失败:" + ex.Message;
                rest = new Tuple<bool, string, decimal, int>(false, "", 0, 0);
            }
            return rest;
        }
        #endregion
    }
}
