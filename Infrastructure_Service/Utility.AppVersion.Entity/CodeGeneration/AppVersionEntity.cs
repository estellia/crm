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
    /// ʵ�壺  
    /// </summary>
    public partial class AppVersionEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public AppVersionEntity()
        {
        }
        #endregion     

        #region ���Լ�
        /// <summary>
        /// 
        /// </summary>
        public Guid? AppVersionID { get; set; }

        /// <summary>
        /// Ӧ��ID
        /// </summary>
        public Int32? AppID { get; set; }

        /// <summary>
        /// Ӧ�ô���
        /// </summary>
        public String AppCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// �汾
        /// </summary>
        public String Version { get; set; }

        /// <summary>
        /// App����
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AndroidPackageUrl { get; set; }

        /// <summary>
        /// ��װ��URL
        /// </summary>
        public String IOSPackageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ClientID { get; set; }

        /// <summary>
        /// 
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