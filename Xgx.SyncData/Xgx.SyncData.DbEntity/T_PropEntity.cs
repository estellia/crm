/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/21 11:43:07
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
using Dapper.Contrib.Extensions;

namespace Xgx.SyncData.DbEntity
{
    /// <summary>
    /// ʵ�壺  
    /// </summary>
    [Table("T_Prop")]
    public partial class T_PropEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_PropEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		[ExplicitKey]
		public String prop_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_eng_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_type { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String parent_prop_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? prop_level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_domain { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_input_flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? prop_max_lenth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_default_value { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? display_index { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }


        #endregion

    }
}