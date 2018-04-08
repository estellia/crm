using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;
using cPos.Dex.ContractModel;
using cPos.Dex.Services;

namespace cPos.Dex.ServicesBs
{
    public class VipService
    {
        #region CheckVips
        /// <summary>
        /// 检查Vip
        /// </summary>
        public Hashtable CheckVip(VipContract vip)
        {
            Hashtable htError = new Hashtable();
            if (vip == null)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "会员信息不能为空", true);
                return htError;
            }
            if (vip.id == null || vip.id.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "会员ID不能为空", true);
                return htError;
            }
            if (vip.no == null || vip.no.Trim().Length == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "会员号码不能为空", true);
                return htError;
            }
            //if (vip.order_type_id == null || vip.order_type_id.Trim().Length == 0)
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A016, "会员类型1不能为空", true);
            //    return htError;
            //}
            //if (vip.order_reason_id == null || vip.order_reason_id.Trim().Length == 0)
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A016, "会员类型2不能为空", true);
            //    return htError;
            //}
            //if (vip.red_flag == null || vip.red_flag.Trim().Length == 0)
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A016, "会员红单标记不能为空", true);
            //    return htError;
            //}
            //if (!(vip.red_flag == "1" || vip.red_flag == "-1"))
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A019, "会员红单标记数据不合法", true);
            //    return htError;
            //}
            //if (vip.order_date == null || vip.order_date.Trim().Length == 0)
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A016, "会员日期不能为空", true);
            //    return htError;
            //}
            //if (vip.create_unit_id == null || vip.create_unit_id.Trim().Length == 0)
            //{
            //    htError = ErrorService.OutputError(ErrorCode.A016, "会员创建单位不能为空", true);
            //    return htError;
            //}
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }

        /// <summary>
        /// 检查Vip集合
        /// </summary>
        public Hashtable CheckVips(IList<VipContract> vips)
        {
            Hashtable htError = new Hashtable();
            if (vips == null || vips.Count == 0)
            {
                htError = ErrorService.OutputError(ErrorCode.A016, "会员集合不能为空", true);
                return htError;
            }
            foreach (var vip in vips)
            {
                htError = CheckVip(vip);
                if (!Convert.ToBoolean(htError["status"])) return htError;
            }
            htError["status"] = Utils.GetStatus(true);
            return htError;
        }
        #endregion

        #region SaveVips
        /// <summary>
        /// 保存Vip集合
        /// </summary>
        public string SaveVips(IList<VipContract> vips,
            string customerId, string unitId, string userId)
        {
            if (customerId == null || customerId.Trim().Length == 0)
                throw new Exception("客户ID不能为空");
            if (userId == null || userId.Trim().Length == 0)
                throw new Exception("用户ID不能为空");

            var orderInfoList = new List<cPos.Model.Promotion.VipExchangeInfo>();
            foreach (var vip in vips)
            {
                var orderInfo = new cPos.Model.Promotion.VipExchangeInfo();
                #region order
                orderInfo.ID = vip.id;
                orderInfo.No = vip.no;
                orderInfo.Name = vip.name;
                orderInfo.Gender = vip.gender;
                orderInfo.EnglishName = vip.english_name;
                orderInfo.IdentityNo = vip.identity_no;
                orderInfo.Address = vip.address;
                orderInfo.Postcode = vip.postcode;
                orderInfo.Birthday = vip.birthday;
                orderInfo.Cell = vip.cell;
                orderInfo.Email = vip.email;
                orderInfo.QQ = vip.qq;
                orderInfo.MSN = vip.msn;
                orderInfo.Weibo = vip.weibo;
                orderInfo.Points = Utils.GetIntVal(vip.points);
                orderInfo.ExpiredDate = vip.expired_date;
                orderInfo.Remark = vip.remark;
                orderInfo.Status = Utils.GetIntVal(vip.status);
                orderInfo.ActivateUnitID = vip.activate_unit_id;
                orderInfo.ActivateTime = Utils.GetDateTimeVal(vip.activate_time);
                orderInfo.Type = vip.type_code;
                orderInfo.CreateUserID = vip.create_user_id;
                orderInfo.CreateUserName = vip.create_user_name;
                orderInfo.CreateTime = Utils.GetDateTimeVal(vip.create_time);
                orderInfo.ModifyUserID = vip.modify_user_id;
                orderInfo.ModifyUserName = vip.modify_user_name;
                orderInfo.ModifyTime = Utils.GetDateTimeVal(vip.modify_time);
                orderInfo.Version = Utils.GetIntVal(vip.version);
                #endregion

                orderInfoList.Add(orderInfo);
            }

            // Save
            var promotionService = new ExchangeBsService.PromotionExchangeService();
            string uploadRet = promotionService.UploadVips(customerId, userId, unitId, orderInfoList);
            return uploadRet;
        }
        #endregion
    }
}
