using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC
{
    public class CC_Menu
    {
        /// <summary>
        /// 菜单标识【保存必须】 --
        /// </summary>
        public string Menu_Id { get; set; }

        /// <summary>
        /// 应用系统标识【保存必须】  --
        /// </summary>
        public string Reg_App_Id { get; set; }

        /// <summary>
        /// 菜单号码【保存必须】--
        /// </summary>
        public string Menu_Code { get; set; }

        /// <summary>
        /// 父节点标识【保存必须】--
        /// </summary>
        public string Parent_Menu_Id { get; set; }

        /// <summary>
        /// 菜单级别【保存必须】--
        /// </summary>
        public int Menu_Level { get; set; }

        /// <summary>
        /// 连接地址   --
        /// </summary>
        public string Url_Path { get; set; }

        /// <summary>
        /// 图片地址  --
        /// </summary>
        public string Icon_Path { get; set; }

        /// <summary>
        /// 显示次序【保存必须】--
        /// </summary>
        public int Display_Index { get; set; }

        /// <summary>
        /// 菜单名称【保存必须】--
        /// </summary>
        public string Menu_Name { get; set; }

        /// <summary>
        /// 用户标识  --
        /// </summary>
        public int User_Flag { get; set; }

        /// <summary>
        /// 是否可访问
        /// </summary>
        public int IsCanAccess { get; set; }

        /// <summary>
        /// 菜单英文名称  --
        /// </summary>
        public string Menu_Eng_Name { get; set; }

        /// <summary>
        /// 状态  --
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建人   --
        /// </summary>
        public string Create_User_Id { get; set; }

        /// <summary>
        /// 创建时间  --
        /// </summary>
        public string Create_Time { get; set; }

        /// <summary>
        /// 修改人  --
        /// </summary>
        public string Modify_User_id { get; set; }

        /// <summary>
        /// 修改时间  --
        /// </summary>
        public string Modify_Time { get; set; }
    }
}
