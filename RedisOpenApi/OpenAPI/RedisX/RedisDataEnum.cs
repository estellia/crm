using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenAPI.RedisX
{
    public enum RedisDataEnum
    {
        /// <summary>
        /// value [String]
        /// </summary>
        String,

        /// <summary>
        /// value [List]
        /// </summary>
        List,

        /// <summary>
        /// value [Set]
        /// </summary>
        Set,

        /// <summary>
        /// value [SortedSet]
        /// </summary>
        SortedSet,

        /// <summary>
        /// value [HashSet]
        /// </summary>
        HashSet,

        /// <summary>
        /// value [None]
        /// </summary>
        None
    }
}