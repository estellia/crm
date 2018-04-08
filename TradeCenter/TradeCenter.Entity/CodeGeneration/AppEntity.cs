/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/8 15:45:54
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
    /// ʵ�壺 Ӧ������Ӧ����ָ��ʹ�ý��������м����ϵͳ�����ڽ������Ķ��ԣ�Ӧ�þ��ǿͻ�ϵͳ 
    /// </summary>
    public partial class AppEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public AppEntity()
        {
        }
        #endregion     

        #region ���Լ�
        /// <summary>
        /// Ӧ��ID����������
        /// </summary>
        public Int32? AppID { get; set; }

        /// <summary>
        /// �ͻ�ϵͳ�ı���
        /// </summary>
        public String AppCode { get; set; }

        /// <summary>
        /// ϵͳ����
        /// </summary>
        public String AppName { get; set; }

        /// <summary>
        /// Ӧ������
        /// </summary>
        public String AppDescription { get; set; }

        /// <summary>
        /// ֧��֪ͨURL
        /// </summary>
        public String NotifyUrl { get; set; }

        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// �������û�ID
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// �������û�ID
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}