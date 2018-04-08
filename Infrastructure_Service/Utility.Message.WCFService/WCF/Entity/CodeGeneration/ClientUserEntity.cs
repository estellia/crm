/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/5 11:30:35
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

namespace JIT.Utility.Message.WCF.Entity
{
    /// <summary>
    /// ʵ�壺 �ͻ���Ա�� 
    /// </summary>
    public partial class ClientUserEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ClientUserEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �û��Զ����
		/// </summary>
		public Int32? ClientUserID { get; set; }

		/// <summary>
		/// �û����
		/// </summary>
		public String UserNo { get; set; }

		/// <summary>
		/// �û���
		/// </summary>
		public String Username { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String UserPWD { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// ְλ���(����ClientPosition��)
		/// </summary>
		public Int32? ClientPositionID { get; set; }

		/// <summary>
		/// ���ڲ���(����ClientStructure��)
		/// </summary>
		public Guid? ClientStructureID { get; set; }

		/// <summary>
		/// �Ա�(1-��,2-Ů)
		/// </summary>
		public Int32? Sex { get; set; }

		/// <summary>
		/// �绰
		/// </summary>
		public String Tel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ProvinceID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CityID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DistrictID { get; set; }

		/// <summary>
		/// ��ַ
		/// </summary>
		public String Addr { get; set; }

		/// <summary>
		/// �ʱ�
		/// </summary>
		public String Postcode { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String BankName { get; set; }

		/// <summary>
		/// �����ʺ�
		/// </summary>
		public String BankAcct { get; set; }

		/// <summary>
		/// ���֤��
		/// </summary>
		public String IDCard { get; set; }

		/// <summary>
		/// �ϼ����
		/// </summary>
		public Int32? ParentID { get; set; }

		/// <summary>
		/// ϵͳְλ(0-����Ա,1-���۴���,3-�ͻ�Ա)
		/// </summary>
		public Int32? SysPosition { get; set; }

		/// <summary>
		/// ����ͨ����JSON��ʽ
		/// </summary>
		public String PushChannel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Photo { get; set; }

		/// <summary>
		/// Ԥ��
		/// </summary>
		public String Col1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col6 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col7 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col8 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col9 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col10 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col11 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col12 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col13 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col14 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col15 { get; set; }

		/// <summary>
		/// ��Ա״̬(0-����,1-�ͻ���)
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// �ʺ�״̬(1-����,0-δ����)
		/// </summary>
		public Int32? IsEnable { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LatestVersion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DefaultPage { get; set; }

		/// <summary>
		/// �ͻ����(����Client��)
		/// </summary>
		public Int32? ClientID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ClientDistributorID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastUpdateBy { get; set; }

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