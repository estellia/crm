using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.Contract.Contract;
using Xgx.SyncData.DbAccess;
using Xgx.SyncData.DbAccess.t_unit;
using Zmind.EventBus.Contract;
using OptEnum = Xgx.SyncData.Contract.OptEnum;

namespace Xgx.SyncData.DomainPublishService
{
    public class OrderPaymentDetailsDomainService : IPublish
    {
        public void Deal(EventContract msg)
        {
            var bus = MqBusMgr.GetInstance();
            OptEnum operation;
            Enum.TryParse(msg.Operation.ToString(), out operation);
            var paymentDetails = new OrderPaymentDetailsContract()
            {
                Operation = operation
            };
            if (msg.Operation != Zmind.EventBus.Contract.OptEnum.Delete)
            {
                var paymentFacade = new PaymentDetailFacade();
                var result = paymentFacade.GetPaymentById(msg.Id);

                paymentDetails.OrderId = result.Inout_Id;
                paymentDetails.PayAmount = result.Price;
                paymentDetails.PayTime = result.CreateTime;
                paymentDetails.PaymentType = GetEnumByCode(result.Payment_Type_Code);

            }

            bus.Publish<IZmindToXgx>(paymentDetails);
        }

        public EnumOrderPaymentType GetEnumByCode(string code)
        {
            switch (code)
            {
                case "BalancePay":
                    return  EnumOrderPaymentType.AmountPay;
                case "OfflinePay":
                    return EnumOrderPaymentType.OfflinePay;
                case "IntegralPay":
                    return EnumOrderPaymentType.IntegralPay;
                case "WXJS":
                    return EnumOrderPaymentType.WeixinPay;
                case "CouponPay":
                    return EnumOrderPaymentType.CounponPay;
                default:
                    throw new Exception();
            }
        }
    }
}