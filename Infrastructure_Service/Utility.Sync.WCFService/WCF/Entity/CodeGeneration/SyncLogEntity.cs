/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/26 14:18:17
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

namespace Utility.Sync.WCFService.Entity
{
    /// <summary>
    /// ʵ�壺  
    /// </summary>
    public partial class SyncLogEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SyncLogEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ��־ID
		/// </summary>
		public Guid? LogID { get; set; }

		/// <summary>
		/// ��ԴID[��Ʒ/�̼�/����]
		/// </summary>
		public String SourceItemID { get; set; }

		/// <summary>
		/// ��Դ����,1���ղأ�2��ȡ���ղ�
		/// </summary>
		public Int32? SourceType { get; set; }

		/// <summary>
		/// �ͻ�ID
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// ��ԱID
		/// </summary>
		public String MemberID { get; set; }

		/// <summary>
		/// ����,���Է���Ҫ����Ĳ���
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// �Ƿ���ͬ����1����ͬ����2��δͬ��
		/// </summary>
		public Int32? IsNotSync { get; set; }

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