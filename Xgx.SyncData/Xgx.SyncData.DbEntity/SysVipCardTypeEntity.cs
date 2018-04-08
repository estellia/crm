/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/27 19:13:55
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
    [Table("SysVipCardType")]
    public  class SysVipCardTypeEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SysVipCardTypeEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// �����ͱ���
		/// </summary>
		public String VipCardTypeCode { get; set; }

		/// <summary>
		/// ����������
		/// </summary>
		public String VipCardTypeName { get; set; }

		/// <summary>
		/// �ۼƳ�ֵ
		/// </summary>
		public Decimal? AddUpAmount { get; set; }

		/// <summary>
		/// �Ƿ���չ����Ա
		/// </summary>
		public Int32? IsExpandVip { get; set; }

		/// <summary>
		/// ��ֵ�Ż�
		/// </summary>
		public String PreferentialAmount { get; set; }

		/// <summary>
		/// �����Ż�
		/// </summary>
		public String SalesPreferentiaAmount { get; set; }

		/// <summary>
		/// ���ֱ���
		/// </summary>
		public String IntegralMultiples { get; set; }

		/// <summary>
		/// �Ƿ�ɴ�ֵ
		/// </summary>
		public Int32? Isprepaid { get; set; }

		/// <summary>
		/// �Ƿ�ɻ���
		/// </summary>
		public Int32? IsPoints { get; set; }

		/// <summary>
		/// �Ƿ�ɴ���
		/// </summary>
		public Int32? IsDiscount { get; set; }

		/// <summary>
		/// �Ƿ�����ϳ�ֵ
		/// </summary>
		public Int32? IsOnlineRecharge { get; set; }

		/// <summary>
		/// �Ƿ�ɼ���
		/// </summary>
		public Int32? IsRegName { get; set; }

		/// <summary>
		/// �Ƿ��ͬ���Ż�ȯ
		/// </summary>
		public Int32? IsUseCoupon { get; set; }

		/// <summary>
		/// �Ƿ�󶨵��ӿ�
		/// </summary>
		public Int32? IsBindECard { get; set; }

		/// <summary>
		/// ������ͼƬ
		/// </summary>
		public String PicUrl { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Category { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardLevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsPassword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? UpgradeAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? UpgradeOnceAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UpgradePoint { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ExchangeIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Prices { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsExtraMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsOnlineSales { get; set; }


        #endregion
        
        
    }
}