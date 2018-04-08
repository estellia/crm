using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Admin.Component.SqlMappers;
using cPos.Model;
using System.Collections;


namespace cPos.Admin.Service
{
    public class CreativityWarehouseService
    {
        #region 首页管理
        #region 首页KV
        /// <summary>
        /// 获取首页KV管理列表
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public IList<T_CTW_BannerData> GetBannerList(string BannerName, int? Status, int PageIndex, int PageSize)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("BannerName", BannerName);
                _ht.Add("Status", Status);
                int StartPage = PageIndex * PageSize;
                int EndPage = (PageIndex + 1) * PageSize;
                _ht.Add("StartPage", StartPage);
                _ht.Add("EndPage", EndPage);

                return MSSqlMapper.Instance().QueryForList<T_CTW_BannerData>("Banner.sql_SelectWhere", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取首页KV管理列表记录总数
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int GetBannerCount(string BannerName, int? Status)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("BannerName", BannerName);
                _ht.Add("Status", Status);
                return MSSqlMapper.Instance().QueryForObject<int>("Banner.sql_GetCount", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取首页KV对象
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public T_CTW_BannerData GetBannerById(Guid AdId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("AdId", AdId);
                return MSSqlMapper.Instance().QueryForObject<T_CTW_BannerData>("Banner.sql_SelectById", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断是否有相同排序数字
        /// </summary>
        /// <param name="DisplayIndex"></param>
        /// <returns></returns>
        public bool IsExistBanner(int DisplayIndex)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("DisplayIndex", DisplayIndex);
                int Count = MSSqlMapper.Instance().QueryForObject<int>("Banner.sql_IsExist", _ht);
                if (Count > 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建KV
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool CreateBanner(T_CTW_BannerData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Insert("Banner.sql_Insert", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 修改KV
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool UpdateBanner(T_CTW_BannerData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("Banner.sql_Update", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 删除KV
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool DeleteBanner(T_CTW_BannerData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("Banner.sql_Delete", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 设置KV上下架
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="AdId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool SetBannerStatus(cPos.Admin.Component.LoggingSessionInfo loggingSessionInfo, string AdId, int Status)
        {
            bool Flag = false;
            try
            {
                var Data = new T_CTW_BannerData();
                Data.AdId = new Guid(AdId);
                Data.Status = Status;
                Data.LastUpdateBy = loggingSessionInfo.UserID;
                Data.LastUpdateTime = DateTime.Now;
                MSSqlMapper.Instance().Update("Banner.sql_UpdateStatus", Data);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        #endregion

        #region 计划活动管理
        /// <summary>
        /// 获取活动计划列表列表
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public IList<T_CTW_SeasonPlanData> GetSeasonPlanList()
        {
            try
            {
                return MSSqlMapper.Instance().QueryForList<T_CTW_SeasonPlanData>("SeasonPlan.sql_SelectAll", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建活动计划
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool CreateSeasonPlan(T_CTW_SeasonPlanData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Insert("SeasonPlan.sql_Insert", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 修改活动计划
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool UpdateSeasonPlan(T_CTW_SeasonPlanData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("SeasonPlan.sql_Update", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 删除活动计划
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool DeleteSeasonPlan(T_CTW_SeasonPlanData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("SeasonPlan.sql_Delete", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }

        #region 计划图片
        /// <summary>
        /// 获取计划图片
        /// </summary>
        /// <returns></returns>
        public T_CTW_HomePageCommonData GetHomePageCommon()
        {
            try
            {
                return MSSqlMapper.Instance().QueryForObject<T_CTW_HomePageCommonData>("HomePageCommon.sql_SelectOne", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建计划图片
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool CreateHomePageCommon(T_CTW_HomePageCommonData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Insert("HomePageCommon.sql_Insert", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 修改计划图片
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool UpdateHomePageCommon(T_CTW_HomePageCommonData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("HomePageCommon.sql_Update", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        #endregion
        #endregion

        #region 发布
        /// <summary>
        /// 发布主题
        /// </summary>
        /// <returns></returns>
        public bool SetLEventTemplateReleaseStatus()
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEventTemplate.sql_UpdateReleaseStatus", null);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 发布KV
        /// </summary>
        /// <returns></returns>
        public bool SetLEventBannerReleaseStatus()
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("Banner.sql_UpdateReleaseStatus", null);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        #endregion
        #endregion

        #region ObjectImages
        public bool CreateObjectImages(ObjectImagesData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Insert("ObjectImages.sql_Insert", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }

        public bool UpdateObjectImages(ObjectImagesData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("ObjectImages.sql_Update", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }

        public bool UpdateObjectImagesByObjectId(ObjectImagesData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("ObjectImages.sql_UpdateByObjectId", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 根据ID获取图片Url
        /// </summary>
        /// <param name="ImagesId"></param>
        /// <returns></returns>
        public ObjectImagesData GetObjectImagesId(string ImagesId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("ImageId", ImagesId);
                return MSSqlMapper.Instance().QueryForObject<ObjectImagesData>("ObjectImages.sql_SelectById", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 根据ID获取图片Url
        /// </summary>
        /// <param name="ImagesId"></param>
        /// <returns></returns>
        public IList<ObjectImagesData> GetObjectImagesByObjectId(string ObjectId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("ObjectId", ObjectId);
                return MSSqlMapper.Instance().QueryForList<ObjectImagesData>("ObjectImages.sql_SelectByObjectId", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region 创意活动
        #region Select
        /// <summary>
        /// 获取营销活动类型下拉框
        /// </summary>
        /// <returns></returns>
        public IList<SysMarketingGroupTypeData> GetSysMarketingGroupTypeList()
        {
            try
            {
                return MSSqlMapper.Instance().QueryForList<SysMarketingGroupTypeData>("SysMarketingGroupType.sql_selectAll", null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 主题类型下拉框
        /// </summary>
        /// <returns></returns>
        public IList<T_CTW_LEventTemplateData> GetLEventTemplateDropDownList()
        {
            try
            {
                return MSSqlMapper.Instance().QueryForList<T_CTW_LEventTemplateData>("LEventTemplate.sql_DropDownList", null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 获取活动工具下拉框列表
        /// </summary>
        /// <param name="InteractionType">互动类型（1：吸粉 2：促销）</param>
        /// <returns></returns>
        public IList<T_CTW_LEventDrawMethodData> GetLEventDrawMethodList()
        {
            try
            {
                return MSSqlMapper.Instance().QueryForList<T_CTW_LEventDrawMethodData>("LEventDrawMethod.sql_selectAll", null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 获取促销具体活动下拉框列表（秒杀/团购/抢购/热销）
        /// </summary>
        /// <returns></returns>
        public IList<T_CTW_PanicbuyingEventData> GetPanicbuyingEventList()
        {
            try
            {
                return MSSqlMapper.Instance().QueryForList<T_CTW_PanicbuyingEventData>("PanicbuyingEvent.sql_SelecAll", null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取游戏具体活动下拉框列表
        /// </summary>
        /// <returns></returns>
        public IList<T_CTW_LEventsData> GetLEventsList()
        {
            try
            {
                return MSSqlMapper.Instance().QueryForList<T_CTW_LEventsData>("LEvents.sql_SelectAll", null);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #region 主题
        /// <summary>
        /// 获取创意主题列表(模板)
        /// </summary>
        /// <param name="TemplateName"></param>
        /// <param name="ActivityGroupId"></param>
        /// <param name="TemplateStatus"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IList<T_CTW_LEventTemplateData> GetLEventTemplateList(string TemplateName, Guid? ActivityGroupId, int? TemplateStatus, int PageIndex, int PageSize)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("TemplateName", TemplateName);
                _ht.Add("ActivityGroupId", ActivityGroupId);
                _ht.Add("TemplateStatus", TemplateStatus);
                int StartPage = PageIndex * PageSize;
                int EndPage = (PageIndex + 1) * PageSize;
                _ht.Add("StartPage", StartPage);
                _ht.Add("EndPage", EndPage);
                return MSSqlMapper.Instance().QueryForList<T_CTW_LEventTemplateData>("LEventTemplate.sql_SelectWhere", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 获取创意主题列表记录总数
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int GetLEventTemplateCount(string TemplateName, Guid? ActivityGroupId, int? TemplateStatus)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("TemplateName", TemplateName);
                _ht.Add("ActivityGroupId", ActivityGroupId);
                _ht.Add("TemplateStatus", TemplateStatus);

                return MSSqlMapper.Instance().QueryForObject<int>("LEventTemplate.sql_GetCount", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断同一类型下，DisplayIndex是否重负
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool IsExistLEventTemplate(Guid ActivityGroupId, int DisplayIndex)
        {
            bool Flag = false;
            try
            {
                
                Hashtable _ht = new Hashtable();
                _ht.Add("ActivityGroupId", ActivityGroupId);
                _ht.Add("DisplayIndex", DisplayIndex);

                var Result = MSSqlMapper.Instance().QueryForObject<int>("LEventTemplate.sql_IsExist", _ht);
                if (Result > 0)
                    Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 根据ID获取创意主题
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public T_CTW_LEventTemplateData GetLEventTemplateByID(Guid TemplateId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("TemplateId", TemplateId);
                return MSSqlMapper.Instance().QueryForObject<T_CTW_LEventTemplateData>("LEventTemplate.sql_SelectById", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 风格
        /// <summary>
        /// 获取分格列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IList<T_CTW_LEventThemeData> GetLEventThemeList(Guid TemplateId, int PageIndex, int PageSize)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                int StartPage = PageIndex * PageSize;
                int EndPage = (PageIndex + 1) * PageSize;
                _ht.Add("TemplateId", TemplateId);
                _ht.Add("StartPage", StartPage);
                _ht.Add("EndPage", EndPage);
                return MSSqlMapper.Instance().QueryForList<T_CTW_LEventThemeData>("LEventTheme.sql_SelectAll", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 获取分格列表记录总数
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int GetLEventThemeCount(Guid TemplateId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("TemplateId", TemplateId);
                return MSSqlMapper.Instance().QueryForObject<int>("LEventTheme.sql_GetCount", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据名称获取风格
        /// </summary>
        /// <param name="ThemeName"></param>
        /// <returns></returns>
        public IList<T_CTW_LEventThemeData> GetLEventThemeByName(Guid TemplateId, string ThemeName)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("ThemeName", ThemeName);
                _ht.Add("TemplateId", TemplateId);
                return MSSqlMapper.Instance().QueryForList<T_CTW_LEventThemeData>("LEventTheme.sql_SelectByName", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 根据ID获取分格
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public T_CTW_LEventThemeData GetLEventThemeByID(Guid ThemeId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("ThemeId", ThemeId);
                return MSSqlMapper.Instance().QueryForObject<T_CTW_LEventThemeData>("LEventTheme.sql_SelectById", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 活动
        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IList<T_CTW_LEventInteractionData> GetLEventInteractionList(Guid TemplateId, int PageIndex, int PageSize)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                int StartPage = PageIndex * PageSize;
                int EndPage = (PageIndex + 1) * PageSize;
                _ht.Add("TemplateId", TemplateId);
                _ht.Add("StartPage", StartPage);
                _ht.Add("EndPage", EndPage);
                return MSSqlMapper.Instance().QueryForList<T_CTW_LEventInteractionData>("LEventInteraction.sql_SelectAll", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取活动列表记录总数
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int GetLEventInteractionCount(Guid TemplateId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("TemplateId", TemplateId);
                return MSSqlMapper.Instance().QueryForObject<int>("LEventInteraction.sql_GetCount", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据ID获取活动
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public T_CTW_LEventInteractionData GetLEventInteractionByID(Guid InteractionId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("InteractionId", InteractionId);
                return MSSqlMapper.Instance().QueryForObject<T_CTW_LEventInteractionData>("LEventInteraction.sql_SelectById", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断活动是否配置过同样的风格
        /// </summary>
        /// <param name="InteractionType"></param>
        /// <param name="ThemeId"></param>
        /// <returns></returns>
        public bool IsExistLEventInteraction(int InteractionType, Guid ThemeId)
        {
            bool Flag = true;
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("InteractionType", InteractionType);
                _ht.Add("ThemeId", ThemeId);
                int result = MSSqlMapper.Instance().QueryForObject<int>("LEventInteraction.sql_IsExist", _ht);

                if (result > 0)
                    Flag = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Flag;
        }
        #endregion
        #region 推广设置
        /// <summary>
        /// 获取当前主题的推广设置
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public IList<T_CTW_SpreadSettingData> GetSpreadSettingList(Guid TemplateId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("TemplateId", TemplateId);
                return MSSqlMapper.Instance().QueryForList<T_CTW_SpreadSettingData>("SpreadSetting.sql_SelectByWhere", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        #endregion
        #region Add，Update，Delete
        #region 主题设置
        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="pData"></param>
        public bool CreateLEventTemplate(T_CTW_LEventTemplateData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Insert("LEventTemplate.sql_Insert", pData);
                Flag = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="TemplateId"></param>
        /// <param name="TemplateStatus"></param>
        /// <returns></returns>
        public bool UpdateLEventTemplate(T_CTW_LEventTemplateData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEventTemplate.sql_Update", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public bool DeleteLEventTemplate(T_CTW_LEventTemplateData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEventTemplate.sql_Delete", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 设置主题上下架
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="TemplateId"></param>
        /// <param name="TemplateStatus"></param>
        /// <returns></returns>
        public bool SetLEventTemplateStatus(T_CTW_LEventTemplateData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEventTemplate.sql_UpdateStatus", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }

        #endregion
        #region 风格设置
        /// <summary>
        /// 创建风格
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool CreateLEventTheme(T_CTW_LEventThemeData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Insert("LEventTheme.sql_Insert", pData);

                Flag = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Flag;
        }

        /// <summary>
        /// 修改风格
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="TemplateId"></param>
        /// <param name="TemplateStatus"></param>
        /// <returns></returns>
        public bool UpdateLEventTheme(T_CTW_LEventThemeData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEventTheme.sql_Update", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 删除风格
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public bool DeleteLEventTheme(T_CTW_LEventThemeData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEventTheme.sql_Delete", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        #endregion
        #region 活动设置
        /// <summary>
        /// 创建活动（促销、游戏）
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool CreateLEventInteraction(T_CTW_LEventInteractionData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Insert("LEventInteraction.sql_Insert", pData);
                Flag = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="TemplateId"></param>
        /// <param name="TemplateStatus"></param>
        /// <returns></returns>
        public bool UpdateLEventInteraction(T_CTW_LEventInteractionData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEventInteraction.sql_Update", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public bool DeleteLEventInteraction(T_CTW_LEventInteractionData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEventInteraction.sql_Delete", pData);
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        #endregion
        #region 推广设置
        /// <summary>
        /// 创建推广设置
        /// </summary>
        /// <param name="pList"></param>
        /// <returns></returns>
        public bool CreateSpreadSetting(List<T_CTW_SpreadSettingData> pList)
        {
            bool Flag = false;
            try
            {

                foreach (var item in pList)
                {
                    MSSqlMapper.Instance().Insert("SpreadSetting.sql_Insert", item);
                }
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 修改推广设置
        /// </summary>
        /// <param name="pList"></param>
        /// <returns></returns>
        public bool UpdateSpreadSetting(List<T_CTW_SpreadSettingData> pList)
        {
            bool Flag = false;
            try
            {

                foreach (var item in pList)
                {
                    MSSqlMapper.Instance().Update("SpreadSetting.sql_Update", item);
                }
                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        #endregion


        #endregion


        #endregion

        #region 营销活动
        /// <summary>
        /// 获取营销列表
        /// </summary>
        /// <param name="EventName"></param>
        /// <param name="EventCode"></param>
        /// <returns></returns>
        public IList<T_CTW_PanicbuyingEventData> GetMarketingList(string EventName, string EventCode, int PageIndex, int PageSize)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("EventName", EventName);
                _ht.Add("EventCode", EventCode);
                int StartPage = PageIndex * PageSize;
                int EndPage = (PageIndex + 1) * PageSize;
                _ht.Add("StartPage", StartPage);
                _ht.Add("EndPage", EndPage);
                return MSSqlMapper.Instance().QueryForList<T_CTW_PanicbuyingEventData>("PanicbuyingEvent.sql_SelectWhere", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 获取营销列表总数
        /// </summary>
        /// <param name="EventName"></param>
        /// <param name="EventCode"></param>
        /// <returns></returns>
        public int GetMarketingCount(string EventName, string EventCode)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("EventName", EventName);
                _ht.Add("EventCode", EventCode);
                return MSSqlMapper.Instance().QueryForObject<int>("PanicbuyingEvent.sql_GetCount", _ht);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建秒杀、团购
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool CreatePanicbuyingEvent(T_CTW_PanicbuyingEventData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Insert("PanicbuyingEvent.sql_Insert", pData);

                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 修改秒杀、团购
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool UpdatePanicbuyingEvent(T_CTW_PanicbuyingEventData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("PanicbuyingEvent.sql_Update", pData);

                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 删除秒杀、团购
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool DeletePanicbuyingEvent(T_CTW_PanicbuyingEventData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("PanicbuyingEvent.sql_Delete", pData);

                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 获取秒杀、团购对象
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public T_CTW_PanicbuyingEventData GetPanicbuyingEvent(Guid EventID)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("EventId", EventID);
                return MSSqlMapper.Instance().QueryForObject<T_CTW_PanicbuyingEventData>("PanicbuyingEvent.sql_SelectById", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 创建游戏
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool CreateLEvents(T_CTW_LEventsData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Insert("LEvents.sql_Insert", pData);

                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }

        /// <summary>
        /// 修改游戏
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool UpdateLEvents(T_CTW_LEventsData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEvents.sql_Update", pData);

                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 删除游戏
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool DeleteLEvents(T_CTW_LEventsData pData)
        {
            bool Flag = false;
            try
            {
                MSSqlMapper.Instance().Update("LEvents.sql_Delete", pData);

                Flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Flag;
        }
        /// <summary>
        /// 获取游戏对象
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public T_CTW_LEventsData GetLEvents(Guid EventID)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("EventID", EventID);
                return MSSqlMapper.Instance().QueryForObject<T_CTW_LEventsData>("LEvents.sql_SelectById", _ht);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion



    }
}
