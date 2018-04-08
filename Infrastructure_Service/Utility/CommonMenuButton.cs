using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility
{
    /// <summary>
    /// 公用菜单按钮信息
    /// </summary>
   public  class CommonMenuButton
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
       public CommonMenuButton()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 自动编号
        /// </summary>
       public string ClientMenuID { get; set; }

        /// <summary>
        /// 用于菜单权限控制
        /// </summary>
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单名称(客户自定义)
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单名称(客户自定义)英文
        /// </summary>
        public string MenuNameEn { get; set; }

        /// <summary>
        /// 菜单顺序
        /// </summary>
        public int? MenuOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// 样式名称
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// 上级编号
        /// </summary>
        public string ParentID { get; set; }
       
        /// <summary>
        /// 客户编号(关联Client表)
        /// </summary>
        public int? ClientID { get; set; }


        public string ClientButtonID { get; set; }
        public string ButtonText { get; set; }
        public string ButtonTextEn { get; set; }
        public string ButtonCode { get; set; }

        #endregion      
    }
}
