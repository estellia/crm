/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/28 14:43:53
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

namespace JIT.TradeCenter.Entity
{
    /// <summary>
    /// ʵ�壺 ��ƽ̨���������Ӧ��¼ 
    /// </summary>
    public partial class PayRequestRecordEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PayRequestRecordEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Int32? RecordID { get; set; }

		/// <summary>
		/// ƽ̨,1-����,2-֧����,3-΢��
		/// </summary>
		public Int32? Platform { get; set; }

		/// <summary>
		/// ��Ӧ��ChannelID
		/// </summary>
		public Int32? ChannelID { get; set; }

		/// <summary>
		/// �����Json�ַ���
		/// </summary>
		public String RequestJson { get; set; }

		/// <summary>
		/// ��Ӧ��Json
		/// </summary>
		public String ResponseJson { get; set; }

		/// <summary>
		/// �ͻ�ID
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// �û�ID
		/// </summary>
		public String UserID { get; set; }

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