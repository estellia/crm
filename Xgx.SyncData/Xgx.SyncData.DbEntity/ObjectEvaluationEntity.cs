using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.DbEntity
{
    public class ObjectEvaluationEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ObjectEvaluationEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public String EvaluationID { get; set; }

        /// <summary>
        /// 商品ID或者门店ID
        /// </summary>
        public String ObjectID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VipID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StarLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StarLevel1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StarLevel2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StarLevel3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StarLevel4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? StarLevel5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Platform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OrderID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsAnonymity { get; set; }


        #endregion
    }
}
