using System;
using Dapper.Contrib.Extensions;

namespace Xgx.SyncData.DbEntity
{
    /// <summary>
    /// ����״̬��Ϣ��¼  
    /// </summary>
    [Table("TInoutStatus")]
    public class TInoutStatusEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TInoutStatusEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
        /// ID
		/// </summary>
		public Guid? InoutStatusID { get; set; }

		/// <summary>
        /// ����ID
		/// </summary>
		public string OrderID { get; set; }

		/// <summary>
        /// ����״̬
		/// </summary>
		public int? OrderStatus { get; set; }

		/// <summary>
        /// δ�������
		/// </summary>
		public int? CheckResult { get; set; }

		/// <summary>
        /// ֧����ʽ
		/// </summary>
		public int? PayMethod { get; set; }

		/// <summary>
        /// ���͹�˾
		/// </summary>
		public string DeliverCompanyID { get; set; }

		/// <summary>
        /// ���͵���
		/// </summary>
		public string DeliverOrder { get; set; }

		/// <summary>
        /// ͼƬ
		/// </summary>
		public string PicUrl { get; set; }

		/// <summary>
        /// ��ע
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
        /// �ͻ�ID
		/// </summary>
		public string CustomerID { get; set; }

		/// <summary>
        /// ������
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
        /// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
        /// ����޸���
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
        /// ����޸�ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
        /// �Ƿ�ɾ��
		/// </summary>
		public int? IsDelete { get; set; }

        /// <summary>
        /// ����״̬����
        /// </summary>
        public string StatusRemark { get; set; }

        #endregion

    }
}