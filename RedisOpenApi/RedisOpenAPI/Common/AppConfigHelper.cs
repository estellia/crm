using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Common
{
    public class AppConfigHelper
    {
        /// <summary>
        /// 读取 AppSettings
        /// </summary>
        public static string GetAppSettingsValue(string key)
        {
            try
            {
                ConfigurationManager.RefreshSection("appSettings");
                return ConfigurationManager.AppSettings[key] ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("方法GetAppSettingsValue出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 设置 AppSettings
        /// </summary>
        public static bool SetAppSettings(string key, string value)
        {
            try
            {
                var _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (!_config.HasFile)
                {
                    throw new Exception("方法UpdateAppSettings出错,程序配置文件缺失！");
                }

                KeyValueConfigurationElement _key = _config.AppSettings.Settings[key];
                if (_key == null)
                {
                    _config.AppSettings.Settings.Add(key, value);
                }
                else
                {
                    _config.AppSettings.Settings[key].Value = value;
                }

                _config.Save(ConfigurationSaveMode.Modified);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("方法UpdateAppSettings出错:" + ex.Message);
            }
        }
    }
}
