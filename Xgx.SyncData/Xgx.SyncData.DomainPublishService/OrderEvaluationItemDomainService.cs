using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.Contract.Contract;
using Xgx.SyncData.DbAccess;
using Xgx.SyncData.DbAccess.ObjectEvaluation;
using Xgx.SyncData.DbAccess.t_unit;
using Zmind.EventBus.Contract;
using OptEnum = Xgx.SyncData.Contract.OptEnum;


namespace Xgx.SyncData.DomainPublishService
{
    public class OrderEvaluationItemDomainService : IPublish
    {
        public void Deal(EventContract msg)
        {
            var bus = MqBusMgr.GetInstance();
            OptEnum operation;
            Enum.TryParse(msg.Operation.ToString(), out operation);

            var evaluation = new ObjectEvaluationFacade();
            var list= evaluation.GetEvaluationEntityListByOrderId(msg.Id);
            foreach (var entity in list)
            {
                var evaluationContract = new OrderCommentsContract()
                {
                    Operation = operation,
                };
                if (msg.Operation != Zmind.EventBus.Contract.OptEnum.Delete)
                {
                    var type = string.Empty;
                    evaluationContract.CommentType = entity.ObjectID == entity.OrderID ? 2 : 1;
                    evaluationContract.CreateTime = entity.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    evaluationContract.IsAnonymity = entity.IsAnonymity.Value;
                    evaluationContract.ItemComment = entity.Content;
                    evaluationContract.ItemLevel = entity.StarLevel.Value;
                    evaluationContract.OrderId = entity.OrderID;
                    evaluationContract.SkuID = entity.ObjectID;
                    evaluationContract.StarLevel1 = entity.StarLevel1.Value;
                    evaluationContract.StarLevel2 = entity.StarLevel2.Value;
                    evaluationContract.StarLevel3 = entity.StarLevel3.Value;
                    evaluationContract.VipId = entity.VipID;

                    bus.Publish<IZmindToXgx>(evaluationContract);

                }
            }
            
        }
    }
}
