using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class Test_TestDns : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Dns.GetHostAddresses(域名）
        //得到的是一个ip数组。
        //IPAdress[] ips=Dns.GetHostAddresses(域名）;
        //一个的话取第一项即可。

        //IPHostEntry hostinfo = Dns.GetHostByName(@"www.baidu.com");
        //IPAddress[] aryIP = hostinfo.AddressList;
        //string result = aryIP[0].ToString();
        this.lb1.Text = Hostname2ip("www.baidu.com");
    }

    public static string Hostname2ip(string hostname)
    {
        try
        {
            IPAddress ip;
            if (IPAddress.TryParse(hostname, out ip))
                return ip.ToString();
            else
                return Dns.GetHostEntry(hostname).AddressList[0].ToString();
        }
        catch (Exception)
        {
            throw new Exception("IP Address Error");
        }
    }
}