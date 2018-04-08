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
    [DataContract(Namespace = Common.Config.NS, Name = "data")]
    public class GetAdOrdersContract : BaseContract
    {
        [DataMember(Name = "bat_id")]
        public string bat_id { get; set; }

        [DataMember(Name = "ad_orders")]
        public IList<AdOrderContract> ad_orders { get; set; }
    }

    [DataContract(Namespace = Common.Config.NS, Name = "ad_order")]
    public class AdOrderContract
    {
        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "order_code")]
        public string order_code { get; set; }

        [DataMember(Name = "order_date")]
        public string order_date { get; set; }

        [DataMember(Name = "date_start")]
        public string date_start { get; set; }

        [DataMember(Name = "date_end")]
        public string date_end { get; set; }

        [DataMember(Name = "time_start")]
        public string time_start { get; set; }

        [DataMember(Name = "time_end")]
        public string time_end { get; set; }

        [DataMember(Name = "playbace_no")]
        public string playbace_no { get; set; }

        [DataMember(Name = "url_address")]
        public string url_address { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "status_desc")]
        public string status_desc { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "ad_list")]
        public IList<AdContract> ad_list { get; set; }

        //[DataMember(Name = "order_ad_list")]
        //public IList<OrderAdContract> order_ad_list { get; set; }
    }

    [DataContract(Namespace = Common.Config.NS, Name = "ad")]
    public class AdContract
    {
        [DataMember(Name = "advertise_id")]
        public string advertise_id { get; set; }

        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "advertise_name")]
        public string advertise_name { get; set; }

        [DataMember(Name = "advertise_code")]
        public string advertise_code { get; set; }

        [DataMember(Name = "file_size")]
        public string file_size { get; set; }

        [DataMember(Name = "file_format")]
        public string file_format { get; set; }

        [DataMember(Name = "display")]
        public string display { get; set; }

        [DataMember(Name = "playback_time")]
        public string playback_time { get; set; }

        [DataMember(Name = "url_address")]
        public string url_address { get; set; }

        [DataMember(Name = "brand_customer_id")]
        public string brand_customer_id { get; set; }

        [DataMember(Name = "brand_id")]
        public string brand_id { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "create_time")]
        public string create_time { get; set; }

        [DataMember(Name = "create_user_id")]
        public string create_user_id { get; set; }

        [DataMember(Name = "modify_time")]
        public string modify_time { get; set; }

        [DataMember(Name = "modify_user_id")]
        public string modify_user_id { get; set; }

        [DataMember(Name = "advertise_order_advertise_id")]
        public string advertise_order_advertise_id { get; set; }
    }

    [DataContract(Namespace = Common.Config.NS, Name = "ad_order_log")]
    public class AdOrderLogContract
    {
        [DataMember(Name = "log_id")]
        public string log_id { get; set; }

        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "unit_id")]
        public string unit_id { get; set; }

        [DataMember(Name = "advertise_date")]
        public string advertise_date { get; set; }

        [DataMember(Name = "advertise_time")]
        public string advertise_time { get; set; }

        [DataMember(Name = "advertis_no")]
        public string advertis_no { get; set; }

        [DataMember(Name = "upload_time")]
        public string upload_time { get; set; }
    }

    [DataContract(Namespace = Common.Config.NS, Name = "order_ad")]
    public class OrderAdContract
    {
        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "order_id")]
        public string order_id { get; set; }

        [DataMember(Name = "advertise_id")]
        public string advertise_id { get; set; }
    }
}