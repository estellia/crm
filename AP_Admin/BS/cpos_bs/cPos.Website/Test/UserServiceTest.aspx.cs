using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using cPos.Service;
using cPos.ExchangeBsService;

public partial class Test_UserServiceTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetDexUserCertificate();
    }

    private void SetDexUserCertificate()
    {
        cUserService userService = new cUserService();
        string customer_code = "IOSDLB";
        cPos.Model.User.UserInfo userInfo = new cPos.Model.User.UserInfo();
        userInfo.User_Id = "11111";
        userInfo.User_Password = "111";
        userInfo.User_Code = "11";
        userInfo.customer_id = "11111";

        //userService.SetDexUserCertificate(customer_code, userInfo);

    }
}