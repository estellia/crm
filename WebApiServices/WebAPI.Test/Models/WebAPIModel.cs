using System.Runtime.Serialization;

namespace WebAPI.Test.Models
{
    /// <summary>
    /// 获取token时返回的Json,转换为List
    /// </summary>

    public class TokenModel
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public string expires_in { get; set; }
        public string userName { get; set; }

        public string issued { get; set; }

        public string expires { get; set; }

    }

    /// <summary>
    /// 执行Post后返回的结果值。
    /// </summary>
    public class APIReturnModel
    {
        public string ErrMsg { get; set; }

        public string SequenceId { get; set; }
    }

    //获取返回的List
    public class SequenceIDModel
    {
        public int SequenceID { get; set; }

        public string ToObjectType { get; set; }
    }


    public class MSG2ReturnModels
    {
        public string ErrMSG { get; set; }

        public int? NextSequenceID { get; set; }
    }
}
