using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using cPos.Dex.ContractModel;

namespace cPos.Dex.WcfService
{
    [ServiceContract(Namespace = Common.Config.NS)]
    public interface IOrderService
    {
        // UploadInoutOrders
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "UploadInoutOrders?user_id={userId}&token={token}&unit_id={unitId}&type={orderType}")]
        UploadInoutOrdersContract UploadInoutOrdersJson(IList<InoutOrderContract> orders,  
            string userId, string token, string unitId, string orderType);

        //// UploadOrders
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    UriTemplate = "UploadOrders?user_id={userId}&token={token}&unit_id={unitId}&type={orderType}")]
        //UploadOrdersContract UploadOrdersJson(IList<OrderContract> orders,
        //    string userId, string token, string unitId, string orderType);

        // UploadCcOrders
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "UploadCcOrders?user_id={userId}&token={token}&unit_id={unitId}&type={orderType}")]
        UploadCcOrdersContract UploadCcOrdersJson(IList<CcOrderContract> orders,
            string userId, string token, string unitId, string orderType);

        //// DownloadInoutOrders
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    UriTemplate = "DownloadInoutOrders?user_id={userId}&token={token}&unit_id={unitId}&type={orderType}")]
        //DownloadInoutOrdersContract DownloadInoutOrdersJson(string userId, string token,
        //    string unitId, string orderType);

        //// DownloadCcOrders
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    UriTemplate = "DownloadCcOrders?user_id={userId}&token={token}&unit_id={unitId}&type={orderType}")]
        //DownloadCcOrdersContract DownloadCcOrdersJson(string userId, string token,
        //    string unitId, string orderType);

        //// GetPriceOrders
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    UriTemplate = "GetPriceOrders?user_id={userId}&token={token}" +
        //    "&unit_id={unitId}&seq={seq}&start_row={startRow}&rows_count={rowsCount}")]
        //GetPriceOrdersContract GetPriceOrdersJson(string userId, string token,
        //    string unitId, int seq, int startRow, int rowsCount);

        //// GetPriceOrdersCount
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    UriTemplate = "GetPriceOrdersCount?user_id={userId}&token={token}" +
        //    "&unit_id={unitId}&seq={seq}")]
        //GetCountContract GetPriceOrdersCountJson(string userId, string token,
        //    string unitId, int seq);

        // SetInoutOrdersDldFlag
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "SetInoutOrdersDldFlag?user_id={userId}&token={token}&unit_id={unitId}&bat_id={batId}")]
        BaseContract SetInoutOrdersDldFlagJson(
            string userId, string token, string unitId, string batId);

        //// SetCcOrdersDldFlag
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    UriTemplate = "SetCcOrdersDldFlag?user_id={userId}&token={token}&unit_id={unitId}")]
        //BaseContract SetCcOrdersDldFlagJson(IList<CcOrderContract> orders,
        //    string userId, string token, string unitId);

        // GetAdOrders
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetAdOrders?user_id={userId}&token={token}&unit_id={unitId}&start_row={startRow}&rows_count={rowsCount}")]
        GetAdOrdersContract GetAdOrdersJson(string userId, string token,
            string unitId, int startRow, int rowsCount);

        // GetAdOrdersCount
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetAdOrdersCount?user_id={userId}&token={token}&unit_id={unitId}")]
        GetCountContract GetAdOrdersCountJson(string userId, string token,
            string unitId);

        // SetAdOrdersFlag
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "SetAdOrdersFlag?user_id={userId}&token={token}&unit_id={unitId}&bat_id={batId}")]
        BaseContract SetAdOrdersFlagJson(string userId, string token,
            string unitId, string batId);

        // UploadAdOrderLogs
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "UploadAdOrderLogs?user_id={userId}&token={token}&unit_id={unitId}")]
        BaseContract UploadAdOrderLogsJson(IList<AdOrderLogContract> logs,
            string userId, string token, string unitId);

        // GetDeliveryOrders
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetDeliveryOrders?user_id={userId}&token={token}&unit_id={unitId}")]
        GetDeliveryOrdersContract GetDeliveryOrdersJson(string userId, string token,
            string unitId);
    }
}
