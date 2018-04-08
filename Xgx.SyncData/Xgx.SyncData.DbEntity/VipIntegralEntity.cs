/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/28 18:36:53
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
    /// ʵ�壺  
    /// </summary>
    [Table("VipIntegral")]
    public class VipIntegralEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipIntegralEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		[ExplicitKey]
		public String VipID { get; set; }

		/// <summary>
		/// ��Ա����
		/// </summary>
		[ExplicitKey]
		public String VipCardCode { get; set; }

		/// <summary>
		/// ��ʼ����
		/// </summary>
		public Int32? BeginIntegral { get; set; }

		/// <summary>
		/// ���ӻ���
		/// </summary>
		public Int32? InIntegral { get; set; }

		/// <summary>
		/// ���ѻ���
		/// </summary>
		public Int32? OutIntegral { get; set; }

		/// <summary>
		/// ���ջ���
		/// </summary>
		public Int32? EndIntegral { get; set; }

		/// <summary>
		/// �ۼ�ʧЧ����
		/// </summary>
		public Int32? InvalidIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ImminentInvalidIntegral { get; set; }

		/// <summary>
		/// ��ǰ��Ч����
		/// </summary>
		public Int32? ValidIntegral { get; set; }

		/// <summary>
		/// �ۻ�����
		/// </summary>
		public Int32? CumulativeIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ValidNotIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}