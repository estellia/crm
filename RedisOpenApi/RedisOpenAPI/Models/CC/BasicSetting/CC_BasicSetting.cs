using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC
{
    public class CC_BasicSetting
    {
        public string CustomerId { get; set; }
        public List<Setting> SettingList { get; set; }

    }
    public class Setting
    {
        public string SettingCode { get; set; }
        public string SettingValue { get; set; }
        public string SettingDesc { get; set; }
    }
}
