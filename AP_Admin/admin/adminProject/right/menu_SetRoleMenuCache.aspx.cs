using JIT.CPOS.BS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminProject.right
{
    public partial class menu_SetRoleMenuCache : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
          //  Response.Write("hello kitty");

            var Service = new SetRoleMenuJobBLL();
            //执行
            Service.AutoSetRoleMenuCache();
            this.InfoBox.ShowPopError("种植缓存成功！");
        }
    }
}