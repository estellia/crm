using adminProject.ApplicationInterface.DTO.Base;
using adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request;
using adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response;
using cPos.Admin.Component;
using cPos.Admin.Model.CreativityWarehouse;
using cPos.Admin.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace adminProject.ApplicationInterface.CreativityWarehouse
{
    /// <summary>
    /// CreativityWarehouseHandle 的摘要说明
    /// </summary>
    public class CreativityWarehouseHandle : BaseGateway
    {

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                //类型下拉框
                case "GetSysMarketingGroupTypeList":
                    rst = this.GetSysMarketingGroupTypeList(pRequest);
                    break;
                //主题下拉框
                case "GetLEventTemplateDropDownList":
                    rst = this.GetLEventTemplateDropDownList(pRequest);
                    break;
                //设置上传图片信息
                case "SetObjectImages":
                    rst = this.SetObjectImages(pRequest);
                    break;

                #region KV
                case "GetBannerList":  //KV列表
                    rst = this.GetBannerList(pRequest);
                    break;
                case "SetBannerStatus":  //设置上下架
                    rst = this.SetBannerStatus(pRequest);
                    break;
                case "SetBanner":  //修改KV 
                    rst = this.SetBanner(pRequest);
                    break;
                case "DeleteBanner":  //删除KV 
                    rst = this.DeleteBanner(pRequest);
                    break;
                #endregion
                #region 计划活动管理、图片
                case "GetSeasonPlanList":  //计划列表
                    rst = this.GetSeasonPlanList(pRequest);
                    break;
                case "SetSeasonPlan":  //修改计划 
                    rst = this.SetSeasonPlan(pRequest);
                    break;
                case "DeleteSeasonPlan":  //删除计划 
                    rst = this.DeleteSeasonPlan(pRequest);
                    break;
                case "GetHomePageCommon":  //获取计划图片
                    rst = this.GetHomePageCommon(pRequest);
                    break;

                #endregion
                #region 预览
                case "GetPreview":
                    rst = this.GetPreview(pRequest);
                    break;
                case "SetLEventTemplateReleaseStatus":
                    rst = this.SetLEventTemplateReleaseStatus(pRequest);
                    break;
                #endregion
                #region 创意主题
                case "GetLEventTemplateList":
                    rst = this.GetLEventTemplateList(pRequest);
                    break;
                case "SetLEventTemplateStatus":
                    rst = this.SetLEventTemplateStatus(pRequest);
                    break;
                case "SetLEventTemplate":
                    rst = this.SetLEventTemplate(pRequest);
                    break;
                case "DeleteLEventTemplate":
                    rst = this.DeleteLEventTemplate(pRequest);
                    break;
                case "GetLEventTemplateByID":
                    rst = this.GetLEventTemplateByID(pRequest);
                    break;
                #endregion
                #region 风格
                case "GetLEventThemeList":
                    rst = this.GetLEventThemeList(pRequest);
                    break;
                //case "GetLEventThemeByID":
                //    rst = this.GetLEventThemeByID(pRequest);
                //    break;
                case "SetLEventTheme":
                    rst = this.SetLEventTheme(pRequest);
                    break;
                case "DeleteLEventTheme":
                    rst = this.DeleteLEventTheme(pRequest);
                    break;
                #endregion
                #region 活动
                case "GetLEventInteractionList":
                    rst = this.GetLEventInteractionList(pRequest);
                    break;
                //case "GetLEventInteractionByID":
                //    rst = this.GetLEventInteractionByID(pRequest);
                //    break;
                case "SetLEventInteraction":
                    rst = this.SetLEventInteraction(pRequest);
                    break;
                case "DeleteLEventInteraction":
                    rst = this.DeleteLEventInteraction(pRequest);
                    break;
                case "GetLEventDropDownList":
                    rst = this.GetLEventDropDownList(pRequest);
                    break;

                #endregion
                #region 推广设置
                case "CreateSpreadSetting":  //创建推广 
                    rst = this.CreateSpreadSetting(pRequest);
                    break;
                case "UpdateSpreadSetting":  //修改推广 
                    rst = this.UpdateSpreadSetting(pRequest);
                    break;
                case "GetSpreadSettingList":  //推广查询
                    rst = this.GetSpreadSettingList(pRequest);
                    break;
                #endregion
                #region 营销

                case "GetMarketingList":
                    rst = this.GetMarketingList(pRequest);
                    break;
                case "SetPanicbuyingEvent":
                    rst = this.SetPanicbuyingEvent(pRequest);
                    break;
                //case "DeletePanicbuyingEvent":
                //    rst = this.DeletePanicbuyingEvent(pRequest);
                //    break;

                case "GetPanicbuyingEvent":
                    rst = this.GetPanicbuyingEvent(pRequest);
                    break;
                case "SetLEvents":
                    rst = this.SetLEvents(pRequest);
                    break;
                case "DeleteLEvents":
                    rst = this.DeleteLEvents(pRequest);
                    break;
                case "GetLEvents":
                    rst = this.GetLEvents(pRequest);
                    break;

                #endregion
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
        /// <summary>
        /// 营销活动类型下拉框
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetSysMarketingGroupTypeList(string pRequest)
        {
            var RD = new SysMarketingGroupTypeRD();
            var Service = new CreativityWarehouseService();

            var Result = Service.GetSysMarketingGroupTypeList();
            RD.SysMarketingGroupTypeDropDownList = new List<SysMarketingGroupTypeInfo>();
            foreach (var item in Result)
            {
                var Data = new SysMarketingGroupTypeInfo();
                Data.ActivityGroupId = item.ActivityGroupId.ToString();
                Data.Name = item.Name;
                //
                RD.SysMarketingGroupTypeDropDownList.Add(Data);
            }

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 主题类型下拉框
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEventTemplateDropDownList(string pRequest)
        {
            var RD = new LEventTemplateRD();
            var Service = new CreativityWarehouseService();

            var Result = Service.GetLEventTemplateDropDownList();

            RD.LEventTemplateDropDownList = new List<LEventTemplateInfo>();
            foreach (var item in Result)
            {
                var Data = new LEventTemplateInfo();
                Data.TemplateId = item.TemplateId.ToString();
                Data.TemplateName = item.TemplateName;
                //
                RD.LEventTemplateDropDownList.Add(Data);
            }


            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();

        }

        #region 设置活动下拉框
        /// <summary>
        /// 设置活动下拉狂
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEventDropDownList(string pRequest)
        {
            var RD = new LEventDropDownRD();
            var Service = new CreativityWarehouseService();

            var LEventDrawMethodResult = Service.GetLEventDrawMethodList();
            //var PanicbuyingEventResult = Service.GetPanicbuyingEventList();
            //var LEventLEventsResult = Service.GetLEventsList();

            RD.LEventDrawMethodInfoList = new List<LEventDrawMethodInfo>();
            //RD.PanicbuyingEventInfoList = new List<PanicbuyingEventInfo>();
            //RD.LEventsInfoList = new List<LEventsInfo>();

            //赋值
            foreach (var item in LEventDrawMethodResult)
            {
                var Data = new LEventDrawMethodInfo();
                Data.DrawMethodId = item.DrawMethodId.ToString();
                Data.DrawMethodName = item.DrawMethodName;
                Data.InteractionType = item.InteractionType;
                Data.DrawMethodCode = item.DrawMethodCode;
                //
                RD.LEventDrawMethodInfoList.Add(Data);
            }
            //foreach (var item in PanicbuyingEventResult)
            //{
            //    var Data = new PanicbuyingEventInfo();
            //    Data.EventId = item.EventId.ToString();
            //    Data.EventCode = item.EventCode;
            //    Data.EventName = item.EventName;
            //    //
            //    RD.PanicbuyingEventInfoList.Add(Data);
            //}
            //foreach (var item in LEventLEventsResult)
            //{
            //    var Data = new LEventsInfo();
            //    Data.EventID = item.EventID;
            //    Data.EventCode = item.EventCode;
            //    Data.Title = item.Title;
            //    //
            //    RD.LEventsInfoList.Add(Data);
            //}

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        #endregion

        #region ObjectImages

        /// <summary>
        /// 设置上传图片信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetObjectImages(string pRequest)
        {
            var RD = new ObjectImagesRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<ObjectImagesRP>>();
            var Service = new CreativityWarehouseService();

            string BatId = null;
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.BatId))
                BatId = reqObj.Parameters.BatId;

            var data = new ObjectImagesData();

            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.ImageId))
            {
                //修改
                data.ImageId = reqObj.Parameters.ImageId;
                data.ImageURL = reqObj.Parameters.ImageURL;
                data.LastUpdateBy = reqObj.UserID;
                Service.UpdateObjectImages(data);
            }
            else
            {
                //创建
                data.ImageId = System.Guid.NewGuid().ToString();
                data.ObjectId = null;
                data.ImageURL = reqObj.Parameters.ImageURL;
                data.DisplayIndex = null;
                data.CreateBy = reqObj.UserID;
                data.LastUpdateBy = reqObj.UserID;
                data.Title = null;
                data.Description = null;
                data.BatId = BatId;
                data.RuleId = null;
                data.RuleContent = null;

                Service.CreateObjectImages(data);

                //计划图片关联拓展表表
                if (reqObj.Parameters.IsHomePage == 1)
                {
                    var Data = new T_CTW_HomePageCommonData();
                    Data.AdId = System.Guid.NewGuid();
                    Data.SetType = null;
                    Data.ImageId = data.ImageId;
                    Data.CommRemark = "计划图片";
                    Data.CreateBy = reqObj.UserID;
                    Data.LastUpdateBy = reqObj.UserID;

                    Service.CreateHomePageCommon(Data);
                }

            }


            RD.ImageId = data.ImageId;

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        #endregion

        #region 首页KV管理
        /// <summary>
        /// 获取KV列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetBannerList(string pRequest)
        {
            var RD = new BannerRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<BannerRP>>();
            var Service = new CreativityWarehouseService();

            string BannerName = null;
            int? Status = null;
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.BannerName))
                BannerName = reqObj.Parameters.BannerName;
            if (reqObj.Parameters.Status > 0)
                Status = reqObj.Parameters.Status;

            var Result = Service.GetBannerList(BannerName, Status, reqObj.Parameters.PageIndex, reqObj.Parameters.PageSize);
            RD.BannerInfoList = new List<BannerInfo>();
            foreach (var item in Result)
            {
                var Data = new BannerInfo();
                Data.AdId = item.AdId.ToString();
                Data.ActivityGroupId = item.ActivityGroupId.ToString();
                Data.ActivityGroupName = item.ActivityGroupName;
                Data.BannerName = item.BannerName;
                Data.Status = item.Status;
                Data.DisplayIndex = item.DisplayIndex;
                Data.BannerUrl = item.BannerUrl;
                Data.TemplateId = item.TemplateId.ToString();
                Data.BannerImageId = item.BannerImageId;
                var ResultData = Service.GetObjectImagesId(item.BannerImageId);
                if (ResultData != null)
                {
                    Data.ImageUrl = ResultData.ImageURL;
                }
                //
                RD.BannerInfoList.Add(Data);
            }

            RD.TotalCount = Service.GetBannerCount(BannerName, Status);
            int PageSum = 0;
            //分页
            if (RD.TotalCount > 0)
            {
                PageSum = RD.TotalCount / reqObj.Parameters.PageSize;
                if (RD.TotalCount % reqObj.Parameters.PageSize != 0)
                    PageSum++;
            }
            RD.TotalPageCount = PageSum;
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 设置KV上下架
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetBannerStatus(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<BannerRP>>();
            var Service = new CreativityWarehouseService();
            var LoggingSession = new LoggingSessionInfo();
            LoggingSession.UserID = reqObj.UserID;

            Service.SetBannerStatus(LoggingSession, reqObj.Parameters.AdId, reqObj.Parameters.Status);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }


        /// <summary>
        /// 设置KV
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetBanner(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<BannerRP>>();
            var Service = new CreativityWarehouseService();
            var data = new T_CTW_BannerData();
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.AdId))
            {
                #region 排序处理
                var Result = Service.GetBannerById(new Guid(reqObj.Parameters.AdId));
                if (reqObj.Parameters.DisplayIndex != Result.DisplayIndex)
                {
                    bool Flag = Service.IsExistBanner(reqObj.Parameters.DisplayIndex);
                    if (Flag == false)
                    {
                        var errRsp = new ErrorResponse();
                        errRsp.Message = "KV已有相同排序，请更换！";
                        return errRsp.ToJSON();
                    }
                }
                #endregion
                //修改
                data.AdId = new Guid(reqObj.Parameters.AdId);
                if (!string.IsNullOrWhiteSpace(reqObj.Parameters.ActivityGroupId))
                    data.ActivityGroupId = new Guid(reqObj.Parameters.ActivityGroupId);
                if (!string.IsNullOrWhiteSpace(reqObj.Parameters.TemplateId))
                    data.TemplateId = new Guid(reqObj.Parameters.TemplateId);
                data.BannerImageId = reqObj.Parameters.BannerImageId;
                data.BannerUrl = reqObj.Parameters.BannerUrl;
                data.BannerName = reqObj.Parameters.BannerName;
                data.DisplayIndex = reqObj.Parameters.DisplayIndex;
                data.LastUpdateBy = reqObj.UserID;

                Service.UpdateBanner(data);
            }
            else
            {
                #region 排序处理
                bool Flag = Service.IsExistBanner(reqObj.Parameters.DisplayIndex);
                if (Flag == false)
                {
                    var errRsp = new ErrorResponse();
                    errRsp.Message = "KV已有相同排序，请更换！";
                    return errRsp.ToJSON();
                }
                #endregion

                //添加
                data.AdId = System.Guid.NewGuid();
                if (!string.IsNullOrWhiteSpace(reqObj.Parameters.ActivityGroupId))
                    data.ActivityGroupId = new Guid(reqObj.Parameters.ActivityGroupId);
                if (!string.IsNullOrWhiteSpace(reqObj.Parameters.TemplateId))
                    data.TemplateId = new Guid(reqObj.Parameters.TemplateId);
                data.BannerImageId = reqObj.Parameters.BannerImageId;
                data.BannerUrl = reqObj.Parameters.BannerUrl;
                data.BannerName = reqObj.Parameters.BannerName;
                data.DisplayIndex = reqObj.Parameters.DisplayIndex;
                data.CreateBy = reqObj.UserID;
                data.LastUpdateBy = reqObj.UserID;
                Service.CreateBanner(data);
            }

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 删除KV
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string DeleteBanner(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<BannerRP>>();
            var Service = new CreativityWarehouseService();
            var Data = new T_CTW_BannerData();
            Data.AdId = new Guid(reqObj.Parameters.AdId);
            Data.LastUpdateBy = reqObj.UserID;
            Service.DeleteBanner(Data);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        #endregion

        #region 计划活动管理、图片
        /// <summary>
        /// 计划列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetSeasonPlanList(string pRequest)
        {
            var RD = new SeasonPlanRD();
            var Service = new CreativityWarehouseService();

            var Result = Service.GetSeasonPlanList();
            RD.SeasonPlanList = new List<SeasonPlanInfo>();
            foreach (var item in Result)
            {
                var Data = new SeasonPlanInfo();
                Data.SeasonPlanId = item.SeasonPlanId;
                Data.PlanName = item.PlanName;
                Data.PlanDate = item.PlanDate.ToString("yyyy-MM-dd");
                //
                RD.SeasonPlanList.Add(Data);
            }

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 设置计划
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetSeasonPlan(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<SeasonPlanRP>>();
            var Service = new CreativityWarehouseService();

            var Data = new T_CTW_SeasonPlanData();
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.SeasonPlanId))
            {

                Data.SeasonPlanId = new Guid(reqObj.Parameters.SeasonPlanId);
                Data.PlanDate = reqObj.Parameters.PlanDate.Date;
                Data.PlanName = reqObj.Parameters.PlanName;
                Data.LastUpdateBy = reqObj.UserID;

                Service.UpdateSeasonPlan(Data);
            }
            else
            {

                Data.SeasonPlanId = System.Guid.NewGuid();
                Data.PlanDate = reqObj.Parameters.PlanDate.Date;
                Data.PlanName = reqObj.Parameters.PlanName;
                Data.CreateBy = reqObj.UserID;
                Data.LastUpdateBy = reqObj.UserID;

                Service.CreateSeasonPlan(Data);
            }

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string DeleteSeasonPlan(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<SeasonPlanRP>>();
            var Service = new CreativityWarehouseService();
            var Data = new T_CTW_SeasonPlanData();
            Data.SeasonPlanId = new Guid(reqObj.Parameters.SeasonPlanId);
            Data.LastUpdateBy = reqObj.UserID;
            Service.DeleteSeasonPlan(Data);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }

        #region 计划图片
        /// <summary>
        /// 获取计划图片
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetHomePageCommon(string pRequest)
        {
            var RD = new HomePageCommonRD();
            var Service = new CreativityWarehouseService();

            RD.HomePageCommon = Service.GetHomePageCommon();
            if (RD.HomePageCommon != null)
            {
                var Result = Service.GetObjectImagesId(RD.HomePageCommon.ImageId);
                if (Result != null)
                {
                    RD.ImageUrl = Result.ImageURL;
                }
            }

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
            //return JsonSerializer<HomePageCommonRD>(RD);
        }

        #endregion
        #endregion
        #region 预览
        /// <summary>
        /// 获取创意预览
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetPreview(string pRequest)
        {
            var RD = new PreviewRD();
            var Service = new CreativityWarehouseService();

            var KVResult = Service.GetBannerList(null, null, 0, 20);
            //KV排序
            if (KVResult.Count > 1)
                KVResult = KVResult.Where(a => (a.Status == 20 || a.Status == 30)).OrderBy(m => m.DisplayIndex).ToList();
            var SeasonPlanResult = Service.GetSeasonPlanList();
            var SysMarketingGroupTypeResult = Service.GetSysMarketingGroupTypeList();
            var LEventTemplateDropDownResult = Service.GetLEventTemplateDropDownList();
            if (LEventTemplateDropDownResult.Count > 0)
                LEventTemplateDropDownResult = LEventTemplateDropDownResult.Where(a => a.TemplateStatus != 10 && a.TemplateStatus != 40).OrderBy(m => m.DisplayIndex).ToList();

            RD.BannerPreviewList = new List<BannerPreview>();
            foreach (var item in KVResult)
            {
                var Data = new BannerPreview();
                var ResultImage = Service.GetObjectImagesId(item.BannerImageId);
                if (ResultImage != null)
                {
                    Data.ImageUrl = ResultImage.ImageURL;
                    Data.DisplayIndex = item.DisplayIndex;
                    RD.BannerPreviewList.Add(Data);
                }
            }
            //计划
            RD.SeasonPlanPreviewList = new List<SeasonPlanPreview>();
            foreach (var item in SeasonPlanResult)
            {
                var Data = new SeasonPlanPreview();
                Data.PlanDate = item.PlanDate.ToString("yyyy-MM-dd");
                Data.PlanName = item.PlanName;
                //
                RD.SeasonPlanPreviewList.Add(Data);
            }


            #region 节日类型
            RD.HolidaySysMarketingGroupTypePreview = new HolidaySysMarketingGroupTypePreview();
            var HolidayResult = SysMarketingGroupTypeResult.FirstOrDefault(m => m.ActivityGroupCode.Trim().Equals("Holiday"));
            if (HolidayResult != null)
            {
                RD.HolidaySysMarketingGroupTypePreview.Name = HolidayResult.Name;
                #region 主题
                var LEventTemplateResult = LEventTemplateDropDownResult.Where(m => m.ActivityGroupId.Equals(HolidayResult.ActivityGroupId));
                RD.HolidaySysMarketingGroupTypePreview.HolidayLEventTemplatePreview = new List<LEventTemplatePreview>();
                foreach (var item in LEventTemplateResult)
                {
                    var Data = new LEventTemplatePreview();
                    Data.TemplateName = item.TemplateName;
                    Data.TemplateStatus = item.TemplateStatus;
                    var ResultImage = Service.GetObjectImagesId(item.ImageId);
                    if (ResultImage != null)
                    {
                        Data.ImageUrl = ResultImage.ImageURL;
                    }
                    //
                    RD.HolidaySysMarketingGroupTypePreview.HolidayLEventTemplatePreview.Add(Data);
                }
                #endregion
            }
            #endregion

            #region 门店类型
            RD.UnitSysMarketingGroupTypePreview = new UnitSysMarketingGroupTypePreview();
            var UnitResult = SysMarketingGroupTypeResult.FirstOrDefault(m => m.ActivityGroupCode.Trim().Equals("Unit"));
            if (UnitResult != null)
            {
                RD.UnitSysMarketingGroupTypePreview.Name = UnitResult.Name;
                #region 主题
                var LEventTemplateResult = LEventTemplateDropDownResult.Where(m => m.ActivityGroupId.Equals(UnitResult.ActivityGroupId));
                RD.UnitSysMarketingGroupTypePreview.UnitLEventTemplatePreview = new List<LEventTemplatePreview>();
                foreach (var item in LEventTemplateResult)
                {
                    var Data = new LEventTemplatePreview();
                    Data.TemplateName = item.TemplateName;
                    Data.TemplateStatus = item.TemplateStatus;
                    var ResultImage = Service.GetObjectImagesId(item.ImageId);
                    if (ResultImage != null)
                    {
                        Data.ImageUrl = ResultImage.ImageURL;
                    }
                    //
                    RD.UnitSysMarketingGroupTypePreview.UnitLEventTemplatePreview.Add(Data);
                }
                #endregion
            }
            #endregion

            #region 产品类型
            RD.ProductSysMarketingGroupTypePreview = new ProductSysMarketingGroupTypePreview();
            var ProductResult = SysMarketingGroupTypeResult.FirstOrDefault(m => m.ActivityGroupCode.Trim().Equals("Product"));
            if (ProductResult != null)
            {
                RD.ProductSysMarketingGroupTypePreview.Name = ProductResult.Name;
                #region 主题
                var LEventTemplateResult = LEventTemplateDropDownResult.Where(m => m.ActivityGroupId.Equals(ProductResult.ActivityGroupId));
                RD.ProductSysMarketingGroupTypePreview.ProductLEventTemplatePreview = new List<LEventTemplatePreview>();
                foreach (var item in LEventTemplateResult)
                {
                    var Data = new LEventTemplatePreview();
                    Data.TemplateName = item.TemplateName;
                    Data.TemplateStatus = item.TemplateStatus;
                    var ResultImage = Service.GetObjectImagesId(item.ImageId);
                    if (ResultImage != null)
                    {
                        Data.ImageUrl = ResultImage.ImageURL;
                    }
                    //
                    RD.ProductSysMarketingGroupTypePreview.ProductLEventTemplatePreview.Add(Data);
                }
                #endregion
            }
            #endregion


            //RD.LEventTemplatePreviewList = new List<LEventTemplatePreview>();
            //foreach (var item in LEventTemplateDropDownResult)
            //{
            //    var Data = new LEventTemplatePreview();
            //    Data.TemplateName = item.TemplateName;
            //    Data.TemplateStatus = item.TemplateStatus;
            //    var ResultImage = Service.GetObjectImagesId(item.ImageId);
            //    if (ResultImage != null)
            //    {
            //        Data.ImageUrl = ResultImage.ImageURL;
            //    }
            //    //
            //    RD.LEventTemplatePreviewList.Add(Data);
            //}
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }


        /// <summary>
        /// 发布KV,主题模板 
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetLEventTemplateReleaseStatus(string pRequest)
        {
            var RD = new EmptyRD();
            var Service = new CreativityWarehouseService();

            //发布KV
            Service.SetLEventBannerReleaseStatus();
            //发布主题
            Service.SetLEventTemplateReleaseStatus();

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        #endregion
        #region 创意活动
        #region 创意主题
        /// <summary>
        /// 创意主题列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEventTemplateList(string pRequest)
        {
            var RD = new LEventTemplateRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventTemplateRP>>();
            var Service = new CreativityWarehouseService();

            string TemplateName = null;
            Guid? ActivityGroupId = null;
            int? TemplateStatus = null;

            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.TemplateName))
                TemplateName = reqObj.Parameters.TemplateName;
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.ActivityGroupId))
                ActivityGroupId = new Guid(reqObj.Parameters.ActivityGroupId);
            if (reqObj.Parameters.TemplateStatus > 0)
                TemplateStatus = reqObj.Parameters.TemplateStatus;

            RD.LEventTemplateList = Service.GetLEventTemplateList(TemplateName, ActivityGroupId, TemplateStatus, reqObj.Parameters.PageIndex, reqObj.Parameters.PageSize);
            //风格二位图片赋值，默认取第一张
            foreach (var item in RD.LEventTemplateList)
            {
                var ThemeData = Service.GetLEventThemeList(item.TemplateId, 0, 10).LastOrDefault();
                item.RCodeUrl = ThemeData == null ? "" : ThemeData.RCodeUrl;
            }
            RD.TotalCount = Service.GetLEventTemplateCount(TemplateName, ActivityGroupId, TemplateStatus);
            int PageSum = 0;
            //分页
            if (RD.TotalCount > 0)
            {
                PageSum = RD.TotalCount / reqObj.Parameters.PageSize;
                if (RD.TotalCount % reqObj.Parameters.PageSize != 0)
                    PageSum++;
            }
            RD.TotalPageCount = PageSum;

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
            //return JsonSerializer<LEventTemplateRD>(RD);
        }
        /// <summary>
        /// 根据Id获取创建主题
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEventTemplateByID(string pRequest)
        {
            var RD = new LEventTemplateRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventTemplateRP>>();
            var Service = new CreativityWarehouseService();

            var ResultData = Service.GetLEventTemplateByID(new Guid(reqObj.Parameters.TemplateId));
            if (ResultData != null)
            {
                RD.LEventTemplateInfo = new LEventTemplateInfo();
                RD.LEventTemplateInfo.TemplateId = ResultData.TemplateId.ToString();
                RD.LEventTemplateInfo.TemplateName = ResultData.TemplateName;
                RD.LEventTemplateInfo.ActivityGroupId = ResultData.ActivityGroupId.ToString();
                RD.LEventTemplateInfo.DisplayIndex = ResultData.DisplayIndex;
                RD.LEventTemplateInfo.ImageId = ResultData.ImageId;
                //获取图片地址
                var ResultImage = Service.GetObjectImagesId(ResultData.ImageId);
                if (ResultImage != null)
                {
                    RD.LEventTemplateInfo.ImageUrl = ResultImage.ImageURL;
                }
            }

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 设置主题上下架
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetLEventTemplateStatus(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventTemplateRP>>();
            var Service = new CreativityWarehouseService();

            var Data = new T_CTW_LEventTemplateData();
            Data.TemplateId = new Guid(reqObj.Parameters.TemplateId);
            Data.TemplateStatus = reqObj.Parameters.TemplateStatus;
            Data.LastUpdateBy = reqObj.UserID;
            //
            Service.SetLEventTemplateStatus(Data);
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetLEventTemplate(string pRequest)
        {
            var RD = new SetLEventTemplateRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventTemplateRP>>();
            var Service = new CreativityWarehouseService();

            var data = new T_CTW_LEventTemplateData();
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.TemplateId))
            {
                //修改 
                data.TemplateId = new Guid(reqObj.Parameters.TemplateId);
                data.TemplateName = reqObj.Parameters.TemplateName;
                data.ActivityGroupId = new Guid(reqObj.Parameters.ActivityGroupId);
                data.ImageId = reqObj.Parameters.ImageId;
                data.DisplayIndex = reqObj.Parameters.DisplayIndex;

                #region DisplayIndex判断重复处理
                var OldData = Service.GetLEventTemplateByID(data.TemplateId);
                if (OldData != null)
                {
                    if ((!OldData.ActivityGroupId.Equals(data.ActivityGroupId)) || (OldData.DisplayIndex != data.DisplayIndex))
                    {//当类型或者排序值发生改变时
                        bool Flag = Service.IsExistLEventTemplate(data.ActivityGroupId, data.DisplayIndex);
                        if (Flag)
                        {
                            var errRsp = new ErrorResponse();
                            errRsp.Message = "排序字段输入值重复！";
                            return errRsp.ToJSON();
                        }
                    }
                }
                else
                {
                    var errRsp = new ErrorResponse();
                    errRsp.Message = "主题不存在！";
                    return errRsp.ToJSON();
                }
                #endregion

                data.LastUpdateBy = reqObj.UserID;
                Service.UpdateLEventTemplate(data);

            }
            else
            {
                //新增
                data.TemplateId = System.Guid.NewGuid();
                data.TemplateName = reqObj.Parameters.TemplateName;
                data.TemplateDesc = "无";
                data.ActivityGroupId = new Guid(reqObj.Parameters.ActivityGroupId);
                data.ImageId = reqObj.Parameters.ImageId;
                data.StartDate = DateTime.Now;
                data.EndDate = DateTime.Now;
                data.DisplayIndex = reqObj.Parameters.DisplayIndex;

                #region DisplayIndex判断重复处理
                bool Flag = Service.IsExistLEventTemplate(data.ActivityGroupId, data.DisplayIndex);
                if (Flag)
                {
                    var errRsp = new ErrorResponse();
                    errRsp.Message = "排序字段输入值重复！";
                    return errRsp.ToJSON();
                }
                #endregion



                data.CreateBy = reqObj.UserID;
                data.LastUpdateBy = reqObj.UserID;
                Service.CreateLEventTemplate(data);
            }

            RD.TemplateId = data.TemplateId.ToString();

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string DeleteLEventTemplate(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventTemplateRP>>();
            var Service = new CreativityWarehouseService();

            var data = new T_CTW_LEventTemplateData();
            data.TemplateId = new Guid(reqObj.Parameters.TemplateId); ;
            data.LastUpdateBy = reqObj.UserID;
            Service.DeleteLEventTemplate(data);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        #endregion
        #region 风格
        /// <summary>
        /// 创意风格列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEventThemeList(string pRequest)
        {
            var RD = new LEventThemeRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventThemeRP>>();
            var Service = new CreativityWarehouseService();

            Guid TemplateId = new Guid(reqObj.Parameters.TemplateId);

            RD.LEventThemeList = Service.GetLEventThemeList(TemplateId, reqObj.Parameters.PageIndex, reqObj.Parameters.PageSize);
            //RD.TotalCount = Service.GetLEventThemeCount(TemplateId);
            //int PageSum = 0;
            ////分页
            //if (RD.TotalCount > 0)
            //{
            //    PageSum = RD.TotalCount / reqObj.Parameters.PageSize;
            //    if (RD.TotalCount % reqObj.Parameters.PageSize != 0)
            //        PageSum++;
            //}
            //RD.TotalPageCount = PageSum;

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();

            //return JsonSerializer<LEventThemeRD>(RD);
        }
        /// <summary>
        /// 根据Id获取风格
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEventThemeByID(string pRequest)
        {
            var RD = new LEventThemeRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventThemeRP>>();
            var Service = new CreativityWarehouseService();

            var ResultData = Service.GetLEventThemeByID(new Guid(reqObj.Parameters.ThemeId));
            if (ResultData != null)
            {
                RD.LEventThemeInfo = new LEventThemeInfo();
                RD.LEventThemeInfo.ThemeId = ResultData.ThemeId.ToString();
                RD.LEventThemeInfo.TemplateId = ResultData.TemplateId.ToString();
                RD.LEventThemeInfo.ThemeName = ResultData.ThemeName;
                RD.LEventThemeInfo.ImageId = ResultData.ImageId;
                RD.LEventThemeInfo.H5Url = ResultData.H5Url;
                //获取图片地址
                var ResultImage = Service.GetObjectImagesId(ResultData.ImageId);
                if (ResultImage != null)
                {
                    RD.LEventThemeInfo.ImageUrl = ResultImage.ImageURL;
                }
            }

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 修改风格
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetLEventTheme(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventThemeRP>>();
            var Service = new CreativityWarehouseService();

            var data = new T_CTW_LEventThemeData();
            Guid TemplateId = new Guid(reqObj.Parameters.TemplateId);
            if (TemplateId == null)
            {
                var errRsp = new ErrorResponse();
                errRsp.Message = "未传主题ID！";
                return errRsp.ToJSON();
            }


            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.ThemeId))
            {
                var ResultData = Service.GetLEventThemeByID(new Guid(reqObj.Parameters.ThemeId));
                if (ResultData == null)
                {
                    var errRsp = new ErrorResponse();
                    errRsp.Message = "找不到当前操作的风格！";
                    return errRsp.ToJSON();
                }
                if (!ResultData.ThemeName.Equals(reqObj.Parameters.ThemeName))
                {
                    //名称处理
                    var Result = Service.GetLEventThemeByName(TemplateId, reqObj.Parameters.ThemeName).ToList();
                    if (Result.Count > 0)
                    {
                        var errRsp = new ErrorResponse();
                        errRsp.Message = "已相同的风格名称！";
                        return errRsp.ToJSON();
                    }
                }

                //修改
                ResultData.ThemeName = reqObj.Parameters.ThemeName;
                ResultData.H5Url = ConfigurationManager.AppSettings["LinKinUrl"] + "id=" + reqObj.Parameters.H5TemplateId;
                //二维码 分格ID 链接Url
                ResultData.RCodeUrl = GeneratedQR(ResultData.ThemeId.ToString(), ResultData.H5Url);
                ResultData.ImageId = reqObj.Parameters.ImageId;
                ResultData.H5TemplateId = reqObj.Parameters.H5TemplateId;
                ResultData.LastUpdateBy = reqObj.UserID;
                //
                Service.UpdateLEventTheme(ResultData);
            }
            else
            {
                //名称处理
                var Result = Service.GetLEventThemeByName(TemplateId, reqObj.Parameters.ThemeName).ToList();
                if (Result.Count > 0)
                {
                    var errRsp = new ErrorResponse();
                    errRsp.Message = "已相同的风格名称！";
                    return errRsp.ToJSON();
                }
                int Count = Service.GetLEventThemeCount(new Guid(reqObj.Parameters.TemplateId));
                if (Count >= 10)
                {
                    var errRsp = new ErrorResponse();
                    errRsp.Message = "风格最多创建10条！";
                    return errRsp.ToJSON();
                }

                //创建
                data.ThemeId = System.Guid.NewGuid();
                data.TemplateId = new Guid(reqObj.Parameters.TemplateId);
                data.ThemeName = reqObj.Parameters.ThemeName;
                data.H5Url = ConfigurationManager.AppSettings["LinKinUrl"] + "id=" + reqObj.Parameters.H5TemplateId;
                data.ImageId = reqObj.Parameters.ImageId;
                data.H5TemplateId = reqObj.Parameters.H5TemplateId;
                //二维码 分格ID 链接Url
                data.RCodeUrl = GeneratedQR(data.ThemeId.ToString(), data.H5Url);
                data.CreateBy = reqObj.UserID;
                data.LastUpdateBy = reqObj.UserID;

                Service.CreateLEventTheme(data);
            }
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 删除风格
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string DeleteLEventTheme(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventThemeRP>>();
            var Service = new CreativityWarehouseService();

            var Data = new T_CTW_LEventThemeData();
            Data.ThemeId = new Guid(reqObj.Parameters.ThemeId); ;
            Data.LastUpdateBy = reqObj.UserID;

            Service.DeleteLEventTheme(Data);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 生成风格二维码
        /// </summary>
        /// <param name="ThemeId"></param>
        /// <param name="H5Url"></param>
        /// <returns></returns>
        private string GeneratedQR(string ThemeId, string H5Url)
        {

            string res = "";
            var qrcode = new StringBuilder();
            qrcode.AppendFormat(H5Url);
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();  //这个类从哪来的？是一个生成二维码的组件
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 6;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            Image qrImage = qrCodeEncoder.Encode(qrcode.ToString(), Encoding.UTF8);//把ThemeId放到 了二维码 信息 里 
            Image bitmap = new System.Drawing.Bitmap(180, 180);
            Graphics g2 = System.Drawing.Graphics.FromImage(bitmap);
            g2.InterpolationMode = InterpolationMode.High;
            g2.SmoothingMode = SmoothingMode.HighQuality;
            g2.Clear(System.Drawing.Color.Transparent);
            g2.DrawImage(qrImage, new System.Drawing.Rectangle(10, 10, 160, 160), new System.Drawing.Rectangle(0, 0, qrImage.Width, qrImage.Height), System.Drawing.GraphicsUnit.Pixel);

            string fileName = ThemeId.ToLower() + ".jpg";
            string host = ConfigurationManager.AppSettings["website_WWW"].ToString();

            if (!host.EndsWith("/")) host += "/";
            string fileUrl = host + "images/QRImages/" + fileName;

            string newFilePath = string.Empty;
            string newFilename = string.Empty;
            string path = HttpContext.Current.Server.MapPath("/images/qrcode.jpg");
            System.Drawing.Image imgSrc = System.Drawing.Image.FromFile(path);
            System.Drawing.Image imgWarter = bitmap;
            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                g.DrawImage(imgWarter, new Rectangle(0, 0, imgWarter.Width, imgWarter.Height), 0, 0, imgWarter.Width, imgWarter.Height, GraphicsUnit.Pixel);
            }
            newFilePath = string.Format("/images/QRImages/{0}", fileName);
            newFilename = HttpContext.Current.Server.MapPath(newFilePath);
            imgSrc.Save(newFilename, System.Drawing.Imaging.ImageFormat.Jpeg);
            imgWarter.Dispose();
            imgSrc.Dispose();
            qrImage.Dispose();
            bitmap.Dispose();
            g2.Dispose();
            res = fileUrl;

            //if (res.IndexOf("http://") < 0)
            //{
            //    res = "http://" + res;
            //}
            ////加背景图
            //res = CombinImage(path, res, fileName);

            return res;
        }
        /// <summary>
        /// 二位吗增加背景
        /// </summary>
        /// <param name="imgBack"></param>
        /// <param name="destImg"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        private static string CombinImage(string imgBack, string destImg, string strData)
        {
            //1、上面的图片部分
            HttpWebRequest request_qrcode = (HttpWebRequest)WebRequest.Create(destImg);
            WebResponse response_qrcode = null;
            Stream qrcode_stream = null;
            response_qrcode = request_qrcode.GetResponse();
            qrcode_stream = response_qrcode.GetResponseStream();//把要嵌进去的图片转换成流


            Bitmap _bmpQrcode1 = new Bitmap(qrcode_stream);//把流转换成Bitmap
            Bitmap _bmpQrcode = new Bitmap(_bmpQrcode1, 327, 327);//缩放图片           
            //把二维码由八位的格式转为24位的
            Bitmap bmpQrcode = new Bitmap(_bmpQrcode.Width, _bmpQrcode.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb); //并用上面图片的尺寸做了一个位图
            //用上面空的位图生成了一个空的画板
            Graphics g3 = Graphics.FromImage(bmpQrcode);
            g3.DrawImageUnscaled(_bmpQrcode, 0, 0);//把原来的图片画了上去


            //2、背景部分
            HttpWebRequest request_backgroup = (HttpWebRequest)WebRequest.Create(imgBack);
            WebResponse response_keleyi = null;
            Stream backgroup_stream = null;
            response_keleyi = request_backgroup.GetResponse();
            backgroup_stream = response_keleyi.GetResponseStream();//把背景图片转换成流

            Bitmap bmp = new Bitmap(backgroup_stream);
            Graphics g = Graphics.FromImage(bmp);//生成背景图片的画板

            //3、画上文字
            //  String str = "文峰美容";
            System.Drawing.Font font = new System.Drawing.Font("黑体", 25);
            SolidBrush sbrush = new SolidBrush(Color.White);
            SizeF sizeText = g.MeasureString(strData, font);

            g.DrawString(strData, font, sbrush, (bmp.Width - sizeText.Width) / 2, 490);


            // g.DrawString(str, font, sbrush, new PointF(82, 490));


            g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);//又把背景图片的位图画在了背景画布上。必须要这个，否则无法处理阴影

            //4.合并图片
            g.DrawImage(bmpQrcode, 130, 118, bmpQrcode.Width, bmpQrcode.Height);

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            System.Drawing.Image newImg = Image.FromStream(ms);//生成的新的图片
            //把新图片保存下来
            string DownloadUrl = ConfigurationManager.AppSettings["website_WWW"];
            string host = DownloadUrl + "/HeadImage/";
            //创建下载根文件夹
            //var dirPath = @"C:\DownloadFile\";
            var dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "HeadImage\\";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            //根据年月日创建下载子文件夹
            var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + @"\";
            host += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            //下载到本地文件
            var fileExt = Path.GetExtension(destImg).ToLower();
            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + ".jpg";//+ fileExt;
            var filePath = dirPath + newFileName;
            host += newFileName;

            newImg.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return host;
        }
        #endregion
        #region 活动
        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEventInteractionList(string pRequest)
        {
            var RD = new LEventInteractionRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventInteractionRP>>();
            var Service = new CreativityWarehouseService();
            Guid TemplateId = new Guid(reqObj.Parameters.TemplateId);
            RD.LEventInteractionList = Service.GetLEventInteractionList(TemplateId, reqObj.Parameters.PageIndex, reqObj.Parameters.PageSize);

            foreach (var item in RD.LEventInteractionList)
            {
                if (item.InteractionType == 1)
                    item.ActivityName = item.Title;
                else
                    item.ActivityName = item.EventName;
            }

            RD.TotalCount = Service.GetLEventInteractionCount(TemplateId);
            int PageSum = 0;
            //分页
            if (RD.TotalCount > 0)
            {
                PageSum = RD.TotalCount / reqObj.Parameters.PageSize;
                if (RD.TotalCount % reqObj.Parameters.PageSize != 0)
                    PageSum++;
            }
            RD.TotalPageCount = PageSum;

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();

            //return JsonSerializer<LEventInteractionRD>(RD);
        }


        /// <summary>
        /// 根据Id获取活动
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEventInteractionByID(string pRequest)
        {
            var RD = new LEventInteractionRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventInteractionRP>>();
            var Service = new CreativityWarehouseService();

            RD.LEventInteractionData = Service.GetLEventInteractionByID(new Guid(reqObj.Parameters.InteractionId));
            //if (Result != null)
            //{
            //    RD.LEventInteractionData = new T_CTW_LEventInteractionData();
            //    RD.LEventInteractionData.InteractionId = Result.in;
            //}
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 设置活动
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetLEventInteraction(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventInteractionRP>>();
            var Service = new CreativityWarehouseService();

            #region MyRegion
            var ResultList = Service.GetLEventThemeList(new Guid(reqObj.Parameters.TemplateId), 0, 10).ToList();
            if (ResultList.Count < 1)
            {
                var errRsp = new ErrorResponse();
                errRsp.Message = "请先设置风格！";
                return errRsp.ToJSON();
            }
            #endregion

            var data = new T_CTW_LEventInteractionData();
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.InteractionId))
            {
                data.InteractionId = new Guid(reqObj.Parameters.InteractionId);

                #region 风格处理
                var Result = Service.GetLEventInteractionByID(new Guid(reqObj.Parameters.InteractionId));
                if (!Result.ThemeId.ToString().ToLower().Equals(reqObj.Parameters.ThemeId.ToString().ToLower()))
                {
                    //判断活动是否配置过相同的风格
                    bool Flag = Service.IsExistLEventInteraction(reqObj.Parameters.InteractionType, new Guid(reqObj.Parameters.ThemeId));
                    if (Flag == false)
                    {
                        var errRsp = new ErrorResponse();
                        errRsp.Message = "已有活动设置了相同的风格！";
                        return errRsp.ToJSON();
                    }
                }
                #endregion


                data.TemplateId = new Guid(reqObj.Parameters.TemplateId);
                data.ThemeId = new Guid(reqObj.Parameters.ThemeId);
                data.InteractionType = reqObj.Parameters.InteractionType;
                data.DrawMethodId = new Guid(reqObj.Parameters.DrawMethodId);
                data.LeventId = reqObj.Parameters.LeventId;
                data.LastUpdateBy = reqObj.UserID;
                //
                Service.UpdateLEventInteraction(data);
            }
            else
            {
                #region 风格处理
                //判断活动是否配置过相同的风格
                bool Flag = Service.IsExistLEventInteraction(reqObj.Parameters.InteractionType, new Guid(reqObj.Parameters.ThemeId));
                if (Flag == false)
                {
                    var errRsp = new ErrorResponse();
                    errRsp.Message = "已有活动设置了相同的风格！";
                    return errRsp.ToJSON();
                }
                #endregion
                //创建
                data.InteractionId = System.Guid.NewGuid();
                data.TemplateId = new Guid(reqObj.Parameters.TemplateId);
                data.ThemeId = new Guid(reqObj.Parameters.ThemeId);
                data.InteractionType = reqObj.Parameters.InteractionType;
                data.DrawMethodId = new Guid(reqObj.Parameters.DrawMethodId);
                data.LeventId = reqObj.Parameters.LeventId;
                data.CreateBy = reqObj.UserID;
                data.LastUpdateBy = reqObj.UserID;

                Service.CreateLEventInteraction(data);

            }
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string DeleteLEventInteraction(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventInteractionRP>>();
            var Service = new CreativityWarehouseService();

            var Data = new T_CTW_LEventInteractionData();
            Data.InteractionId = new Guid(reqObj.Parameters.InteractionId);
            Data.LastUpdateBy = reqObj.UserID;

            Service.DeleteLEventInteraction(Data);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        #endregion
        #region 推广设置
        /// <summary>
        /// 获取主题下的推广设置
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetSpreadSettingList(string pRequest)
        {
            var RD = new SpreadSettingRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<SpreadSettingRP>>();
            var Service = new CreativityWarehouseService();

            RD.SpreadSettingList = Service.GetSpreadSettingList(new Guid(reqObj.Parameters.TemplateId));

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
            //return JsonSerializer<SpreadSettingRD>(RD);
        }
        /// <summary>
        /// 创建推广设置
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string CreateSpreadSetting(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<SpreadSettingRP>>();
            var Service = new CreativityWarehouseService();

            #region MyRegion
            int Count = Service.GetLEventInteractionCount(new Guid(reqObj.Parameters.TemplateId));
            if (Count < 1)
            {
                var errRsp = new ErrorResponse();
                errRsp.Message = "请先设置活动！";
                return errRsp.ToJSON();
            }
            #endregion

            var List = new List<T_CTW_SpreadSettingData>();
            foreach (var item in reqObj.Parameters.SpreadSettingList)
            {
                var Data = new T_CTW_SpreadSettingData();
                Data.Id = System.Guid.NewGuid();
                Data.SpreadType = item.SpreadType;
                Data.Title = item.Title;
                Data.ImageId = item.ImageId;
                Data.Summary = item.Summary;
                Data.PromptText = item.PromptText;
                Data.LeadPageSharePromptText = "无";
                Data.LeadPageFocusPromptText = "无";
                Data.LeadPageRegPromptText = "无";
                Data.TemplateId = new Guid(reqObj.Parameters.TemplateId);
                Data.CreateBy = reqObj.UserID;
                Data.LastUpdateBy = reqObj.UserID;
                //
                List.Add(Data);
            }


            Service.CreateSpreadSetting(List);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 修改推广设置
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string UpdateSpreadSetting(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<SpreadSettingRP>>();
            var Service = new CreativityWarehouseService();

            var List = new List<T_CTW_SpreadSettingData>();
            foreach (var item in reqObj.Parameters.SpreadSettingList)
            {
                var Data = new T_CTW_SpreadSettingData();
                Data.Id = item.Id;
                Data.Title = item.Title;
                Data.ImageId = item.ImageId;
                Data.Summary = item.Summary;
                Data.PromptText = item.PromptText;
                Data.LastUpdateBy = reqObj.UserID;
                //
                List.Add(Data);
            }


            Service.UpdateSpreadSetting(List);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }

        #endregion
        #endregion

        #region 营销活动
        /// <summary>
        /// 获取营销列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetMarketingList(string pRequest)
        {
            var RD = new PanicbuyingEventRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<PanicbuyingEventRP>>();
            var Service = new CreativityWarehouseService();

            string EventName = null;
            string EventCode = null;
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.EventName))
                EventName = reqObj.Parameters.EventName;
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.EventCode))
                EventCode = reqObj.Parameters.EventCode;

            RD.PanicbuyingEventList = Service.GetMarketingList(EventName, EventCode, reqObj.Parameters.PageIndex, reqObj.Parameters.PageSize);
            RD.TotalCount = Service.GetMarketingCount(EventName, EventCode);
            int PageSum = 0;
            //分页
            if (RD.TotalCount > 0)
            {
                PageSum = RD.TotalCount / reqObj.Parameters.PageSize;
                if (RD.TotalCount % reqObj.Parameters.PageSize != 0)
                    PageSum++;
            }
            RD.TotalPageCount = PageSum;
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 设置秒杀、团购
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetPanicbuyingEvent(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<PanicbuyingEventRP>>();
            var Service = new CreativityWarehouseService();


            var data = new T_CTW_PanicbuyingEventData();
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.EventId))
            {

                data.EventId = new Guid(reqObj.Parameters.EventId);
                data.EventName = reqObj.Parameters.EventName;
                data.EventCode = reqObj.Parameters.EventCode;
                data.ImageId = reqObj.Parameters.ImageId;
                data.LastUpdateBy = reqObj.UserID;
                //
                Service.UpdatePanicbuyingEvent(data);
            }
            else
            {
                //创建
                data.EventId = System.Guid.NewGuid();
                data.EventName = reqObj.Parameters.EventName;
                data.EventCode = reqObj.Parameters.EventCode;
                data.ImageId = reqObj.Parameters.ImageId;
                data.CreateBy = reqObj.UserID;
                data.LastUpdateBy = reqObj.UserID;

                Service.CreatePanicbuyingEvent(data);

            }
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 删除秒杀、团购
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string DeletePanicbuyingEvent(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<PanicbuyingEventRP>>();
            var Service = new CreativityWarehouseService();


            var data = new T_CTW_PanicbuyingEventData();
            data.EventId = new Guid(reqObj.Parameters.EventId);
            data.LastUpdateBy = reqObj.UserID;
            //
            Service.DeletePanicbuyingEvent(data);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 获取秒杀、团购对象
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetPanicbuyingEvent(string pRequest)
        {
            var RD = new PanicbuyingEventRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<PanicbuyingEventRP>>();
            var Service = new CreativityWarehouseService();

            var Result = Service.GetPanicbuyingEvent(new Guid(reqObj.Parameters.EventId));
            RD.PanicbuyingEventInfoData = new PanicbuyingEventInfos();
            if (Result != null)
            {
                RD.PanicbuyingEventInfoData.EventId = Result.EventId.ToString();
                RD.PanicbuyingEventInfoData.EventName = Result.EventName;
                RD.PanicbuyingEventInfoData.ImageId = Result.ImageId;
                var ResultData = Service.GetObjectImagesId(Result.ImageId);
                if (ResultData != null)
                {
                    RD.PanicbuyingEventInfoData.ImageUrl = ResultData.ImageURL;
                }
            }

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 设置游戏
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetLEvents(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventsRP>>();
            var Service = new CreativityWarehouseService();


            var data = new T_CTW_LEventsData();
            if (!string.IsNullOrWhiteSpace(reqObj.Parameters.EventId))
            {

                data.EventID = reqObj.Parameters.EventId;
                data.Title = reqObj.Parameters.Title;
                data.Description = reqObj.Parameters.Description;
                data.LastUpdateBy = reqObj.UserID;
                //
                Service.UpdateLEvents(data);
            }
            else
            {
                //创建
                data.EventID = System.Guid.NewGuid().ToString();
                data.EventCode = reqObj.Parameters.EventCode;
                data.Title = reqObj.Parameters.Title;
                data.Description = reqObj.Parameters.Description;
                data.ImageURL = "";
                data.LastUpdateBy = reqObj.UserID;
                data.CreateBy = reqObj.UserID;
                data.LastUpdateBy = reqObj.UserID;

                data.EventLevel = 0;
                data.ParentEventID = null;
                data.URL = null;
                data.IsSubEvent = 0;
                data.EventStatus = 0;
                data.DisplayIndex = 0;
                data.DrawMethodId = 0;
                data.EventFlag = null;

                Service.CreateLEvents(data);

            }

            #region 与上传图片做关联
            foreach (var item in reqObj.Parameters.ImageIdList)
            {
                var ImageData = new ObjectImagesData();
                ImageData.ImageId = item;
                ImageData.ObjectId = data.EventID.ToString();
                //
                Service.UpdateObjectImagesByObjectId(ImageData);
            }
            #endregion

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 删除游戏
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string DeleteLEvents(string pRequest)
        {
            var RD = new EmptyRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventsRP>>();
            var Service = new CreativityWarehouseService();

            //游戏
            var data = new T_CTW_LEventsData();
            data.EventID = reqObj.Parameters.EventId;
            data.LastUpdateBy = reqObj.UserID;
            //
            Service.DeleteLEvents(data);

            //团购、秒杀
            var Pdata = new T_CTW_PanicbuyingEventData();
            Pdata.EventId = new Guid(reqObj.Parameters.EventId);
            Pdata.LastUpdateBy = reqObj.UserID;
            //
            Service.DeletePanicbuyingEvent(Pdata);

            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 获取游戏对象
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetLEvents(string pRequest)
        {
            var RD = new LEventsRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<LEventsRP>>();
            var Service = new CreativityWarehouseService();

            RD.LEventsData = Service.GetLEvents(new Guid(reqObj.Parameters.EventId));
            var Result = Service.GetObjectImagesByObjectId(reqObj.Parameters.EventId);
            var Image1 = Result.FirstOrDefault(m => m.BatId == "BackGround");
            RD.BackGroundImageUrl = Image1 == null ? "" : Image1.ImageURL;
            RD.BackGroundImageId = Image1 == null ? "" : Image1.ImageId;
            var Image2 = Result.FirstOrDefault(m => m.BatId == "Logo");
            RD.LogoImageUrl = Image2 == null ? "" : Image2.ImageURL;
            RD.LogoImageId = Image2 == null ? "" : Image2.ImageId;
            var Image3 = Result.FirstOrDefault(m => m.BatId == "NotReceive");
            RD.NotReceiveImageUrl = Image3 == null ? "" : Image3.ImageURL;
            RD.NotReceiveImageId = Image3 == null ? "" : Image3.ImageId;
            var Image4 = Result.FirstOrDefault(m => m.BatId == "Receive");
            RD.ReceiveImageUrl = Image4 == null ? "" : Image4.ImageURL;
            RD.ReceiveImageId = Image4 == null ? "" : Image4.ImageId;
            var Image5 = Result.FirstOrDefault(m => m.BatId == "Rule");
            RD.RuleImageUrl = Image5 == null ? "" : Image5.ImageURL;
            RD.RuleImageId = Image5 == null ? "" : Image5.ImageId;



            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            return rsp.ToJSON();
        }
        #endregion
    }


}