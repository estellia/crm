using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;
using cPos.Dex.Common;
using System.Xml.Serialization;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "data")]
    public class BaseContract
    {
        public override string ToString()
        {
            return Dex.Common.Json.GetJsonString(this);
        }

        [DataMember(Name = "status")]
        public string status
        {
            get { return _status == null ? Utils.GetStatus(false) : _status.ToLower(); }
            set { _status = value; }
        }
        private string _status;
        

        [DataMember(Name = "error_code")]
        public string error_code
        {
            get;
            set;
        }

        [DataMember(Name = "error_desc")]
        public string error_desc
        {
            get
            {
                return Utils.GetLimitedStr(this.error_full_desc, Config.ErrorLogMaxLengthForClient);
            }
            set
            {
                this.error_full_desc = value;
            }
        }

        public string error_full_desc
        {
            get;
            set;
        }
    }
}