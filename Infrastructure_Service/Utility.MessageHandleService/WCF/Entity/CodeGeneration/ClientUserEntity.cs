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
    /// 实体： 客户人员表 
    /// </summary>
    public partial class ClientUserEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ClientUserEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 用户自动编号
		/// </summary>
		public Int32? ClientUserID { get; set; }

		/// <summary>
		/// 用户编号
		/// </summary>
		public String UserNo { get; set; }

		/// <summary>
		/// 用户名
		/// </summary>
		public String Username { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		public String UserPWD { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// 职位编号(关联ClientPosition表)
		/// </summary>
		public Int32? ClientPositionID { get; set; }

		/// <summary>
		/// 所在部门(关联ClientStructure表)
		/// </summary>
		public Guid? ClientStructureID { get; set; }

		/// <summary>
		/// 性别(1-男,2-女)
		/// </summary>
		public Int32? Sex { get; set; }

		/// <summary>
		/// 电话
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
		/// 地址
		/// </summary>
		public String Addr { get; set; }

		/// <summary>
		/// 邮编
		/// </summary>
		public String Postcode { get; set; }

		/// <summary>
		/// 银行名称
		/// </summary>
		public String BankName { get; set; }

		/// <summary>
		/// 银行帐号
		/// </summary>
		public String BankAcct { get; set; }

		/// <summary>
		/// 身份证号
		/// </summary>
		public String IDCard { get; set; }

		/// <summary>
		/// 上级编号
		/// </summary>
		public Int32? ParentID { get; set; }

		/// <summary>
		/// 系统职位(0-促销员,1-销售代表,3-送货员)
		/// </summary>
		public Int32? SysPosition { get; set; }

		/// <summary>
		/// 推送通道，JSON格式
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
		/// 预留
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
		/// 人员状态(0-空闲,1-送货中)
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 帐号状态(1-启用,0-未启用)
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
		/// 客户编号(关联Client表)
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