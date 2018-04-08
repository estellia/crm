/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/17 11:16:35
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
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.Utility.AppVersion.Entity
{
    /// <summary>
    /// ʵ�壺 ��Ȧ�� 
    /// </summary>
    public partial class BusinessZoneEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public BusinessZoneEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Int32? BusinessZoneID { get; set; }

		/// <summary>
		/// ��Ȧ����
		/// </summary>
		public String BusinessZoneCode { get; set; }

		/// <summary>
		/// ��Ȧ��
		/// </summary>
		public String BusinessZoneName { get; set; }

		/// <summary>
		/// ��Ȧ����:1-����,2-��ҵ,3-����
		/// </summary>
		public Int32? Type { get; set; }

		/// <summary>
		/// �Ƿ���Ҷ�ڵ�
		/// </summary>
		public Boolean? IsLeaf { get; set; }

		/// <summary>
		/// ���ڵ�ID
		/// </summary>
		public Int32? ParentID { get; set; }

		/// <summary>
		/// Web�����ַ
		/// </summary>
		public String ServiceUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

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


        #endregion

    }
}