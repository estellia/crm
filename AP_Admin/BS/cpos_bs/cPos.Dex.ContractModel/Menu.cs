using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "menu")]
    public class Menu
    {
        [DataMember(Name = "menu_id")]
        public string menu_id { get; set; }

        [DataMember(Name = "menu_code")]
        public string menu_code { get; set; }

        [DataMember(Name = "parent_menu_id")]
        public string parent_menu_id { get; set; }

        [DataMember(Name = "menu_level")]
        public string menu_level { get; set; }

        [DataMember(Name = "url_path")]
        public string url_path { get; set; }

        [DataMember(Name = "icon_path")]
        public string icon_path { get; set; }

        [DataMember(Name = "display_index")]
        public string display_index { get; set; }

        [DataMember(Name = "user_flag")]
        public string user_flag { get; set; }

        [DataMember(Name = "menu_name")]
        public string menu_name { get; set; }

        [DataMember(Name = "menu_eng_name")]
        public string menu_eng_name { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }
    }
}