using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.T_Item_Category;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DomainSubscribeService
{
    public class ItemCategoryDomainService
    {
        public void Deal(ItemCategoryContract contract)
        {
            var dbEntity = Convert(contract);
            var facade = new T_Item_CategoryFacade();
            switch (contract.Operation)
            {
                case OptEnum.Create:
                    facade.Create(dbEntity);
                    break;
                case OptEnum.Update:
                    facade.Update(dbEntity);
                    break;
                case OptEnum.Delete:
                    facade.Delete(dbEntity);
                    break;
            }
        }

        private T_Item_CategoryEntity Convert(ItemCategoryContract contract)
        {
            var dbEntity = new T_Item_CategoryEntity
            {
                item_category_id = contract.ItemCategoryId,
                item_category_code = contract.ItemCategoryCode,
                item_category_name = contract.ItemCategoryName,
                status = contract.Status,
                parent_id = string.IsNullOrEmpty(contract.ParentId) ? ConfigMgr.HeadItemCategoryId : contract.ParentId,
                create_time = contract.CreateTime != null ? contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : null,
                modify_time = contract.ModifyTime != null ? contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : null,
                DisplayIndex = contract.DisplayIndex,
                CustomerID = ConfigMgr.CustomerId
            };
            return dbEntity;
        }
    }
}
