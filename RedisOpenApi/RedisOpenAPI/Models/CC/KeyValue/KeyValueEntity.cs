using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.KeyValue
{
    public class KeyValueEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        /// <summary>
        /// SET/GET/DEL
        /// </summary>
        public string ActionType { get; set; }
    }
}
