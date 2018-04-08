/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/25 16:16:36
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;
namespace Xgx.SyncData.DbEntity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    [Table("T_Item_Delivery_Mapping")]
    public partial class T_Item_Delivery_MappingEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Item_Delivery_MappingEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
        [ExplicitKey]
		public String Item_Delivery_Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Item_Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DeliveryId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Create_Time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Create_User_Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Modify_Time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Modify_User_Id { get; set; }


        #endregion

    }
}