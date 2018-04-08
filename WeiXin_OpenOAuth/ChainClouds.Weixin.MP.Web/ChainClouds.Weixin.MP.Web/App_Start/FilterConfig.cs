using System.Web;
using System.Web.Mvc;

namespace ChainClouds.Weixin.MP.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}