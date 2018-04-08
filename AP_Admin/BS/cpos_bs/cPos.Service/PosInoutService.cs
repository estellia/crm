using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// pos小票类
    /// </summary>
    public class PosInoutService:BaseService
    {
        #region POS 小票查询
        /// <summary>
        /// pos小票查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_no">订单号</param>
        /// <param name="unit_id">门店</param>
        /// <param name="item_name">商品</param>
        /// <param name="order_date_begin">开始日期</param>
        /// <param name="order_date_end">结束日期</param>
        /// <param name="maxRowCount">每页数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public InoutInfo SearchPosInfo(LoggingSessionInfo loggingSessionInfo
                                            , string order_no
                                            , string unit_id
                                            , string item_name
                                            , string order_date_begin
                                            , string order_date_end
                                            , int maxRowCount
                                            , int startRowIndex
                                            )
        {
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.order_no = order_no;
            orderSearchInfo.unit_id = unit_id;
            orderSearchInfo.item_name = item_name;
            orderSearchInfo.order_date_begin = order_date_begin;
            orderSearchInfo.order_date_end = order_date_end;
            orderSearchInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            orderSearchInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            orderSearchInfo.StartRow = startRowIndex;
            orderSearchInfo.EndRow = startRowIndex + maxRowCount;
            
            return new InoutService().SearchInoutInfo(loggingSessionInfo, orderSearchInfo);
        }
        #endregion


        #region pos小票保存
        /// <summary>
        /// 单个POS小票保存处理
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="inoutInfo">inoutmodel</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <param name="strError">输出错误信息</param>
        /// <returns>返回真假</returns>
        public bool SetPosInoutInfo(LoggingSessionInfo loggingSessionInfo, InoutInfo inoutInfo,bool IsTrans, out string strError)
        {
            
            try
            {
                if (inoutInfo.order_id == null || inoutInfo.order_id.Equals(""))
                {
                    inoutInfo.order_id = NewGuid();
                }
                if (inoutInfo.BillKindCode == null || inoutInfo.BillKindCode.Equals(""))
                {
                    inoutInfo.BillKindCode = "DO";
                }
                string strResult = string.Empty;
                cPos.Service.cBillService bs = new cBillService();
                if (bs.CanHaveBill(loggingSessionInfo, inoutInfo.order_id,out strResult))
                {
                    //Jermyn20130717 单据重复也可以上传
                    //throw (new System.Exception(inoutInfo.order_no + "单据重复"));
                    inoutInfo.operate = "Modify";
                }
                if (inoutInfo.operate == null || inoutInfo.operate.Equals(""))
                {
                    inoutInfo.operate = "Create";
                }
                //提交信息
                bool bResult = new InoutService().SetInoutInfo(loggingSessionInfo, inoutInfo, IsTrans, out strResult);
                string aa = strResult;
                #region 
                if (bResult && inoutInfo.operate.Equals("Create"))
                {
                    //审批
                    if (new InoutService().SetInoutOrderStatus(loggingSessionInfo, inoutInfo.order_id, BillActionType.Approve,out strResult))
                    {
                        //扣库存
                        if (new StockBalanceService().SetStockBalance(loggingSessionInfo, inoutInfo.order_id))
                        {
                            strError = "成功！";
                            return true;
                        }
                        else {
                            strError = "扣库存失败";
                            return false;
                        }
                    }
                    else
                    {
                        strError = "审批失败--" +aa + "--" + strResult ;
                        throw (new System.Exception(strError));
                    }
                }
                else {
                    strError = strResult;
                    //throw (new System.Exception(strError));
                    return bResult;
                }
                #endregion
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        /// <summary>
        /// 批量POS小票保存处理
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="inoutList">inoutmodel</param>
        /// <param name="strError">输出错误信息</param>
        /// <returns>返回真假</returns>
        public bool SetPosInoutInfo(LoggingSessionInfo loggingSessionInfo, IList<InoutInfo> inoutList,out string strError)
        {
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            try
            {
                string sError = string.Empty;
                bool bReturn = true; 
                foreach (InoutInfo inoutInfo in inoutList)
                {
                    bReturn = SetPosInoutInfo(loggingSessionInfo, inoutInfo,false, out sError);
                    if (!bReturn) { break; }
                }
                strError = sError;
                if (bReturn)
                {
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
                    return true;
                }
                else {
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                    return false;
                }
                
            }
            catch (Exception ex) {
                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                throw (ex);
            }
        }
        #endregion

        #region POS小票上传处理
        /// <summary>
        /// 获取未打包的POS小票数量
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <returns></returns>
        public int GetPosInoutNotPackagedCountWeb(string Customer_Id, string Unit_Id)
        {
            //设置参数
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.customer_id = Customer_Id;
            orderSearchInfo.unit_id = Unit_Id;
            orderSearchInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            orderSearchInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            //获取结果
            InoutService inoutService = new InoutService();
            int icount = inoutService.GetInoutNotPackagedCountWeb(orderSearchInfo);
            return icount;
        }
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="maxRowCount">最大行数</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <param name="bat_id">批次号</param>
        /// <returns></returns>
        public IList<InoutInfo> GetPosInoutListPackagedWeb(string Customer_Id, string Unit_Id, int maxRowCount, int startRowIndex, string bat_id)
        {
            //设置参数
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.customer_id = Customer_Id;
            orderSearchInfo.unit_id = Unit_Id;
            orderSearchInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            orderSearchInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            orderSearchInfo.StartRow = startRowIndex;
            orderSearchInfo.EndRow = startRowIndex + maxRowCount;
            
            //
            InoutService inoutService = new InoutService();
            //获取集合
            IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
            inoutInfoList = inoutService.GetInoutListPackagedWeb(orderSearchInfo);
            if (inoutInfoList.Count > 0)
            {
                //修改获取的监控信息批次号
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo.bat_id = bat_id;
                inoutInfo.InoutInfoList = inoutInfoList;
                bool b = inoutService.SetInoutUpdateUnDownloadBatIdWeb(Customer_Id, inoutInfo);
            }
            return inoutInfoList;
        }

        /// <summary>
        /// 获取需要打包的POS小票明细集合
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="inoutInfoList">订单明细集合</param>
        /// <returns></returns>
        public IList<InoutDetailInfo> GetPosInoutDetailListPackageWeb(string Customer_Id, string Unit_Id, List<InoutInfo> inoutInfoList)
        {
            InoutService inoutService = new InoutService();
            return inoutService.GetInoutDetailListPackageWeb(Customer_Id, Unit_Id, inoutInfoList);
        }

        /// <summary>
        /// 更新Pos小票表打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="bat_id">批次标识</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetPosInoutIfFlagInfoWeb(string Customer_Id, string bat_id, out string strError)
        {
            InoutInfo inoutInfo = new InoutInfo();
            inoutInfo.bat_id = bat_id;
            InoutService inoutService = new InoutService();
            bool b = inoutService.SetInoutIfFlagInfoWeb(Customer_Id, inoutInfo);
            strError = "Success";
            return b;
        }
        #endregion
    }
}
