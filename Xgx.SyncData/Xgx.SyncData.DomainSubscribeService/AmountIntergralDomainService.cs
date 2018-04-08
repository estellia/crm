using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.VipAmount;
using Xgx.SyncData.DbAccess.VipAmountDetail;
using Xgx.SyncData.DbAccess.VipCardVipMapping;
using Xgx.SyncData.DbAccess.VipIntegral;
using Xgx.SyncData.DbAccess.VipIntegralDetail;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DomainSubscribeService
{
    public class AmountIntergralDomainService
    {
        public void Deal(AmountIntergralContract contract)
        {
            var vipIntegralFacade = new VipIntegralFacade();
            var vipIntegralDetailFacade = new VipIntegralDetailFacade();
            var vipAmountFacade = new VipAmountFacade();
            var vipAmountDetailFacade = new VipAmountDetailFacade();
            var vipCardVipMappingFacade = new VipCardVipMappingFacade();
            var vipIntegralDetailEntity = ConvertToVipIntegralDetailEntity(contract);
            var vipAmountDetailEntity = ConvertToVipAmountDetailEntity(contract);
            switch (contract.Operation)
            {
                case OptEnum.Create:
                    if (contract.VipIntegral != 0)
                    {
                        vipIntegralDetailFacade.Create(vipIntegralDetailEntity);
                        if (vipIntegralFacade.GetVipIntegralCountByVipId(contract.VipId) == 0)
                        {
                            var vipIntegral = new VipIntegralEntity
                            {
                                VipID = contract.VipId,
                                VipCardCode = vipCardVipMappingFacade.GetVipCardCodeByVipId(contract.VipId),
                                BeginIntegral = contract.VipIntegral,
                                InIntegral = contract.VipIntegral,
                                OutIntegral = 0,
                                EndIntegral = contract.VipIntegral,
                                InvalidIntegral = 0,
                                ImminentInvalidIntegral = 0,
                                ValidIntegral = contract.VipIntegral,
                                CumulativeIntegral = contract.VipIntegral,
                                CustomerID = ConfigMgr.CustomerId,
                                CreateTime = contract.CreateTime == null ? DateTime.Now : contract.CreateTime,
                                CreateBy = "xgx",
                                LastUpdateBy = "xgx",
                                LastUpdateTime = contract.ModifyTime == null ? DateTime.Now : contract.ModifyTime,
                                IsDelete = 0                                
                            };
                            vipIntegralFacade.Create(vipIntegral);
                        }
                        else
                        {
                            vipIntegralFacade.UpdateVipIntegral(contract.VipId, contract.VipIntegral);
                        }
                    }
                    if (contract.VipAmount != 0)
                    {
                        vipAmountDetailFacade.Create(vipAmountDetailEntity);
                        if (vipAmountFacade.GetVipAmountCountByVipId(contract.VipId) == 0)
                        {
                            var vipAmount= new VipAmountEntity
                            {
                                VipId = contract.VipId,
                                VipCardCode = vipCardVipMappingFacade.GetVipCardCodeByVipId(contract.VipId),
                                BeginAmount = contract.VipAmount,
                                InAmount = contract.VipAmount,
                                OutAmount = 0,
                                EndAmount = contract.VipAmount,
                                TotalAmount = contract.VipAmount,
                                BeginReturnAmount = 0,
                                ReturnAmount = 0,
                                InReturnAmount = 0,
                                OutReturnAmount = 0,
                                ImminentInvalidRAmount = 0,
                                InvalidReturnAmount = 0,
                                ValidReturnAmount = 0,
                                TotalReturnAmount = 0,
                                CustomerID = ConfigMgr.CustomerId,
                                CreateTime = contract.CreateTime == null ? DateTime.Now : contract.CreateTime,
                                CreateBy = "xgx",
                                LastUpdateBy = "xgx",
                                LastUpdateTime = contract.ModifyTime == null ? DateTime.Now : contract.ModifyTime,
                                IsDelete = 0,
                                IsLocking = 0
                            };
                            vipAmountFacade.Create(vipAmount);
                        }
                        else
                        {
                            vipAmountFacade.UpdateVipAmount(contract.VipId, contract.VipAmount);
                        }
                    }
                    break;
                case OptEnum.Update:
                    break;
                case OptEnum.Delete:
                    break;
            }
        }
        private VipIntegralDetailEntity ConvertToVipIntegralDetailEntity(AmountIntergralContract contract)
        {
            var vipCardVipMappingFacade = new VipCardVipMappingFacade();
            var result = new VipIntegralDetailEntity
            {
                VipIntegralDetailID = Guid.NewGuid().ToString("N"),
                VIPID = contract.VipId,
                VipCardCode = vipCardVipMappingFacade.GetVipCardCodeByVipId(contract.VipId),
                Integral = contract.VipIntegral,
                UsedIntegral = 0,
                Reason = "从新干线同步",
                CustomerID = ConfigMgr.CustomerId,
                CreateTime = contract.CreateTime == null ? DateTime.Now : contract.CreateTime,
                CreateBy = "xgx",
                LastUpdateTime = contract.ModifyTime == null ? DateTime.Now : contract.ModifyTime,
                LastUpdateBy = "xgx",
                IsDelete = 0,
                IntegralSourceID = "36"
            };
            return result;
        }
        private VipAmountDetailEntity ConvertToVipAmountDetailEntity(AmountIntergralContract contract)
        {
            var vipCardVipMappingFacade = new VipCardVipMappingFacade();
            var result = new VipAmountDetailEntity
            {
                VipAmountDetailId = Guid.NewGuid(),
                VipId = contract.VipId,
                VipCardCode = vipCardVipMappingFacade.GetVipCardCodeByVipId(contract.VipId),
                Amount = contract.VipAmount,
                UsedReturnAmount = 0,
                Reason = "从新干线同步",
                CustomerID = ConfigMgr.CustomerId,
                CreateTime = contract.CreateTime == null ? DateTime.Now : contract.CreateTime,
                CreateBy = "xgx",
                LastUpdateTime = contract.ModifyTime == null ? DateTime.Now : contract.ModifyTime,
                LastUpdateBy = "xgx",
                IsDelete = 0,
                AmountSourceId = "45"
            };
            return result;
        }
    }
}
