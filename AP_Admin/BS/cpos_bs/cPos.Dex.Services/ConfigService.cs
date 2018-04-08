using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;

namespace cPos.Dex.Services
{
    public class ConfigService
    {
        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="ht">CertId, CertTypeId, UserId, UserCode, CustomerId, CustomerCode, CertStatus, CertPwd</param>
        public IList<ConfigInfo> GetConfigs(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForList<ConfigInfo>("ConfigInfo.GetConfigs", ht);
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        public ConfigInfo GetConfigByKey(string code)
        {
            return SqlMapper.Instance().QueryForObject<ConfigInfo>("ConfigInfo.GetConfigByKey", code);
            //在数据库里指定地址
        }

        /// <summary>
        /// 获取String类型配置的值
        /// </summary>
        public string GetStringConfigByKey(string code)
        {
            ConfigInfo info = GetConfigByKey(code);
            if (info != null && info.CfgValue != null && info.CfgValue.Trim().Length > 0)
                return info.CfgValue.Trim();
            return string.Empty;
        }

        /// <summary>
        /// 获取int类型配置的值
        /// </summary>
        public int GetIntConfigByKey(string code)
        {
            ConfigInfo info = GetConfigByKey(code);
            if (info != null && info.CfgValue != null && info.CfgValue.Trim().Length > 0)
                return int.Parse(info.CfgValue.Trim());
            return -999;
        }

        /// <summary>
        /// 获取bool类型配置的值
        /// </summary>
        public bool GetBoolConfigByKey(string code)
        {
            ConfigInfo info = GetConfigByKey(code);
            if (info != null && info.CfgValue != null && info.CfgValue.Trim().Length > 0)
                return bool.Parse(info.CfgValue.Trim());
            return false;
        }

        /// <summary>
        /// 插入配置
        /// </summary>
        public bool InsertConfig(ConfigInfo info)
        {
            if (info.CfgStatus == null)
                info.CfgStatus = "0";
            if (info.CreateTime == null)
                info.CreateTime = Utils.GetNow();
            if (info.ModifyTime == null)
                info.ModifyTime = Utils.GetNow();

            SqlMapper.Instance().Insert("ConfigInfo.InsertConfig", info);
            return true;
        }

        /// <summary>
        /// 更新配置
        /// </summary>
        public bool UpdateConfig(ConfigInfo info)
        {
            SqlMapper.Instance().Update("ConfigInfo.UpdateConfig", info);
            return true;
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        public bool DeleteConfig(string id)
        {
            SqlMapper.Instance().Update("ConfigInfo.DeleteConfig", id);
            return true;
        }

        #region 获取配置
        /// <summary>
        /// 获取配置：令牌存活周期（单位：毫秒）
        /// </summary>
        public int GetCertTokenCycleTimeCfg()
        {
            return GetIntConfigByKey(Dex.Common.Config.CertTokenCycleTime);
        }

        /// <summary>
        /// 获取配置：业务平台凭证类型
        /// </summary>
        public string GetPosBsCertTypeCodeCfg()
        {
            return GetStringConfigByKey(Dex.Common.Config.PosBsCertTypeCode);
        }

        /// <summary>
        /// 获取配置：是否开启连接业务平台
        /// </summary>
        public bool GetEnableConnectPosBSCfg()
        {
            return GetBoolConfigByKey(Dex.Common.Config.EnableConnectPosBS);
        }

        /// <summary>
        /// 获取配置：文件（FTP）存放路径
        /// </summary>
        public string GetFileServerFolder()
        {
            string folder = GetStringConfigByKey(Dex.Common.Config.FileServerFolder);
            if (!folder.EndsWith(@"\")) folder = folder + @"\";
            return folder;
        }

        /// <summary>
        /// 获取配置：数据包文件扩展名
        /// </summary>
        public string GetPackageFileExtension()
        {
            return GetStringConfigByKey(Dex.Common.Config.PackageFileExtension);
        }

        /// <summary>
        /// 获取配置：读取FTP文件账户类型
        /// </summary>
        public string GetFtpServerReadAccountType()
        {
            return GetStringConfigByKey(Dex.Common.Config.FtpServerReadAccountType);
        }

        /// <summary>
        /// 获取配置：写入FTP文件账户类型
        /// </summary>
        public string GetFtpServerWriteAccountType()
        {
            return GetStringConfigByKey(Dex.Common.Config.FtpServerWriteAccountType);
        }

        /// <summary>
        /// 获取配置：UsersProfile文件夹
        /// </summary>
        public string GetUsersProfileFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.UsersProfileFolder);
        }

        /// <summary>
        /// 获取配置：Items文件夹
        /// </summary>
        public string GetItemsFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.ItemsFolder);
        }
        /// <summary>
        /// 获取配置：ObjectImages文件夹
        /// </summary>
        public string GetObjectImagesFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.ObjectImagesFolder);//这个文件夹地址要 在数据库配置，方法是通过键值来取 的
        }

        /// <summary>
        /// 获取配置：Skus文件夹
        /// </summary>
        public string GetSkusFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.SkusFolder);
        }

        /// <summary>
        /// 获取配置：Units文件夹
        /// </summary>
        public string GetUnitsFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.UnitsFolder);
        }

        /// <summary>
        /// 获取配置：ItemCategorys文件夹
        /// </summary>
        public string GetItemCategorysFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.ItemCategorysFolder);
        }

        /// <summary>
        /// 获取配置：SkuProps文件夹
        /// </summary>
        public string GetSkuPropsFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.SkuPropsFolder);
        }

        /// <summary>
        /// 获取配置：ItemProps文件夹
        /// </summary>
        public string GetItemPropsFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.ItemPropsFolder);
        }

        /// <summary>
        /// 获取配置：SkuPrices文件夹
        /// </summary>
        public string GetSkuPricesFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.SkuPricesFolder);
        }

        /// <summary>
        /// 获取配置：ItemPrices文件夹
        /// </summary>
        public string GetItemPricesFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.ItemPricesFolder);
        }

        /// <summary>
        /// 获取配置：ItemPropDefs文件夹
        /// </summary>
        public string GetItemPropDefsFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.ItemPropDefsFolder);
        }

        /// <summary>
        /// 获取配置：PosVersionFolder
        /// </summary>
        public string GetPosVersionFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.PosVersionFolder);
        }

        /// <summary>
        /// 获取配置：IP
        /// </summary>
        public string GetIP()
        {
            string ip = GetStringConfigByKey(Dex.Common.Config.IP);
            if (ip.Trim().Length == 0) 
                throw new Exception("终端访问入口IP地址尚未配置");
            return ip;
        }

        /// <summary>
        /// 获取配置：Mobile文件夹
        /// </summary>
        public string GetMobileFolder()
        {
            return GetStringConfigByKey(Dex.Common.Config.MobileFolder);
        }
        #endregion
    }
}
