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
    public class ValidateContract : BaseContract
    {
        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "token")]
        public string token { get; set; }

        #region Serialize/Deserialize
        [OnDeserializing]
        void OnDeserializing(StreamingContext context)
        {
            if (context.Context != null)
            {
                Dex.Common.Utils.SaveFile(@"C:\cPos.Dex\Log\ValidateContract\Deserializing\",
                    Dex.Common.Utils.NewGuid() + ".txt",
                    context.Context.ToString());
            }
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            if (context.Context != null)
            {
                Dex.Common.Utils.SaveFile(@"C:\cPos.Dex\Log\ValidateContract\Deserialized\",
                    Dex.Common.Utils.NewGuid() + ".txt",
                    context.Context.ToString());
            }
        }

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            if (context.Context != null)
            {
                Dex.Common.Utils.SaveFile(@"C:\cPos.Dex\Log\ValidateContract\Serializing\",
                    Dex.Common.Utils.NewGuid() + ".txt",
                    context.Context.ToString());
            }
        }

        [OnSerialized]
        void OnSerialized(StreamingContext context)
        {
            if (context.Context != null)
            {
                Dex.Common.Utils.SaveFile(@"C:\cPos.Dex\Log\ValidateContract\Serialized\",
                    Dex.Common.Utils.NewGuid() + ".txt",
                    context.Context.ToString());
            }
        }
        #endregion
    }
}